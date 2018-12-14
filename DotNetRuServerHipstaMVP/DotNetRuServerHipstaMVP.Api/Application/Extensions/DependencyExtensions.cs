using DotNetRuServerHipstaMVP.Api.Application.Auth;
using DotNetRuServerHipstaMVP.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetRuServerHipstaMVP.Api.Application.Extensions
{
    public static class DependencyExtensions
    {
        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DotNetRuServerContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Database")));

            services.AddTransient<IAuthService, AuthService>();

        }
    }
}