using Tracr.Server.Models;

namespace Tracr.Server.Data
{
    public class AnalyzerRepo : IAnalyzerRepo
    {
        private readonly ApplicationDbContext _context;

        public AnalyzerRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public Task<bool> AddResponseAsync(string requestor, string data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddResponseAsync(string requestor, string data, string? state, string? city, string? zip)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteResponseAsync(string requestor)
        {
            throw new NotImplementedException();
        }

        public Task<REAnalyzerResponse> GetResponseAsync(string requestor)
        {
            throw new NotImplementedException();
        }

        public Task<REAnalyzerResponse> GetResponseAsync(string requestor, string? state, string? city, string? zip)
        {
            throw new NotImplementedException();
        }
    }
}
