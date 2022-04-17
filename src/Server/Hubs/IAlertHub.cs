using Tracr.Server.Models;

namespace Tracr.Server.Hubs
{
    public interface IAlertHub
    {
        Task AlertPaymentReceived(Property property);

        Task AlertPropertySuggestion(Property property);

        Task AlertQuaterlyReport();

        Task AlertAnnualReport();
    }
}
