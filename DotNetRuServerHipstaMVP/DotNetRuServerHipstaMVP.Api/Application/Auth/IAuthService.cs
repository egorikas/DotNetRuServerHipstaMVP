using System.Threading.Tasks;

namespace DotNetRuServerHipstaMVP.Api.Application.Auth
{
    public interface IAuthService
    {
        Task<AuthToken> Generate(string email, string password);
        Task<AuthToken> Generate(int userId);
        Task CheckPassword(int userId, string oldPassword);
    }
}