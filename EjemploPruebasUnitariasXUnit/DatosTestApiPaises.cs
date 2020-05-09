using System.IO;

namespace EjemploPruebasUnitariasXUnit
        {public class DatosTestApiPaises
        {
                // Paises en formato json recuperados desde archivo
                public static readonly string JsonAmerica = System.IO.File.ReadAllText(Path.Combine("Files", nameof(DatosTestApiPaises) + ".America.json"));
                public static readonly string JsonOtrossNoAmerica = System.IO.File.ReadAllText(Path.Combine("Files", nameof(DatosTestApiPaises) + ".OtrosNoAmerica.json"));
                public static readonly string JsonArgentina = System.IO.File.ReadAllText(Path.Combine("Files", nameof(DatosTestApiPaises) + ".Argentina.json"));
    }
}