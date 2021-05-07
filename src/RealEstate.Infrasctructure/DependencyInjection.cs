using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RealEstate.Infrasctructure.Data;
using RealEstate.Infrasctructure.Repository;
using RealEstate.Infrasctructure.Token;
using System;
using System.Text;

namespace RealEstate.Infrasctructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomizedDataStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(
                opt =>
                {
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequiredLength = 4;

                    opt.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<AppDbContext>();

            services.AddDbContext<AppDbContext>(
                options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            return services;
        }

        public static IServiceCollection AddCustomizedRepository(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }

        public static IServiceCollection AddCustomizeAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IJWTTokenGeneratorService, JWTTokenGeneratorService>();

            return services;
        }
    }
}
