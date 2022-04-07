namespace Tracr.Shared.Models
{
    public class MortageCalculation
    {
        public int TermInYears { get; set; }

        public decimal Principal { get; set; }

        public decimal MonthlyPayment { get; set; }

        public decimal APR { get; set; }
    }
}
