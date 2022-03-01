using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Tracr.Server.Data;
using Tracr.Server.Models;
using Tracr.Shared.DTOs;

namespace Tracr.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterNewUser(RegisterUserDto registerUserDto)
        {
            try
            {
                var user = new ApplicationUser { UserName = registerUserDto.UserName, Email = registerUserDto.Email };
                var password = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(registerUserDto.Password));

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    var userDetail = new ApplicationUserDetail()
                    {
                        ApplicationUserId = user.Id,
                        FirstName = registerUserDto.FirstName,
                        LastName = registerUserDto.LastName
                    };

                    _context.ApplicationUserDetails.Add(userDetail);
                    await _context.SaveChangesAsync();

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, code);

                    if (confirmEmailResult.Succeeded)
                        return StatusCode(StatusCodes.Status201Created);

                    throw new Exception(string.Join(Environment.NewLine, confirmEmailResult.Errors.Select(error => error.Description)));
                }

                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(error => error.Description)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
