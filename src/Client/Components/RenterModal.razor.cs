using AutoMapper;
using BlazorStrap;
using Microsoft.AspNetCore.Components;
using Tracr.Client.Services;
using Tracr.Client.ViewModels;
using Tracr.Shared.DTOs;

namespace Tracr.Client.Components
{
    public partial class RenterModal : ComponentBase
    {
        public enum ViewMode
        {
            Add,
            Edit
        }

        private BSModal Modal;

        [Inject]
        public IPropertyService? PropertyService { get; set; }

        [Inject]
        public IMapper? Mapper { get; set; }

        [Parameter]
        public EventCallback OnSubmit { get; set; }

        public int PropertyId { get; set; }

        public int? RenterId { get; set; }

        public ViewMode CurrentViewMode { get; set; }

        public RenterViewModel RenterViewModel { get; set; } = new RenterViewModel();

        public string ErrorMessage { get; set; } = "";

        public bool LoadingData { get; set; } = true;

        public async void Show()
        {
            await Modal.ShowAsync();

            if (CurrentViewMode == ViewMode.Add)
            {
                RenterViewModel = new RenterViewModel();
                LoadingData = false;
            }
            else if (CurrentViewMode == ViewMode.Edit && RenterId.HasValue)
            {
                RenterId = RenterId.Value;
                await GetRenterInformation();
            }

            StateHasChanged();
        }

        private async Task GetRenterInformation()
        {
            if (PropertyService == null || Mapper == null || !RenterId.HasValue)
                return;

            ErrorMessage = "";

            try
            {
                LoadingData = true;

                var renterDto = await PropertyService.GetRenter(RenterId.Value);
                RenterViewModel = Mapper.Map<RenterViewModel>(renterDto);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
            }
            finally
            {
                LoadingData = false;
            }

            StateHasChanged();
        }

        protected async Task SubmitChanges()
        {
            if (CurrentViewMode == ViewMode.Add)
            {
                await CreateRenter();
            }
            else if (CurrentViewMode == ViewMode.Edit)
            {
                await UpdateRenter();
            }
        }

        private async Task CreateRenter()
        {
            if (PropertyService == null || Mapper == null)
                return;

            ErrorMessage = "";

            try
            {
                LoadingData = true;

                var renterDto = Mapper.Map<RenterDto>(RenterViewModel);
                renterDto.PropertyId = PropertyId;

                await PropertyService.CreateRenter(renterDto);
                await Modal.HideAsync();
                await OnSubmit.InvokeAsync(renterDto);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
                LoadingData = false;
            }
        }

        private async Task UpdateRenter()
        {
            if (PropertyService == null || Mapper == null || !RenterId.HasValue)
                return;

            ErrorMessage = "";

            try
            {
                LoadingData = true;

                var renterDto = Mapper.Map<RenterDto>(RenterViewModel);
                renterDto.PropertyId = PropertyId;

                await PropertyService.UpdateRenter(renterDto);
                await Modal.HideAsync();
                await OnSubmit.InvokeAsync(renterDto);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
                LoadingData = false;
            }
        }
    }
}
