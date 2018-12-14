using System.Net;
using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Api.Application.Auth;
using DotNetRuServerHipstaMVP.Api.Application.ExceptionFilter;
using DotNetRuServerHipstaMVP.Api.Application.Extensions;
using DotNetRuServerHipstaMVP.Api.Dto.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRuServerHipstaMVP.Api.Controllers.Auth
{
    [Produces("application/json")]
    [Route("/oauth/tokens")]
    public class AuthTokenController : Controller
    {
        private readonly IAuthService _authService;

        public AuthTokenController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthTokenResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotAcceptable)]
        public async Task<AuthTokenResponse> Generate([FromBody] AuthTokenRequest request)
        {
            this.ValidateRequest(request);

            var token = await _authService.Generate(request.Email.ToLower(), request.Password);
            return new AuthTokenResponse {AuthToken = token.Token};
        }
    }

}