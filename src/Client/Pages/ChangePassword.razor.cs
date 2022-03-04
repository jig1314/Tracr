using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Tracr.Client.Services;
using Tracr.Client.ViewModels;
using Tracr.Shared.DTOs;

namespace Tracr.Client.Pages
{
    public partial class ChangePassword : ComponentBase
    {
        [Inject]
        public IUnauthorizedUserService? UserService { get; set; }

        public ResetPasswordViewModel ResetPasswordViewModel { get; set; } = new ResetPasswordViewModel();

        public string ErrorMessage { get; set; } = "";

        public bool SubmittingData { get; set; } = false;

        [Parameter]
        public EventCallback PasswordChanged { get; set; }

        protected async Task ResetUserPassword()
        {
            var resetPasswordDto = new ResetPasswordDto()
            {
                UserName = ResetPasswordViewModel.UserName,
                NewPassword = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(ResetPasswordViewModel.Password))
            };

            ErrorMessage = "";

            if (UserService == null)
                return;

            try
            {
                SubmittingData = true;
                await UserService.ResetPassword(resetPasswordDto);
                await PasswordChanged.InvokeAsync();
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
