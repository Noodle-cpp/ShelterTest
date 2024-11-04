using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelterServiceClient
{
    public class ShelterServiceConfigurationOptions
    {
        public ShelterApiConfigurationOptions ShelterApi { get; set; }
    }

    public class ShelterApiConfigurationOptions
    {
        public string ServerUrl { get; set; }
        public string Key { get; set; }
    }
}
