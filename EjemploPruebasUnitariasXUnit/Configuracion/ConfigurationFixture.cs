using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace EjemploPruebasUnitariasXUnit
{
    public class ConfigurationFixture
    {
        public readonly IConfiguration Configuration;
        public ConfigurationFixture()
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }
    }
}
