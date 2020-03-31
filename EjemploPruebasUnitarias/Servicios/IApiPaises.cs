using EjemploPruebasUnitarias.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploPruebasUnitarias.Servicios
{
    /// <summary>
    /// Servicio de la API de paises
    /// </summary>
    public interface IApiPaises: IDisposable
    {
        /// <summary>
        /// Devuelve la lista de paises cuyo nombre contiene el valor especificado
        /// </summary>
        /// <param name="parteNombre"></param>
        /// <returns></returns>
        Task<IList<PaisDto>> BuscarPaisesPorNombreAsync(string parteNombre);

        /// <summary>
        /// Devuelve uno o más paises, especificado por su código de país de 3 caracteres
        /// </summary>
        /// <param name="codigosPais"></param>
        /// <returns></returns>
        Task<IList<PaisDto>> BuscarPaisesPorCodigoAsync(string[] codigosPais);

    }
}
