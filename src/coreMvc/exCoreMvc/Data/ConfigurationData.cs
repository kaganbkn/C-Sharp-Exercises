using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exCoreMvc.Data
{
    public class ConfigurationData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public Dictionary<string,int> Codes { get; set; }

    }
}
