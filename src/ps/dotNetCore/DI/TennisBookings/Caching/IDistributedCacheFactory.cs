using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisBookings.Caching
{
    public interface IDistributedCacheFactory
    {
        IDistributedCache<T> GetCache<T>();
    }
}
