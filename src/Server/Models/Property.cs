namespace Tracr.Server.Models
{
    public class Property
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string Name { get; set; }

        public int NumBedrooms { get; set; }

        public int NumBathrooms { get; set; }

        public Mortage Mortage { get; set; }

        public Address Address { get; set; }

        public List<Renter> Renters { get; set; }
    }
}
