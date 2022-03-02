using Microsoft.AspNetCore.Components;
using Tracr.Client.ViewModels;

namespace Tracr.Client.Pages
{
    public partial class Login : ComponentBase
    {
        [Parameter]
        public string ReturnUrl { get; set; } = "";

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        public LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();

        public string ErrorMessage { get; set; } = "";

        public bool SubmittingData { get; set; } = false;

        protected async Task LoginUser()
        {

        }
    }
}