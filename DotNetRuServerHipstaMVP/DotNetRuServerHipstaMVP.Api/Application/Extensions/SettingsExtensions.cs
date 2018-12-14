using DotNetRuServerHipstaMVP.Api.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetRuServerHipstaMVP.Api.Application.Extensions
{
    public static class SettingsExtensions
    {
        public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthOptions>(configuration.GetSection("Auth"));
        }
    }
}