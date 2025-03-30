using System;
using System.Collections.Generic;

namespace CompiladorFinalGermanVeras
{
    public class Simbolo
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Ambito { get; set; }  // Global, Local, etc.
        public string Valor { get; set; }   // Si tiene un valor asignado

        public Simbolo(string nombre, string tipo, string ambito, string valor = null)
        {
            Nombre = nombre;
            Tipo = tipo;
            Ambito = ambito;
            Valor = valor;
        }

        public override string ToString()
        {
            return $"{Nombre} ({Tipo}, {Ambito}, Valor: {Valor ?? "N/A"})";
        }
    }

    public class TablaSimbolos
    {
        private Dictionary<string, Simbolo> tabla;

        public TablaSimbolos()
        {
            tabla = new Dictionary<string, Simbolo>();
        }

        public void Agregar(string nombre, string tipo, string ambito, string valor = null)
        {
            if (!tabla.ContainsKey(nombre))
            {
                tabla[nombre] = new Simbolo(nombre, tipo, ambito, valor);
            }
            else
            {
                // Si el símbolo ya existe, podríamos actualizar su valor o lanzar un error
                Console.WriteLine($"Advertencia: La variable '{nombre}' ya está declarada.");
            }
        }

        public bool Existe(string nombre)
        {
            return tabla.ContainsKey(nombre);
        }

        public Simbolo Obtener(string nombre)
        {
            return tabla.ContainsKey(nombre) ? tabla[nombre] : null;
        }

        public void ImprimirSimbolos()
        {
            Console.WriteLine("\nTabla de Símbolos:");
            foreach (var simbolo in tabla.Values)
            {
                Console.WriteLine(simbolo);
            }
        }


    }
}

