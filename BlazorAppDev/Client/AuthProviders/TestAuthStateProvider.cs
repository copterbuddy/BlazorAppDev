using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorAppDev.Client.AuthProviders
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(1500);
            var claim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Cop Ter"),
                new Claim(ClaimTypes.Role,"Admin"),
            };

            var anonymous = new ClaimsIdentity(claim, "testAuthType");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
        }
    }
}
