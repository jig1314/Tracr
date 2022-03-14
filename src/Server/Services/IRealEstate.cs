using Tracr.Server.Models;
using Tracr.Shared.DTOs;
namespace Tracr.Server.Services
{
    public interface IRealEstate
    {

        /// <summary>
        /// https://rapidapi.com/datascraper/api/us-real-estate/
        /// </summary>
        /// <param name="mls_id"></param>
        /// <returns>RealEstateResponse</returns>
        Task<RealEstateResponse> PropertyDetail(string property_id);
        Task<RealEstateResponse> PropertyByMlsId(string mls_id);
        Task<RealEstateResponse> PropertyByProperty(string property_id);
        Task<RealEstateResponse> LocationSuggest(string input);
        Task<RealEstateResponse> LocationSchools(string input, string language);
        Task<RealEstateResponse> LocationNoiseScore(decimal longitude, decimal latitude);
        Task<RealEstateResponse> ForSale(RealEstateDto dto);
        Task<RealEstateResponse> ForSaleCount(RealEstateDto dto);
        Task<RealEstateResponse> SimilarHomes(int property_id);
        Task<RealEstateResponse> EstimatedValue(int property_id);
        Task<RealEstateResponse> SoldHomes(RealEstateDto dto);
        Task<RealEstateResponse> ForRent(RealEstateDto dto);
        Task<RealEstateResponse> ForRentCount(RealEstateDto dto);
        Task<RealEstateResponse> MortgageCalc(RealEstateDto dto);
        Task<RealEstateResponse> RateTrends(bool is_refinance);
        Task<RealEstateResponse> AverageRateByZip(int postal_code);
    }
}
