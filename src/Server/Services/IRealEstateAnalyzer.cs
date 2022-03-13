using Tracr.Shared.DTOs;
using Tracr.Server.Models;


namespace Tracr.Server.Services
{
    /// <summary>
    /// https://rapidapi.com/mashvisor-team/api/mashvisor/
    /// Mashvisor APIs allow you to conduct real estate market analysis of any US housing market for both 
    /// long term rental properties (traditional rentals) and short term rental properties (Airbnb rentals).
    /// Furthermore, you get access to nationwide real estate data for traditional rental listings as well as Airbnb listings. 
    /// The data includes but is not limited to price estimate of MLS listings and off market properties, traditional rental rates (rental income), 
    /// recurring rental expenses, Airbnb nightly rates, Airbnb occupancy rate, Airbnb rental income traditional and Airbnb return on 
    /// investment including cash flow, cash on cash return, and cap rate.
    /// </summary>

    public interface IRealEstateAnalyzer
    {
        /// <summary>
        /// The endpoint retrieves rental income rates for Airbnb or traditional
        /// way for a city, zip code, or a neighborhood, 
        /// you'll be able to fetch Airbnb rental rates - short term rentals, or long term rentals, 
        /// calculated based on the location Airbnb occupancy rates
        /// </summary>
        /// <param name="state"></param>
        /// <param name="source"></param>
        /// <param name="city"></param>
        /// <param name="zipCode"></param>
        /// <param name="neighboorhoodId"></param>
        /// <returns></returns>
        Task<REAnalyzerResponse> RentalRates(REAnalyzerDTO param);

        /// <summary>
        /// List all active short term rentals - Airbnb listings - for a specific location: city, zip code, or a neighborhood
        /// </summary>
        /// <param name="state"></param>
        /// <param name="items"></param>
        /// <param name="city"></param>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        Task<REAnalyzerResponse> ShortTermRentalListings(REAnalyzerDTO param);

        /// <summary>
        /// For each Airbnb listing, we calculate its occupancy rate, month per month, and an annual rate, 
        /// and we offer our clients a 12-month historical performance for the occupancy rates. 
        /// Market occupancy rates for a zip code or a neighborhood.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="city"></param>
        /// <param name="zipCode"></param>
        /// <param name="neighborhood"></param>
        /// <returns></returns>
        Task<REAnalyzerResponse> OccupancyRates(REAnalyzerDTO param);

        /// <summary>
        /// Get a summary an overview for a specific Airbnb market location: city, zip code, or a neighborhood
        /// </summary>
        /// <param name="state"></param>
        /// <param name="city"></param>
        /// <param name="zipCode"></param>
        /// <param name="neighborhoodId"></param>
        /// <returns></returns>
        Task<REAnalyzerResponse> ShortTermRentalMarketSummary(REAnalyzerDTO param);

        /// <summary>
        /// This endpoint retrieves the traditional - long term rental - property's detailed data set stored in Mashvisor database.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="zipCode"></param>
        /// <param name="propertyId"></param>
        /// <param name="mlsId"></param>
        /// <param name="address"></param>
        /// <param name="parcel_number"></param>
        /// <returns></returns>
        Task<REAnalyzerResponse> LongTermRentals(REAnalyzerDTO param);

        /// <summary>
        /// Top Airbnb Cities, this endpoint retrieves the cities has the highest occupancy rates with their total Airbnb active listings in a specific state.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<REAnalyzerResponse> TopAirbnbCities(REAnalyzerDTO param);

        /// <summary>
        /// This endpoint retrieves a summary of airbnb properties, traditional properties, investment properties, and active neighborhoods available on Mashvisor.com for a specific .
        /// </summary>
        /// <param name="state"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        Task<REAnalyzerResponse> CitySummary(REAnalyzerDTO param);

        /// <summary>
        /// This endpoint retrieves the property's detailed data set stored in Mashvisor database.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="city"></param>
        /// <param name="propertyId"></param>
        /// <param name="zipCode"></param>
        /// <param name="address"></param>
        /// <param name="mlsId"></param>
        /// <returns></returns>
        Task<REAnalyzerResponse> PropertyByAddress(REAnalyzerDTO param);

        /// <summary>
        /// This endpoint retrieves the top housing cities with their active homes for sale count in a specific state.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<REAnalyzerResponse> TopMarkets(REAnalyzerDTO param);

        /// <summary>
        /// This endpoint retrieves the property's investment performance.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="propertyId"></param>
        /// <param name="rentalIncome"></param>
        /// <param name="insurance"></param>
        /// <param name="interestRate"></param>
        /// <param name="startupCost"></param>
        /// <param name="paymentType"></param>
        /// <param name="loanType"></param>
        /// <param name="downPayment"></param>
        /// <returns></returns>
        Task<REAnalyzerResponse> PropertyPerformance(REAnalyzerDTO param);

        /// <summary>
        /// This endpoint retrieves the property's investment breakdown performance for Airbnb or Traditional.
        /// </summary>
        /// <param name="recurringCost"></param>
        /// <param name="state"></param>
        /// <param name="startupCost"></param>
        /// <param name="source"></param>
        /// <param name="propertyId"></param>
        /// <param name="rentalIncome"></param>
        /// <param name="turnoverCost"></param>
        /// <param name="occupancy"></param>
        /// <returns></returns>
        Task<REAnalyzerResponse> InvestmentBreakdown(REAnalyzerDTO param);

        /// <summary>
        ///  This endpoint retrieves snapshot data about a specific property
        /// </summary>
        /// <param name="state"></param>
        /// <param name="payment"></param>
        /// <param name="propertyId"></param>
        /// <param name="investmentType"></param>
        /// <param name="startupCost"></param>
        /// <param name="loanType"></param>
        /// <param name="loanInterestOnlyYears"></param>
        /// <param name="downPayment"></param>
        /// <param name="loanArmType"></param>
        /// <param name="interestRate"></param>
        /// <param name="loanArmRate"></param>
        /// <returns></returns>
        Task<REAnalyzerResponse> PropertyMarker(REAnalyzerDTO param);
    }
}
