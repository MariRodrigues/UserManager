using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserManager.Data;
using UserManager.Model;

namespace UserManager.Extensions
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection service)
        {
            service.AddIdentity<CustomUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            service.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return service;
        }
    }
}
