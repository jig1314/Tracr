using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Tracr.Client.Services;
using Tracr.Client.ViewModels;
using Tracr.Shared.DTOs;

namespace Tracr.Client.Pages
{
    public partial class Register : ComponentBase
    {
        [Parameter]
        public string ReturnUrl { get; set; } = "";

        [Inject]
        public IUnauthorizedUserService? UserService { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        public RegisterViewModel RegisterViewModel { get; set; } = new RegisterViewModel();

        public string ErrorMessage { get; set; } = "";

        public bool SubmittingData { get; set; } = false;

        protected async Task RegisterUser()
        {
            var registerUserDto = new RegisterUserDto()
            {
                FirstName = RegisterViewModel.FirstName,
                LastName = RegisterViewModel.LastName,
                Email = RegisterViewModel.Email,
                UserName = RegisterViewModel.UserName,
                Password = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(RegisterViewModel.Password))
            };

            ErrorMessage = "";

            try
            {
                SubmittingData = true;

                if (UserService != null)
                    await UserService.RegisterNewUser(registerUserDto);

                if (NavigationManager != null)
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
