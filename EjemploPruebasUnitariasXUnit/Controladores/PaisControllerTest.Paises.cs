using EjemploPruebasUnitarias.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EjemploPruebasUnitariasXUnit.Controladores
{
    partial class PaisControllerTest
    {
        static IDictionary<string, PaisDto> _paisesDePrueba = new Dictionary<string, PaisDto>  {
            {  "ARG",
                new PaisDto() {
                    Nombre = "Argentina",
                    Codigo2 = "AR",
                    Codigo3 = "ARG",
                    Region = "Americas",
                    Subregion = "South America",
                    Poblacion = 40000000,
                    CodigoTelefonico = new string[] { "54"},
                    CodigoLimitrofes = new string[] { "BRA", "URY", "BOL", "CHL"}
                } },
                {  "BOL", new PaisDto() {
                    Nombre = "Bolivia",
                    Codigo2 = "BO",
                    Codigo3 = "BOL",
                    Region = "Americas",
                    Subregion = "South America",
                    Poblacion = 11000000,
                    CodigoTelefonico = new string[] { "591"},
                    CodigoLimitrofes = new string[] { "PER", "ARG", "CHI"}
                } },
                {  "BRA", new PaisDto() {
                    Nombre = "Brazil",
                    Codigo2 = "BR",
                    Codigo3 = "BRA",
                    Region = "Americas",
                    Subregion = "South America",
                    Poblacion = 206000000,
                    CodigoTelefonico = new string[] { "55"},
                    CodigoLimitrofes = new string[] { "ARG", "URY"}
                } },
                {  "URY", new PaisDto() {
                    Nombre = "Uruguay",
                    Codigo2 = "UY",
                    Codigo3 = "URY",
                    Region = "Americas",
                    Subregion = "South America",
                    Poblacion = 3000000,
                    CodigoTelefonico = new string[] { "598"},
                    CodigoLimitrofes = new string[] { "BRA", "ARG"}
                }},
                {  "CHL", new PaisDto() {
                    Nombre = "Chile",
                    Codigo2 = "CL",
                    Codigo3 = "CHL",
                    Region = "Americas",
                    Subregion = "South America",
                    Poblacion = 18000000,
                    CodigoTelefonico = new string[] { "56"},
                    CodigoLimitrofes = new string[] { "BRA", "URY", "BOL"}
                }},
                {  "PER", new PaisDto() {
                    Nombre = "Peru",
                    Codigo2 = "PE",
                    Codigo3 = "PER",
                    Region = "Americas",
                    Subregion = "South America",
                    Poblacion = 31000000,
                    CodigoTelefonico = new string[] { "51"},
                    CodigoLimitrofes = new string[] { "CHI", "BOL"}
                }},
                // Sin limítrofes
                {  "NZL", new PaisDto() {
                    Nombre = "New Zealand",
                    Codigo2 = "NZ",
                    Codigo3 = "NZL",
                    Region = "Oceania",
                    Subregion = "Australia and New Zealand",
                    Poblacion = 4000000,
                    CodigoTelefonico = new string[] { "64"},
                    CodigoLimitrofes = new string[] { }
                }},
            };

        static IList<PaisDto> GetPaisesTest()
        {
            return _paisesDePrueba.Values.ToList();
        }
    }
}
