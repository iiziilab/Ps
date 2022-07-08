using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PS
{
    public static class ConfigurationHelper
    {
        public static IConfiguration config;
        public static void Initialize(IConfiguration Configuration)
        {
            config = Configuration;
        }
    }
}
