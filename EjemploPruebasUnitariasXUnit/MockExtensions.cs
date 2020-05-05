using Moq;
using Moq.Language.Flow;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EjemploPruebasUnitariasXUnit
{
    public static class MockExtensions
    {
        public static ISetup<HttpMessageHandler, HttpResponseMessage> SetupHttpMessage<TMocked>(this TMocked mock, string metodo = "SendAsync")
            where TMocked: Mock<HttpMessageHandler>
        {
            var setup = mock.Protected()
                        .Setup<HttpResponseMessage>(metodo, ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
            return setup;
        }

        public static ISetup<HttpMessageHandler, Task<HttpResponseMessage>> SetupAsyncHttpMessage<TMocked>(this TMocked mock, string metodo = "SendAsync")
            where TMocked : Mock<HttpMessageHandler>
        {
            var setup = mock.Protected()
                        .Setup<Task<HttpResponseMessage>>(metodo, ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
            return setup;
        }
        public static IReturnsResult<HttpMessageHandler> SetupHttpMessageAndResult<TMocked>(this TMocked mock, string metodo = "SendAsync", HttpStatusCode status = HttpStatusCode.OK, string content = null)
            where TMocked : Mock<HttpMessageHandler>
        {
            var setup = mock.SetupHttpMessage(metodo);
            var returns = setup.Returns(new HttpResponseMessage
            {
                StatusCode = status,
                Content = new StringContent(content)
            }); ;
            return returns;
        }

        public static IReturnsResult<HttpMessageHandler> SetupAsyncHttpMessageAndResult<TMocked>(this TMocked mock, string metodo = "SendAsync", HttpStatusCode status = HttpStatusCode.OK, string content = null)
            where TMocked : Mock<HttpMessageHandler>
        {
            var setup = mock.SetupAsyncHttpMessage(metodo);
            var returns = setup.ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = status,
                Content = new StringContent(content)
            }); ;
            return returns;
        }
    }
}
