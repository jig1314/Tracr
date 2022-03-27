using Microsoft.AspNetCore.Components;

namespace Tracr.Client.Pages.Dashboard
{
    public partial class Dashboard : ComponentBase
    {
        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        protected void GoToManageProperties()
        {
            if (NavigationManager == null)
                return;

            NavigationManager.NavigateTo($"/userProfile/manageProperties");
        }
    }
}
