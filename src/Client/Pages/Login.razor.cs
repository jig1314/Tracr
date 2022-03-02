using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Tracr.Client.Services;
using Tracr.Client.ViewModels;
using Tracr.Shared.DTOs;

namespace Tracr.Client.Pages
{
    public partial class Login : ComponentBase
    {
        [Parameter]
        public string ReturnUrl { get; set; } = "";

        [Inject]
        public IUnauthorizedUserService? UserService { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        public LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();

        public string ErrorMessage { get; set; } = "";

        public bool SubmittingData { get; set; } = false;

        protected async Task LoginUser()
        {
            var loginDto = new LoginDto()
            {
                UserName = LoginViewModel.UserName,
                Password = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(LoginViewModel.Password)),
                RememberMe = LoginViewModel.RememberMe
            };

            ErrorMessage = "";

            if (NavigationManager == null || UserService == null)
                return;

            try
            {
                SubmittingData = true;
                await UserService.Login(loginDto);
                if (!string.IsNullOrWhiteSpace(ReturnUrl))
                    NavigationManager.NavigateTo($"authentication/login?returnUrl={ReturnUrl}");
                else
                    NavigationManager.NavigateTo("authentication/login");
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