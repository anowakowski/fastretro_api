using Fastretro.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fastretro.Infrastructure.ApplicationStartup
{
    public static class AuthenticationConfigurationExtensions
    {
        public static void AddAndConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                var issuerKeyProvider = new FirebaseIssuerKeyProvider(configuration.GetSection("Authentication:PublicKeysRequestUri").Value);
                var signingKeys = issuerKeyProvider.GetSigningKeys().Result;

                options.Authority = configuration.GetSection("Authentication:Authority").Value;
                options.TokenValidationParameters =
                    new FirebaseTokenValidationParameters(
                        configuration.GetSection("Authentication:FirebaseProjectId").Value,
                        signingKeys,
                        configuration.GetSection("Authentication:ValidIssuer").Value);
            });
        }
    }
}
