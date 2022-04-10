using Tracr.Shared.Models;
using Tracr.Shared.ResourceParameters;

namespace Tracr.Client.Services
{
    public interface IRealEstateService
    {
        Task<List<StateCode>> GetStates();
        Task<List<SortByOption>> GetSortByOptions();
        Task<List<PropertyForSale>> GetPropertiesForSale(ForSaleResourceParameters resourceParameters);
    }
}
