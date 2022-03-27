using Microsoft.AspNetCore.Components;
using Tracr.Client.Services;
using Tracr.Shared.DTOs;

namespace Tracr.Client.Pages.Dashboard
{
    public partial class UserPropertyDashboardTile : ComponentBase
    {
        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public IPropertyService? PropertyService { get; set; }

        [Parameter]
        public List<PropertyDto> UserProperties { get; set; }

        [Parameter]
        public HashSet<int> SelectedPropertyIds { get; set; }

        [Parameter]
        public EventCallback<HashSet<int>> SelectedPropertyIdsChanged { get; set; }

        protected void SelectedPropertyChanged(int propertyId, bool isChecked)
        {
            if (!isChecked && SelectedPropertyIds.Contains(propertyId))
            {
                SelectedPropertyIds.Remove(propertyId);
            }
            else if (isChecked && !SelectedPropertyIds.Contains(propertyId))
            {
                SelectedPropertyIds.Add(propertyId);
            }

            SelectedPropertyIdsChanged.InvokeAsync(SelectedPropertyIds);
        }
    }
}
