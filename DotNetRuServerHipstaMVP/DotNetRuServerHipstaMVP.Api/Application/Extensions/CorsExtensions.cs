using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetRuServerHipstaMVP.Api.Application.Extensions
{
    public static class CorsExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            services.AddCors(options => { options.AddPolicy("MyCorsPolicy", corsBuilder.Build()); });
        }
    }
}