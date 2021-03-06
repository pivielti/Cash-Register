﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace CashRegister.Web.App_Start
{
    public static class Startup_Auth
    {
        public static void ConfigureAuth(this IApplicationBuilder app, IConfigurationRoot conf)
        {
            var secretKey = conf.GetSection("TokenAuthentication:SecretKey").Value;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var issuer = conf.GetSection("TokenAuthentication:Issuer").Value;
            var audience = conf.GetSection("TokenAuthentication:Audience").Value;
            var expiration = int.Parse(conf.GetSection("TokenAuthentication:Expiration").Value);

            var tokenValidationParameters = new TokenValidationParameters {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = audience,

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero,

                //LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => {
                //    return true;
                //}
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });
        }
    }
}
