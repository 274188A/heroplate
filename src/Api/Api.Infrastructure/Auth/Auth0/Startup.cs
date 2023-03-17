using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Heroplate.Api.Infrastructure.Auth.Auth0;

internal static class Startup
{
    internal static IServiceCollection AddAuth0(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
                    {
                        c.Authority = $"https://{config["Auth0:Domain"]}";
                        c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidAudience = config["Auth0:Audience"],
                            ValidIssuer = $"https://{config["Auth0:Domain"]}"
                        };
                    });

        return services;
    }
}