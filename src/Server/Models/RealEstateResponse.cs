namespace Tracr.Server.Models
{
    public class RealEstateResponse
    {
        public int ResponseId { get; set; }
        public string? RequestMethod { get; set; }
        public string? Data { get; set; }
        public RealEstateResponse() { }
        public RealEstateResponse(string requestor, string data)
        {
            this.RequestMethod = requestor;
            this.Data = data;
        }
    }
}
