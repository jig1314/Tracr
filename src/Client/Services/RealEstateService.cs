using System.Net.Http.Json;
using Tracr.Client.Utility;
using Tracr.Shared.Models;
using Tracr.Shared.ResourceParameters;

namespace Tracr.Client.Services
{
    public class RealEstateService : IRealEstateService
    {
        private readonly HttpClient httpClient;

        public RealEstateService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<StateCode>> GetStates()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<StateCode>>($"api/realestate/states");

                if (response == null)
                    throw new Exception("State Codes were not found!");

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<SortByOption>> GetSortByOptions()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<SortByOption>>($"api/realestate/sortByOptions");

                if (response == null)
                    throw new Exception("Sort By Options were not found!");

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PropertyForSale>> GetPropertiesForSale(ForSaleResourceParameters resourceParameters)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<PropertyForSale>>($"api/realestate/For-Sale?{resourceParameters.GetQueryString()}");

                if (response == null)
                    throw new Exception("For Sale Properties were not found!");

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
