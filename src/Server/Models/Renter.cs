namespace Tracr.Server.Models
{
    public class Renter
    {
        public int Id { get; set; }

        public Property Property { get; set; }

        public int PropertyId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal MonthlyRent { get; set; }

        public DateOnly StartingMonth { get; set; }

        public DateOnly EndingMonth { get; set; }
    }
}
