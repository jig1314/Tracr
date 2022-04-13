using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
using Tracr.Shared.Models;

namespace Tracr.Client.Shared
{
    public partial class Notifications : ComponentBase, IAsyncDisposable
    {
        private const string _hubURL = "/notificationHub";
        private HubConnection _hubConnection;
        private bool _started = false;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAccessTokenProvider AccessTokenProvider { get; set; }

        protected List<Notification> UserNotifications { get; set; } =  new List<Notification>();

        protected override async Task OnInitializedAsync()
        {
            if (!_started)
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(NavigationManager.ToAbsoluteUri(_hubURL), options =>
                    {
                        options.AccessTokenProvider = async () =>
                        {
                            var accessTokenResult = await AccessTokenProvider.RequestAccessToken();
                            accessTokenResult.TryGetToken(out var accessToken);
                            return accessToken.Value;
                        };
                    }).Build();

                _hubConnection.On<string, string>("Notification", (title, message) =>
                {
                    UserNotifications.Add(new Notification()
                    {
                        Title = title,
                        Message = message
                    });

                    StateHasChanged();
                });

                _hubConnection.Closed += async (error) =>
                {
                    _started = false;
                    await Task.Delay(new Random().Next(0, 3) * 1000);
                    await ConnectWithRetryAsync(_hubConnection);
                };

                // start the connection
                await ConnectWithRetryAsync(_hubConnection);

                _started = true;
            }
            
            await _hubConnection.StartAsync();
        }

        public async Task ConnectWithRetryAsync(HubConnection connection)
        {
            // Keep trying to until we can start or the token is canceled.
            while (true)
            {
                try
                {
                    await connection.StartAsync();
                    return;
                }
                catch
                {
                    // Failed to connect, trying in between 0-3 seconds.
                    await Task.Delay(new Random().Next(0, 3) * 1000);
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            await StopAsync();
        }

        /// <summary>
        /// Stop the client (if started)
        /// </summary>
        public async Task StopAsync()
        {
            if (_started)
            {
                // disconnect the client
                await _hubConnection.StopAsync();

                // There is a bug in the mono/SignalR client that does not
                // close connections even after stop/dispose
                // see https://github.com/mono/mono/issues/18628
                // this means the demo won't show "xxx left the chat" since 
                // the connections are left open
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
                _started = false;
            }
        }
    }
}
