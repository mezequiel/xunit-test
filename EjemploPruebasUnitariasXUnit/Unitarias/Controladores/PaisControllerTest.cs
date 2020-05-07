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

namespace EjemploPruebasUnitariasXUnit.Unitarias.Controladores
{
    /// <summary>
    /// Pruebas unitarias del controlador 'PaisController'
    /// </summary>
    public partial class PaisControllerTest
    {

        void VerificarMockLogger(Mock<ILogger<PaisController>> mockLogger, Times veces)
        {
            mockLogger.Verify(x => x.Log(
                                        It.IsAny<LogLevel>(),
                                        It.IsAny<EventId>(),
                                        It.IsAny<It.IsAnyType>(),
                                        It.IsAny<Exception>(),
                                        (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), veces);
        }

        void VerificarMockLogger(Mock<ILogger<PaisController>> mockLogger)
        {
            VerificarMockLogger(mockLogger, Times.AtLeastOnce());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetPorNombreAsync_Devuelve400_SiNoIndicaNombre(string parteNombre)
        {
            // preparo
            var mockPaisesApi = new Mock<IApiPaises>();
            mockPaisesApi
                .Setup(api => api.BuscarPaisesPorNombreAsync(parteNombre))
                .ReturnsAsync(new[] { _paisesDePrueba["ARG"] });

            var mockLogger = new Mock<ILogger<PaisController>>();

            // ejecuto
            var controller = new PaisController(mockPaisesApi.Object, mockLogger.Object);
            var respuesta = await controller.GetPorNombreAsync(parteNombre);

            // valido
            Assert.NotNull(respuesta);
            var resultadoApi = Assert.IsAssignableFrom<BadRequestObjectResult>(respuesta.Result) as ObjectResult;
            var datosResultado = respuesta.Value;


            Assert.NotNull(resultadoApi);
            Assert.Null(datosResultado);

            var validacion = Assert.IsType<MensajeValidacion>(resultadoApi.Value);
            Assert.Equal("P01", validacion?.Mensaje);
            Assert.Equal("parteNombre", validacion?.Detalles?.FirstOrDefault());

        }

        [Fact]
        public async Task GetPorNombreAsync_DevuelveArrayVacio_SiNoEncuentraPais()
        {
            // preparo
            var mockPaisesApi = new Mock<IApiPaises>();
            mockPaisesApi.Setup(api => api.BuscarPaisesPorNombreAsync("XXX"))
                                    .ReturnsAsync(new PaisDto[0])
                                    .Verifiable();

            var mockLogger = new Mock<ILogger<PaisController>>();

            // ejecuto
            var controller = new PaisController(mockPaisesApi.Object, mockLogger.Object);
            var respuesta = await controller.GetPorNombreAsync("XXX");


            // valido
            var resultadoApi = respuesta.Result;
            var datosResultado = respuesta?.Value;

            Assert.Null(resultadoApi);
            Assert.Equal(0, datosResultado?.Count());

            mockPaisesApi.Verify();
        }

        [Fact]
        public async Task GetPorNombreAsync_Devuelve500YLoguear_SiDaErrorLaApi()
        {
            // preparo
            var mockPaisesApi = new Mock<IApiPaises>();
            mockPaisesApi.Setup(api => api.BuscarPaisesPorNombreAsync("XXX"))
                                    .Throws(new Exception("Error de prueba!"))
                                    .Verifiable();

            var mockLogger = new Mock<ILogger<PaisController>>();
            

            // ejecuto
            var controller = new PaisController(mockPaisesApi.Object, mockLogger.Object);
            var respuesta = await controller.GetPorNombreAsync("XXX");

            // valido
            Assert.NotNull(respuesta);
            var resultadoApi = Assert.IsAssignableFrom<ObjectResult>(respuesta.Result);
            var datosResultado = respuesta.Value;

            Assert.NotNull(resultadoApi);
            Assert.Equal(500, resultadoApi.StatusCode);
            Assert.Null(datosResultado);

            var validacion = Assert.IsType<MensajeValidacion>(resultadoApi.Value);
            Assert.Equal("E01", validacion?.Mensaje);

            mockPaisesApi.Verify();
            VerificarMockLogger(mockLogger);
        }


        [Theory]
        [InlineData("il")]
        [InlineData("IL")]
        [InlineData("iL")]
        public async Task GetPorNombreAsync_DevuelveVariosPaises_Cuando_IndicaParteNombre(string parteNombre)
        {
            // preparo
            var mockPaisesApi = new Mock<IApiPaises>();
            mockPaisesApi.Setup(api => api.BuscarPaisesPorNombreAsync(parteNombre))
                                    .ReturnsAsync(new PaisDto[] { _paisesDePrueba["BRA"], _paisesDePrueba["CHL"] })
                                    .Verifiable();

            var mockLogger = new Mock<ILogger<PaisController>>();

            // ejecuto
            var controller = new PaisController(mockPaisesApi.Object, mockLogger.Object);
            var respuesta = await controller.GetPorNombreAsync(parteNombre);

            // valido
            Assert.NotNull(respuesta);
            var resultadoApi = respuesta.Result;
            var datosResultado = respuesta?.Value;

            Assert.Null(resultadoApi);
            Assert.NotNull(datosResultado);
            Assert.Equal(2, datosResultado.Where(x => x.Codigo3 == "BRA" || x.Codigo3 == "CHL").Distinct().Count());

            mockPaisesApi.Verify();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task GetLimitrofesPaisAsync_Devuelve400_SiNoIndicaCodigoPais3(string codPais3)
        {
            // preparo
            var codPais3ONulo = codPais3?.Trim().ToUpper();
            var mockPaisesApi = new Mock<IApiPaises>();
            mockPaisesApi
                .Setup(api => api.BuscarPaisesPorCodigoAsync(new string[] { codPais3ONulo }))
                .ReturnsAsync(new[] { _paisesDePrueba["ARG"] });

            var mockLogger = new Mock<ILogger<PaisController>>();

            // ejecuto
            var controller = new PaisController(mockPaisesApi.Object, mockLogger.Object);
            var respuesta = await controller.GetLimitrofesPaisAsync(codPais3);

            // valido
            Assert.NotNull(respuesta);
            var resultadoApi = Assert.IsAssignableFrom<BadRequestObjectResult>(respuesta.Result) as ObjectResult;
            var datosResultado = respuesta.Value;


            Assert.NotNull(resultadoApi);
            Assert.Null(datosResultado);

            var validacion = Assert.IsType<MensajeValidacion>(resultadoApi.Value);
            Assert.Equal("P02", validacion?.Mensaje);
            Assert.Equal("codPais3", validacion?.Detalles?.FirstOrDefault());

        }

        [Theory]
        [InlineData("A  ")]
        [InlineData("A")]
        [InlineData("AB")]
        [InlineData("ABDE")]
        public async Task GetLimitrofesPaisAsync_Devuelve400_SiCodPaisNoTiene3Caracteres(string codPais3)
        {
            // preparo
            var mockPaisesApi = new Mock<IApiPaises>();
            mockPaisesApi
                .Setup(api => api.BuscarPaisesPorCodigoAsync(new string[] { codPais3.Trim().ToUpper() }))
                .ReturnsAsync(new[] { _paisesDePrueba["ARG"] });

            var mockLogger = new Mock<ILogger<PaisController>>();

            // ejecuto
            var controller = new PaisController(mockPaisesApi.Object, mockLogger.Object);
            var respuesta = await controller.GetLimitrofesPaisAsync(codPais3);

            // valido
            Assert.NotNull(respuesta);
            var resultadoApi = Assert.IsAssignableFrom<BadRequestObjectResult>(respuesta.Result) as ObjectResult;
            var datosResultado = respuesta.Value;


            Assert.NotNull(resultadoApi);
            Assert.Null(datosResultado);

            var validacion = Assert.IsType<MensajeValidacion>(resultadoApi.Value);
            Assert.Equal("P03", validacion?.Mensaje);
            Assert.Equal("codPais3", validacion?.Detalles?.FirstOrDefault());

        }

        [Fact]
        public async Task GetLimitrofesPaisAsync_Devuelve404_SiNoEncuentraPais()
        {
            // preparo
            var mockPaisesApi = new Mock<IApiPaises>();
            mockPaisesApi
                .Setup(api => api.BuscarPaisesPorCodigoAsync(new string[] { "XXX" }))
                .ReturnsAsync(new PaisDto[] {  });

            var mockLogger = new Mock<ILogger<PaisController>>();

            // ejecuto
            var controller = new PaisController(mockPaisesApi.Object, mockLogger.Object);
            var respuesta = await controller.GetLimitrofesPaisAsync("XXX");

            // valido
            Assert.NotNull(respuesta);
            var resultadoApi = Assert.IsAssignableFrom<ObjectResult>(respuesta.Result);
            var datosResultado = respuesta.Value;


            Assert.NotNull(resultadoApi);
            Assert.Equal(404, resultadoApi.StatusCode);
            Assert.Null(datosResultado);

            var validacion = Assert.IsType<MensajeValidacion>(resultadoApi.Value);
            Assert.Equal("P04", validacion?.Mensaje);
            Assert.Equal("XXX", validacion?.Detalles?.FirstOrDefault());

            mockPaisesApi.Verify();
        }

        [Theory]
        [InlineData("arg")]
        [InlineData("ARG ")]
        public async Task GetLimitrofesPaisAsync_DevuelveArrayDeLimitrofesOrdenPoblacDesc_SiTieneLimitrofes(string codPais3)
        {
            // preparo
            var limitrofesEsperados = new PaisDto[] { _paisesDePrueba["URY"], _paisesDePrueba["BRA"], _paisesDePrueba["BOL"], _paisesDePrueba["CHL"] };

            var mockPaisesApi = new Mock<IApiPaises>();
            mockPaisesApi
                .Setup(api => api.BuscarPaisesPorCodigoAsync(new string[] { codPais3.Trim().ToUpper() }))
                .ReturnsAsync( new PaisDto[] { _paisesDePrueba[codPais3.Trim().ToUpper()] })
                .Verifiable();

            mockPaisesApi
                .Setup(api => api.BuscarPaisesPorCodigoAsync(_paisesDePrueba[codPais3.Trim().ToUpper()].CodigoLimitrofes))
                .ReturnsAsync(limitrofesEsperados)
                .Verifiable();

            var mockLogger = new Mock<ILogger<PaisController>>();

            // ejecuto
            var controller = new PaisController(mockPaisesApi.Object, mockLogger.Object);
            var respuesta = await controller.GetLimitrofesPaisAsync(codPais3);

            // valido
            Assert.NotNull(respuesta);
            var resultadoApi = respuesta.Result;
            var datosResultado = respuesta.Value;


            Assert.Null(resultadoApi);
            Assert.NotNull(datosResultado);

            var ordenEsperado = "BRA,CHL,BOL,URY";

            Assert.Equal(ordenEsperado, string.Join(",",datosResultado.Select(x => x.Codigo3)));

            mockPaisesApi.Verify();
        }

        [Fact]
        public async Task GetLimitrofesPaisAsync_DevuelveArrayVacio_SiNoTieneLimitrofes()
        {
            // preparo
            var limitrofesEsperados = new PaisDto[0];

            var mockPaisesApi = new Mock<IApiPaises>();
            mockPaisesApi
                .Setup(api => api.BuscarPaisesPorCodigoAsync(new string[] { "NZL" }))
                .ReturnsAsync(new PaisDto[] { _paisesDePrueba["NZL"] })
                .Verifiable();

            var mockLogger = new Mock<ILogger<PaisController>>();

            // ejecuto
            var controller = new PaisController(mockPaisesApi.Object, mockLogger.Object);
            var respuesta = await controller.GetLimitrofesPaisAsync("NZL");

            // valido
            Assert.NotNull(respuesta);
            var resultadoApi = respuesta.Result;
            var datosResultado = respuesta.Value;


            Assert.Null(resultadoApi);
            Assert.NotNull(datosResultado);

            Assert.Equal(0, datosResultado.Count());

            mockPaisesApi.Verify();
        }

        [Fact]
        public async Task GetLimitrofesPaisAsync_Devuelve500YLoguear_SiDaErrorLaApi()
        {
            // preparo
            var mockPaisesApi = new Mock<IApiPaises>();
            mockPaisesApi.Setup(api => api.BuscarPaisesPorCodigoAsync(new[] { "XXX" }))
                                    .Throws(new Exception("Error de prueba!"))
                                    .Verifiable();

            var mockLogger = new Mock<ILogger<PaisController>>();


            // ejecuto
            var controller = new PaisController(mockPaisesApi.Object, mockLogger.Object);
            var respuesta = await controller.GetLimitrofesPaisAsync("XXX");

            // valido
            Assert.NotNull(respuesta);
            var resultadoApi = Assert.IsAssignableFrom<ObjectResult>(respuesta.Result);
            var datosResultado = respuesta.Value;

            Assert.NotNull(resultadoApi);
            Assert.Equal(500, resultadoApi.StatusCode);
            Assert.Null(datosResultado);

            var validacion = Assert.IsType<MensajeValidacion>(resultadoApi.Value);
            Assert.Equal("E01", validacion?.Mensaje);

            mockPaisesApi.Verify();
            VerificarMockLogger(mockLogger);
        }
    }
}
