using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using Tracr.Server.Data;
using Tracr.Server.Hubs;
using Tracr.Server.Repositories;
using Tracr.Shared.ResourceParameters;

namespace Tracr.Server.Services
{
    public class AlertService: BackgroundService
    {
        private readonly IHubContext<AlertHub> _alertHub;

        public AlertService(IHubContext<AlertHub> alertHub, IRealEstateRepo realEstateRepo)
        {
            this._alertHub = alertHub;

        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //daily check for something
                await Task.Delay(86400000);
                DailyNotificationCheck();
            }
        }


        private async void DailyNotificationCheck()
        {
            await _alertHub.Clients.All.SendAsync("AlertPropertyForSale", $"10 Homes are available in Atlanta");

        }
    }
}
