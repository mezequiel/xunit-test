using System.Net.WebSockets;
using System;
using System.Linq;
using EjemploPruebasUnitarias.Controllers;
using EjemploPruebasUnitarias.Servicios;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using EjemploPruebasUnitarias;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using EjemploPruebasUnitarias.Dtos;

namespace EjemploPruebasUnitariasXUnit.Integracion
{


    public class TestWebApplicationFactory
        : WebApplicationFactory<Startup>
    {
        public Mock<IApiPaises> MockApiPaises { get; private set; }
        public Mock<ILogger<PaisController>> MockLoggerPaisesController { get; private set; }
        public static Mock<IApiPaises> CrearApiPaisesMock()
        {
            var mock = new Mock<IApiPaises>();
            return mock;
        }
        public static Mock<ILogger<PaisController>> CrearLoggerPaisesControllerMock()
        {
            return new Mock<ILogger<PaisController>>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            MockApiPaises = CrearApiPaisesMock();
            MockLoggerPaisesController = CrearLoggerPaisesControllerMock();

            builder.ConfigureTestServices((services) =>
                {
                    services.RemoveAll(typeof(IApiPaises));
                    services.AddTransient<IApiPaises>(sp => MockApiPaises.Object);
                    services.AddTransient<ILogger<PaisController>>(sp => MockLoggerPaisesController.Object);
                });
        }
    }
}
