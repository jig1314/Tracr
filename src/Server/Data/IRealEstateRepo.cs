using Tracr.Server.Models;

namespace Tracr.Server.Data
{
    public interface IRealEstateRepo
    {
        Task<RealEstateResponse> GetResponseAsync(string requestor);
        Task<bool> DeleteResponeAsync(string requestor);
        Task<bool> AddResponeAsync(string requestor, string data);
    }
}
