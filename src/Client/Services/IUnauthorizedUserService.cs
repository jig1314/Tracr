using Tracr.Shared.DTOs;

namespace Tracr.Client.Services
{
    public interface IUnauthorizedUserService
    {
        Task RegisterNewUser(RegisterUserDto registerUserDto);
        Task Login(LoginDto loginDto);
        Task ResetPassword(ResetPasswordDto resetPasswordDto);
    }
}
