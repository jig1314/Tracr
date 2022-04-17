using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracr.Server.Repositories;
using Tracr.Shared.DTOs;
using Tracr.Shared.Models;
using Tracr.Shared.ResourceParameters;

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

        //[HttpGet("Propery-Value")]
        //public async Task<ActionResult<RealEstateDto>> GetEstimatedValue(long propertyId)
        //{
        //    try
        //    {
        //        var data = await _realestate.EstimatedValue(propertyId);

        //        if (data != null && data.Data != null)
        //            return Ok(new RealEstateDto() { Data = data.Data });

        //        return NotFound($"Unable to retrieve property by Id: {propertyId}");
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        [HttpGet("For-Sale")]
        public async Task<ActionResult<List<PropertyForSale>>> GetForSale([FromQuery] ForSaleResourceParameters forSaleResourceParameters)
        {
            try
            {
                var data = await _realestate.ForSale(forSaleResourceParameters);
                return Ok(data);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[HttpGet("Suggested-Locations")]
        //public async Task<ActionResult<RealEstateDto>> GetSuggestedLocations(string input)
        //{
        //    try
        //    {
        //        var data = await _realestate.LocationSuggest(input);

        //        if (data != null && data.Data != null)
        //            return Ok(new RealEstateDto() { Data = data.Data });

        //        return NotFound();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        [HttpGet("Mortgage-Calc")]
        public async Task<ActionResult<MortageCalculation>> MortgageCalc(MortageCalcResourceParameters mortageCalcResourceParameters)
        {
            try
            {
                var data = await _realestate.MortgageCalc(mortageCalcResourceParameters);
                return Ok(data);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[HttpGet("Property-Detail")]
        //public async Task<ActionResult<RealEstateDto>> PropertyDetailById(long propertyId)
        //{
        //    try
        //    {
        //        var data = await _realestate.PropertyDetail(propertyId);

        //        if (data != null && data.Data != null)
        //            return Ok(new RealEstateDto() { Data = data.Data });

        //        return NotFound();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //[HttpGet("Rate-Trends")]
        //public async Task<ActionResult<RealEstateDto>> GetRateTrends(bool isRefinance)
        //{
        //    try
        //    {
        //        var data = await _realestate.RateTrends(isRefinance);

        //        if (data != null && data.Data != null)
        //            return Ok(new RealEstateDto() { Data = data.Data });

        //        return NotFound();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
       
        //[HttpGet("Similar-Homes")]
        //public async Task<ActionResult<RealEstateDto>> GetSimilarHomes(long propertyId)
        //{
        //    try
        //    {
        //        var data = await _realestate.SimilarHomes(propertyId);

        //        if (data != null && data.Data != null)
        //            return Ok(new RealEstateDto() { Data = data.Data });

        //        return NotFound();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        [HttpGet("states")]
        public async Task<ActionResult<List<StateCode>>> GetStates()
        {
            try
            {
                var states = _realestate.GetStateCodes();

                return Ok(states);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }


        [HttpGet("sortByOptions")]
        public async Task<ActionResult<List<StateCode>>> GetSortByOptions()
        {
            try
            {
                var sortByOptions = _realestate.GetSortByOptions();

                return Ok(sortByOptions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
