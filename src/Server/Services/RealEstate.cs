using Microsoft.AspNetCore.WebUtilities;
using Tracr.Server.Models;
using Tracr.Shared.DTOs;

namespace Tracr.Server.Services
{
    public class RealEstate : IRealEstate
    {
        private readonly HttpClient _httpClient;

        public RealEstate(IHttpClientFactory clientFactory)
        {
            this._httpClient = clientFactory.CreateClient("realestate");
        }

        public async Task<RealEstateResponse> AverageRateByZip(int postal_code)
        {
            var query = new Dictionary<string, string?>() { { "postal_code", postal_code.ToString() } };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("finance/average-rate", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new RealEstateResponse("AverageRateByZip", body);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public Task<RealEstateResponse> EstimatedValue(int property_id)
        {
            throw new NotImplementedException();
        }

        public Task<RealEstateResponse> ForRent(RealEstateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<RealEstateResponse> ForRentCount(RealEstateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<RealEstateResponse> ForSale(RealEstateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<RealEstateResponse> ForSaleCount(RealEstateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<RealEstateResponse> LocationNoiseScore(decimal longitude, decimal latitude)
        {
            throw new NotImplementedException();
        }

        public Task<RealEstateResponse> LocationSchools(string input, string language)
        {
            throw new NotImplementedException();
        }

        public Task<RealEstateResponse> LocationSuggest(string input)
        {
            throw new NotImplementedException();
        }

        public Task<RealEstateResponse> MortgageCalc(RealEstateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<RealEstateResponse> PropertyByMlsId(string mls_id)
        {
            throw new NotImplementedException();
        }

        public Task<RealEstateResponse> PropertyByProperty(string property_id)
        {
            throw new NotImplementedException();
        }

        public Task<RealEstateResponse> PropertyDetail(string property_id)
        {
            throw new NotImplementedException();
        }

        public Task<RealEstateResponse> RateTrends(bool is_refinance)
        {
            throw new NotImplementedException();
        }

        public Task<RealEstateResponse> SimilarHomes(int property_id)
        {
            throw new NotImplementedException();
        }

        public Task<RealEstateResponse> SoldHomes(RealEstateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
