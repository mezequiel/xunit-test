using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EjemploPruebasUnitarias.Dtos
{
    public class PaisDto
    {
        [JsonPropertyName("name")]
        public string Nombre { get; set; }
        [JsonPropertyName("callingCodes")]
        public string[] CodigoTelefonico { get; set; }

        [JsonPropertyName("alpha2Code")]
        public string Codigo2 { get; set; }
        [JsonPropertyName("alpha3Code")]
        public string Codigo3 { get; set; }
        [JsonPropertyName("region")]
        public string Region { get; set; }
        [JsonPropertyName("subregion")]
        public string Subregion { get; set; }

        [JsonPropertyName("population")] 
        public int Poblacion { get; set; }

        [JsonPropertyName("borders")]
        public string[] CodigoLimitrofes { get; set; }
    }
}
