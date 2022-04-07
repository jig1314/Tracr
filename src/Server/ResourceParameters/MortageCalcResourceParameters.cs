namespace Tracr.Server.ResourceParameters
{
    public class MortageCalcResourceParameters
    {
        public bool show_amortization { get; set; }
        public decimal? hoa_fees { get; set; }
        public decimal? percent_tax_rate { get; set; }
        public int? year_term { get; set; }
        public decimal? percent_rate { get; set; }
        public decimal? down_payment { get; set; }
        public decimal? monthly_home_insurance { get; set; }
        public decimal? price { get; set; }
    }
}
