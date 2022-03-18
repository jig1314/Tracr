using System.Net.Http.Json;
using Tracr.Shared.DTOs;

namespace Tracr.Client.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly HttpClient httpClient;

        public PropertyService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<PropertyDto>> GetUserProperties()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<PropertyDto>>($"api/property/getUserProperties");

                if (response == null)
                    throw new Exception("User property information was not found!");

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
