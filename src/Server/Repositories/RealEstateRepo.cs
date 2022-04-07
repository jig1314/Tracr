using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Tracr.Server.Models;
using Tracr.Shared.DTOs;

namespace Tracr.Server.Repositories
{
    public class RealEstateRepo : IRealEstateRepo
    {
        private readonly HttpClient _httpClient;

        public RealEstateRepo(IHttpClientFactory clientFactory)
        {
            this._httpClient = clientFactory.CreateClient("realestate");
        }

        public async Task<RealEstateResponse> AverageRateByZip(int postal_code)
        {
            if (postal_code < 5)
                throw new ArgumentException();

            var query = new Dictionary<string, string?>() { { "postal_code", postal_code.ToString() } };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("finance/average-rate", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new RealEstateResponse("AverageRateByZip", body);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<RealEstateResponse> EstimatedValue(long property_id)
        {
            var query = new Dictionary<string, string?>() { { "property_id", property_id.ToString() } };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("for-sale/home-estimate-value", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new RealEstateResponse("EstimatedValue", body);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<RealEstateResponse> ForRent(RealEstateDto param)
        {
            var query = new Dictionary<string, string?>();

            // load required params
            if (string.IsNullOrEmpty(param.city) || string.IsNullOrEmpty(param.state_code))
                throw new ArgumentException();

            query.Add("city", param.city);
            query.Add("state_code", param.state_code);
            query.Add("limit", param.limit.ToString());
            query.Add("offset", param.offset.ToString());
            query.Add("sort", param.sort.ToString());

            //optional params
            if (param.location.HasValue)
                query.Add("location", param.location.ToString());

            if (param.price_min.HasValue)
                query.Add("price_min", param.price_min.ToString());

            if (param.price_max.HasValue)
                query.Add("price_max", param.price_max.ToString());

            if (param.beds_min.HasValue)
                query.Add("beds_min", param.beds_min.ToString());

            if (param.beds_max.HasValue)
                query.Add("beds_max", param.beds_max.ToString());

            if (param.baths_min.HasValue)
                query.Add("baths_min", param.baths_min.ToString());

            if (param.baths_max.HasValue)
                query.Add("baths_max", param.baths_max.ToString());

            if (param.property_type.HasValue)
                query.Add("property_type", param.property_type.ToString());

            if (param.community_ammenities.Count > 0)
            {
                var sb = new StringBuilder();

                param.community_ammenities.ForEach(a =>
                {
                    sb.Append($"{a.ToString()},");
                });

                query.Add("community_ammenities", sb.ToString());
            }                  

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("/v2/for-rent", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new RealEstateResponse("ForRent", body);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<RealEstateResponse> ForSale(RealEstateDto param)
        {
            var query = new Dictionary<string, string?>();

            // load required params
            if (string.IsNullOrEmpty(param.city) || string.IsNullOrEmpty(param.state_code))
                throw new ArgumentException();

            query.Add("city", param.city);
            query.Add("state_code", param.state_code);
            query.Add("limit", param.limit.ToString());
            query.Add("offset", param.offset.ToString());
            query.Add("sort", param.sort.ToString());

            //optional params
            if (param.location.HasValue)
                query.Add("location", param.location.ToString());

            if (param.price_min.HasValue)
                query.Add("price_min", param.price_min.ToString());

            if (param.price_max.HasValue)
                query.Add("price_max", param.price_max.ToString());

            if (param.beds_min.HasValue)
                query.Add("beds_min", param.beds_min.ToString());

            if (param.beds_max.HasValue)
                query.Add("beds_max", param.beds_max.ToString());

            if (param.baths_min.HasValue)
                query.Add("baths_min", param.baths_min.ToString());

            if (param.baths_max.HasValue)
                query.Add("baths_max", param.baths_max.ToString());

            if (param.property_type.HasValue)
                query.Add("property_type", param.property_type.ToString());

            if (param.new_construction.HasValue)
                query.Add("new_construction", param.new_construction.ToString());

            if (param.home_size_max.HasValue)
                query.Add("home_size_max", param.home_size_max.ToString());

            if (param.community_ammenities.Count > 0)
            {
                var sb = new StringBuilder();

                param.community_ammenities.ForEach(a =>
                {
                    sb.Append($"{a.ToString()},");
                });

                query.Add("community_ammenities", sb.ToString());
            }


            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("/v2/for-sale", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new RealEstateResponse("ForSale", body);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<RealEstateResponse> LocationNoiseScore(decimal longitude, decimal latitude)
        {
            var query = new Dictionary<string, string?>() { 
                { "latitude", latitude.ToString() },
                { "longitude", longitude.ToString() }
            };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("location/noise-score", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new RealEstateResponse("LocationNoiseScore", body);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<RealEstateResponse> LocationSchools(string city, string state_code, int postal_code)
        {
            var query = new Dictionary<string, string?>() {
                { "ciy", city },
                { "state_code", state_code },
                { "postal_code", postal_code.ToString() }
            };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("location/schools", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new RealEstateResponse("LocationNoiseScore", body);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<RealEstateResponse> LocationSuggest(string input)
        {
            var query = new Dictionary<string, string?>() { { "input", input } };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("location/suggest", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new RealEstateResponse("LocationSuggest", body);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<RealEstateResponse> MortgageCalc(RealEstateDto param)
        {            
             var query = new Dictionary<string, string?>();

            // load required params
            if (!param.hoa_fees.HasValue || !param.percent_tax_rate.HasValue || !param.year_term.HasValue
                || !param.percent_rate.HasValue || !param.monthly_home_insurance.HasValue || !param.price.HasValue )
                throw new ArgumentException();

            query.Add("show_amortization", param.city);
            query.Add("year_term", param.year_term.ToString());
            query.Add("hoa_fees", param.hoa_fees.ToString());
            query.Add("percent_tax_rate", param.percent_tax_rate.ToString());
            query.Add("percent_rate", param.percent_rate.ToString());
            query.Add("monthly_home_insurance", param.monthly_home_insurance.ToString());
            query.Add("price", param.price.ToString());

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("finance/mortgage-calculate", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new RealEstateResponse("ForSale", body);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<RealEstateResponse> PropertyByMlsId(string mls_id)
        {
            var query = new Dictionary<string, string?>() { { "mls_id", mls_id } };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("property-by-mls-id", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new RealEstateResponse("PropertyByMlsId", body);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<RealEstateResponse> PropertyDetail(long property_id)
        {
            var query = new Dictionary<string, string?>() { { "property_id", property_id.ToString() } };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("v2/property-detail", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new RealEstateResponse("PropertyDetail", body);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<RealEstateResponse> RateTrends(bool is_refinance)
        {
            var query = new Dictionary<string, string?>() { { "is_refinance", is_refinance.ToString() } };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("finance/rate-trends", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new RealEstateResponse("RateTrends", body);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<RealEstateResponse> SimilarHomes(long property_id)
        {
            var query = new Dictionary<string, string?>() { { "property_id", property_id.ToString() } };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("for-sale/similiar-homes", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new RealEstateResponse("SimilarHomes", body);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<RealEstateResponse> SoldHomes(RealEstateDto param)
        {
            var query = new Dictionary<string, string?>();

            // load required params
            if (string.IsNullOrEmpty(param.city) || string.IsNullOrEmpty(param.state_code))
                throw new ArgumentException();

            query.Add("city", param.city);
            query.Add("state_code", param.state_code);
            query.Add("limit", param.limit.ToString());
            query.Add("offset", param.offset.ToString());
            query.Add("sort", param.sort.ToString());
            query.Add("max_sold_days", param.max_sold_days.ToString());


            //optional params
            if (param.location.HasValue)
                query.Add("location", param.location.ToString());

            if (param.price_min.HasValue)
                query.Add("price_min", param.price_min.ToString());

            if (param.price_max.HasValue)
                query.Add("price_max", param.price_max.ToString());

            if (param.beds_min.HasValue)
                query.Add("beds_min", param.beds_min.ToString());

            if (param.beds_max.HasValue)
                query.Add("beds_max", param.beds_max.ToString());

            if (param.baths_min.HasValue)
                query.Add("baths_min", param.baths_min.ToString());

            if (param.baths_max.HasValue)
                query.Add("baths_max", param.baths_max.ToString());

            if (param.property_type.HasValue)
                query.Add("property_type", param.property_type.ToString());

            if (param.new_construction.HasValue)
                query.Add("new_construction", param.new_construction.ToString());

            if (param.home_size_max.HasValue)
                query.Add("home_size_max", param.home_size_max.ToString());


            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("/sold-homes", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new RealEstateResponse("SoldHomes", body);
            }

            throw new Exception(response.StatusCode.ToString());
        }
    }
}
