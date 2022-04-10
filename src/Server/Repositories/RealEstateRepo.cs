using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tracr.Shared.Models;
using Tracr.Shared.ResourceParameters;

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
            if (string.IsNullOrEmpty(param.city) || !param.stateId.HasValue)
                throw new ArgumentException();

            query.Add("city", param.city);
            query.Add("state_code", GetStateCodes().FirstOrDefault(s => s.Id == param.stateId).Code);
            query.Add("sort", GetSortByOptions().FirstOrDefault(s => s.Id == param.sortByOption).Code);
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
                    PhotoURL = property.SelectToken("primary_photo").HasValues ? (string)property.SelectToken("primary_photo")["href"] : null,
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

        public List<StateCode> GetStateCodes()
        {
            return new List<StateCode>()
            {
                new StateCode() { Id = 1, Code = "AL", Description = "Alabama" },
                new StateCode() { Id = 2, Code = "AK", Description = "Alaska" },
                new StateCode() { Id = 3, Code = "AZ", Description = "Arizona" },
                new StateCode() { Id = 4, Code = "AR", Description = "Arkansas" },
                new StateCode() { Id = 5, Code = "CA", Description = "California" },
                new StateCode() { Id = 6, Code = "CO", Description = "Colorado" },
                new StateCode() { Id = 7, Code = "CT", Description = "Connecticut" },
                new StateCode() { Id = 8, Code = "DE", Description = "Delaware" },
                new StateCode() { Id = 9, Code = "DC", Description = "District of Columbia" },
                new StateCode() { Id = 10, Code = "FL", Description = "Florida" },
                new StateCode() { Id = 11, Code = "GA", Description = "Georgia" },
                new StateCode() { Id = 12, Code = "HI", Description = "Hawaii" },
                new StateCode() { Id = 13, Code = "ID", Description = "Idaho" },
                new StateCode() { Id = 14, Code = "IL", Description = "Illinois" },
                new StateCode() { Id = 15, Code = "IN", Description = "Indiana" },
                new StateCode() { Id = 16, Code = "IA", Description = "Iowa" },
                new StateCode() { Id = 17, Code = "KS", Description = "Kansas" },
                new StateCode() { Id = 18, Code = "KY", Description = "Kentucky" },
                new StateCode() { Id = 19, Code = "LA", Description = "Louisiana" },
                new StateCode() { Id = 20, Code = "ME", Description = "Maine" },
                new StateCode() { Id = 21, Code = "MD", Description = "Maryland" },
                new StateCode() { Id = 22, Code = "MA", Description = "Massachusetts" },
                new StateCode() { Id = 23, Code = "MI", Description = "Michigan" },
                new StateCode() { Id = 24, Code = "MN", Description = "Minnesota" },
                new StateCode() { Id = 25, Code = "MS", Description = "Mississippi" },
                new StateCode() { Id = 26, Code = "MO", Description = "Missouri" },
                new StateCode() { Id = 27, Code = "MT", Description = "Montana" },
                new StateCode() { Id = 28, Code = "NE", Description = "Nebraska" },
                new StateCode() { Id = 29, Code = "NV", Description = "Nevada" },
                new StateCode() { Id = 30, Code = "NH", Description = "New Hampshire" },
                new StateCode() { Id = 31, Code = "NJ", Description = "New Jersey" },
                new StateCode() { Id = 32, Code = "NM", Description = "New Mexico" },
                new StateCode() { Id = 33, Code = "NY", Description = "New York" },
                new StateCode() { Id = 34, Code = "NC", Description = "North Carolina" },
                new StateCode() { Id = 35, Code = "ND", Description = "North Dakota" },
                new StateCode() { Id = 36, Code = "OH", Description = "Ohio" },
                new StateCode() { Id = 37, Code = "OK", Description = "Oklahoma" },
                new StateCode() { Id = 38, Code = "OR", Description = "Oregon" },
                new StateCode() { Id = 39, Code = "PA", Description = "Pennsylvania" },
                new StateCode() { Id = 40, Code = "RI", Description = "Rhode Island" },
                new StateCode() { Id = 41, Code = "SC", Description = "South Carolina" },
                new StateCode() { Id = 42, Code = "SD", Description = "South Dakota" },
                new StateCode() { Id = 43, Code = "TN", Description = "Tennessee" },
                new StateCode() { Id = 44, Code = "TX", Description = "Texas" },
                new StateCode() { Id = 45, Code = "UT", Description = "Utah" },
                new StateCode() { Id = 46, Code = "VT", Description = "Vermont" },
                new StateCode() { Id = 47, Code = "VA", Description = "Virginia" },
                new StateCode() { Id = 48, Code = "WA", Description = "Washington" },
                new StateCode() { Id = 49, Code = "WV", Description = "West Virginia" },
                new StateCode() { Id = 50, Code = "WI", Description = "Wisconsin" },
                new StateCode() { Id = 51, Code = "WY", Description = "Wyoming" }
            };
        }

        public List<SortByOption> GetSortByOptions()
        {
            return new List<SortByOption>()
            {
                new SortByOption() { Id = 1, Code = "relevant", DisplayName = "Relevant" },
                new SortByOption() { Id = 2, Code = "newest", DisplayName = "Newest" },
                new SortByOption() { Id = 3, Code = "lowest_price", DisplayName = "Lowest Price" },
                new SortByOption() { Id = 4, Code = "highest_price", DisplayName = "Highest Price" }
            };
        }

    }
}
