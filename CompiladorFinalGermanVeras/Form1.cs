using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CompiladorFinalGermanVeras
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private async void button2_Click(object sender, EventArgs e)
        {

            string codigoFuente = entrada.Text;

            // --- Análisis Léxico ---
            AnalizadorLexico analizadorLexico = new AnalizadorLexico(codigoFuente);
            List<Token> tokens = new List<Token>();
            Token token;
            do
            {
                token = analizadorLexico.ObtenerSiguienteToken();
                tokens.Add(token);
            } while (token.Tipo != TokenType.FinArchivo);

            // Limpiar y formatear la salida
            StringBuilder resultadoSalida = new StringBuilder();
            resultadoSalida.AppendLine("════════════════════════════════");
            resultadoSalida.AppendLine("         Análisis Léxico        ");
            resultadoSalida.AppendLine("════════════════════════════════");

            foreach (var item in tokens)
            {
                resultadoSalida.AppendLine($"→ {item}");
            }

            // --- Análisis Sintáctico ---
            AnalizadorSintactico parser = new AnalizadorSintactico(tokens);
            parser.Parse();

            // Formatear la salida de errores
            StringBuilder erroresSalida = new StringBuilder();
            erroresSalida.AppendLine("════════════════════════════════");
            erroresSalida.AppendLine("       Errores Sintácticos      ");
            erroresSalida.AppendLine("════════════════════════════════");

            if (parser.Errores.Count == 0)
            {
                erroresSalida.AppendLine("✅ No se encontraron errores sintácticos.");
            }
            else
            {
                foreach (var item in parser.Errores)
                {
                    erroresSalida.AppendLine($"❌ {item}");
                }
            }

            // --- Análisis Semántico ---
            AnalizadorSemantico analizadorSemantico = new AnalizadorSemantico();
            foreach (var nodo in parser.AST)
            {
                nodo.Aceptar(analizadorSemantico);
            }

            erroresSalida.AppendLine("════════════════════════════════");
            erroresSalida.AppendLine("       Errores Semánticos       ");
            erroresSalida.AppendLine("════════════════════════════════");

            if (analizadorSemantico.Errores.Count == 0)
            {
                erroresSalida.AppendLine("✅ No se encontraron errores semánticos.");
            }
            else
            {
                foreach (var item in analizadorSemantico.Errores)
                {
                    erroresSalida.AppendLine($"❌ {item}");
                }
            }

            // Imprimir resultados en los TextBox
            salida.Text = resultadoSalida.ToString();
            errores.Text = erroresSalida.ToString();




            // --- Generación de Código Intermedio ---
            GeneradorCodigoIntermedio generador = new GeneradorCodigoIntermedio();
            foreach (var nodo in parser.AST)
            {
                nodo.Aceptar(generador);
            }


            // --- Optimización del Código Intermedio ---
            OptimizadorCodigoIntermedio optimizador = new OptimizadorCodigoIntermedio();
            List<string> codigoOpt = optimizador.Optimizar(generador.CodigoIntermedio);

            //StringBuilder Codint = new StringBuilder();
            //Codint.AppendLine("═════════════════════════════════");
            //Codint.AppendLine("      Optimizacion de Código ");
            //Codint.AppendLine("═════════════════════════════════");
            //foreach (var item in codigoOpt)
            //{
            //    Codint.AppendLine($"→ {item}");
            //}

            GeneradorCodigoCpp generadorCpp = new GeneradorCodigoCpp();
            string codigoCpp = generadorCpp.GenerarCodigoCpp(codigoOpt);

            StringBuilder cin = new StringBuilder();
            cin.AppendLine("═════════════════════════════════");
            cin.AppendLine("           Código C++ ");
            cin.AppendLine("═════════════════════════════════");

            // **Corrección importante**: Dividir en líneas antes de iterar
            foreach (var item in codigoCpp.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None))
            {
                cin.AppendLine($"→   {item}");
            }
            string codigoCSharp = codigoFuente;
            EjecutarCodigo ejecutor = new EjecutarCodigo();
            string resultado = await ejecutor.EjecutarCodigoAsync(codigoCSharp);

            cin.AppendLine("═════════════════════════════════");
            cin.AppendLine("           Código C#  ");
            cin.AppendLine("═════════════════════════════════");

            // Validar si hay resultado
            if (!string.IsNullOrWhiteSpace(resultado))
            {
                foreach (var item in resultado.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None))
                {
                    cin.AppendLine($"→ {item}");
                }
            }
            else
            {
                cin.AppendLine("→ No hay resultado o no se realizó ninguna operación.");
            }

            //codinter.Text = Codint.ToString();
            sal.Text = cin.ToString();


            //codinter.Text = Codint.ToString();
            sal.Text = cin.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            entrada.Clear();
            errores.Clear();
            salida.Clear();
            sal.Clear();
        }
    }
}

