using Tracr.Shared.DTOs;

namespace Tracr.Client.Services
{
    public interface IPropertyService
    {
        Task<List<PropertyDto>> GetUserProperties();
    }
}
