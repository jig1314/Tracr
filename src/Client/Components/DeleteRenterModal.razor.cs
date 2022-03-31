using BlazorStrap;
using Microsoft.AspNetCore.Components;

namespace Tracr.Client.Components
{
    public partial class DeleteRenterModal : ComponentBase
    {
        private int _renterId;
        protected BSModal Modal { get; set; }

        [Parameter]
        public EventCallback<int> OnDelete { get; set; }

        public void Show(int renterId)
        {
            _renterId = renterId;

            Modal.ShowAsync();
        }

        public async void Delete()
        {
            await Modal.HideAsync();

            await OnDelete.InvokeAsync(_renterId);
        }
    }
}
