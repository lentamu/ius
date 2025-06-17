using Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authentication;

public static class ConfigureAuthentication
{
    public static void AddAuthentications(this IServiceCollection services)
    {
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IJwtDecoder, JwtDecoder>();
    }
}