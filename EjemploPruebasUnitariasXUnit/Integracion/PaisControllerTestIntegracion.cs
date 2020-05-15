using System.Net.Http;
using EjemploPruebasUnitarias;
using EjemploPruebasUnitarias.Controllers;
using EjemploPruebasUnitarias.Dtos;
using EjemploPruebasUnitarias.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.ComponentModel;
using WireMock.Matchers.Request;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using System.Threading;
using EjemploPruebasUnitariasXUnit.Configuracion;
using WireMock.Server;
using WireMock;

namespace EjemploPruebasUnitariasXUnit.Integracion
{

    [Collection("Global Fixtures")]
    public partial class PaisControllerTestIntegracion : IDisposable
    {
        private readonly TestWebApplicationConfigFixture _mockConfig;
        private readonly JsonSerializerOptions _jsonSettings = new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        };
        private Mock<ILogger<PaisController>> _paisesLogger;
        private Mock<IApiPaises> _apiPaises;
        private WireMockServer _apiServer;
        private TestWebApplicationFactoryFixture _factory;
        private HttpClient _client;

        public PaisControllerTestIntegracion(ConfigurationFixture configuration)
        {
            _mockConfig = new TestWebApplicationConfigFixture(configuration);
        }


        [Fact]
        public async Task GetPorNombreAsync_Flujo_OK()
        {
            // Arrange
            const string BUSQUEDA = "argentin";
            const string NOM_ESPERADO = "Argentina";
            const int HAB_ESPERADOS = 4700001;

            _paisesLogger = _mockConfig.MockearPaisesControllerLoggerMock();
            _apiServer = _mockConfig.MockearPaisesApiServerMock();
            _apiServer.Given(Request.Create()
                                        .WithPath($"/name/{BUSQUEDA}"))
                                        .RespondWith(Response.Create()
                                                                .WithSuccess()
                                                                .WithBody(JsonSerializer.Serialize(new PaisDto[] { new PaisDto { Nombre = NOM_ESPERADO, Poblacion = HAB_ESPERADOS }}))
                                                                .WithHeader("Content-Type", "application/json;charset=utf-8")
                                                                );

            _factory = _mockConfig.Create();
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            //while (true)
            //    Thread.Sleep(1000);
            // Act
            var response = await _client.GetAsync($"/pais/nombre/{BUSQUEDA}");
            // Assert
            var respCode = response.StatusCode;
            var json = await response.Content.ReadAsStringAsync();
            var resultado = JsonSerializer.Deserialize<PaisDto[]>(json);
            var unicoRdo = resultado?.FirstOrDefault();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(resultado?.Length == 1);
            Assert.NotNull(unicoRdo);
            Assert.Equal(NOM_ESPERADO, unicoRdo.Nombre);
            Assert.Equal(HAB_ESPERADOS, unicoRdo.Poblacion);
            Assert.True(_apiServer.VerifyAll());
        }

        [Fact]
        public async Task GetPorNombreAsync_Flujo_ErrorYEscribeLog()
        {
            // Arrange
            const HttpStatusCode ERROR_ESPERADO = HttpStatusCode.InternalServerError;
            const string CODIGO_VALIDACION = "E01";
            Func<string, bool> ALGUNA_DESC_ERROR = (string msj) => !string.IsNullOrWhiteSpace(msj);

            _paisesLogger = _mockConfig.MockearPaisesControllerLoggerMock();
            _apiPaises = _mockConfig.MockearApiPaises();
            _factory = _mockConfig.Create();

            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _apiPaises.Setup(s => 
                    s.BuscarPaisesPorNombreAsync("xxx"))
                        .ThrowsAsync(new Exception("Excepción de prueba"))
                        .Verifiable();

            _paisesLogger.SetupAnyLog();

            // Act
            var response = await _client.GetAsync($"/pais/nombre/xxx");

            // Assert
            var respCode = response.StatusCode;
            var json = await response.Content.ReadAsStringAsync();
            var validacion = JsonSerializer.Deserialize<MensajeValidacion>(json, _jsonSettings);

            Assert.Equal(ERROR_ESPERADO, response.StatusCode);
            Assert.NotNull(validacion);
            Assert.Equal(CODIGO_VALIDACION, validacion.Mensaje);
            Assert.True(ALGUNA_DESC_ERROR(validacion.Descripcion));

            _apiPaises.Verify();
            _paisesLogger.VerifyAll();
 
        }

        public void Dispose()
        {
            _client?.Dispose();
            _factory?.Dispose();
            _apiPaises = null;
            _apiServer?.Dispose();
            _paisesLogger = null;
            _mockConfig?.Dispose();
        }
    }
}