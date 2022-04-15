namespace Tracr.Shared.Models
{
    public class Notification
    {
        public int PropertyId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool Seen { get; set; }
    }
}
