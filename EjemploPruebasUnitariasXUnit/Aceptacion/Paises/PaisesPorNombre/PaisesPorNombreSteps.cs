using EjemploPruebasUnitarias.Dtos;
using EjemploPruebasUnitariasXUnit.Configuracion;
using EjemploPruebasUnitariasXUnit.Integracion;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace EjemploPruebasUnitariasXUnit.Aceptacion.Paises.PaisesPorNombre
{
    [Binding]
    public class PaisesPorNombreSteps: PaisesStepsBase
    {
        public PaisesPorNombreSteps(ScenarioContext scenarioContext, TestWebApplicationConfigFixture config): base(scenarioContext, config)
        {
        }
    }
}
