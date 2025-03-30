using System;
using System.Collections.Generic;
using System.Linq;


namespace CompiladorFinalGermanVeras
{
    public class AnalizadorLexico
    {
        private string codigoFuente;
        private int indice;
        private int linea;
        private int columna;
        private TablaSimbolos tablaSimbolos;

        public AnalizadorLexico(string codigo)
        {
            codigoFuente = codigo;
            indice = 0;
            linea = 1;
            columna = 1;
            tablaSimbolos = new TablaSimbolos();
        }

        public Token ObtenerSiguienteToken()
        {
            // Saltar espacios y actualizar líneas/columnas
            while (indice < codigoFuente.Length && char.IsWhiteSpace(codigoFuente[indice]))
            {
                if (codigoFuente[indice] == '\n')
                {
                    linea++;
                    columna = 1;
                }
                else
                {
                    columna++;
                }
                indice++;
            }

            if (indice >= codigoFuente.Length)
                return new Token { Tipo = TokenType.FinArchivo, Valor = "EOF", Linea = linea, Columna = columna };

            char actual = codigoFuente[indice];

            // Manejo de identificadores y palabras reservadas
            if (char.IsLetter(actual) || actual == '_')
            {
                int inicio = indice;
                int colInicio = columna;
                while (indice < codigoFuente.Length && (char.IsLetterOrDigit(codigoFuente[indice]) || codigoFuente[indice] == '_'))
                {
                    indice++;
                    columna++;
                }
                string lexema = codigoFuente.Substring(inicio, indice - inicio);
                if (EsPalabraReservada(lexema))
                {
                    return new Token { Tipo = TokenType.PalabraReservada, Valor = lexema, Linea = linea, Columna = colInicio };
                }
                else
                {
                    if (!tablaSimbolos.Existe(lexema))
                                          tablaSimbolos.Agregar(lexema, "Identificador", "Global");


                    return new Token { Tipo = TokenType.Identificador, Valor = lexema, Linea = linea, Columna = colInicio };
                }
            }

            // Manejo de números (enteros y decimales)
            if (char.IsDigit(actual))
            {
                int inicio = indice;
                int colInicio = columna;
                bool esDecimal = false;
                while (indice < codigoFuente.Length && (char.IsDigit(codigoFuente[indice]) || codigoFuente[indice] == '.'))
                {
                    if (codigoFuente[indice] == '.')
                    {
                        if (esDecimal) break; // No puede haber más de un punto decimal
                        esDecimal = true;
                    }
                    indice++;
                    columna++;
                }
                string lexema = codigoFuente.Substring(inicio, indice - inicio);
                return new Token { Tipo = esDecimal ? TokenType.Decimal : TokenType.Numero, Valor = lexema, Linea = linea, Columna = colInicio };
            }

            // Manejo de literales de cadena
            if (actual == '"')
            {
                int colInicio = columna;
                indice++;
                columna++;
                int inicio = indice;
                while (indice < codigoFuente.Length && codigoFuente[indice] != '"')
                {
                    if (codigoFuente[indice] == '\\' && indice + 1 < codigoFuente.Length)
                    {
                        indice++;
                        columna++;
                    }
                    indice++;
                    columna++;
                }
                if (indice < codigoFuente.Length)
                {
                    string lexema = codigoFuente.Substring(inicio, indice - inicio);
                    indice++;
                    columna++;
                    return new Token { Tipo = TokenType.Cadena, Valor = lexema, Linea = linea, Columna = colInicio };
                }
                else
                {
                    return new Token { Tipo = TokenType.Error, Valor = "Cadena no cerrada", Linea = linea, Columna = colInicio };
                }
            }

            // Manejo de literales de caracteres
            if (actual == '\'')
            {
                int colInicio = columna;
                indice++;
                columna++;
                if (indice < codigoFuente.Length - 1 && codigoFuente[indice + 1] == '\'')
                {
                    string valor = codigoFuente[indice].ToString();
                    indice += 2;
                    columna += 2;
                    return new Token { Tipo = TokenType.Caracter, Valor = valor, Linea = linea, Columna = colInicio };
                }
                else
                {
                    return new Token { Tipo = TokenType.Error, Valor = "Carácter inválido", Linea = linea, Columna = colInicio };
                }
            }

            // Manejo de comentarios
            if (actual == '/' && indice + 1 < codigoFuente.Length)
            {
                if (codigoFuente[indice + 1] == '/') // Comentario de una línea
                {
                    while (indice < codigoFuente.Length && codigoFuente[indice] != '\n')
                    {
                        indice++;
                    }
                    return ObtenerSiguienteToken();
                }
                if (codigoFuente[indice + 1] == '*') // Comentario de varias líneas
                {
                    indice += 2;
                    columna += 2;
                    while (indice < codigoFuente.Length - 1 && !(codigoFuente[indice] == '*' && codigoFuente[indice + 1] == '/'))
                    {
                        if (codigoFuente[indice] == '\n')
                        {
                            linea++;
                            columna = 1;
                        }
                        else
                        {
                            columna++;
                        }
                        indice++;
                    }
                    if (indice < codigoFuente.Length - 1)
                    {
                        indice += 2;
                        columna += 2;
                    }
                    return ObtenerSiguienteToken();
                }
            }

            // Manejo de operadores relacionales y lógicos
            if ("=!<>|&+-*/".Contains(actual))
            {
                int colInicio = columna;
                string operador = actual.ToString();
                if (indice + 1 < codigoFuente.Length)
                {
                    string posibleDoble = operador + codigoFuente[indice + 1];
                    if (new[] { "==", "!=", "<=", ">=", "&&", "||" }.Contains(posibleDoble))
                    {
                        operador = posibleDoble;
                        indice++;
                        columna++;
                    }
                }
                indice++;
                columna++;
                return new Token { Tipo = TokenType.Operador, Valor = operador, Linea = linea, Columna = colInicio };
            }
            // Manejo de delimitadores y otros símbolos
            int colToken = columna;
            switch (actual)
            {
                case '(':
                case ')':
                case '{':
                case '}':
                case '[':
                case ']':
                case ';':
                case '.':
                case ',':
                case ':':
                    indice++;
                    columna++;
                    return new Token { Tipo = TokenType.Delimitador, Valor = actual.ToString(), Linea = linea, Columna = colToken };
            }

            // Si el carácter no es reconocido
            indice++;
            columna++;
            return new Token { Tipo = TokenType.Error, Valor = actual.ToString(), Linea = linea, Columna = colToken };
        }

        private bool EsPalabraReservada(string lexema)
        {
            string[] reservadas = { "if", "else", "while", "for", "class", "public", "private",
                                    "void", "int", "string", "decimal", "DateTime", "return",
                                    "break", "continue", "switch", "case", "default", "using"};
            return Array.Exists(reservadas, palabra => palabra.Equals(lexema, StringComparison.OrdinalIgnoreCase));
        }
    }
}
