using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TennisBookings.Services;

namespace TennisBookings.DependencyInjection
{
    public static class GuidServiceCollectionExtensions
    {
        public static IServiceCollection AddGuidService(this IServiceCollection services)
        {
            //services.AddTransient<GuidGenerator>();
            //services.AddScoped<GuidGenerator>();
            services.AddSingleton<GuidGenerator>();

            return services;
        }
    }
}
