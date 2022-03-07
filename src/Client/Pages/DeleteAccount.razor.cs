using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Tracr.Client.Services;
using Tracr.Client.ViewModels;
using Tracr.Shared.DTOs;

namespace Tracr.Client.Pages
{
    public partial class DeleteAccount : ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState>? AuthenticationStateTask { get; set; }

        public AuthenticationState? AuthenticationState { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public IUserService? UserService { get; set; }

        [Inject]
        public SignOutSessionStateManager? SignOutManager { get; set; }

        public DeleteAccountViewModel DeleteAccountViewModel { get; set; } = new DeleteAccountViewModel();

        public string ErrorMessage { get; set; } = "";

        public bool SubmittingData { get; set; } = false;

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

        protected async Task DeleteUserAccount()
        {
            if (SignOutManager == null || UserService == null || NavigationManager == null)
                return;

            ErrorMessage = "";

            try
            {
                SubmittingData = true;

                var deleteAccountDto = new DeleteAccountDto()
                {
                    Password = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(DeleteAccountViewModel.Password))
                };

                await SignOutManager.SetSignOutState();
                await UserService.DeleteAccount(deleteAccountDto);
                NavigationManager.NavigateTo("/", true);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
            }
            finally
            {
                SubmittingData = false;
            }
        }
    }
}
