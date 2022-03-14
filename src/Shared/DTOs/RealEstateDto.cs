using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracr.Shared.DTOs
{
    public class RealEstateDto
    {
        public string Data { get; set; }
    }

    public class RealEstateEnum
    {
        public enum AreaType
        {
            city,
            neighborhood
        }
    }
}
