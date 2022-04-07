namespace Tracr.Server.Models
{
    public class REAnalyzerResponse
    { 
        public int ResponseId { get; set; }
        public string? RequestMethod { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public string? Data { get; set; }
        public REAnalyzerResponse()  { }
        public REAnalyzerResponse(string requestor, string data, string? state)
        {
            this.RequestMethod = requestor;
            this.Data = data;
            this.State = state;
        }
    }
}
