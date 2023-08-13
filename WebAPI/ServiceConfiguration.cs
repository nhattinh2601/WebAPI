
using System.Runtime.CompilerServices;
using WebAPI.repositories.impl;
using WebAPI.repositories;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebAPI.helpers;

namespace WebAPI
{
    public static class ServiceConfiguration
    {
        public static void RegisterDI(this IServiceCollection service)
        {
            service.AddScoped<ICategoryRepository, CategoryRepositoryImpl>();
            service.AddScoped<IProductRepository, ProductRepositoryImpl>();
            service.AddScoped<IRoleRepository, RoleRepositoryImpl>();
        }

        public static void ConfigureAuthentication(this IServiceCollection service, IConfiguration configuration)
        {            
            var secretKey = configuration["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey!);

            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                    ClockSkew = TimeSpan.Zero,
                };
            });
        }

    }
}
