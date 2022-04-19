using Microsoft.AspNetCore.SignalR;
using Tracr.Server.Hubs;
using Tracr.Server.Repositories;

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
                await DailyNotificationCheck();
            }
        }


        private async Task DailyNotificationCheck()
        {
        }
    }
}
