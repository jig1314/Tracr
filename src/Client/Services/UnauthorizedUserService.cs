using System.Net.Http.Json;
using Tracr.Shared.DTOs;

namespace Tracr.Client.Services
{
    public class UnauthorizedUserService : IUnauthorizedUserService
    {
        private readonly HttpClient httpClient;

        public UnauthorizedUserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task RegisterNewUser(RegisterUserDto registerUserDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/user/register", registerUserDto);
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
