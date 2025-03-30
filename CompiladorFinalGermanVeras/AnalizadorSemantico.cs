using System;
using System.Collections.Generic;

namespace CompiladorFinalGermanVeras
{
    public class AnalizadorSemantico : IVisitorSemantico
    {
        // Tabla de símbolos: clave es el nombre de la variable, valor es el tipo declarado.
        private Dictionary<string, string> tablaSimbolos;
        public List<string> Errores { get; private set; }

        public AnalizadorSemantico()
        {
            tablaSimbolos = new Dictionary<string, string>();
            Errores = new List<string>();
        }

        public void Analizar(Nodo nodo)
        {
            nodo.Aceptar(this);
        }

        // Visita para nodo de declaración
        public void Visitar(NodoDeclaracion nodo)
        {
            if (!tablaSimbolos.ContainsKey(nodo.Identificador))
            {
                tablaSimbolos.Add(nodo.Identificador, nodo.Tipo);
            }
            else
            {
                Errores.Add($"Error semántico: La variable '{nodo.Identificador}' ya ha sido declarada.");
            }
        }

        // Visita para nodo de asignación
        public void Visitar(NodoAsignacion nodo)
        {
            if (!tablaSimbolos.ContainsKey(nodo.Identificador))
            {
                Errores.Add($"Error semántico: La variable '{nodo.Identificador}' no ha sido declarada.");
                return;
            }

            string tipoDeclarado = tablaSimbolos[nodo.Identificador];
            string tipoExpresion = EvaluarTipo(nodo.Expresion);

            if (tipoDeclarado != tipoExpresion)
            {
                Errores.Add($"Error semántico: Tipo incompatible en la asignación de '{nodo.Identificador}'. Se esperaba '{tipoDeclarado}', pero se obtuvo '{tipoExpresion}'.");
            }
        }

        // Visita para nodo "using"
        public void Visitar(NodoUsing nodo)
        {
            if (nodo.Expresion != null)
                nodo.Expresion.Aceptar(this);
            if (nodo.Sentencia != null)
                nodo.Sentencia.Aceptar(this);
        }

        // Visita para nodo "if"
        public void Visitar(NodoIf nodo)
        {
            if (nodo.Condicion != null)
                nodo.Condicion.Aceptar(this);
            if (nodo.SentenciaIf != null)
                nodo.SentenciaIf.Aceptar(this);
            if (nodo.SentenciaElse != null)
                nodo.SentenciaElse.Aceptar(this);
        }

        // Visita para nodo "return"
        public void Visitar(NodoReturn nodo)
        {
            if (nodo.Expresion != null)
                nodo.Expresion.Aceptar(this);
        }

        // Visita para nodo "expresión"
        public void Visitar(NodoExpresion nodo)
        {
            // Este método ahora solo evalúa el tipo de los literales
            if (!string.IsNullOrEmpty(nodo.Valor))
            {
                // Si la cadena comienza y termina con comillas, es un string.
                if (nodo.Valor.StartsWith("\"") && nodo.Valor.EndsWith("\""))
                {
                    nodo.Tipo = "string";
                }
                // Intentar parsear a entero
                else if (int.TryParse(nodo.Valor, out _))
                {
                    nodo.Tipo = "int";
                }
                // Aquí puedes agregar más tipos (decimal, bool, etc.)
            }
        }

        // Método auxiliar para determinar el tipo de una expresión (soporta operaciones)
        private string EvaluarTipo(Nodo nodo)
        {
            if (nodo is NodoExpresion exp)
            {
                // Evaluar tipo de literales
                if (!string.IsNullOrEmpty(exp.Valor))
                {
                    // Si es un literal string
                    if (exp.Valor.StartsWith("\"") && exp.Valor.EndsWith("\""))
                        return "string";
                    // Si es un literal entero
                    else if (int.TryParse(exp.Valor, out _))
                        return "int";
                }

                // Si la expresión es una operación, evaluamos los operandos
                if (exp is NodoOperacion operacion)
                {
                    string tipoIzquierda = EvaluarTipo(operacion.Izquierda);
                    string tipoDerecha = EvaluarTipo(operacion.Derecha);

                    // Verificamos si los tipos son compatibles con el operador
                    if (tipoIzquierda == tipoDerecha)
                    {
                        return tipoIzquierda;  // Si ambos operandos son del mismo tipo, se devuelve ese tipo
                    }
                    else
                    {
                        // Aquí podrías agregar más validaciones de compatibilidad de tipos según tu lenguaje
                        Errores.Add($"Error semántico: Los operandos de la operación no son compatibles. '{tipoIzquierda}' y '{tipoDerecha}'.");
                    }
                }
            }

            return "Desconocido";  // Si no se puede determinar el tipo
        }

        public void Visitar(NodoOperacion nodo)
        {
            throw new NotImplementedException();
        }

        public void Visitar(NodoBloque nodo)
        {
            throw new NotImplementedException();
        }
    }
}
