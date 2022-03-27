using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tracr.Server.Data;
using Tracr.Server.Models;
using Tracr.Shared.DTOs;
using Tracr.Shared.Models;

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

        [HttpGet("getUserProperties")]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetUserProperties()
        {
            try
            {
                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to retrieve user information!");
                }

                var idUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var properties = await _context.Properties.Where(p => p.ApplicationUserId == idUser)
                    .Include(p => p.Mortage)
                    .Include(p => p.Address)
                    .ToListAsync();

                var propertyDtos = properties.Select(p => new PropertyDto()
                {
                    Id = p.Id,
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

        [HttpGet("getProperty/{propertyId}")]
        public async Task<ActionResult<PropertyDto>> GetProperty(int propertyId)
        {
            try
            {
                var property = await _context.Properties
                                        .Include(p => p.Mortage)
                                        .Include(p => p.Address)
                                        .FirstOrDefaultAsync(p => p.Id == propertyId);

                if (property == null)
                    return StatusCode(StatusCodes.Status404NotFound, "This property was not found!");

                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated
                    || property.ApplicationUserId != HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to retrieve information for this property!");
                }

                var propertyDto = new PropertyDto()
                {
                    Id = property.Id,
                    Name = property.Name,
                    NumBathrooms = property.NumBathrooms,
                    NumBedrooms = property.NumBedrooms,
                    Mortage = new MortageDto()
                    {
                        PropertyId = property.Id,
                        Principal = property.Mortage.Principal,
                        MonthlyPayment = property.Mortage.MonthlyPayment,
                        APR = property.Mortage.APR
                    },
                    Address = new AddressDto()
                    {
                        PropertyId = property.Id,
                        StreetAddress = property.Address.StreetAddress,
                        City = property.Address.City,
                        State = property.Address.State,
                        ZipCode = property.Address.ZipCode
                    }
                };

                return Ok(propertyDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpGet("getPropertyRenters/{propertyId}")]
        public async Task<ActionResult<IEnumerable<RenterDto>>> GetRentersByProperty(int propertyId)
        {
            try
            {
                var property = await _context.Properties
                                        .Include(p => p.Renters)
                                        .FirstOrDefaultAsync(p => p.Id == propertyId);

                if (property == null)
                    return StatusCode(StatusCodes.Status404NotFound, "This property was not found!");

                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated
                    || property.ApplicationUserId != HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to retrieve information for this property!");
                }

                var renters = property.Renters.Select(r => new RenterDto()
                {
                    Id = r.Id,
                    PropertyId = r.PropertyId,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    MonthlyRent = r.MonthlyRent,
                    StartingMonth = r.StartingMonth.ToDateTime(TimeOnly.MinValue),
                    EndingMonth = r.EndingMonth.ToDateTime(TimeOnly.MinValue)
                });

                return Ok(renters);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpGet("getRenter/{renterId}")]
        public async Task<ActionResult<RenterDto>> GetRenter(int renterId)
        {
            try
            {
                var renter = await _context.Renters.Include(p => p.Property).FirstOrDefaultAsync(r => r.Id == renterId);

                if (renter == null)
                    return StatusCode(StatusCodes.Status404NotFound, "This renter was not found!");

                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated
                    || renter.Property.ApplicationUserId != HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to retrieve renter information for this property!");
                }

                var renterDto = new RenterDto()
                {
                    Id = renter.Id,
                    PropertyId = renter.PropertyId,
                    FirstName = renter.FirstName,
                    LastName = renter.LastName,
                    MonthlyRent = renter.MonthlyRent,
                    StartingMonth = renter.StartingMonth.ToDateTime(TimeOnly.MinValue),
                    EndingMonth = renter.EndingMonth.ToDateTime(TimeOnly.MinValue)
                };

                return Ok(renterDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpGet("getUserPropertyIncome")]
        public async Task<ActionResult<List<PropertyIncome>>> GetUserPropertyIncome()
        {
            try
            {
                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to retrieve user property information!");
                }

                var idUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var income = await _context.GetAllUserPropertyIncome(idUser);

                return Ok(income);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPost("createProperty")]
        public async Task<ActionResult> CreateProperty(PropertyDto propertyDto)
        {
            try
            {
                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to create a property!");
                }

                var idUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var property = new Property()
                {
                    ApplicationUserId = idUser,
                    Name = propertyDto.Name,
                    NumBedrooms = propertyDto.NumBedrooms,
                    NumBathrooms = propertyDto.NumBathrooms
                };

                _context.Properties.Add(property);
                await _context.SaveChangesAsync();

                var mortage = new Mortage()
                {
                    PropertyId = property.Id,
                    Principal = propertyDto.Mortage.Principal,
                    MonthlyPayment = propertyDto.Mortage.MonthlyPayment,
                    APR = propertyDto.Mortage.APR
                };

                var address = new Address()
                {
                    PropertyId = property.Id,
                    StreetAddress = propertyDto.Address.StreetAddress,
                    City = propertyDto.Address.City,
                    State = propertyDto.Address.State,
                    ZipCode = propertyDto.Address.ZipCode
                };

                _context.Mortages.Add(mortage);
                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();

                propertyDto.Id = property.Id;

                return StatusCode(StatusCodes.Status202Accepted, propertyDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPost("createRenter")]
        public async Task<ActionResult> CreateRenter(RenterDto renterDto)
        {
            try
            {
                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to create a renter!");
                }

                var idUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var property = await _context.Properties.FirstOrDefaultAsync(p => p.Id == renterDto.PropertyId);

                if(property == null)
                    return StatusCode(StatusCodes.Status404NotFound, "This property was not found!");

                if (idUser != property.ApplicationUserId)
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to create a renter for this property!");

                var renter = new Renter()
                {
                    PropertyId = renterDto.PropertyId,
                    FirstName = renterDto.FirstName,
                    LastName = renterDto.LastName,
                    MonthlyRent = renterDto.MonthlyRent,
                    StartingMonth = DateOnly.FromDateTime(renterDto.StartingMonth),
                    EndingMonth = DateOnly.FromDateTime(renterDto.EndingMonth)
                };

                _context.Renters.Add(renter);
                await _context.SaveChangesAsync();

                renterDto.Id = renter.Id;

                return StatusCode(StatusCodes.Status202Accepted, renterDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPut("updateProperty")]
        public async Task<ActionResult> UpdateProperty(PropertyDto propertyDto)
        {
            try
            {
                var property = await _context.Properties
                                        .Include(p => p.Mortage)
                                        .Include(p => p.Address)
                                        .FirstOrDefaultAsync(p => p.Id == propertyDto.Id);

                if (property == null)
                    return StatusCode(StatusCodes.Status404NotFound, "This property was not found!");

                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated
                    || property.ApplicationUserId != HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to update information for this property!");
                }

                property.Name = propertyDto.Name;
                property.NumBedrooms = propertyDto.NumBedrooms;
                property.NumBathrooms = propertyDto.NumBathrooms;

                property.Mortage.Principal = propertyDto.Mortage.Principal;
                property.Mortage.MonthlyPayment = propertyDto.Mortage.MonthlyPayment;
                property.Mortage.APR = propertyDto.Mortage.APR;

                property.Address.StreetAddress = propertyDto.Address.StreetAddress;
                property.Address.City = propertyDto.Address.City;
                property.Address.State = propertyDto.Address.State;
                property.Address.ZipCode = propertyDto.Address.ZipCode;

                _context.Entry(property.Mortage).State = EntityState.Modified;
                _context.Entry(property.Address).State = EntityState.Modified;
                _context.Entry(property).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPut("updateRenter")]
        public async Task<ActionResult> UpdateRenter(RenterDto renterDto)
        {
            try
            {
                var renter = await _context.Renters.Include(p => p.Property).FirstOrDefaultAsync(r => r.Id == renterDto.Id);

                if (renter == null)
                    return StatusCode(StatusCodes.Status404NotFound, "This renter was not found!");

                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated
                    || renter.Property.ApplicationUserId != HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to update renter information for this property!");
                }

                renter.FirstName = renterDto.FirstName;
                renter.LastName = renterDto.LastName;
                renter.MonthlyRent = renterDto.MonthlyRent;
                renter.StartingMonth = DateOnly.FromDateTime(renterDto.StartingMonth);
                renter.EndingMonth = DateOnly.FromDateTime(renterDto.EndingMonth);

                _context.Entry(renter).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpDelete("deleteProperty/{propertyId}")]
        public async Task<ActionResult> DeleteProperty(int propertyId)
        {
            try
            {
                var property = await _context.Properties
                                        .Include(p => p.Mortage)
                                        .Include(p => p.Address)
                                        .Include(p => p.Renters)
                                        .FirstOrDefaultAsync(p => p.Id == propertyId);

                if (property == null)
                    return StatusCode(StatusCodes.Status404NotFound, "This property was not found!");

                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated 
                    || property.ApplicationUserId != HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to delete this property!");
                }

                _context.Mortages.Remove(property.Mortage);
                _context.Addresses.Remove(property.Address);
                _context.Renters.RemoveRange(property.Renters);
                _context.Properties.Remove(property);

                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpDelete("deleteRenter/{renterId}")]
        public async Task<ActionResult> DeleteRenter(int renterId)
        {
            try
            {
                var renter = await _context.Renters.Include(r => r.Property).FirstOrDefaultAsync(r => r.Id == renterId);

                if (renter == null)
                    return StatusCode(StatusCodes.Status404NotFound, "This renter was not found!");

                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated
                    || renter.Property.ApplicationUserId != HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to delete this renter from this property!");
                }

                _context.Renters.Remove(renter);

                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
