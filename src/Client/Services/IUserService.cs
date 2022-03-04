using Tracr.Shared.DTOs;

namespace Tracr.Client.Services
{
    public interface IUserService
    {
        Task<BasicUserInfoDto> GetBasicUserInfo();
        Task UpdateBasicUserInfo(BasicUserInfoDto basicUserInfoDto);
        Task DeleteAccount(DeleteAccountDto deleteAccountDto);
    }
}