using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracr.Server.Data;
using Tracr.Server.Services;
using Tracr.Shared.DTOs;

namespace Tracr.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnalyzerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public readonly IRealEstateAnalyzer _analyzer;

        public AnalyzerController(IRealEstateAnalyzer analyzer, ApplicationDbContext context)
        {
            this._analyzer = analyzer;
            this._context = context;
        }

        [HttpGet("City-Summary")]
        public async Task<ActionResult<REAnalyzerDTO>> GetCitySummary(REAnalyzerDTO dto)
        {
            var cached = _context.AnalyzerResponse
                .Where(r => r.RequestMethod == "CitySummary")
                .FirstOrDefault();

            if (cached != null)
                return Ok(dto);

            var result = await _analyzer.CitySummary(dto);

            if(result.Data != null)
            {
                _context.AnalyzerResponse.Add(result);
                await _context.SaveChangesAsync();
                return Ok(dto);
            }

            return NotFound(dto);
        }

        [HttpGet("Investment-Breakdown")]
        public async Task<ActionResult<REAnalyzerDTO>> GetInvestmentBreakdown(REAnalyzerDTO dto)
        {
            var cached = _context.AnalyzerResponse
                .Where(r => r.RequestMethod == "InvestmentBreakdown")
                .FirstOrDefault();

            if (cached != null)
                return Ok(dto);

            var result = await _analyzer.InvestmentBreakdown(dto);

            if (result.Data != null)
            {
                _context.AnalyzerResponse.Add(result);
                await _context.SaveChangesAsync();
                return Ok(dto);
            }

            return NotFound(dto);
        }

        [HttpGet("LongTerm-Rentals")]
        public async Task<ActionResult<REAnalyzerDTO>> GetLongTermRentals(REAnalyzerDTO dto)
        {
            var cached = _context.AnalyzerResponse
                .Where(r => r.RequestMethod == "LongTermRentals")
                .FirstOrDefault();

            if (cached != null)
                return Ok(dto);

            var result = await _analyzer.LongTermRentals(dto);

            if (result.Data != null)
            {
                _context.AnalyzerResponse.Add(result);
                await _context.SaveChangesAsync();
                return Ok(dto);
            }

            return NotFound(dto);
        }

        [HttpGet("Occupancy")]
        public async Task<ActionResult<REAnalyzerDTO>> GetOccupancyRates(REAnalyzerDTO dto)
        {
            var cached = _context.AnalyzerResponse
                .Where(r => r.RequestMethod == "OccupancyRates")
                .FirstOrDefault();

            if (cached != null)
                return Ok(dto);

            var result = await _analyzer.OccupancyRates(dto);

            if (result.Data != null)
            {
                _context.AnalyzerResponse.Add(result);
                await _context.SaveChangesAsync();
                return Ok(dto);
            }

            return NotFound(dto);
        }

        [HttpGet("Property-By-Address")]
        public async Task<ActionResult<REAnalyzerDTO>> GetPropertyByAddress(REAnalyzerDTO dto)
        {
            var cached = _context.AnalyzerResponse
                .Where(r => r.RequestMethod == "PropertyByAddress")
                .FirstOrDefault();

            if (cached != null)
                return Ok(dto);

            var result = await _analyzer.PropertyByAddress(dto);

            if (result.Data != null)
            {
                _context.AnalyzerResponse.Add(result);
                await _context.SaveChangesAsync();
                return Ok(dto);
            }

            return NotFound(dto);
        }

        [HttpGet("Property-By-Id")]
        public async Task<ActionResult<REAnalyzerDTO>> GetPropertyMarker(REAnalyzerDTO dto)
        {
            var cached = _context.AnalyzerResponse
                .Where(r => r.RequestMethod == "PropertyMarker")
                .FirstOrDefault();

            if (cached != null)
                return Ok(dto);

            var result = await _analyzer.PropertyMarker(dto);

            if (result.Data != null)
            {
                _context.AnalyzerResponse.Add(result);
                await _context.SaveChangesAsync();
                return Ok(dto);
            }

            return NotFound(dto);
        }

        [HttpGet("Investment-Performance")]
        public async Task<ActionResult<REAnalyzerDTO>> GetPropertyPerformance(REAnalyzerDTO dto)
        {
            var cached = _context.AnalyzerResponse
                .Where(r => r.RequestMethod == "PropertyPerformance")
                .FirstOrDefault();

            if (cached != null)
                return Ok(dto);

            var result = await _analyzer.PropertyPerformance(dto);

            if (result.Data != null)
            {
                _context.AnalyzerResponse.Add(result);
                await _context.SaveChangesAsync();
                return Ok(dto);
            }

            return NotFound(dto);
        }

        [HttpGet("Rental-Rates")]
        public async Task<ActionResult<REAnalyzerDTO>> GetRentalRates(REAnalyzerDTO dto)
        {
            var cached = _context.AnalyzerResponse
                .Where(r => r.RequestMethod == "RentalRates")
                .FirstOrDefault();

            if (cached != null)
                return Ok(dto);

            var result = await _analyzer.RentalRates(dto);

            if (result.Data != null)
            {
                _context.AnalyzerResponse.Add(result);
                await _context.SaveChangesAsync();
                return Ok(dto);
            }

            return NotFound(dto);
        }

        [HttpGet("ShortTerm-Listings")]
        public async Task<ActionResult<REAnalyzerDTO>> GetShortTermRentalListings(REAnalyzerDTO dto)
        {
            var cached = _context.AnalyzerResponse
                .Where(r => r.RequestMethod == "ShortTermRentalListings")
                .FirstOrDefault();

            if (cached != null)
                return Ok(dto);

            var result = await _analyzer.ShortTermRentalListings(dto);

            if (result.Data != null)
            {
                _context.AnalyzerResponse.Add(result);
                await _context.SaveChangesAsync();
                return Ok(dto);
            }

            return NotFound(dto);
        }

        [HttpGet("ShortTerm-Market-Summary")]
        public async Task<ActionResult<REAnalyzerDTO>> GetShortTermRentalMarketSummary(REAnalyzerDTO dto)
        {
            var cached = _context.AnalyzerResponse
                .Where(r => r.RequestMethod == "ShortTermRentalMarketSummary")
                .FirstOrDefault();

            if (cached != null)
                return Ok(dto);

            var result = await _analyzer.ShortTermRentalMarketSummary(dto);

            if (result.Data != null)
            {
                _context.AnalyzerResponse.Add(result);
                await _context.SaveChangesAsync();
                return Ok(dto);
            }

            return NotFound(dto);
        }

        [HttpGet("Airbnb-Top-Cities")]
        public async Task<ActionResult<REAnalyzerDTO>> GetTopAirbnbCities(REAnalyzerDTO dto)
        {
            var cached = _context.AnalyzerResponse
                .Where(r => r.RequestMethod == "TopAirbnbCities")
                .FirstOrDefault();

            if (cached != null)
                return Ok(dto);

            var result = await _analyzer.TopAirbnbCities(dto);

            if (result.Data != null)
            {
                _context.AnalyzerResponse.Add(result);
                await _context.SaveChangesAsync();
                return Ok(dto);
            }

            return NotFound(dto);
        }

        [HttpGet("Top-Markets")]
        public async Task<ActionResult<REAnalyzerDTO>> GetTopMarkets(REAnalyzerDTO dto)
        {
            var cached = _context.AnalyzerResponse
                .Where(r => r.RequestMethod == "TopMarkets")
                .FirstOrDefault();

            if (cached != null)
                return Ok(dto);

            var result = await _analyzer.TopMarkets(dto);

            if (result.Data != null)
            {
                _context.AnalyzerResponse.Add(result);
                await _context.SaveChangesAsync();
                return Ok(dto);
            }

            return NotFound(dto);
        }
    }
}
