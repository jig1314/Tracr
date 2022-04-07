using Tracr.Server.Models;

namespace Tracr.Server.Data
{
    public interface IAnalyzerRepo
    {
        Task<REAnalyzerResponse> GetResponseAsync(string requestor);
        Task<REAnalyzerResponse> GetResponseAsync(string requestor, string? state, string? city, string? zip);
        Task<bool> DeleteResponseAsync(string requestor);
        Task<bool> AddResponseAsync(string requestor, string data);
        Task<bool> AddResponseAsync(string requestor,string data, string? state, string? city, string? zip);
    }
}
