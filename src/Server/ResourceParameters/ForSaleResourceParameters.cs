using System.ComponentModel.DataAnnotations;

namespace Tracr.Server.ResourceParameters
{
    public class ForSaleResourceParameters
    {
        [Range(0, 9800)]
        public int offset { get; set; } = 0;

        [Range(0, 200)]
        public int limit { get; set; } = 42;

        public string? state_code { get; set; }

        public string? city { get; set; }

        public int? location { get; set; }

        public int? price_min { get; set; }

        public int? price_max { get; set; }

        public int? beds_min { get; set; }

        public int? beds_max { get; set; }

        public int? baths_min { get; set; }

        public int? baths_max { get; set; }
    }
}
