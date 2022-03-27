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

        protected List<PropertyDto> UserPropertyDtos { get; set; } = new List<PropertyDto>();

        public string ErrorMessage { get; set; } = "";

        public bool LoadingUserPropertyData { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await GetUserProperties();
        }

        private async Task GetUserProperties()
        {
            if (PropertyService == null)
                return;

            ErrorMessage = "";

            try
            {
                LoadingUserPropertyData = true;
                UserPropertyDtos = await PropertyService.GetUserProperties();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
            }
            finally
            {
                LoadingUserPropertyData = false;
            }
        }
    }
}
