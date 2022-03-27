using Microsoft.AspNetCore.Components;
using Tracr.Client.Services;
using Tracr.Shared.DTOs;

namespace Tracr.Client.Pages.Dashboard
{
    public partial class UserPropertyDashboardTile : ComponentBase
    {
        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public IPropertyService? PropertyService { get; set; }

        [Parameter]
        public List<PropertyDto> UserProperties { get; set; }
    }
}
