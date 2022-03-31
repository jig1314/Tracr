using BlazorStrap;
using Microsoft.AspNetCore.Components;

namespace Tracr.Client.Components
{
    public partial class DeletePropertyModal : ComponentBase
    {
        private int _propertyId;
        protected BSModal Modal { get; set; }

        [Parameter]
        public EventCallback<int> OnDelete { get; set; }

        public void Show(int propertyId)
        {
            _propertyId = propertyId;

            Modal.ShowAsync();
        }

        public async void Delete()
        {
            await Modal.HideAsync();

            await OnDelete.InvokeAsync(_propertyId);
        }
    }
}
