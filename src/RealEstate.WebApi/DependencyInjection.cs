using Microsoft.Extensions.DependencyInjection;
using RealEstate.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomizedServices(this IServiceCollection services)
        {
            services.AddTransient<IOwnerService, OwnerService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IPropertyImageService, PropertyImageService>();
            services.AddTransient<IPropertyTraceService, PropertyTraceService>();

            return services;
        }
    }
}
