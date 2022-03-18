namespace Tracr.Server.Models
{
    public class Mortage
    {
        public Property Property { get; set; }

        public int PropertyId { get; set; }

        public decimal Principal { get; set; }

        public decimal MonthlyPayment { get; set; }

        public decimal APR { get; set; }
    }
}
