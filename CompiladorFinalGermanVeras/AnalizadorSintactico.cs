using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorFinalGermanVeras
{
    public class AnalizadorSintactico
    {
       
            private List<Token> tokens;
            private int currentIndex;
            private Dictionary<string, string> variables;  // Diccionario para almacenar las variables declaradas

            // Lista para almacenar los errores sintácticos
            public List<string> Errores { get; private set; }

            // Lista que contendrá los nodos del AST generados dinámicamente
            public List<Nodo> AST { get; private set; }

            public AnalizadorSintactico(List<Token> tokens)
            {
                this.tokens = tokens;
                this.currentIndex = 0;
                this.variables = new Dictionary<string, string>(); // Inicializa el diccionario de variables
                Errores = new List<string>();
                AST = new List<Nodo>();
            }

            // Método principal de análisis sintáctico que genera el AST
            public void Parse()
            {
            while (currentIndex < tokens.Count && tokens[currentIndex].Tipo != TokenType.FinArchivo)
            {
                try
                {
                    Nodo nodo = ParseStatement();
                    if (nodo != null)
                        AST.Add(nodo);
                }
                catch (Exception ex)
                {
                    Errores.Add("Error sintáctico: " + ex.Message);
                    break;
                }
            }
        }

        private NodoIf ParseIf()
        {
            Consume(TokenType.PalabraReservada, "if");
            Consume(TokenType.Delimitador, "(");
            NodoExpresion condicion = (NodoExpresion)ParseExpresion();  // Asegurándote de que sea un NodoExpresion
            Consume(TokenType.Delimitador, ")");
            Nodo sentenciaIf = ParseSentencia();
            Nodo sentenciaElse = null;

            if (Match(TokenType.PalabraReservada, "else"))
            {
                Advance();
                sentenciaElse = ParseSentencia();
            }

            return new NodoIf
            {
                Condicion = condicion,
                SentenciaIf = sentenciaIf,
                SentenciaElse = sentenciaElse
            };
        }
        // Método para analizar una sentencia
        private Nodo ParseSentencia()
        {
            if (Match(TokenType.PalabraReservada, "if"))
            {
                return ParseIf();
            }
            else if (Match(TokenType.PalabraReservada, "return"))
            {
                return ParseReturn();
            }

            // Añadir más casos según las sentencias que tu compilador necesite soportar (while, for, etc.)
            throw new Exception("Sentencia no reconocida.");
        }

        public Nodo ParseReturn()
        {
            // Consume el "return"
            Consume(TokenType.PalabraReservada, "return");

            // Analiza la expresión que sigue al "return"
            NodoExpresion expresion = ParseExpresion();

            // Crea y retorna un NodoReturn con la expresión analizada
            return new NodoReturn { Expresion = expresion };
        }

        // Determina qué tipo de sentencia se está analizando y retorna el nodo correspondiente
        private Nodo ParseStatement()
            {
                // Si el token actual es una palabra reservada que indica un tipo, asumimos que es una declaración
                if (IsTipoDeclaracion())
                {
                    return ParseDeclaracion();
                }
                // Si el token actual es un identificador y el siguiente es "=", es una asignación
                if (Match(TokenType.Identificador) && LookAhead(1)?.Valor == "=")
                {
                    return ParseAsignacion();
                }
                // Otras sentencias se pueden agregar aquí
                throw new Exception("Sentencia no reconocida en el contexto actual.");
            }

            // Verifica si el token actual es un tipo de dato (por ejemplo, "int", "string", etc.)
            private bool IsTipoDeclaracion()
            {
                Token token = tokens[currentIndex];
                return token.Tipo == TokenType.PalabraReservada && (token.Valor == "int" || token.Valor == "string" || token.Valor == "decimal" || token.Valor == "DateTime");
            }

            // Analiza una declaración de variable: por ejemplo, "int x;"
            private NodoDeclaracion ParseDeclaracion()
            {
                // Se asume que el primer token es el tipo
                string tipo = tokens[currentIndex].Valor;
                Consume(TokenType.PalabraReservada); // consume el tipo
                                                     // Se espera un identificador
                string identificador = tokens[currentIndex].Valor;
                Consume(TokenType.Identificador);

                // Almacena la variable en el diccionario
                variables[identificador] = tipo;

                // Se espera el delimitador ";" al final
                Consume(TokenType.Delimitador, ";");

                return new NodoDeclaracion { Tipo = tipo, Identificador = identificador };
            }

            // Analiza una asignación: por ejemplo, "x = 5;"
            private NodoAsignacion ParseAsignacion()
            {
                string identificador = tokens[currentIndex].Valor;
                Consume(TokenType.Identificador);  // Consume el identificador (x)
                Consume(TokenType.Operador, "=");  // Consume el operador "="

                // Aquí analizamos la expresión del lado derecho de la asignación
                NodoExpresion expresionDerecha = ParseExpresion();

                // Almacena la asignación
                Consume(TokenType.Delimitador, ";");

                return new NodoAsignacion { Identificador = identificador, Expresion = expresionDerecha };
            }

        // Analiza una expresión con operadores, como 0 + 1 o 2 * 3
        // Método ParseExpresion
        private NodoExpresion ParseExpresion()
        {
            NodoExpresion izquierda = ParseTermino();  // Asegurándonos que este es NodoExpresion

            // Procesar operadores '+' o '-'
            while (Match(TokenType.Operador, "+") || Match(TokenType.Operador, "-"))
            {
                string operador = tokens[currentIndex].Valor;
                Advance();
                NodoExpresion derecha = ParseTermino();
                izquierda = new NodoOperacion
                {
                    Operador = operador,
                    Izquierda = izquierda,
                    Derecha = derecha
                };
            }

            return izquierda;  // Retorna el NodoExpresion
        }

        // Método ParseTermino
        private NodoExpresion ParseTermino()
        {
            // Llamamos a ParseFactor, que probablemente analiza los números o identificadores
            NodoExpresion izquierdo = ParseFactor();

            // Si el token actual es un operador de multiplicación (*) o división (/), seguimos procesando
            while (Match(TokenType.Operador, "*") || Match(TokenType.Operador, "/"))
            {
                string operador = tokens[currentIndex].Valor;
                Advance();
                NodoExpresion derecho = ParseFactor();  // Continuamos con el siguiente factor

                // Creamos una operación de multiplicación o división
                izquierdo = new NodoOperacion
                {
                    Operador = operador,
                    Izquierda = izquierdo,
                    Derecha = derecho
                };
            }

            return izquierdo;  // Retorna el NodoExpresion (puede ser una operación o factor simple)
        }
        // Método ParseFactor
        private NodoExpresion ParseFactor()
        {
            Token token = tokens[currentIndex];

            // Si el token es un número o identificador, lo tomamos como parte de la expresión
            // Si el token es un número o identificador, lo tomamos como parte de la expresión
            if (token.Tipo == TokenType.Numero || token.Tipo == TokenType.Identificador)
            {
                Advance();
                return new NodoExpresion { Valor = token.Valor };  // Debería ser NodoExpresion, no Nodo
            }

            // Si encontramos un paréntesis, analizamos la expresión dentro de los paréntesis
            if (Match(TokenType.Delimitador, "("))
            {
                Advance();
                NodoExpresion expresion = ParseExpresion();
                if (!Match(TokenType.Delimitador, ")"))
                {
                    Errores.Add($"Error: Falta cerrar paréntesis en la línea {token.Linea}");
                }
                else
                {
                    Advance();
                }
                return expresion;  // Retornamos el NodoExpresion dentro de los paréntesis
            }

            // Si no encontramos un número, identificador ni paréntesis, lanzamos un error
            Errores.Add($"Error sintáctico: Se esperaba número, identificador o paréntesis, pero se encontró '{token.Valor}' en la línea {token.Linea}");
            Advance();  // Avanzamos para evitar bucles infinitos
            return null;
        }




        // Métodos auxiliares de consumo y avance de tokens
        private Token Consume(TokenType type, string value = null)
            {
                if (Match(type, value))
                {
                    return Advance();
                }
                else
                {
                    throw new Exception($"Se esperaba {type} '{value}' pero se encontró '{tokens[currentIndex].Valor}' en la línea {tokens[currentIndex].Linea}, columna {tokens[currentIndex].Columna}.");
                }
            }

            private bool Match(TokenType type, string value = null)
            {
                Token current = tokens[currentIndex];
                return current.Tipo == type && (value == null || current.Valor == value);
            }
        private NodoIf ParseIfStatement()
        {
            Consume(TokenType.PalabraReservada, "if");
            Consume(TokenType.Delimitador, "(");
            NodoExpresion condicion = ParseExpresion();
            Consume(TokenType.Delimitador, ")");

            Nodo cuerpo = ParseStatement();
            NodoIf nodoIf = new NodoIf { Condicion = condicion, Cuerpo = cuerpo };

            if (Match(TokenType.PalabraReservada, "else"))
            {
                Advance();
                nodoIf.Sino = ParseStatement();
            }

            return nodoIf;
        }

        private Token Advance()
            {
                return tokens[currentIndex++];
            }

            private Token LookAhead(int offset)
            {
                int index = currentIndex + offset;
                return index < tokens.Count ? tokens[index] : null;
            }
        }

    }
