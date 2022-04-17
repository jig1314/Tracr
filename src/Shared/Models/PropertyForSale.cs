namespace Tracr.Shared.Models
{
    public class PropertyForSale
    {
        public string PhotoURL { get;set; }

        public decimal ListPrice { get; set; }

        public int YearBuilt { get; set; }

        public int NumBedrooms { get; set; }

        public decimal NumBathrooms { get; set; }

        public int SqaureFootage { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }
    }
}
