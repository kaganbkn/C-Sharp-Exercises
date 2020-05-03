using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace TennisBookings.Caching
{
    public class DistributedCacheFactory : IDistributedCacheFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DistributedCacheFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDistributedCache<T> GetCache<T>() => _serviceProvider.GetRequiredService<IDistributedCache<T>>();
    }
}
