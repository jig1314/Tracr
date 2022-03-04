using Microsoft.AspNetCore.Components;
using Tracr.Client.ViewModels;

namespace Tracr.Client.Pages
{
    public partial class DeleteAccount : ComponentBase
    {

        public DeleteAccountViewModel DeleteAccountViewModel { get; set; } = new DeleteAccountViewModel();

        public string ErrorMessage { get; set; } = "";

        public bool SubmittingData { get; set; } = false;

        protected async Task DeleteUserAccount()
        {
            
        }
    }
}
