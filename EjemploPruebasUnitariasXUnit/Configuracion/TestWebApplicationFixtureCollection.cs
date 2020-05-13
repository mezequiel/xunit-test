using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EjemploPruebasUnitariasXUnit.Configuracion
{
    [CollectionDefinition("Global Fixtures")]
    public class TestWebApplicationFixtureCollection<TConfiguracion> : ICollectionFixture<TestWebApplicationFactoryFixture>
    {
    }
}
