using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonObjects.Utilities
{
    public static class ServiceCollectionAuthExtensions
    {
        public static void AddJsonWebToken(this IServiceCollection services, string key, TimeSpan expires)
        {
            services.AddJsonWebToken(new JsonWebTokenSettings(key, expires));
        }

        public static void AddJsonWebToken(this IServiceCollection services,
                                           string key,
                                           TimeSpan expires,
                                           string audience,
                                           string issuer)
        {
            services.AddJsonWebToken(jsonWebTokenSettings: new JsonWebTokenSettings(key,
                                                              expires: expires,
                                                              audience: audience,
                                                              issuer: issuer));
        }

        public static void AddJsonWebToken(this IServiceCollection services, JsonWebTokenSettings jsonWebTokenSettings)
        {
            services.AddSingleton(_ => jsonWebTokenSettings);
            services.AddSingleton<IJsonWebTokenService, JsonWebTokenService>();
        }
        public static AuthenticationBuilder AddAuthenticationJwtBearer(this IServiceCollection services)
        {
            var jsonWebTokenSettings = services.BuildServiceProvider().GetRequiredService<JsonWebTokenSettings>();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jsonWebTokenSettings.Key));

            void JwtBearer(JwtBearerOptions options)
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = securityKey,
                    ValidAudience = jsonWebTokenSettings.Audience,
                    ValidIssuer = jsonWebTokenSettings.Issuer,
                    ValidateAudience = !string.IsNullOrEmpty(jsonWebTokenSettings.Audience),
                    ValidateIssuer = !string.IsNullOrEmpty(jsonWebTokenSettings.Issuer),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            }

            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearer);
        }
    }
}
