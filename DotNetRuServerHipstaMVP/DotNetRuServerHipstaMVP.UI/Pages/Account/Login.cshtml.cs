using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Api.Dto.Auth;
using DotNetRuServerHipstaMVP.UI.Application.Api;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetRuServerHipstaMVP.UI.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAuthApi _authApi;

        public LoginModel(IAuthApi authApi)
        {
            _authApi = authApi;
        }

        [BindProperty]
        public AuthTokenRequest TokenRequest { get; set; }
             
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            AuthTokenResponse token;
            try
            {
                token = await _authApi.Generate(TokenRequest);
            }
            catch (Exception e)
            {
                // handle error here
                throw;
            }
     
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Authentication, token.AuthToken),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),               
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            var aa = User.Identity.IsAuthenticated;

            return LocalRedirect(Request.Query["ReturnUrl"]);
        }
    }
}