namespace Tracr.Client.Models
{
    public class Notification
    {
        public string AlertType { get; set; } = String.Empty;
        public string AlertMessage { get; set; } =   String.Empty;
        public DateTime AlertTime { get; set; }
    }
}
