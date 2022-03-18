namespace Tracr.Shared.DTOs
{
    public class PropertyDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumBedrooms { get; set; }

        public int NumBathrooms { get; set; }

        public MortageDto Mortage { get; set; }

        public AddressDto Address { get; set; }
    }
}
