using Microsoft.AspNetCore.Mvc;
using UserManager.Features.Users.Interfaces;
using UserManager.Features.Users;
using UserManager.Shared.Interface;
using UserManager.Shared;
using UserManager.Features.Authentication.Interfaces;
using UserManager.Features.Authentication;

namespace UserManager.Extensions
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddService(this IServiceCollection service, WebApplicationBuilder builder)
        {
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IEmailService, EmailService>();
            service.AddScoped<ILoginService, LoginService>();
            service.AddScoped<ITokenService, TokenService>();

            service.AddControllers();

            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();

            service.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            return service;
        }
    }
}
