using System;
using System.Collections.Generic;

namespace CompiladorFinalGermanVeras
{
    public abstract class Nodo
    {
        public abstract void Aceptar(IVisitorSemantico visitor);
    }

    // Interfaz para el Visitor Semántico
    public interface IVisitorSemantico
    {
        void Visitar(NodoUsing nodo);
        void Visitar(NodoIf nodo);
        void Visitar(NodoReturn nodo);
        void Visitar(NodoExpresion nodo);
        void Visitar(NodoDeclaracion nodo);
        void Visitar(NodoAsignacion nodo);
        void Visitar(NodoOperacion nodo);
        void Visitar(NodoBloque nodo);
        void Visitar(NodoWhile nodo);
        void Visitar(NodoFor nodo);
    }

    public class NodoFor : Nodo
    {
        public Nodo Inicializacion { get; set; }
        public NodoExpresion Condicion { get; set; }
        public Nodo Incremento { get; set; }
        public Nodo Cuerpo { get; set; }

        public override void Aceptar(IVisitorSemantico visitor)
        {
            visitor.Visitar(this);
        }
    }
    // Nodo para la sentencia "using"
    public class NodoUsing : Nodo
    {
        public string Namespace { get; set; }

        public NodoExpresion Expresion { get; set; }  // Agregar la propiedad Expresion

        public Nodo Sentencia { get; set; }
        public override void Aceptar(IVisitorSemantico visitor)
        {
            visitor.Visitar(this);
        }
    }

    public class NodoWhile : Nodo
    {
        public NodoExpresion Condicion { get; set; }
        public Nodo Cuerpo { get; set; }

        public override void Aceptar(IVisitorSemantico visitor)
        {
            // Si tienes un método específico para NodoWhile en el visitor, agrégalo aquí
            // visitor.Visitar(this);
        }
    }
    // Nodo para la sentencia "if"
    public class NodoIf : Nodo
    {
        public Nodo Condicion { get; set; }
        public Nodo SentenciaIf { get; set; }
        public Nodo SentenciaElse { get; set; } // Opcional
    
      
            public Nodo Cuerpo { get; set; }  // El cuerpo de la sentencia if
            public Nodo Sino { get; set; }   // El cuerpo del else (opcional)
    

        public override void Aceptar(IVisitorSemantico visitor)
        {
            visitor.Visitar(this);
        }
    }
    // Nodo para la sentencia "if" (con opción de "else")
 
    // Nodo para la sentencia "return"
    public class NodoReturn : Nodo
    {
        public NodoExpresion Expresion { get; set; }

        public override void Aceptar(IVisitorSemantico visitor)
        {
            visitor.Visitar(this);
        }
    }

    // Nodo para declaraciones de variables
    public class NodoDeclaracion : Nodo
    {
        internal NodoExpresion Expresion;

        public string Tipo { get; set; }
        public string Identificador { get; set; }

        public override void Aceptar(IVisitorSemantico visitor)
        {
            visitor.Visitar(this);
        }
    }

    // Nodo para asignaciones
    public class NodoAsignacion : Nodo
    {
        public string Identificador { get; set; }
        public NodoExpresion Expresion { get; set; }

        public override void Aceptar(IVisitorSemantico visitor)
        {
            visitor.Visitar(this);
        }
    }

    // Nodo para expresiones
    public class NodoExpresion : Nodo
    {
        public string Valor { get; set; } // Puede representar un identificador o literal
        public NodoExpresion Izquierda { get; set; }  // Propiedad para la parte izquierda de la expresión
        public NodoExpresion Derecha { get; set; }    // Propiedad para la parte derecha de la expresión
        public string Tipo { get; set; }  // Agregado para almacenar el tipo de la expresión

        public override void Aceptar(IVisitorSemantico visitor)
        {
            visitor.Visitar(this);
        }
    }


    // Nodo para operaciones (Ejemplo: x + y, a * b)
    public class NodoOperacion : NodoExpresion
    {
        public string Operador { get; set; }  // +, -, *, /
        public NodoExpresion Izquierda { get; set; }
        public NodoExpresion Derecha { get; set; }

        public override void Aceptar(IVisitorSemantico visitor)
        {
            visitor.Visitar(this);
        }
    }
    


    // Nodo para bloques de código (Lista de sentencias)
    public class NodoBloque : Nodo
    {
        public List<Nodo> Sentencias { get; set; } = new List<Nodo>();

        public override void Aceptar(IVisitorSemantico visitor)
        {
            visitor.Visitar(this);
        }
    }
}
