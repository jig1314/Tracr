using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tracr.Server.Data;
using Tracr.Shared.DTOs;

namespace Tracr.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("getProperties")]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetProperties()
        {
            try
            {
                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to retrieve user information");
                }

                var idUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var properties = await _context.Properties.Where(p => p.ApplicationUserId == idUser)
                    .Include(p => p.Mortage)
                    .Include(p => p.Address)
                    .ToListAsync();

                var propertyDtos = properties.Select(p => new PropertyDto()
                {
                    Name = p.Name,
                    NumBathrooms = p.NumBathrooms,
                    NumBedrooms = p.NumBedrooms,
                    Mortage = new MortageDto()
                    {
                        PropertyId = p.Id,
                        Principal = p.Mortage.Principal,
                        MonthlyPayment = p.Mortage.MonthlyPayment,
                        APR = p.Mortage.APR
                    },
                    Address = new AddressDto()
                    {
                        PropertyId = p.Id,
                        StreetAddress = p.Address.StreetAddress,
                        City = p.Address.City,
                        State = p.Address.State,
                        ZipCode = p.Address.ZipCode
                    }
                });

                return Ok(propertyDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
