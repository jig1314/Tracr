using Tracr.Shared.Models;
using Tracr.Shared.ResourceParameters;

namespace Tracr.Server.Repositories
{
    /// <summary>
    /// https://rapidapi.com/datascraper/api/us-real-estate/
    /// </summary>
    public interface IRealEstateRepo
    {
        //Task<RealEstateDto> PropertyDetail(long property_id);
        //Task<RealEstateDto> LocationSuggest(string input);
        Task<List<PropertyForSale>> ForSale(ForSaleResourceParameters param);
        //Task<RealEstateDto> SimilarHomes(long property_id);
        //Task<RealEstateDto> EstimatedValue(long property_id);
        Task<MortageCalculation> MortgageCalc(MortageCalcResourceParameters param);
        //Task<RealEstateDto> RateTrends(bool is_refinance);
        List<StateCode> GetStateCodes();
        List<SortByOption> GetSortByOptions();
    }
}
