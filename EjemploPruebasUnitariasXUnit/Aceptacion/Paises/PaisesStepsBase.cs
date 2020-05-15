using EjemploPruebasUnitariasXUnit.Configuracion;
using EjemploPruebasUnitariasXUnit.Integracion;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace EjemploPruebasUnitariasXUnit.Aceptacion.Paises
{
    [Collection("Global Fixtures")]
    public class PaisesStepsBase
    {
        public TestWebApplicationConfigFixture Conf => (TestWebApplicationConfigFixture) this.Valores["conf"];
        public Dictionary<string, object> Valores { get; } = new Dictionary<string, object>();
        protected readonly ScenarioContext scenarioContext;
        public PaisesStepsBase(ScenarioContext scenarioContext, TestWebApplicationConfigFixture config)
        {
            if (scenarioContext == null)
                throw new ArgumentNullException("scenarioContext");

            this.scenarioContext = scenarioContext;
            this.Valores.Add("conf", config);
        }



        [When(@"se invoca el endpoint '(.*)' con los parametros '(.*)'")]
        public void CuandoSeInvocaElEndpointConLosParametros(string endpoint, string parametros)
        {
            this.Valores["endpoint-codigo"] = null;
            this.Valores["endpoint-json"] = null;

            var app = Conf.Create();
            using (var client = app.CreateClient())
            {
                var resp = client.GetAsync($"{endpoint}/{string.Join("/", new string[] { parametros })}".TrimEnd('/')).Result;
                this.Valores["endpoint-codigo"] = (int) resp.StatusCode;
                this.Valores["endpoint-json"] = resp.Content.ReadAsStringAsync().Result;
            }
        }

        [Given(@"que la API de terceros devolvera el codigo (.*)")]
        public void DadoQueLaAPIDeTercerosDevuelveElCodigo(int codigo)
        {
            var apiTerceros = Conf.MockearPaisesApiServerMock();

            apiTerceros.Given(Request.Create()
                                                .WithPath("/*")
                                                .UsingGet())
                                        .RespondWith(Response.Create().WithStatusCode(codigo));
        }

        [Given(@"que la API de terceros devolvera el codigo (.*) y el json '(.*)'")]
        public void DadoQueLaAPIDeTercerosDevuelveElCodigoYElJson(int codigo, string json)
        {
            var apiTerceros = Conf.MockearPaisesApiServerMock();

            apiTerceros.Given(Request.Create()
                                                .WithPath("/*")
                                                .UsingGet())
                                        .RespondWith(Response.Create().WithStatusCode(codigo)
                                                                        .WithBody(json)
                                                                        .WithHeader("Content-Type", "application/json;charset=utf-8"));
        }

 
        protected void EntoncesLaAPIDevuelve(int? codigo, string json, bool validaJson = true)
        {
            var validaCodigo = codigo != null;
            if (validaCodigo)
                Assert.Equal(codigo, this.Valores.GetValueOrDefault("endpoint-codigo"));

            if (validaJson)
                Assert.Equal(json, this.Valores.GetValueOrDefault("endpoint-json"));

        }

        [Then(@"la API devuelve el codigo (.*) y el json '(.*)'")]
        public void EntoncesLaAPIDevuelveElCodigoYElJson(int codigo, string json)
        {
            EntoncesLaAPIDevuelve(codigo, json, true);
        }

        [Then(@"la API devuelve el json '(.*)'")]
        public void EntoncesLaAPIDevuelveElJson(string json)
        {
            EntoncesLaAPIDevuelve(null, json, true);
        }

        [Then(@"la API devuelve codigo (.*)")]
        public void EntoncesLaAPIDevuelveElCodigo(int codigo)
        {
            EntoncesLaAPIDevuelve(codigo, null, false);
        }


    }
}
