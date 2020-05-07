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

namespace EjemploPruebasUnitariasXUnit.Integracion
{

    public partial class PaisControllerTestIntegracion : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly TestWebApplicationFactory _factory;

        private readonly JsonSerializerOptions _jsonSettings = new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        };
        
        public PaisControllerTestIntegracion(TestWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }


        [Fact]
        public async Task GetPorNombreAsync_Flujo_OK()
        {
            // Arrange
            const string BUSQUEDA = "argentin";
            const string NOM_ESPERADO = "Argentina";
            const int HAB_ESPERADOS = 4700001;

            _factory.MockApiPaises.Setup(s => 
                    s.BuscarPaisesPorNombreAsync(BUSQUEDA))
                        .ReturnsAsync(new PaisDto[] { new PaisDto() { Nombre = NOM_ESPERADO, Poblacion = HAB_ESPERADOS } })
                        .Verifiable();

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

            _factory.MockApiPaises.Verify();
        }

        [Fact]
        public async Task GetPorNombreAsync_Flujo_ErrorYEscribeLog()
        {
            // Arrange
            const HttpStatusCode ERROR_ESPERADO = HttpStatusCode.InternalServerError;
            const string CODIGO_VALIDACION = "E01";
            Func<string, bool> ALGUNA_DESC_ERROR = (string msj) => !string.IsNullOrWhiteSpace(msj);

            _factory.MockApiPaises.Setup(s => 
                    s.BuscarPaisesPorNombreAsync("xxx"))
                        .ThrowsAsync(new Exception("Excepción de prueba"))
                        .Verifiable();

            _factory.MockLoggerPaisesController.SetupAnyLog();

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

            _factory.MockApiPaises.Verify();
            _factory.MockLoggerPaisesController.VerifyAll();
 
        }        
    }
}