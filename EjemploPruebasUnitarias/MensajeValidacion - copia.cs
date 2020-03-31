using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EjemploPruebasUnitarias
{
    /// <summary>
    /// Mensaje de validación para las respuestas con errores.
    /// </summary>
    public class MensajeValidacion
    {
        public string Mensaje { get; set; }
        public string Descripcion { get; set; }
        public object[] Detalles { get; set; }
    }
}
