using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TennisBookings.Caching;

namespace TennisBookings.DependencyInjection
{
    public static class CachingServiceCollectionExtensions
    {
        public static IServiceCollection AddCachingService(this IServiceCollection services)
        {

            services.AddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>)); // Generic type registration.
            return services;
        }
    }
}
