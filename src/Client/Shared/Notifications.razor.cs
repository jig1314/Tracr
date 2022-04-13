using Microsoft.AspNetCore.Components;
using Tracr.Shared.Models;

namespace Tracr.Client.Shared
{
    public partial class Notifications : ComponentBase
    {
        protected List<Notification> UserNotifications { get; set; } =  new List<Notification>();
    }
}
