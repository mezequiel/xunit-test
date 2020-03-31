using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EjemploPruebasUnitarias.Dtos;
using EjemploPruebasUnitarias.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EjemploPruebasUnitarias.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaisController : ControllerBase
    {
        private readonly ILogger<PaisController> _logger;
        private readonly IApiPaises _api;

        public PaisController(IApiPaises api,  ILogger<PaisController> logger)
        {
            _logger = logger;
            _api = api;
        }


        /// <summary>
        /// Crea una respuesta "Bad Request" con los datos de validación especificados
        /// </summary>
        BadRequestObjectResult BadRequest(string mensaje, string descripcion = null, params object[] detalles)
        {
            var validacion = new MensajeValidacion
            {
                Mensaje = mensaje,
                Descripcion = descripcion,
                Detalles = detalles.Length != 0 ? detalles : null
            };

            return BadRequest(validacion);
        }

        /// <summary>
        /// Crea una respuesta con el código HTTP especificado, y con los datos de validación especificados
        /// </summary>
        /// <returns></returns>
        ObjectResult StatusCode(int statusCode, string mensaje, string descripcion = null, params object[] detalles)
        {
            var validacion = new MensajeValidacion
            {
                Mensaje = mensaje,
                Descripcion = descripcion,
                Detalles = detalles.Length != 0 ? detalles : null
            };

            return StatusCode(statusCode, validacion);
        }

        /// <summary>
        /// Devuelve la información de los paises buscando por parte del nombre
        /// </summary>
        /// <param name="parteNombre">Parte del nombre a buscar (obligatorio)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("nombre/{parteNombre}")]
        public async Task<ActionResult<IEnumerable<PaisDto>>> GetPorNombreAsync(string parteNombre)
        {
            if (string.IsNullOrWhiteSpace(parteNombre)) 
            {
                return BadRequest("P01", "Debe especificar nombre a buscar", nameof(parteNombre));
            }

            try
            {
                var rdo = await _api.BuscarPaisesPorNombreAsync(parteNombre);
                return new ActionResult<IEnumerable<PaisDto>>(rdo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando api");
                return StatusCode((int) HttpStatusCode.InternalServerError, "E01", "Error inesperado");
            }
        }

        /// <summary>
        /// Devuelve los paises limítrofes del pais especificado a pertir del código de 3 caracteres (ej: "ARG").
        /// Los paises se ordenan descendentemente por población.
        /// </summary>
        /// <param name="codPais3">País a buscar sus limítrofes (código de 3 carateres) - Obligatorio</param>
        /// <returns></returns>
        [Route("limitrofes/{codPais3}")]
        public async Task<ActionResult<IEnumerable<PaisDto>>> GetLimitrofesPaisAsync(string codPais3)
        {
            if (string.IsNullOrWhiteSpace(codPais3))
            {
                return BadRequest("P02", "Debe especificar un código de país.", nameof(codPais3));
            }

            if (codPais3.Trim().Length != 3)
            {
                return BadRequest("P03", "El código de pais debe tener 3 caracteres.", nameof(codPais3));
            }

            try
            {
                var pais = (await _api.BuscarPaisesPorCodigoAsync(new[] { codPais3.Trim().ToUpper() })).FirstOrDefault();
                if (pais == null)
                {
                    return StatusCode( (int) HttpStatusCode.NotFound, "P04", "El codigo de pais no se encontró en la base de datos.", codPais3);
                }

                if (pais.CodigoLimitrofes?.Length == 0)
                {
                    return new ActionResult<IEnumerable<PaisDto>>(new PaisDto [0]);
                }

                var limitrofes = await _api.BuscarPaisesPorCodigoAsync(pais.CodigoLimitrofes);
                var ordenado = limitrofes.OrderByDescending(p => p.Poblacion);

                return new ActionResult<IEnumerable<PaisDto>>(ordenado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando api");
                return StatusCode((int)HttpStatusCode.InternalServerError, "E01", "Error inesperado");
            }
        }
    }
}
