using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Api.Application.ExceptionFilter;
using DotNetRuServerHipstaMVP.Api.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DotNetRuServerHipstaMVP.Api.Application.Extensions
{
    public static class AuthExtension
    {
        public static void ConfigureAuth(this IServiceCollection services, AuthOptions authOptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,
                        ValidateLifetime = false,
                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = OnTokenValidatedAction,
                        OnChallenge = OnChallenge
                    };
                });


            async Task OnTokenValidatedAction(TokenValidatedContext context)
            {
                context.Success();
            }

            async Task OnChallenge(JwtBearerChallengeContext context)
            {
                if (context.AuthenticateFailure != null)
                {
                    var exception = new ErrorResponse {Errors = new[] {context.AuthenticateFailure.Message}};
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(exception));
                }
                else
                {
                    var exception = new ErrorResponse {Errors = new[] {"Запрос без токена"}};
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(exception));
                    context.HandleResponse();
                }
            }
        }
    }
}