namespace Tracr.Shared.Models
{
    public class PropertyIncome
    {
        public int RenterId { get; set; }

        public int PropertyId { get; set; }

        public decimal Income { get; set; }

        public DateTime Month { get; set; }
    }
}
