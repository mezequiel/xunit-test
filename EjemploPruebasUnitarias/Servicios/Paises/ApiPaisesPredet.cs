using EjemploPruebasUnitarias.Dtos;
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
        HttpClient _httpClient = new HttpClient();

        public async Task<IList<PaisDto>> BuscarPaisesPorNombreAsync(string parteNombre)
        {
            var jsonString = await _httpClient.GetStringAsync($"https://restcountries.eu/rest/v2/name/{parteNombre}");
            var paises = JsonSerializer.Deserialize<List<PaisDto>>(jsonString);

            return paises;
        }

        public async Task<IList<PaisDto>> BuscarPaisesPorCodigoAsync(string[] codigosPais)
        {
            var paramCodes = string.Join(";", codigosPais);
            var jsonString = await _httpClient.GetStringAsync($"https://restcountries.eu/rest/v2/alpha?codes={paramCodes}");
            var paises = JsonSerializer.Deserialize<List<PaisDto>>(jsonString);

            return paises;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
