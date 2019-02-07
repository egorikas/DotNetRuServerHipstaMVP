using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Api.Dto.Auth;
using Refit;

namespace DotNetRuServerHipstaMVP.UI.Application.Api
{
    public interface IAuthApi
    {
        [Post("/oauth/tokens")]
        Task<AuthTokenResponse> Generate([Body] AuthTokenRequest request);
    }
}