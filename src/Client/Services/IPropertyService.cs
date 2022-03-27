using Tracr.Shared.DTOs;
using Tracr.Shared.Models;

namespace Tracr.Client.Services
{
    public interface IPropertyService
    {
        Task<List<PropertyDto>> GetUserProperties();
        Task DeleteProperty(int propertyId);
        Task<int> CreateProperty(PropertyDto propertyDto);
        Task<PropertyDto> GetProperty(int propertyId);
        Task UpdateProperty(PropertyDto propertyDto);
        Task<List<RenterDto>> GetRentersByProperty(int propertyId);
        Task CreateRenter(RenterDto renterDto);
        Task<RenterDto> GetRenter(int renterId);
        Task UpdateRenter(RenterDto renterDto);
        Task DeleteRenter(int renterId);
        Task<List<PropertyIncome>> GetUserPropertyIncome();
    }
}
