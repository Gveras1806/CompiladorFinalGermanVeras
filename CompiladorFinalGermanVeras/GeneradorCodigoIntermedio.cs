using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorFinalGermanVeras
{
    public class GeneradorCodigoIntermedio : IVisitorSemantico
    {
        // Lista que contendrá las instrucciones generadas
        public List<string> CodigoIntermedio { get; private set; }
        private int contadorTemp = 0;
        private int contadorEtiquetas = 0;

        public GeneradorCodigoIntermedio()
        {
            CodigoIntermedio = new List<string>();
        }

        // Genera un nombre para un nuevo temporal
        public string NuevoTemporal()
        {
            return "t" + (contadorTemp++);
        }

        // Genera un nombre para una nueva etiqueta
        public string NuevaEtiqueta()
        {
            return "L" + (contadorEtiquetas++);
        }

        public void Visitar(NodoDeclaracion nodo)
        {
            // Se genera una instrucción que representa la declaración
            CodigoIntermedio.Add($"DECLARE {nodo.Identificador} : {nodo.Tipo}");
        }

        public void Visitar(NodoAsignacion nodo)
        {
            // Se evalúa la expresión y se genera el código para la asignación
            string valor = EvaluarExpresion(nodo.Expresion);
            CodigoIntermedio.Add($"{nodo.Identificador} = {valor}");
        }

        public void Visitar(NodoExpresion nodo)
        {
            // En este ejemplo simple, la evaluación se realiza en EvaluarExpresion.
            // Este método se deja para completar la interfaz.
        }

        public void Visitar(NodoReturn nodo)
        {
            string valor = "";
            if (nodo.Expresion != null)
                valor = EvaluarExpresion(nodo.Expresion);
            CodigoIntermedio.Add($"RETURN {valor}");
        }

        public void Visitar(NodoIf nodo)
        {
            // Se asume que la condición es una expresión booleana que se evalúa en un temporal
            string tempCond = EvaluarExpresion(nodo.Condicion);
            string etiquetaElse = NuevaEtiqueta();
            string etiquetaFin = NuevaEtiqueta();

            CodigoIntermedio.Add($"IF_FALSE {tempCond} GOTO {etiquetaElse}");

            // Generar código para la parte del 'if'
            nodo.SentenciaIf.Aceptar(this);
            CodigoIntermedio.Add($"GOTO {etiquetaFin}");

            CodigoIntermedio.Add($"{etiquetaElse}:");
            if (nodo.SentenciaElse != null)
                nodo.SentenciaElse.Aceptar(this);
            CodigoIntermedio.Add($"{etiquetaFin}:");
        }

        public void Visitar(NodoUsing nodo)
        {
            // Se evalúa la expresión del using y se genera un bloque
            string valor = EvaluarExpresion(nodo.Expresion);
            CodigoIntermedio.Add($"USING {valor} BEGIN");
            nodo.Sentencia.Aceptar(this);
            CodigoIntermedio.Add("END_USING");
        }

        // Método auxiliar para evaluar una expresión simple
        private string EvaluarExpresion(Nodo nodo)
        {
            if (nodo is NodoExpresion exp)
            {
                // Si el valor representa un literal o un identificador, se devuelve tal cual.
                // En implementaciones más complejas, se generarían instrucciones y se devolvería un temporal.
                return exp.Valor;
            }
            // Para otros casos se podría extender el método.
            return "";
        }

        public void Visitar(NodoOperacion nodo)
        {

            string tempIzquierda = EvaluarExpresion(nodo.Izquierda);
            string tempDerecha = EvaluarExpresion(nodo.Derecha);
            string resultado = NuevoTemporal();

            // Generamos la instrucción para la operación
            string operador = nodo.Operador;
            CodigoIntermedio.Add($"{resultado} = {tempIzquierda} {operador} {tempDerecha}");

            // Asignamos el resultado de la operación al temporal
            nodo.Tipo = "int";  // Esto puede depender del tipo de los operandos
        }

        public void Visitar(NodoBloque nodo)
        {
            // Visitamos todas las sentencias dentro del bloque
            foreach (var sentencia in nodo.Sentencias)
            {
                sentencia.Aceptar(this); // Recursión: visita cada sentencia
            }
        }

        public void Visitar(NodoWhile nodo)
        {
            string etiquetaInicio = NuevaEtiqueta();
            string etiquetaFin = NuevaEtiqueta();

            CodigoIntermedio.Add($"{etiquetaInicio}:");

            // Evaluamos la condición
            string tempCond = EvaluarExpresion(nodo.Condicion);
            CodigoIntermedio.Add($"IF_FALSE {tempCond} GOTO {etiquetaFin}");

            // Evaluamos el cuerpo del ciclo
            nodo.Cuerpo.Aceptar(this);

            // Salto al inicio para repetir el ciclo
            CodigoIntermedio.Add($"GOTO {etiquetaInicio}");

            // Etiqueta de fin del ciclo
            CodigoIntermedio.Add($"{etiquetaFin}:");
        }

        public void Visitar(NodoFor nodo)
        {

            string etiquetaInicio = NuevaEtiqueta();
            string etiquetaFin = NuevaEtiqueta();

            // Evaluamos la inicialización
            nodo.Inicializacion.Aceptar(this);

            CodigoIntermedio.Add($"{etiquetaInicio}:");

            // Evaluamos la condición
            string tempCond = EvaluarExpresion(nodo.Condicion);
            CodigoIntermedio.Add($"IF_FALSE {tempCond} GOTO {etiquetaFin}");

            // Evaluamos el cuerpo del ciclo
            nodo.Cuerpo.Aceptar(this);

            // Evaluamos el incremento
            nodo.Incremento.Aceptar(this);

            // Salto al inicio para repetir el ciclo
            CodigoIntermedio.Add($"GOTO {etiquetaInicio}");

            // Etiqueta de fin del ciclo
            CodigoIntermedio.Add($"{etiquetaFin}:");
        }
    }
}
