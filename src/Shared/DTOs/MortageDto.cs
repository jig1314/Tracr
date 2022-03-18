namespace Tracr.Shared.DTOs
{
    public class MortageDto
    {
        public int PropertyId { get; set; }

        public decimal Principal { get; set; }

        public decimal MonthlyPayment { get; set; }

        public decimal APR { get; set; }
    }
}
