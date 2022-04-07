using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracr.Server.Repositories;
using Tracr.Shared.DTOs;

namespace Tracr.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateController : Controller
    {
        private readonly IRealEstateRepo _realestate;
        public RealEstateController(IRealEstateRepo realEstate)
        {
            this._realestate = realEstate;
        }

        [HttpGet("Average-Rate")]
        public async Task<ActionResult<RealEstateDto>> GetAverageRateByZip(int zip)
        {
            try
            {
                var data = await _realestate.AverageRateByZip(zip);

                if (data != null && data.Data != null)
                    return Ok(new RealEstateDto() { Data = data.Data });

                return NotFound($"Unable to retrieve interest rate data for {zip}");
            }
            catch (Exception)
            {
               return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("Propery-Value")]
        public async Task<ActionResult<RealEstateDto>> GetEstimatedValue(long propertyId)
        {
            try
            {
                var data = await _realestate.EstimatedValue(propertyId);

                if (data != null && data.Data != null)
                    return Ok(new RealEstateDto() { Data = data.Data });

                return NotFound($"Unable to retrieve property by Id: {propertyId}");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Get-Rentals")]
        public async Task<ActionResult<RealEstateDto>> GetRentals(RealEstateDto dto)
        {
            try
            {
                var data = await _realestate.ForRent(dto);

                if (data != null && data.Data != null)
                    return Ok(new RealEstateDto() { Data = data.Data });

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("For-Sale")]
        public async Task<ActionResult<RealEstateDto>> GetForSale(RealEstateDto dto)
        {
            try
            {
                var data = await _realestate.ForSale(dto);

                if (data != null && data.Data != null)
                    return Ok(new RealEstateDto() { Data = data.Data });

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Location-Noise")]
        public async Task<ActionResult<RealEstateDto>> GetLocationNoieScore(decimal lat, decimal lng)
        {
            try
            {
                var data = await _realestate.LocationNoiseScore(lng, lat);

                if (data != null && data.Data != null)
                    return Ok(new RealEstateDto() { Data = data.Data });

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Location-Schools")]
        public async Task<ActionResult<RealEstateDto>> GetLocationSchools(string city, string state, int zip)
        {
            try
            {
                var data = await _realestate.LocationSchools(city, state, zip);

                if (data != null && data.Data != null)
                    return Ok(new RealEstateDto() { Data = data.Data });

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Suggested-Locations")]
        public async Task<ActionResult<RealEstateDto>> GetSuggestedLocations(string input)
        {
            try
            {
                var data = await _realestate.LocationSuggest(input);

                if (data != null && data.Data != null)
                    return Ok(new RealEstateDto() { Data = data.Data });

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Mortgage-Calc")]
        public async Task<ActionResult<RealEstateDto>> MortgageCalc(RealEstateDto dto)
        {
            try
            {
                var data = await _realestate.MortgageCalc(dto);

                if (data != null && data.Data != null)
                    return Ok(new RealEstateDto() { Data = data.Data });

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Property-Detail-MLSId")]
        public async Task<ActionResult<RealEstateDto>> PropertyDetailByMlsId(string mlsId)
        {
            try
            {
                var data = await _realestate.PropertyByMlsId(mlsId);

                if (data != null && data.Data != null)
                    return Ok(new RealEstateDto() { Data = data.Data });

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Property-Detail")]
        public async Task<ActionResult<RealEstateDto>> PropertyDetailById(long propertyId)
        {
            try
            {
                var data = await _realestate.PropertyDetail(propertyId);

                if (data != null && data.Data != null)
                    return Ok(new RealEstateDto() { Data = data.Data });

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Rate-Trends")]
        public async Task<ActionResult<RealEstateDto>> GetRateTrends(bool isRefinance)
        {
            try
            {
                var data = await _realestate.RateTrends(isRefinance);

                if (data != null && data.Data != null)
                    return Ok(new RealEstateDto() { Data = data.Data });

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
       
        [HttpGet("Similar-Homes")]
        public async Task<ActionResult<RealEstateDto>> GetSimilarHomes(long propertyId)
        {
            try
            {
                var data = await _realestate.SimilarHomes(propertyId);

                if (data != null && data.Data != null)
                    return Ok(new RealEstateDto() { Data = data.Data });

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
