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
using System.Net.Http;

namespace EjemploPruebasUnitariasXUnit.Integracion
{
    public class MockConfiguration
    {

        public MockConfiguration()
        {
        }

        protected virtual void CrearApiPaisesMock<TMockConfiguration>(TestWebApplicationFactory<TMockConfiguration> app, IWebHostBuilder builder)
        where TMockConfiguration : MockConfiguration, new()
        {
            app.MockApiPaises = app.MockApiPaises ?? new Mock<IApiPaises>();

            if (builder == null)
                return;

            builder.ConfigureTestServices((services) =>
            {
                services.RemoveAll(typeof(IApiPaises));
                services.AddTransient<IApiPaises>(sp => app.MockApiPaises.Object);

            });
        }
        protected virtual void CrearLoggerPaisesControllerMock<TMockConfiguration>(TestWebApplicationFactory<TMockConfiguration> app, IWebHostBuilder builder)
        where TMockConfiguration : MockConfiguration, new()
        {
            app.MockLoggerPaisesController = app.MockLoggerPaisesController ?? new Mock<ILogger<PaisController>>();

            if (builder == null)
                return;

            builder.ConfigureTestServices((services) =>
            {
                services.RemoveAll(typeof(ILogger<PaisController>));
                services.AddTransient<ILogger<PaisController>>(sp => app.MockLoggerPaisesController.Object);
            });
        }

        protected virtual void CrearHttpClientFactoryMock<TMockConfiguration>(TestWebApplicationFactory<TMockConfiguration> app, IWebHostBuilder builder)
        where TMockConfiguration : MockConfiguration, new()
        {
            app.MockHttpClientFactory = app.MockHttpClientFactory ?? new Mock<IHttpClientFactory>();

            if (builder == null)
                return;

            builder.ConfigureTestServices((services) =>
            {
                services.RemoveAll(typeof(IHttpClientFactory));
                services.AddTransient<IHttpClientFactory>(sp => app.MockHttpClientFactory.Object);

            });
        }

        protected virtual void CrearHttpMessageHandlerMock<TMockConfiguration>(TestWebApplicationFactory<TMockConfiguration> app, IWebHostBuilder builder)
        where TMockConfiguration : MockConfiguration, new()
        {
            app.MockHttpMessageHandler = app.MockHttpMessageHandler ?? new Mock<HttpMessageHandler>();
            if (builder == null)
                return;

        }

        public virtual void Apply<TMockConfiguration>(TestWebApplicationFactory<TMockConfiguration> app, IWebHostBuilder builder = null)
        where TMockConfiguration : MockConfiguration, new()
        {
        }

    }

    public class MockConfigurationIntegrationTest: MockConfiguration
    {
        public override void Apply<TMockConfiguration>(TestWebApplicationFactory<TMockConfiguration> app, IWebHostBuilder builder = null)
        {
            base.Apply(app, builder);
            CrearLoggerPaisesControllerMock(app, builder);
            CrearApiPaisesMock(app, builder);
        }
    }


    public class MockConfigurationAcceptanceTest : MockConfiguration
    {
        public override void Apply<TMockConfiguration>(TestWebApplicationFactory<TMockConfiguration> app, IWebHostBuilder builder = null)
        {
            base.Apply(app, builder);
            CrearLoggerPaisesControllerMock(app, builder);
            CrearHttpMessageHandlerMock(app, builder);
            CrearHttpClientFactoryMock(app, builder);
        }
    }

    public class TestWebApplicationFactory<TMockConfiguration>
        : WebApplicationFactory<Startup>
        where TMockConfiguration: MockConfiguration, new()
    {
        public TestWebApplicationFactory(): base()
        {
            new TMockConfiguration().Apply(this);
        }
        public Mock<IApiPaises> MockApiPaises { get; internal set; }
        public Mock<ILogger<PaisController>> MockLoggerPaisesController { get; internal set; }
        public Mock<IHttpClientFactory> MockHttpClientFactory { get; internal set; }
        public Mock<HttpMessageHandler> MockHttpMessageHandler { get; internal set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            new TMockConfiguration().Apply(this, builder);
        }
    }
}
