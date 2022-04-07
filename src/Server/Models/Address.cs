namespace Tracr.Server.Models
{
    public class Address
    {
        public Property Property { get; set; }

        public int PropertyId { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }
    }
}
