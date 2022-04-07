using Tracr.Server.Models;

namespace Tracr.Server.Data
{
    public class RealEstateRepo : IRealEstateRepo
    {
        private readonly ApplicationDbContext _context;
        public RealEstateRepo(ApplicationDbContext context)
        {
            this._context  = context; 
        }

        public Task<bool> AddResponeAsync(string requestor, string data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteResponeAsync(string requestor)
        {
            throw new NotImplementedException();
        }

        public Task<RealEstateResponse> GetResponseAsync(string requestor)
        {
            throw new NotImplementedException();
        }
    }
}
