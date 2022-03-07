using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Tracr.Client.Pages
{
    public partial class UserProfile : ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState>? AuthenticationStateTask { get; set; }

        public AuthenticationState? AuthenticationState { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (AuthenticationStateTask == null || NavigationManager == null)
                return;

            AuthenticationState = await AuthenticationStateTask;

            if (AuthenticationState.User.Identity == null || !AuthenticationState.User.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo($"/login/{Uri.EscapeDataString(NavigationManager.Uri)}");
            }
        }
    }
}
