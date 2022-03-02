using Microsoft.AspNetCore.Components;
using Tracr.Client.ViewModels;

namespace Tracr.Client.Pages
{
    public partial class ResetPassword : ComponentBase
    {
        public ResetPasswordViewModel ResetPasswordViewModel { get; set; } = new ResetPasswordViewModel();

        public string ErrorMessage { get; set; } = "";

        public bool SubmittingData { get; set; } = false;

        protected async Task ResetUserPassword()
        {
            
        }
    }
}
