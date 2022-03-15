
using Microsoft.AspNetCore.WebUtilities;
using Tracr.Server.Models;
using Tracr.Shared.DTOs;

namespace Tracr.Server.Services
{
    public class RealEstateAnalyzerService : IRealEstateAnalyzerService
    {
        private readonly HttpClient _httpClient;

        public RealEstateAnalyzerService(IHttpClientFactory clientFactory)
        {
            this._httpClient = clientFactory.CreateClient("mash");
        }

        public async Task<REAnalyzerResponse> CitySummary(REAnalyzerDTO param)
        {
            var query = new Dictionary<string, string?>();

            // verify required fields
            if (string.IsNullOrEmpty(param.State) || string.IsNullOrEmpty(param.City))
                throw new ArgumentException();

            query.Add("state", param.State);
            query.Add("city", param.City);

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("trends/summary", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new REAnalyzerResponse("CitySummary", body, param.State);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<REAnalyzerResponse> InvestmentBreakdown(REAnalyzerDTO param)
        {
            var query = new Dictionary<string, string?>();

            // verify required fields
            if (string.IsNullOrEmpty(param.State) || !param.RecurringCost.HasValue || !param.PropertyId.HasValue || !param.Source.HasValue)
                throw new ArgumentException();

            query.Add("state", param.State);
            query.Add("recurring_cost", param.RecurringCost.Value.ToString());
            query.Add("startup_cost", param.State);
            query.Add("source", param.Source.Value.ToString());
            query.Add("id", param.PropertyId.Value.ToString());
            query.Add("is_days", "1");

            if (param.TurnoverCost.HasValue)
                query.Add("turnover_cost", param.TurnoverCost.Value.ToString());

            if (param.MaxPrice.HasValue)
                query.Add("max_bid", param.MaxPrice.Value.ToString());

            var prefix = (param.Source == REAnalyzerEnum.Source.AirBnB) ? "airbnb" : "traditional";

            if (param.RentalIncome.HasValue)
                query.Add($"{prefix}_rental", param.RentalIncome.Value.ToString());

            if (param.Occupancy.HasValue)
                query.Add($"{prefix}_occupancy", param.Occupancy.Value.ToString());

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("664367/investment/breakdown", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new REAnalyzerResponse("InvestmentBreakdown", body, param.State);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<REAnalyzerResponse> LongTermRentals(REAnalyzerDTO param)
        {
            var query = new Dictionary<string, string?>();

            // verify required fields
            if (!string.IsNullOrEmpty(param.State))
                throw new ArgumentException();

            query.Add("state", param.State);

            if (param.PropertyId.HasValue)
                query.Add("id", param.PropertyId.Value.ToString());

            if (!string.IsNullOrEmpty(param.MlsId))
                query.Add("mls_id", param.MlsId);

            if (param.RentalIncome.HasValue)
                query.Add("address", param.RentalIncome.Value.ToString());

            if (param.Occupancy.HasValue)
                query.Add("city", param.Occupancy.Value.ToString());

            if (!string.IsNullOrEmpty(param.ParcelNumber))
                query.Add("parcel_number", param.ParcelNumber);

            if (!string.IsNullOrEmpty(param.ZipCode))
                query.Add("zip_code", param.ZipCode);

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("traditional-property", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new REAnalyzerResponse("LongTermRentals", body, param.State);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<REAnalyzerResponse> OccupancyRates(REAnalyzerDTO param)
        {
            var query = new Dictionary<string, string?>();

            // verify required fields
            if (!string.IsNullOrEmpty(param.State))
                throw new ArgumentException();

            query.Add("state", param.State);

            if (!string.IsNullOrEmpty(param.City))
                query.Add("city", param.City);

            if (!string.IsNullOrEmpty(param.ZipCode))
                query.Add("zip_code", param.ZipCode);

            if (param.NeighborhoodId.HasValue)
                query.Add("neighborhood", param.NeighborhoodId.Value.ToString());

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("airbnb-property/occupancy-rates", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new REAnalyzerResponse("OccupancyRates", body, param.State);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<REAnalyzerResponse> PropertyByAddress(REAnalyzerDTO param)
        {
            var query = new Dictionary<string, string?>();

            // verify required fields
            if (!string.IsNullOrEmpty(param.State))
                throw new ArgumentException();

            query.Add("state", param.State);

            if (param.PropertyId.HasValue)
                query.Add("id", param.PropertyId.Value.ToString());

            if (!string.IsNullOrEmpty(param.MlsId))
                query.Add("mls_id", param.MlsId);

            if (param.RentalIncome.HasValue)
                query.Add("address", param.RentalIncome.Value.ToString());

            if (param.Occupancy.HasValue)
                query.Add("city", param.Occupancy.Value.ToString());

            if (!string.IsNullOrEmpty(param.ParcelNumber))
                query.Add("parcel_number", param.ParcelNumber);

            if (!string.IsNullOrEmpty(param.ZipCode))
                query.Add("zip_code", param.ZipCode);

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("property", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new REAnalyzerResponse("PropertyByAddress", body, param.State);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<REAnalyzerResponse> PropertyMarker(REAnalyzerDTO param)
        {
            var query = new Dictionary<string, string?>();

            // verify required fields
            if (string.IsNullOrEmpty(param.State) || !param.PropertyId.HasValue || !param.InvestmentType.HasValue || !param.PaymentType.HasValue)
                throw new ArgumentException();
            
            query.Add("state", param.State);
            query.Add("pid", param.PropertyId.Value.ToString());
            query.Add("payment", param.InvestmentType.Value.ToString());
            query.Add("type", param.PaymentType.Value.ToString());

            if (param.LoanYears.HasValue)
                query.Add("loanType", param.LoanYears.Value.ToString());                  

            if (param.InterestRate.HasValue)
                query.Add("interestRate", param.InterestRate.Value.ToString());

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("property/marker", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new REAnalyzerResponse("PropertyMarker", body, param.State);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<REAnalyzerResponse> PropertyPerformance(REAnalyzerDTO param)
        {

            var query = new Dictionary<string, string?>();

            // verify required fields
            if (string.IsNullOrEmpty(param.State) || !param.PropertyId.HasValue)
                throw new ArgumentException();
            
            query.Add("state", param.State);
            query.Add("id", param.PropertyId.Value.ToString());
            query.Add("is_days", "1");

            if (param.StartupCost.HasValue)
                query.Add("startup_cost", param.StartupCost.Value.ToString());

            if (param.DownPayment.HasValue)
                query.Add("down_payment", param.DownPayment.Value.ToString());

            if (param.MaxPrice.HasValue)
                query.Add("max_bid", param.MaxPrice.Value.ToString());

            var prefix = (param.Source == REAnalyzerEnum.Source.AirBnB) ? "airbnb" : "traditional";

            if (param.InsuranceCost.HasValue)
                query.Add($"{prefix}_home_owner_insurance", param.InsuranceCost.Value.ToString());

            if (param.RentalIncome.HasValue)
                query.Add($"{prefix}_rental", param.RentalIncome.Value.ToString());

            if (param.Occupancy.HasValue)
                query.Add($"{prefix}_occupancy", param.Occupancy.Value.ToString());

            if (param.TotalExpense.HasValue)
                query.Add($"{prefix}_total_expenses", param.TotalExpense.Value.ToString());

            if (param.PropertyTax.HasValue)
                query.Add($"{prefix}_property_tax", param.PropertyTax.Value.ToString());

            if (param.ManagementCost.HasValue)
                query.Add($"{prefix}_management_cost", param.ManagementCost.Value.ToString());

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("property/664367/investment", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new REAnalyzerResponse("PropertyPerformance", body, param.State);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<REAnalyzerResponse> RentalRates(REAnalyzerDTO param)
        {
            var query = new Dictionary<string, string?>();

            // verify required fields
            if (string.IsNullOrEmpty(param.State) || !param.Source.HasValue)
                throw new ArgumentException();
            
            query.Add("state", param.State);
            query.Add("source", param.Source.Value.ToString());

            if (!string.IsNullOrEmpty(param.City))
                query.Add("city", param.City);

            if (!string.IsNullOrEmpty(param.ZipCode))
                query.Add("zip_code", param.ZipCode);

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("rental-rates", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new REAnalyzerResponse("RentalRates", body, param.State);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<REAnalyzerResponse> ShortTermRentalListings(REAnalyzerDTO param)
        {
            var query = new Dictionary<string, string?>();

            // verify required fields
            if (string.IsNullOrEmpty(param.State))
                throw new ArgumentException();
            
            query.Add("state", param.State);
            query.Add("items", param.Items.ToString());

            if (!string.IsNullOrEmpty(param.City))
                query.Add("city", param.City);

            if (!string.IsNullOrEmpty(param.ZipCode))
                query.Add("zip_code", param.ZipCode);

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("airbnb-property/active-listings", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new REAnalyzerResponse("ShortTermRentalListings", body, param.State);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<REAnalyzerResponse> ShortTermRentalMarketSummary(REAnalyzerDTO param)
        {
            var query = new Dictionary<string, string?>();

            // verify required fields
            if (string.IsNullOrEmpty(param.State))
                throw new ArgumentException();

            query.Add("state", param.State);

            if (!string.IsNullOrEmpty(param.City))
                query.Add("city", param.City);

            if (!string.IsNullOrEmpty(param.ZipCode))
                query.Add("zip_code", param.ZipCode);

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("airbnb-property/market-summary", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new REAnalyzerResponse("ShortTermRentalMarketSummary", body, param.State);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<REAnalyzerResponse> TopAirbnbCities(REAnalyzerDTO param)
        {
            var query = new Dictionary<string, string?>();

            if (string.IsNullOrEmpty(param.State))
                throw new ArgumentException();

            query.Add("state", param.State);
            query.Add("items", param.Items.ToString());

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("trends/cities", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new REAnalyzerResponse("TopAirbnbCities", body, param.State);
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<REAnalyzerResponse> TopMarkets(REAnalyzerDTO param)
        {
            var query = new Dictionary<string, string?>();

            if (string.IsNullOrEmpty(param.State))
                throw new ArgumentException();

            query.Add("state", param.State);
            query.Add("items", param.Items.ToString());

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("city/top-markets", query));

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new REAnalyzerResponse("TopMarkets", body, param.State);
            }

            throw new Exception(response.StatusCode.ToString());
        }
    }
}
