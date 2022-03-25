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
        
        public async Task<PropertyDto> GetProperty(int propertyId)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<PropertyDto>($"api/property/getProperty/{propertyId}");

                if (response == null)
                    throw new Exception("Property information was not found!");

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<RenterDto>> GetRentersByProperty(int propertyId)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<RenterDto>>($"api/property/getPropertyRenters/{propertyId}");

                if (response == null)
                    throw new Exception("Property's renter information was not found!");

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RenterDto> GetRenter(int renterId)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<RenterDto>($"api/property/getRenter/{renterId}");

                if (response == null)
                    throw new Exception("Renter information was not found!");

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CreateProperty(PropertyDto propertyDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"api/property/createProperty", propertyDto);
                var content = await response.Content.ReadFromJsonAsync<PropertyDto>();
                response.EnsureSuccessStatusCode();

                if (content == null)
                    throw new Exception("Property information was not returned!");

                return content.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateRenter(RenterDto renterDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"api/property/createRenter", renterDto);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateProperty(PropertyDto propertyDto)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"api/property/updateProperty", propertyDto);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateRenter(RenterDto renterDto)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"api/property/updateRenter", renterDto);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteProperty(int propertyId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/property/deleteProperty/{propertyId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteRenter(int renterId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/property/deleteRenter/{renterId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
