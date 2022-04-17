using Microsoft.AspNetCore.SignalR;
using Tracr.Server.Models;

namespace Tracr.Server.Hubs
{
    public class AlertHub: Hub, IAlertHub
    {
        public async Task AlertPaymentReceived(Property property)
        {
            var message = $" Payment has been received for {property.Name}";
            await Clients.All.SendAsync("AlertPaymentReceived", "Payment Received", message, DateTime.Now);
        }

        public async Task AlertPropertySuggestion(Property property)
        {
            var message = $"{property.Name}";
            await Clients.All.SendAsync("AlertPropertySuggestion", "Property Suggestion", message, DateTime.Now);
        }

        public async Task AlertQuaterlyReport()
        {
            var message = "Quaterly Report has been generated";
            await Clients.All.SendAsync("AlertQuaterlyReport", "Quaterly Report Available", message, DateTime.Now);
        }

        public async Task AlertAnnualReport()
        {
            var message = "Annual Report has been generated";
            await Clients.All.SendAsync("AlertAnnualReport", "Annual Report Available", message, DateTime.Now);
        }
    }
}
