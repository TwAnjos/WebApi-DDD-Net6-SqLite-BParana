using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace WebAPIs.ProgramConfigs
{
    
    public class SwaggerConfig
    {
        internal static Action<SwaggerUIOptions> GetEndpoint()
        {
            return options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI V1");
                
            };
        }

        internal static Action<SwaggerGenOptions> SwaggerOptions() => options =>
        {
            options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "WebAPI - PB",
                        Version = "v1",
                        Description = "A sample API to tests.",
                        TermsOfService = new Uri("http://localhost:5001/swagger/index.html"),
                        Contact = new OpenApiContact
                        {
                            Name = "Tw Anjos",
                            Email = "thiago_anjos@live.com"
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Apache 2.0",
                            Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
                        }
                    });

            
            options.IncludeXmlComments(Path.Combine( AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().FullName?.Split(',')[0]}.xml"));

            options.SupportNonNullableReferenceTypes();

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme." +
                "\r\n\r\n Enter 'Bearer' [Space] and then you token in the text input below." +
                "\r\n\r\n Example: Bearer 12345abcdef"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        };


    }
}
