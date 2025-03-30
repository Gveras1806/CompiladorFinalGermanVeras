using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorFinalGermanVeras
{
    // Clase Token
    public class Token
    {
        public TokenType Tipo { get; set; }
        public string Valor { get; set; }
        public int Linea { get; set; }
        public int Columna { get; set; }

        public override string ToString()
        {
            return $"{Tipo}: {Valor} (Línea: {Linea}, Columna: {Columna})";
        }
    }
}
