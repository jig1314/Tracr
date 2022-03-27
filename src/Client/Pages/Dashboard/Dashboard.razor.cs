﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Tracr.Client.Services;
using Tracr.Shared.DTOs;
using Tracr.Shared.Models;

namespace Tracr.Client.Pages.Dashboard
{
    public partial class Dashboard : ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState>? AuthenticationStateTask { get; set; }

        public AuthenticationState? AuthenticationState { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public IPropertyService? PropertyService { get; set; }

        public string ErrorMessage { get; set; } = "";

        public bool LoadingData { get; set; } = false;

        protected List<PropertyDto> UserPropertyDtos { get; set; }

        protected List<PropertyIncome> PropertyIncome { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (AuthenticationStateTask == null || NavigationManager == null || PropertyService == null)
                return;

            AuthenticationState = await AuthenticationStateTask;

            if (AuthenticationState.User.Identity == null || !AuthenticationState.User.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo($"/login/{Uri.EscapeDataString(NavigationManager.Uri)}");
            }
            else
            {
                await GetUserProperties();
                await RefreshUserPropertyIncome();
            }
        }

        private async Task GetUserProperties()
        {
            if (PropertyService == null)
                return;

            ErrorMessage = "";

            try
            {
                LoadingData = true;
                UserPropertyDtos = await PropertyService.GetUserProperties();
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

        private async Task RefreshUserPropertyIncome()
        {
            if (PropertyService == null)
                return;

            ErrorMessage = "";

            try
            {
                LoadingData = true;

                PropertyIncome = await PropertyService.GetUserPropertyIncome();
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

        protected void GoToManageProperties()
        {
            if (NavigationManager == null)
                return;

            NavigationManager.NavigateTo($"/userProfile/manageProperties");
        }
    }
}
