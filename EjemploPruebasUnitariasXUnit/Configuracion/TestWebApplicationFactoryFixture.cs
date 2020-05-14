using EjemploPruebasUnitarias;
using EjemploPruebasUnitarias.Controllers;
using EjemploPruebasUnitarias.Servicios;
using EjemploPruebasUnitariasXUnit.Integracion;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using WireMock.Server;
using WireMock.Settings;

namespace EjemploPruebasUnitariasXUnit.Configuracion
{
    public class TestWebApplicationConfigFixture: IDisposable
    {
        public readonly ConfigurationFixture ConfigurationFixture;
        internal IList<Func<TestWebApplicationConfigFixture, TestWebApplicationFactoryFixture, IHostBuilder, IHostBuilder>> OnCreateHostFn = new List<Func<TestWebApplicationConfigFixture, TestWebApplicationFactoryFixture, IHostBuilder, IHostBuilder>>();
        public TestWebApplicationConfigFixture OnCreateHost(Func<TestWebApplicationConfigFixture, TestWebApplicationFactoryFixture, IHostBuilder, IHostBuilder>config)
        {
            OnCreateHostFn.Add(config);
            return this;
        }

        public TestWebApplicationConfigFixture(ConfigurationFixture configuration)
        {
            ConfigurationFixture = configuration;
            OnCreateHost((_this, app, builder) =>
            {
                return builder.ConfigureAppConfiguration(config =>
                {
                    config.AddConfiguration(this.ConfigurationFixture.Configuration);
                });
            });
            
        }
        public TestWebApplicationFactoryFixture Create()
        {
            return new TestWebApplicationFactoryFixture(this);
        }

        public virtual Mock<IApiPaises> MockearApiPaises()
        {
            var mock = new Mock<IApiPaises>();

            OnCreateHost((_this, app, builder) => {
                return builder.ConfigureServices(config =>
                {
                    config.RemoveAll<IApiPaises>();
                    config.AddTransient<IApiPaises>((sp) => mock.Object);
                });
            });
            return mock;
        }

        public virtual Mock<ILogger<PaisController>> MockearPaisesControllerLoggerMock()
        {
            var mock = new Mock<ILogger<PaisController>>();

            OnCreateHost((_this, app, builder) => {
                return builder.ConfigureServices(config =>
                {
                    config.RemoveAll<ILogger<PaisController>>();
                    config.AddTransient<ILogger<PaisController>>((sp) => mock.Object);
                });
            });
            return mock;
        }


        protected virtual Mock<ILogger<PaisController>> MockearPaisesControllerMock()
        {
            var mock = new Mock<ILogger<PaisController>>();

            OnCreateHost((_this, app, builder) => {
                return builder.ConfigureServices(config =>
                {
                    config.RemoveAll<ILogger<PaisController>>();
                    config.AddTransient((sp) => mock.Object);
                });
            });

            return mock;
        }

        public virtual WireMockServer MockearPaisesApiServerMock()
        {
            var mock = WireMockServer.Start();
            this.ConfigurationFixture.Configuration["Api:Paises:UrlBase"] = mock.Urls.First();
            return mock;
        }

        public virtual Mock<IHttpClientFactory> MockearHttpClientFactoryMock()
        {
            var mock = new Mock<IHttpClientFactory>();

            OnCreateHost((_this, app, builder) => {
                return builder.ConfigureServices(config =>
                {
                    config.RemoveAll<IHttpClientFactory>();
                    config.AddTransient((sp) => mock.Object);
                });
            });
            return mock;
        }

        public virtual Mock<HttpMessageHandler> CreateMockHttpMessageHandler()
        {
            return new Mock<HttpMessageHandler>();
        }

        public void Dispose()
        {
        }
    }
    public class TestWebApplicationFactoryFixture : WebApplicationFactory<Startup>
    {
        TestWebApplicationConfigFixture _config;


        public TestWebApplicationFactoryFixture(TestWebApplicationConfigFixture config)
        {
            _config = config;
        }
        protected override void ConfigureClient(HttpClient client)
        {
            base.ConfigureClient(client);
        }
        protected override IHost CreateHost(IHostBuilder builder)
        {
            foreach (var config in _config.OnCreateHostFn)
            {
                builder = config(this._config, this, builder) ?? builder;
            }
            return base.CreateHost(builder);
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            return base.CreateHostBuilder();
        }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            return base.CreateServer(builder);
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return base.CreateWebHostBuilder();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _config = null;
        }
    }
}
