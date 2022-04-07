using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using Tracr.Server.Models;
using Tracr.Server.ResourceParameters;
using Tracr.Shared.Models;

namespace Tracr.Server.Repositories
{
    public class RealEstateRepo : IRealEstateRepo
    {
        private readonly HttpClient _httpClient;

        public RealEstateRepo(IHttpClientFactory clientFactory)
        {
            this._httpClient = clientFactory.CreateClient("realestate");
        }

        //public async Task<RealEstateResponse> EstimatedValue(long property_id)
        //{
        //    var query = new Dictionary<string, string?>() { { "property_id", property_id.ToString() } };

        //    var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("for-sale/home-estimate-value", query));

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var body = await response.Content.ReadAsStringAsync();
        //        return new RealEstateResponse("EstimatedValue", body);
        //    }

        //    throw new Exception(response.StatusCode.ToString());
        //}

        public async Task<List<PropertyForSale>> ForSale(ForSaleResourceParameters param)
        {
            var query = new Dictionary<string, string?>();

            // load required params
            if (string.IsNullOrEmpty(param.city) || string.IsNullOrEmpty(param.state_code))
                throw new ArgumentException();

            query.Add("city", param.city);
            query.Add("state_code", param.state_code);
            query.Add("limit", param.limit.ToString());
            query.Add("offset", param.offset.ToString());

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

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("/v2/for-sale", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<JToken>(body);
                var data = content.SelectToken("data");
                var homeSearch = data.SelectToken("home_search");
                var results = homeSearch.SelectTokens("results[*]");

                var properties = results.Select(property => new PropertyForSale()
                {
                    PhotoURL = (string)property.SelectToken("primary_photo")["href"],
                    ListPrice = Convert.ToDecimal((string)property["list_price"]),
                    YearBuilt = Convert.ToInt32((string)property.SelectToken("description")["year_built"]),
                    NumBedrooms = Convert.ToInt32((string)property.SelectToken("description")["beds"]),
                    NumBathrooms = (decimal)(Convert.ToInt32((string)property.SelectToken("description")["baths_full"]) + (Convert.ToInt32((string)property.SelectToken("description")["baths_half"]) * .5)),
                    SqaureFootage = Convert.ToInt32((string)property.SelectToken("description")["sqft"]),
                    StreetAddress = (string)property.SelectToken("location").SelectToken("address")["line"],
                    City = (string)property.SelectToken("location").SelectToken("address")["city"],
                    State = (string)property.SelectToken("location").SelectToken("address")["state_code"],
                    ZipCode = (string)property.SelectToken("location").SelectToken("address")["postal_code"]
                });

                return properties.ToList();
            }

            throw new Exception(response.StatusCode.ToString());
        }

        //public async Task<RealEstateResponse> LocationSuggest(string input)
        //{
        //    var query = new Dictionary<string, string?>() { { "input", input } };

        //    var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("location/suggest", query));

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var body = await response.Content.ReadAsStringAsync();
        //        return new RealEstateResponse("LocationSuggest", body);
        //    }

        //    throw new Exception(response.StatusCode.ToString());
        //}

        public async Task<MortageCalculation> MortgageCalc(MortageCalcResourceParameters param)
        {            
             var query = new Dictionary<string, string?>();

            // load required params
            if (!param.hoa_fees.HasValue || !param.percent_tax_rate.HasValue || !param.year_term.HasValue
                || !param.percent_rate.HasValue || !param.monthly_home_insurance.HasValue || !param.price.HasValue )
                throw new ArgumentException();

            query.Add("show_amortization", param.show_amortization ? "True" : "False");
            query.Add("year_term", param.year_term.ToString());
            query.Add("hoa_fees", param.hoa_fees.ToString());
            query.Add("percent_tax_rate", param.percent_tax_rate.ToString());
            query.Add("percent_rate", param.percent_rate.ToString());
            query.Add("down_payment", param.down_payment.ToString());
            query.Add("monthly_home_insurance", param.monthly_home_insurance.ToString());
            query.Add("price", param.price.ToString());

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("finance/mortgage-calculate", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<JToken>(body);
                var data = content.SelectToken("data");

                var mortageCalculation = new MortageCalculation()
                {
                    TermInYears = Convert.ToInt32((string)data["term"]),
                    Principal = Convert.ToDecimal((string)data["loan_amount"]),
                    MonthlyPayment = Convert.ToDecimal((string)data["monthly_payment"]),
                    APR = Convert.ToDecimal((string)data["rate"]) * 100
                };

                return mortageCalculation;
            }

            throw new Exception(response.StatusCode.ToString());
        }

        //public async Task<RealEstateResponse> PropertyDetail(long property_id)
        //{
        //    var query = new Dictionary<string, string?>() { { "property_id", property_id.ToString() } };

        //    var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("v2/property-detail", query));

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var body = await response.Content.ReadAsStringAsync();
        //        return new RealEstateResponse("PropertyDetail", body);
        //    }

        //    throw new Exception(response.StatusCode.ToString());
        //}

        //public async Task<RealEstateResponse> RateTrends(bool is_refinance)
        //{
        //    var query = new Dictionary<string, string?>() { { "is_refinance", is_refinance.ToString() } };

        //    var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("finance/rate-trends", query));

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var body = await response.Content.ReadAsStringAsync();
        //        return new RealEstateResponse("RateTrends", body);
        //    }

        //    throw new Exception(response.StatusCode.ToString());
        //}

        //public async Task<RealEstateResponse> SimilarHomes(long property_id)
        //{
        //    var query = new Dictionary<string, string?>() { { "property_id", property_id.ToString() } };

        //    var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("for-sale/similiar-homes", query));

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var body = await response.Content.ReadAsStringAsync();
        //        return new RealEstateResponse("SimilarHomes", body);
        //    }

        //    throw new Exception(response.StatusCode.ToString());
        //}
    }
}
