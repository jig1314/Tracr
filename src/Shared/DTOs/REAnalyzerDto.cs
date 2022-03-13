using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracr.Shared.Dto
{
    public class REAnalyzerDTO
    {     
        public string? State { get; set; } 
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public int? NeighborhoodId { get; set; }
        public int Items { get; set; } = 10;
        public string? Year { get; set; }
        public string? Month { get; set; }
        public int? Beds { get; set; }
        public string? MlsId { get; set; }
        public string? Address { get; set; }
        public string? ParcelNumber { get; set; }
        public int? PropertyId { get; set; }
        public int? RentalIncome { get; set; }
        public int? InsuranceCost { get; set; }
        public int? InterestRate { get; set; }
        public int? StartupCost { get; set; }      
        public int? TotalExpense { get; set; }
        public int? DownPayment { get; set; }
        public int? MaxPrice { get; set; }
        public int? Occupancy { get; set; }
        public int? TurnoverCost { get; set; }
        public int? ManagementCost { get; set; }
        public int? LoanYears { get; set; }
        public int? RecurringCost { get; set; }
        public int? PropertyTax { get; set; }
        public REAnalyzerEnum.InvestmentType? InvestmentType { get; set; }
        public REAnalyzerEnum.Source? Source { get; set; }
        public REAnalyzerEnum.SortBy? SortBy { get; set; }
        public REAnalyzerEnum.Payment? PaymentType { get; set; }

    }

    public class REAnalyzerEnum
    {
        public enum Source { AirBnB, Traditional }
        public enum Payment { CASH, LOAN }
        public enum InvestmentType { Investment, Airbnb, Traditional }
        public enum SortBy { name, similarity, distance, address, occupancy, nightprice, rentalincome, numofbaths, numofrooms, reviews_count }
    }
}
