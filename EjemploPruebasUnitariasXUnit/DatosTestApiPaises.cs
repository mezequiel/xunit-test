using System.IO;

namespace EjemploPruebasUnitariasXUnit
        {public class DatosTestApiPaises
        {
                // Paises en formato json recuperados desde archivo
                public static readonly string JsonPaisesAmerica = System.IO.File.ReadAllText(Path.Combine("Files", nameof(DatosTestApiPaises) + ".PaisesAmerica.json"));
                public static readonly string JsonPaisesOtros = System.IO.File.ReadAllText(Path.Combine("Files", nameof(DatosTestApiPaises) + ".PaisesOtros.json"));
        }
}