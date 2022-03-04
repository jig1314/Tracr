using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Net.Http.Json;
using System.Security.Claims;
using Tracr.Shared.DTOs;

namespace Tracr.Client.Factories
{
    public class CustomAccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        private readonly IHttpClientFactory clientFactory;
        public CustomAccountClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor, IHttpClientFactory clientFactory)
            : base(accessor) 
        { 
            this.clientFactory = clientFactory;
        }

        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
        {
            var user = await base.CreateUserAsync(account, options);

            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                var httpClient = clientFactory.CreateClient("Tracr.ServerAPI");
                var identity = (ClaimsIdentity)user.Identity;

                var userBasicInfo = await httpClient.GetFromJsonAsync<BasicUserInfo>($"api/user/basicInfo");
                if (userBasicInfo != null)
                {
                    identity.AddClaim(new Claim("UserFirstName", userBasicInfo.FirstName));
                }
            }

            return user;
        }
    }
}
