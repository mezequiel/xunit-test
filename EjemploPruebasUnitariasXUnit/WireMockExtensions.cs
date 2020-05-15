using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseProviders;
using WireMock.Server;
using WireMock.Settings;

namespace WireMock.RequestBuilders
{

    public static class WireMockProviderExtensions
    {
        public static IResponseProvider AsVerifiable(this IResponseProvider _this, string alias = null)
        {
            return new CustomResponseProvider(_this, alias);
        }
    }
}

namespace WireMock
{

    public class CustomResponseProvider : IResponseProvider
    {
        public string Group { get; }

        IResponseProvider _provider;
        public bool Invoked { get; protected set; }

        public CustomResponseProvider(IResponseProvider provider, string group = null)
        {
            Group = group;
            _provider = provider;
        }

        public Task<ResponseMessage> ProvideResponseAsync(RequestMessage requestMessage, IWireMockServerSettings settings)
        {
            Invoked = true;
            return _provider.ProvideResponseAsync(requestMessage, settings);
        }
    }


    public static class WireMockExtensions
    {
        public static bool VerifyAll(this WireMockServer _this, string title = null)
        {
            var toCheck = _this.Mappings.Where(m => string.IsNullOrWhiteSpace(title) || m.Title == title);
            var all = toCheck.All(m => _this.LogEntries.Any(l => l.MappingGuid == m.Guid));
            return all;
        }
        public static bool VerifyAny(this WireMockServer _this, string title = null)
        {
            var toCheck = _this.Mappings.Where(m => string.IsNullOrWhiteSpace(title) || m.Title == title);
            var all = toCheck.Any(m => _this.LogEntries.Any(l => l.MappingGuid == m.Guid));
            return all;
        }
    }
}
