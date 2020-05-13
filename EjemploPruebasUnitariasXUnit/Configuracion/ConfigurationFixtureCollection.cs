using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EjemploPruebasUnitariasXUnit
{
    [CollectionDefinition("Global Fixtures")]
    public class ConfigurationFixtureCollection: ICollectionFixture<ConfigurationFixture>
    {
    }
}
