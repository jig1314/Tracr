namespace Tracr.Shared.DTOs
{
    public class RenterDto
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal MonthlyRent { get; set; }

        public DateTime StartingMonth { get; set; }

        public DateTime EndingMonth { get; set; }
    }
}
