using Microsoft.AspNetCore.Components;
using Tracr.Client.ViewModels;

namespace Tracr.Client.Pages
{
    public partial class Register : ComponentBase
    {
        [Parameter]
        public string ReturnUrl { get; set; } = "";

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        public RegisterViewModel RegisterViewModel { get; set; } = new RegisterViewModel();

        public string ErrorMessage { get; set; } = "";

        public bool SubmittingData { get; set; } = false;

        protected override void OnInitialized()
        {
            
        }

        protected async Task RegisterUser()
        {
            
        }
    }
}
