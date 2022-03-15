using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracr.Server.Services;
using Tracr.Shared.DTOs;

namespace Tracr.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateController : Controller
    {
        private readonly IRealEstateService _realestate;
        public RealEstateController(IRealEstateService realEstate)
        {
            this._realestate = realEstate;
        }

        [HttpGet("Average-Rate")]
        public async Task<ActionResult<RealEstateDto>> Get(int zip)
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
    }
}
