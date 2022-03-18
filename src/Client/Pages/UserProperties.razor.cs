using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Tracr.Client.Services;
using Tracr.Shared.DTOs;

namespace Tracr.Client.Pages
{
    public  partial class UserProperties : ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState>? AuthenticationStateTask { get; set; }

        public AuthenticationState? AuthenticationState { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public IPropertyService? PropertyService { get; set; }

        protected List<PropertyDto> UserPropertyDtos { get; set; } = new List<PropertyDto>();

        public string ErrorMessage { get; set; } = "";

        public bool RetrievingData { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            if (AuthenticationStateTask == null || NavigationManager == null || PropertyService == null)
                return;

            AuthenticationState = await AuthenticationStateTask;

            if (AuthenticationState.User.Identity == null || !AuthenticationState.User.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo($"/login/{Uri.EscapeDataString(NavigationManager.Uri)}");
            }
            else
            {
                await GetUserProperties();
            }
        }

        private async Task GetUserProperties()
        {
            if (PropertyService == null)
                return;

            ErrorMessage = "";

            try
            {
                RetrievingData = true;
                UserPropertyDtos = await PropertyService.GetUserProperties();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
            }
            finally
            {
                RetrievingData = false;
            }
        }
    }
}
