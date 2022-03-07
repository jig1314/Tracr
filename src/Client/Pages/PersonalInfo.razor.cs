using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Tracr.Client.Services;
using Tracr.Client.ViewModels;
using Tracr.Shared.DTOs;

namespace Tracr.Client.Pages
{
    public partial class PersonalInfo : ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState>? AuthenticationStateTask { get; set; }

        public AuthenticationState? AuthenticationState { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public IUserService? UserService { get; set; }

        public PersonalInfoViewModel PersonalInfoViewModel { get; set; } = new PersonalInfoViewModel();
        private BasicUserInfoDto? CachedBasicUserInfo;

        public string ErrorMessage { get; set; } = "";

        public bool SubmittingData { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            if (AuthenticationStateTask == null || NavigationManager == null)
                return;

            AuthenticationState = await AuthenticationStateTask;

            if (AuthenticationState.User.Identity == null || !AuthenticationState.User.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo($"/login/{Uri.EscapeDataString(NavigationManager.Uri)}");
            }
            else
            {
                await GetBasicUserData();
            }
        }

        private async Task GetBasicUserData()
        {
            if (UserService == null)
                return;

            ErrorMessage = "";

            try
            {
                SubmittingData = true;

                CachedBasicUserInfo = await UserService.GetBasicUserInfo();
                PersonalInfoViewModel = new PersonalInfoViewModel()
                {
                    FirstName = CachedBasicUserInfo.FirstName,
                    LastName = CachedBasicUserInfo.LastName
                };
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
            }
            finally
            {
                SubmittingData = false;
            }
        }

        protected async Task UpdateInformation()
        {
            if (UserService == null || CachedBasicUserInfo == null || NavigationManager ==  null)
                return;

            ErrorMessage = "";

            try
            {
                SubmittingData = true;

                CachedBasicUserInfo.FirstName = PersonalInfoViewModel.FirstName;
                CachedBasicUserInfo.LastName = PersonalInfoViewModel.LastName;

                await UserService.UpdateBasicUserInfo(CachedBasicUserInfo);
                NavigationManager.NavigateTo($"/userProfile/personalInfo", true);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"{ex.Message}";
            }
            finally
            {
                SubmittingData = false;
            }
        }
    }
}
