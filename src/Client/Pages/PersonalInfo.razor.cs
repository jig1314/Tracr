using Microsoft.AspNetCore.Components;
using Tracr.Client.ViewModels;

namespace Tracr.Client.Pages
{
    public partial class PersonalInfo : ComponentBase
    {

        public PersonalInfoViewModel PersonalInfoViewModel { get; set; } = new PersonalInfoViewModel();

        public string ErrorMessage { get; set; } = "";

        public bool SubmittingData { get; set; } = false;

        protected async Task UpdateInformation()
        {
            
        }
    }
}
