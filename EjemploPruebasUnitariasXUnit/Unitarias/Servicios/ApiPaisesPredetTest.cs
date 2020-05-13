using EjemploPruebasUnitarias;
using EjemploPruebasUnitarias.Dtos;
using EjemploPruebasUnitarias.Servicios.Paises;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
namespace EjemploPruebasUnitariasXUnit.Unitarias.Servicios
{
    [Collection("Global Fixtures")]
    public partial class ApiPaisesPredetTest
    {
        IConfiguration _configuration;
        public ApiPaisesPredetTest(ConfigurationFixture configuration)
        {
            _configuration = configuration.Configuration;
        }
        [Fact]
        public async Task BuscarPaisesPorCodigoAsync_DevuelveDTOsDePaisesConsultados()
        {
            // preparo
            var paisesABuscar = new string[] { "ARG", "CHL", "PER", "URU", "BRA", "BOL" };
            var mockClientFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.SetupAsyncHttpMessageAndResult(content: DatosTestApiPaises.JsonAmerica);
            
            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);


            // ejecuto
            var servicio = new ApiPaisesPredet(mockClientFactory.Object, _configuration);
            var respuesta = await servicio.BuscarPaisesPorCodigoAsync(paisesABuscar);

            // valido
            Assert.NotNull(respuesta);
            Assert.Equal(paisesABuscar.Length, respuesta.Count);

            mockClientFactory.Verify();
        }


        [Theory]
        [InlineData("[null]")]
        [InlineData("[]")]
        public async Task BuscarPaisesPorCodigoAsync_DevuelveListaVacia_SiNoEncuentraPaisesConsultados(string jsonVacio)
        {
            // preparo
            var mockClientFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.SetupAsyncHttpMessageAndResult(content: jsonVacio);

            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);


            // ejecuto
            var servicio = new ApiPaisesPredet(mockClientFactory.Object, _configuration);
            var respuesta = await servicio.BuscarPaisesPorCodigoAsync(new[] { "XXX" });

            // valido
            Assert.NotNull(respuesta);
            Assert.Equal(0, respuesta.Count);

            mockClientFactory.Verify();
        }

        [Fact]
        public async Task BuscarPaisesPorCodigoAsync_NoCapturaErrores()
        {
            // preparo
            var mockClientFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.SetupAsyncHttpMessageAndResult(content: "/*esto no es un json*/").Verifiable();

            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);


            // ejecuto
            var servicio = new ApiPaisesPredet(mockClientFactory.Object, _configuration);
            Func<Task<IList<PaisDto>>> ejecutar = async() => await servicio.BuscarPaisesPorCodigoAsync(new []{ "XXX"});

            // valido
            await Assert.ThrowsAnyAsync<Exception>(ejecutar);
            mockClientFactory.Verify();
            mockHttpMessageHandler.Verify();
        }
    }
}
