using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Tracr.Client.Services;
using Tracr.Client.ViewModels;
using Tracr.Shared.DTOs;

namespace Tracr.Client.Pages
{
    public partial class ResetPassword : ComponentBase
    {
        [Inject]
        public IUnauthorizedUserService? UserService { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        public ResetPasswordViewModel ResetPasswordViewModel { get; set; } = new ResetPasswordViewModel();

        public string ErrorMessage { get; set; } = "";

        public bool SubmittingData { get; set; } = false;

        protected async Task ResetUserPassword()
        {
            var resetPasswordDto = new ResetPasswordDto()
            {
                UserName = ResetPasswordViewModel.UserName,
                NewPassword = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(ResetPasswordViewModel.Password))
            };

            ErrorMessage = "";

            if (NavigationManager == null || UserService == null)
                return;

            try
            {
                SubmittingData = true;
                await UserService.ResetPassword(resetPasswordDto);
                NavigationManager.NavigateTo("login");
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
