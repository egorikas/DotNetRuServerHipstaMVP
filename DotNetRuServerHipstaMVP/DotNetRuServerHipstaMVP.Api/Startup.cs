using System.Collections.Generic;
using DotNetRuServerHipstaMVP.Api.Application.ExceptionFilter;
using DotNetRuServerHipstaMVP.Api.Application.Extensions;
using DotNetRuServerHipstaMVP.Api.Application.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DotNetRuServerHipstaMVP.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;

        public Startup(
            IConfiguration configuration,
            IHostingEnvironment env
        )
        {
            _configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();

            services.ConfigureSettings(_configuration);

            var authOption = new AuthOptions();
            _configuration.GetSection("Auth").Bind(authOption);
            services.ConfigureAuth(authOption);

            services.ConfigureDependencies(_configuration);

            services
                .AddMvc(options => { options.Filters.Add(typeof(ExceptionFilter)); })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);
                c.SwaggerDoc("v1", new Info {Title = "DotNetRu", Version = "v1"});
                c.DescribeAllEnumsAsStrings();

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseCors("MyCorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNetRu V1"); });

            app.UseMvc();
        }
    }
}