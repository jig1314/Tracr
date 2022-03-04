using System.Net.Http.Json;
using Tracr.Shared.DTOs;

namespace Tracr.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<BasicUserInfoDto> GetBasicUserInfo()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<BasicUserInfoDto>($"api/user/basicInfo");

                if (response == null)
                    throw new Exception("User information was not found!");

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateBasicUserInfo(BasicUserInfoDto basicUserInfoDto)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync("api/user/updateBasicInfo", basicUserInfoDto);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAccount(DeleteAccountDto deleteAccountDto)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync("api/user/deleteAccount", deleteAccountDto);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
