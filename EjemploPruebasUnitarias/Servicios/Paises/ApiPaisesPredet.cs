using EjemploPruebasUnitarias.Dtos;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace EjemploPruebasUnitarias.Servicios.Paises
{
    /// <summary>
    /// Implementación del servicio "IApiPaises" basada en HttpClient
    /// </summary>
    public class ApiPaisesPredet: IApiPaises
    {

        private IHttpClientFactory _httpFactory;

        public ApiPaisesPredet(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        public async Task<IList<PaisDto>> BuscarPaisesPorNombreAsync(string parteNombre)
        {
            using (HttpClient httpclient = _httpFactory.CreateClient())
            using (HttpResponseMessage response = await httpclient.GetAsync($"https://restcountries.eu/rest/v2/name/{parteNombre}"))
            {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var paises = JsonSerializer.Deserialize<List<PaisDto>>(jsonString);
                    return paises;
            }
        }

        public async Task<IList<PaisDto>> BuscarPaisesPorCodigoAsync(string[] codigosPais)
        {
            var paramCodes = string.Join(";", codigosPais);

            using (HttpClient httpclient = _httpFactory.CreateClient())
            using (HttpResponseMessage response = await httpclient.GetAsync($"https://restcountries.eu/rest/v2/alpha?codes={paramCodes}"))
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var paises = JsonSerializer.Deserialize<List<PaisDto>>(jsonString).Where(x => x != null).ToList();
                return paises;
            }
        }

        public void Dispose()
        {
            _httpFactory = null;
        }
    }
}
