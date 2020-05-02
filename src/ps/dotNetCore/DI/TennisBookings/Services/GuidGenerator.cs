using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisBookings.Services
{
    public class GuidGenerator
    {
        public Guid Guid { get; }

        public GuidGenerator()
        {
            Guid = Guid.NewGuid();
        }
    }
}
