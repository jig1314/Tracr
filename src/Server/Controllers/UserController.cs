using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginDto login)
        {
            try
            {
                var password = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(login.Password));
                var result = await _signInManager.PasswordSignInAsync(login.UserName, password, login.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                    return StatusCode(StatusCodes.Status202Accepted);

                throw new Exception($"Login failed! {(result.IsLockedOut ? "Your account is locked! Please reset your password!" : "")}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("resetPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(resetPasswordDto.UserName);

                if (user == null)
                    throw new Exception("Username could not be found!");

                var password = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordDto.NewPassword));
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, code, password);

                if (result.Succeeded)
                    return StatusCode(StatusCodes.Status202Accepted);

                throw new Exception(string.Join(System.Environment.NewLine, result.Errors.Select(error => error.Description)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpGet("basicInfo")]
        public async Task<ActionResult<BasicUserInfoDto>> GetBasicUserInfo()
        {
            try
            {
                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to retrieve user information");
                }

                var idUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _context.Users.Where(d => d.Id == idUser).Include(user => user.ApplicationUserDetail).FirstAsync();
                var userBasicInfo = new BasicUserInfoDto()
                {
                    UserId = idUser,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.ApplicationUserDetail.FirstName,
                    LastName = user.ApplicationUserDetail.LastName
                };

                return Ok(userBasicInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPut("updateBasicInfo")]
        public async Task<ActionResult<BasicUserInfoDto>> UpdateBasicUserInfo(BasicUserInfoDto basicUserInfoDto)
        {
            try
            {
                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to update account information!");
                }

                var idUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var applicationUserDetail = await _context.ApplicationUserDetails.FirstAsync(detail => detail.ApplicationUserId == idUser);
                applicationUserDetail.FirstName = basicUserInfoDto.FirstName;
                applicationUserDetail.LastName = basicUserInfoDto.LastName;

                _context.Entry(applicationUserDetail).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status202Accepted, applicationUserDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpPut("deleteAccount")]
        public async Task<ActionResult> DeleteAccount(DeleteAccountDto deleteAccountDto)
        {
            try
            {
                if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "You are not authorized to delete this account!");
                }

                var idUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _context.Users
                    .Include(user => user.ApplicationUserDetail)
                    .FirstAsync(user => user.Id == idUser);

                var password = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(deleteAccountDto.Password));
                if (!await _userManager.CheckPasswordAsync(user, password))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Incorrect password.");
                }

                _context.ApplicationUserDetails.Remove(user.ApplicationUserDetail);

                await _context.SaveChangesAsync();

                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Unexpected error occurred deleting the account for {user.UserName}.", new Exception(string.Join(",", result.Errors.Select(e => $"{e.Code} - {e.Description}"))));
                }

                await _signInManager.SignOutAsync();

                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpDelete("delete/{userName}")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteUser(string userName)
        {
            try
            {
                var user = await _context.Users.Include(user => user.ApplicationUserDetail).FirstAsync(user => user.UserName == userName);

                _context.ApplicationUserDetails.Remove(user.ApplicationUserDetail);
                await _context.SaveChangesAsync();

                await _userManager.DeleteAsync(user);

                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
