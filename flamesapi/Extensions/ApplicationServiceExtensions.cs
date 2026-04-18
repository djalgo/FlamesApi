using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flamesapi.Data;
using flamesapi.Helpers;
using flamesapi.Interfaces;
using flamesapi.Services;
using Microsoft.EntityFrameworkCore;

namespace flamesapi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhotoService, PhotoServices>();
            services.AddScoped<LogUserActivity>();
            services.AddAutoMapper(typeof(AutomapperProfiles).Assembly);
            return services;            
        }
    }
}