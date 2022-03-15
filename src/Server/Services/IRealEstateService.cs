using Tracr.Server.Models;
using Tracr.Shared.DTOs;
namespace Tracr.Server.Services
{
    public interface IRealEstateService
    {

        /// <summary>
        /// https://rapidapi.com/datascraper/api/us-real-estate/
        /// </summary>
        /// <param name="mls_id"></param>
        /// <returns>RealEstateResponse</returns>
        Task<RealEstateResponse> PropertyDetail(int property_id);
        Task<RealEstateResponse> PropertyByMlsId(string mls_id);
        Task<RealEstateResponse> LocationSuggest(string input);
        Task<RealEstateResponse> LocationSchools(string ciy, string state_code, int postal_code);
        Task<RealEstateResponse> LocationNoiseScore(decimal longitude, decimal latitude);
        Task<RealEstateResponse> ForSale(RealEstateDto param);
        Task<RealEstateResponse> SimilarHomes(int property_id);
        Task<RealEstateResponse> EstimatedValue(int property_id);
        Task<RealEstateResponse> SoldHomes(RealEstateDto param);
        Task<RealEstateResponse> ForRent(RealEstateDto param);
        Task<RealEstateResponse> MortgageCalc(RealEstateDto param);
        Task<RealEstateResponse> RateTrends(bool is_refinance);
        Task<RealEstateResponse> AverageRateByZip(int postal_code);
    }
}
