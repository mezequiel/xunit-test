using EjemploPruebasUnitarias.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace EjemploPruebasUnitariasXUnit
{
    public static class SpeflowToPaisExtensions
    {
        public static PaisDto[] TableToPaisesDto(this Table _this, string colNombreONull = "Nombre", string colPoblacionONull = "Poblacion", string colCod2ONull = "Cod2", string colCod3ONull = "Cod3", string colLimitrofesONull = "Limítrofes")
        {
            var paises = _this.Rows.Select(r =>
               new PaisDto()
               {
                   Nombre = string.IsNullOrWhiteSpace(colNombreONull) ? null : r[colNombreONull].Trim(),
                   Poblacion = string.IsNullOrWhiteSpace(colPoblacionONull) ? -1 : int.Parse(r[colPoblacionONull]),
                   Codigo2 = string.IsNullOrWhiteSpace(colCod2ONull) ? null : r[colCod2ONull].Trim(),
                   Codigo3 = string.IsNullOrWhiteSpace(colCod3ONull) ? null : r[colCod3ONull].Trim(),
                   CodigoLimitrofes = string.IsNullOrWhiteSpace(colLimitrofesONull) ? new string [0] : r[colLimitrofesONull].Split(',', StringSplitOptions.RemoveEmptyEntries),
               }
           ).ToArray();
            return paises;
        }
    }
}
