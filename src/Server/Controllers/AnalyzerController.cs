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
        private readonly IAnalyzerRepo _repository;
        public readonly IRealEstateAnalyzerService _analyzer;

        public AnalyzerController(IRealEstateAnalyzerService analyzer, IAnalyzerRepo repository)
        {
            this._analyzer = analyzer;
            this._repository = repository;
        }

        [HttpGet("City-Summary")]
        public async Task<ActionResult<REAnalyzerDTO>> GetCitySummary(REAnalyzerDTO dto)
        {
            try
            {
                var cached = await _repository.GetResponseAsync("CitySummary");

                if (cached != null)
                    return Ok(dto);

                var result = await _analyzer.CitySummary(dto);

                if (result.Data != null)
                {
                    await _repository.AddResponseAsync("CitySummary", result.Data);
                    return Ok(dto);
                }

                return NotFound(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("Investment-Breakdown")]
        public async Task<ActionResult<REAnalyzerDTO>> GetInvestmentBreakdown(REAnalyzerDTO dto)
        {
            try
            {
                var cached = await _repository.GetResponseAsync("InvestmentBreakdown");

                if (cached != null)
                    return Ok(dto);

                var result = await _analyzer.InvestmentBreakdown(dto);

                if (result.Data != null)
                {
                    await _repository.AddResponseAsync("InvestmentBreakdown", result.Data);
                    return Ok(dto);
                }

                return NotFound(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("LongTerm-Rentals")]
        public async Task<ActionResult<REAnalyzerDTO>> GetLongTermRentals(REAnalyzerDTO dto)
        {
            try
            {
                var cached = await _repository.GetResponseAsync("LongTermRentals");

                if (cached != null)
                    return Ok(dto);

                var result = await _analyzer.LongTermRentals(dto);

                if (result.Data != null)
                {
                    await _repository.AddResponseAsync("LongTermRentals", result.Data);
                    return Ok(dto);
                }

                return NotFound(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("Occupancy")]
        public async Task<ActionResult<REAnalyzerDTO>> GetOccupancyRates(REAnalyzerDTO dto)
        {
            try
            {
                var cached = await _repository.GetResponseAsync("OccupancyRates");

                if (cached != null)
                    return Ok(dto);

                var result = await _analyzer.OccupancyRates(dto);

                if (result.Data != null)
                {
                    await _repository.AddResponseAsync("OccupancyRates", result.Data);
                    return Ok(dto);
                }

                return NotFound(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Property-By-Address")]
        public async Task<ActionResult<REAnalyzerDTO>> GetPropertyByAddress(REAnalyzerDTO dto)
        {
            try
            {
                var cached = await _repository.GetResponseAsync("PropertyByAddress");

                if (cached != null)
                    return Ok(dto);

                var result = await _analyzer.PropertyByAddress(dto);

                if (result.Data != null)
                {
                    await _repository.AddResponseAsync("PropertyByAddress", result.Data);
                    return Ok(dto);
                }

                return NotFound(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Property-By-Id")]
        public async Task<ActionResult<REAnalyzerDTO>> GetPropertyMarker(REAnalyzerDTO dto)
        {
            try
            {
                var cached = await _repository.GetResponseAsync("PropertyMarker");

                if (cached != null)
                    return Ok(dto);

                var result = await _analyzer.PropertyMarker(dto);

                if (result.Data != null)
                {
                    await _repository.AddResponseAsync("PropertyMarker", result.Data);
                    return Ok(dto);
                }

                return NotFound(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("Investment-Performance")]
        public async Task<ActionResult<REAnalyzerDTO>> GetPropertyPerformance(REAnalyzerDTO dto)
        {
            try
            {
                var cached = await _repository.GetResponseAsync("PropertyPerformance");

                if (cached != null)
                    return Ok(dto);

                var result = await _analyzer.PropertyPerformance(dto);

                if (result.Data != null)
                {
                    await _repository.AddResponseAsync("PropertyPerformance", result.Data);
                    return Ok(dto);
                }

                return NotFound(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Rental-Rates")]
        public async Task<ActionResult<REAnalyzerDTO>> GetRentalRates(REAnalyzerDTO dto)
        {
            try
            {
                var cached = await _repository.GetResponseAsync("RentalRates");

                if (cached != null)
                    return Ok(dto);

                var result = await _analyzer.RentalRates(dto);

                if (result.Data != null)
                {
                    await _repository.AddResponseAsync("RentalRates", result.Data);
                    return Ok(dto);
                }

                return NotFound(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("ShortTerm-Listings")]
        public async Task<ActionResult<REAnalyzerDTO>> GetShortTermRentalListings(REAnalyzerDTO dto)
        {
            try
            {
                var cached = _repository.GetResponseAsync("ShortTermRentalListings");
                if (cached != null)
                    return Ok(dto);

                var result = await _analyzer.ShortTermRentalListings(dto);

                if (result.Data != null)
                {
                    await _repository.AddResponseAsync("ShortTermRentalListings", result.Data);
                    return Ok(dto);
                }

                return NotFound(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("ShortTerm-Market-Summary")]
        public async Task<ActionResult<REAnalyzerDTO>> GetShortTermRentalMarketSummary(REAnalyzerDTO dto)
        {
            try
            {
                var cached = await _repository.GetResponseAsync("ShortTermRentalMarketSummary");

                if (cached != null)
                    return Ok(dto);

                var result = await _analyzer.ShortTermRentalMarketSummary(dto);

                if (result.Data != null)
                {
                    await _repository.AddResponseAsync("ShortTermRentalMarketSummary", result.Data);
                    return Ok(dto);
                }

                return NotFound(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Airbnb-Top-Cities")]
        public async Task<ActionResult<REAnalyzerDTO>> GetTopAirbnbCities(REAnalyzerDTO dto)
        {
            try
            {
                var cached = await _repository.GetResponseAsync("TopAirbnbCities");

                if (cached != null)
                    return Ok(dto);

                var result = await _analyzer.TopAirbnbCities(dto);

                if (result.Data != null)
                {
                    await _repository.AddResponseAsync("TopAirbnbCities", result.Data);
                    return Ok(dto);
                }

                return NotFound(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Top-Markets")]
        public async Task<ActionResult<REAnalyzerDTO>> GetTopMarkets(REAnalyzerDTO dto)
        {
            try
            {
                var cached = _repository.GetResponseAsync("TopMarkets");

                if (cached != null)
                    return Ok(dto);

                var result = await _analyzer.TopMarkets(dto);

                if (result.Data != null)
                {
                    await _repository.AddResponseAsync("TopMarkets", result.Data);
                    return Ok(dto);
                }

                return NotFound(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
