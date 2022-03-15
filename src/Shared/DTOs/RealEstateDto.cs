using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tracr.Shared.DTOs
{
    public class RealEstateDto
    {
        public string? Data { get; set; }
        public string? city { get; set; }

        public string? state_code { get; set; }

        /// <summary>
        /// Get from /location/suggest
        /// </summary>
        public int? location { get; set; }

        [Range(0,200)]
        public int limit { get; set; } = 50;

        [Range(0, 9800)]
        public int offset { get; set; } = 0;
        public int? price_min { get; set; }
        public int? price_max { get; set; }
        public int? beds_min { get; set; }
        public int? beds_max { get; set; }
        public int? baths_min { get; set; }
        public int? baths_max { get; set; }
        public int? expand_search_radius { get; set; }

        /// <summary>
        /// leave null for any
        /// </summary>
        public bool? new_construction { get; set; }

        /// <summary>
        /// valid optins
        /// 500|750|1000|1250|1500|1750|2000|2250|2500|2750|3000
        /// </summary>
        public int? home_size_min { get; set; }

        /// <summary>
        /// valid optins
        /// 500|750|1000|1250|1500|1750|2000|2250|2500|2750|3000
        /// </summary>
        public int? home_size_max { get; set; }
        public bool show_amortization { get; set; }
        public int? hoa_fees { get; set; }
        public int? percent_tax_rate { get; set; }
        public int? year_term { get; set; }
        public int? percent_rate { get; set; }
        public int? monthly_home_insurance { get; set; }
        public int? price { get; set; }
        public int? max_sold_days { get; set; } = 360;

        public List<RealEstateEnum.Ammenities> community_ammenities { get; set; }
        public RealEstateEnum.SortBy sort { get; set; } = RealEstateEnum.SortBy.lowest_price;
        public RealEstateEnum.PropertyType? property_type { get; set; }

    }

    public class RealEstateEnum
    {
        public enum AreaType
        {
            city,
            neighborhood
        }

        public enum SortBy
        {
            frehsnest,
            recently_added_update,
            lowest_price,
            highest_price
        }

        public enum PropertyType
        {
            townhome, 
            coop, 
            single_family, 
            apartment, 
            condo, 
            condop
        }

        public enum Ammenities
        {
            garage_1_or_more,
            swimming_pool,
            community_doorman,
            community_outdoor_space,
            community_elevator,
            laundry_room,
            community_gym
        }


    }
}
