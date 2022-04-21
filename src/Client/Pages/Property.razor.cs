using AutoMapper;
using BlazorStrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Tracr.Client.Components;
using Tracr.Client.Services;
using Tracr.Client.ViewModels;
using Tracr.Shared.DTOs;

namespace Tracr.Client.Pages
{
    public partial class Property : ComponentBase
    {
        public enum ViewMode
        {
            Add,
            Edit
        }

        [CascadingParameter]
        public Task<AuthenticationState>? AuthenticationStateTask { get; set; }

        public AuthenticationState? AuthenticationState { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public IPropertyService? PropertyService { get; set; }

        [Inject]
        public IMapper? Mapper { get; set; }

        [Parameter]
        public ViewMode CurrentViewMode { get; set; }

        [Parameter]
        public int? PropertyId { get; set; }

        protected DeletePropertyModal DeletePropertyModal { get; set; }

        protected DeleteRenterModal DeleteRenterModal { get; set; }

        protected RenterModal RenterModal { get; set; }

        public PropertyViewModel PropertyViewModel { get; set; } = new PropertyViewModel();
        protected List<RenterTableViewModel> Renters = new List<RenterTableViewModel>();

        protected List<RenterDto> RenterDtos = new List<RenterDto>();

        public string ErrorMessage { get; set; } = "";

        public bool LoadingData { get; set; } = false;

        public bool LoadingRenterData { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            if (AuthenticationStateTask == null || NavigationManager == null || PropertyService == null || Mapper == null)
                return;

            AuthenticationState = await AuthenticationStateTask;

            if (AuthenticationState.User.Identity == null || !AuthenticationState.User.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo($"/login/{Uri.EscapeDataString(NavigationManager.Uri)}");
            }
            else
            {
                if (CurrentViewMode == ViewMode.Edit)
                {
                    await GetPropertyInformation();
                    await GetRentersInformation();
                }
            }
        }

        private async Task GetPropertyInformation()
        {
            if (PropertyService == null || Mapper == null || !PropertyId.HasValue)
                return;

            ErrorMessage = "";

            try
            {
                LoadingData = true;

                var propertyDto = await PropertyService.GetProperty(PropertyId.Value);
                PropertyViewModel = Mapper.Map<PropertyViewModel>(propertyDto);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
            }
            finally
            {
                LoadingData = false;
            }
        }

        private async Task GetRentersInformation()
        {
            if (PropertyService == null || Mapper == null || !PropertyId.HasValue)
                return;

            ErrorMessage = "";

            try
            {
                LoadingRenterData = true;

                RenterDtos = await PropertyService.GetRentersByProperty(PropertyId.Value);
                Renters = Mapper.Map<List<RenterTableViewModel>>(RenterDtos);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
            }
            finally
            {
                LoadingRenterData = false;
            }
        }

        protected async Task SubmitProperty()
        {
            if (CurrentViewMode == ViewMode.Add)
            {
                await CreateProperty();
            }
            else if (CurrentViewMode == ViewMode.Edit)
            {
                await UpdateProperty();
            }
            else if (CurrentViewMode == ViewMode.Add)
            {
                await CancelProperty();
            }
            else if (CurrentViewMode == ViewMode.Edit)
            {
                await CancelProperty();
            }
        }

        private async Task CreateProperty()
        {
            if (PropertyService == null || Mapper == null || NavigationManager == null)
                return;

            ErrorMessage = "";

            try
            {
                LoadingData = true;
                var propertyDto = Mapper.Map<PropertyDto>(PropertyViewModel);

                var propertyId = await PropertyService.CreateProperty(propertyDto);
                NavigationManager.NavigateTo($"/userProfile/manageProperties/edit/{propertyId}");
                StateHasChanged();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
            }
            finally
            {
                LoadingData = false;
            }
        }

        private async Task UpdateProperty()
        {
            if (PropertyService == null || Mapper == null || !PropertyId.HasValue || NavigationManager == null)
                return;

            ErrorMessage = "";

            try
            {
                LoadingData = true;
                var propertyDto = Mapper.Map<PropertyDto>(PropertyViewModel);
                propertyDto.Id = PropertyId.Value;

                await PropertyService.UpdateProperty(propertyDto);
                NavigationManager.NavigateTo($"/userProfile/manageProperties/");
                await GetPropertyInformation();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
            }
            finally
            {
                LoadingData = false;
            }
        }

        private async Task CancelProperty() //Cancel Button
        {
            if (PropertyService == null || Mapper == null || NavigationManager == null)
                return;

            //ErrorMessage = "";

            try
            {
               /* LoadingData = false;
                var propertyDto = Mapper.Map<PropertyDto>(PropertyViewModel);

                var propertyId = await PropertyService.CreateProperty(propertyDto);
                await PropertyService.DeleteProperty(propertyId);*/
                NavigationManager.NavigateTo($"/userProfile/manageProperties/");
            }
            /*catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
            }*/
            finally
            {
                //LoadingData = false;
            }
        }

        protected async Task DeleteProperty(int propertyId)
        {
            if (PropertyService == null || NavigationManager == null)
                return;

            ErrorMessage = "";

            try
            {
                LoadingData = true;
                await PropertyService.DeleteProperty(propertyId);
                NavigationManager.NavigateTo("/userProfile/manageProperties/");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
            }
            finally
            {
                LoadingData = false;
            }
        }

        public void OpenAddRenterModal()
        {
            if (CurrentViewMode == ViewMode.Edit && PropertyId.HasValue)
            {
                RenterModal.PropertyId = PropertyId.Value;
                RenterModal.CurrentViewMode = RenterModal.ViewMode.Add;
                RenterModal.Show();
            }
        }

        public void OpenEditRenterModal(int renterId)
        {
            if (CurrentViewMode == ViewMode.Edit && PropertyId.HasValue)
            {
                RenterModal.PropertyId = PropertyId.Value;
                RenterModal.RenterId = renterId;
                RenterModal.CurrentViewMode = RenterModal.ViewMode.Edit;
                RenterModal.Show();
            }
        }

        //Cancel Button Added
       /* private void btnClose_Click(object sender, EventArgs e)
        {

            if (unsavedChanges)
            {
                var result = MessageBox.Show("Save changes?", "unsaved changes", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    SaveChanges();
                }

                if (result == DialogResult.Cancel)
                {
                    result = DialogResult.None;
                }

                this.DialogResult = result;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = cancelClose;
            cancelClose = false;
        }*/

        protected async Task DeleteRenter(int renterId)
        {
            if (PropertyService == null)
                return;

            ErrorMessage = "";

            try
            {
                LoadingRenterData = true;
                await PropertyService.DeleteRenter(renterId);
                await GetRentersInformation();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
            }
            finally
            {
                LoadingRenterData = false;
            }
        }
    }
}
