using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.CodeAnalysis.Formatting;
using Formatter = Microsoft.CodeAnalysis.Formatting.Formatter;



namespace CompiladorFinalGermanVeras
{
    public partial class Form1 : Form
    {
        //Para los archivos

        private string archivoActual = string.Empty;
        private string proyectoActual = string.Empty;
        private Dictionary<string, string> archivosProyecto = new Dictionary<string, string> ();

        public Form1()
        {
            InitializeComponent();
        }



        private void nuevoProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    proyectoActual = dialog.SelectedPath;
                    archivosProyecto.Clear();
                    ActualizarArbolDeProyecto();
                }
            }
        }

        private void abrirProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Archivos CSharp (*.csharp)|*.csharp|Todos los archivos (*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    proyectoActual = Path.GetDirectoryName(dialog.FileName);
                    archivosProyecto.Clear();

                    var archivos = Directory.GetFiles(proyectoActual, "*.csharp", SearchOption.AllDirectories);
                    foreach (var archivo in archivos)
                    {
                        archivosProyecto[Path.GetFileName(archivo)] = archivo;
                    }

                    ActualizarArbolDeProyecto();
                }
            }
        }

        private void ActualizarArbolDeProyecto()
        {
            projectTree.Nodes.Clear();

            foreach (var archivo in archivosProyecto)
            {
                TreeNode node = new TreeNode(archivo.Key) { Tag = archivo.Value };
                projectTree.Nodes.Add(node);
            }

            projectTree.AfterSelect += (s, e) =>
            {
                if (e.Node?.Tag is string rutaArchivo)
                {
                    archivoActual = rutaArchivo;
                    entrada.Text = File.ReadAllText(rutaArchivo);
                }
            };
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(archivoActual))
                guardarComoToolStripMenuItem_Click(sender, e);
            else
                File.WriteAllText(archivoActual, entrada.Text);
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Archivos CSharp (*.csharp)|*.csharp|Todos los archivos (*.*)|*.*";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    archivoActual = saveDialog.FileName;
                    File.WriteAllText(archivoActual, entrada.Text);

                    if (!archivosProyecto.ContainsKey(Path.GetFileName(archivoActual)))
                    {
                        archivosProyecto[Path.GetFileName(archivoActual)] = archivoActual;
                        ActualizarArbolDeProyecto();
                    }
                }
            }
        }





        public ObservableCollection<string> SymbolTable { get; set; } = new ObservableCollection<string>();



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


            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(entrada.Text);
            CompilationUnitSyntax root = syntaxTree.GetCompilationUnitRoot();

            // --- Generación de Código Intermedio ---
            GeneradorCodigoIntermedio generador = new GeneradorCodigoIntermedio();
            foreach (var nodo in parser.AST)
            {
                nodo.Aceptar(generador);
            }

            GenerateSymbolTable(root);
            tablaSim.Text = string.Empty;
            foreach (var x in SymbolTable)
            {
                tablaSim.Text += x.ToString() + Environment.NewLine;
            }

            // Generar Código Intermedio
            GenerateIntermediateCode(root);

            // Traducción del código C# a C++
            string codigoCpp = TranslateToCpp(root);

            // Asignamos el código C++ traducido al texto de la interfaz (sal)
            sal.Text = codigoCpp;  // Muestra el código C++ traducido en la interfaz

            // Crear un StringBuilder para almacenar la salida
            StringBuilder cin = new StringBuilder();

            // Añadir encabezado al StringBuilder
            cin.AppendLine("═════════════════════════════════");
            cin.AppendLine("           Código C++  ");
            cin.AppendLine("═════════════════════════════════");

            // Agregar el código C++ traducido al StringBuilder
            cin.AppendLine(codigoCpp); // Aquí se añade el código C++ al StringBuilder

            // Ejecutar el código C# en un entorno de ejecución si es necesario
            string codigoCSharp = codigoFuente;
            EjecutarCodigo ejecutor = new EjecutarCodigo();
            string resultado = await ejecutor.EjecutarCodigoAsync(codigoCSharp);

            // Agregar el resultado de la ejecución del código C# si existe
            cin.AppendLine("═════════════════════════════════");
            cin.AppendLine("           Resultado C#  ");
            cin.AppendLine("═════════════════════════════════");

            // Validar si hay resultado de la ejecución
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

            // Mostrar el contenido del StringBuilder (C++ + Resultado C#) en el control 'sal'
            sal.Text = cin.ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            entrada.Clear();
            errores.Clear();
            salida.Clear();
            codint.Clear();
            sal.Clear();
        }



        private Dictionary<string, int> variableValues = new Dictionary<string, int>();
        private int EvaluateExpression(ExpressionSyntax expr, Dictionary<string, int> variables)
        {
            if (expr is LiteralExpressionSyntax literal)
            {
                // Se asume que el literal es entero.
                return int.Parse(literal.Token.ValueText);
            }
            else if (expr is IdentifierNameSyntax identifier)
            {
                string name = identifier.Identifier.Text;
                if (variables.TryGetValue(name, out int value))
                    return value;
                else
                    throw new Exception($"Variable '{name}' no definida.");
            }
            else if (expr is BinaryExpressionSyntax binaryExpr)
            {
                int left = EvaluateExpression(binaryExpr.Left, variables);
                int right = EvaluateExpression(binaryExpr.Right, variables);
                switch (binaryExpr.OperatorToken.Text)
                {
                    case "+": return left + right;
                    case "-": return left - right;
                    case "*": return left * right;
                    case "/": return right != 0 ? left / right : throw new DivideByZeroException();
                    default: throw new Exception($"Operador '{binaryExpr.OperatorToken.Text}' no soportado.");
                }
            }
            else
            {
                throw new Exception("Expresión no soportada para evaluación.");
            }
        }

        private void GenerateSymbolTable(CompilationUnitSyntax root)
        {
            // Limpia la tabla de símbolos y el diccionario de variables.
            tablaSim.Clear();
            variableValues.Clear();

            // Procesar declaraciones locales (por ejemplo, "int x = 0;")
            foreach (var localDecl in root.DescendantNodes().OfType<LocalDeclarationStatementSyntax>())
            {
                string tipo = localDecl.Declaration.Type.ToString().Trim();
                foreach (var variable in localDecl.Declaration.Variables)
                {
                    string nombre = variable.Identifier.Text;
                    int valor = 0;
                    if (variable.Initializer != null)
                    {
                        try
                        {
                            valor = EvaluateExpression(variable.Initializer.Value, variableValues);
                        }
                        catch (Exception ex)
                        {
                            // Si la expresión no es evaluable, se deja un valor por defecto o se registra el error.
                            valor = 0;
                        }
                    }
                    // Almacena el valor inicial en el diccionario.
                    variableValues[nombre] = valor;
                    SymbolTable.Add($"Nombre: {nombre}, Tipo: {tipo}, Valor: {valor}");
                }
            }

            // Procesar asignaciones (por ejemplo, "x = x + 5;")
            foreach (var assignExpr in root.DescendantNodes().OfType<AssignmentExpressionSyntax>())
            {
                // Se asume que el lado izquierdo es un identificador.
                string nombre = assignExpr.Left.ToString().Trim();
                try
                {
                    int valor = EvaluateExpression(assignExpr.Right, variableValues);
                    // Actualiza el valor en el diccionario.
                    variableValues[nombre] = valor;
                    // Actualiza la entrada en la tabla de símbolos (si existe).
                    for (int i = 0; i < SymbolTable.Count; i++)
                    {
                        if (SymbolTable[i].Contains($"Nombre: {nombre},"))
                        {
                            // Extraer el tipo de la cadena existente (o mantenerlo) y actualizar el valor.
                            // Para simplificar, se reconstruye la entrada.
                            string tipo = SymbolTable[i].Split(',')[1].Split(':')[1].Trim();
                            SymbolTable[i] = $"Nombre: {nombre}, Tipo: {tipo}, Valor: {valor}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Si no se puede evaluar la asignación, se puede registrar el error o ignorarlo.
                }
            }
        }



        private void GenerateIntermediateCode(CompilationUnitSyntax root)
        {
            StringBuilder intermediate = new StringBuilder();

            // Añadir un salto de línea al principio si es necesario
            intermediate.AppendLine("");

            // Procesar declaraciones globales (para programas de nivel superior)
            foreach (var globalStatement in root.Members.OfType<GlobalStatementSyntax>())
            {
                ProcessStatement(globalStatement.Statement, intermediate);
                intermediate.AppendLine(""); // Añadir salto de línea después de cada declaración global
            }

            // Procesar declaraciones de campos en clases/estructuras
            foreach (var field in root.DescendantNodes().OfType<FieldDeclarationSyntax>())
            {
                foreach (var variable in field.Declaration.Variables)
                {
                    intermediate.AppendLine($"Campo: {field.Declaration.Type.ToString().Trim()} {variable.Identifier.Text}");
                    if (variable.Initializer != null)
                        intermediate.AppendLine($" = {variable.Initializer.Value.ToString().Trim()}");
                    intermediate.AppendLine(".");  // Añadir salto de línea tras cada campo.
                    intermediate.AppendLine(""); // Añadir un salto de línea extra después de cada campo
                }
            }

            // Procesar métodos
            foreach (var method in root.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                intermediate.AppendLine($"Método {method.Identifier.Text}:");
                if (method.Body != null)
                {
                    foreach (var statement in method.Body.Statements)
                    {
                        ProcessStatement(statement, intermediate);
                    }
                }
                else if (method.ExpressionBody != null)
                {
                    intermediate.AppendLine($"Expresión: {method.ExpressionBody.Expression.ToString().Trim()}. ");
                }
                intermediate.AppendLine("FinMétodo.");
                intermediate.AppendLine(""); // Añadir salto de línea después de cada método
            }

            // Resultado final, agregar un salto de línea adicional
            codint.Text += intermediate.ToString() + Environment.NewLine;
        }




        private void ProcessStatement(StatementSyntax statement, StringBuilder intermediate)
        {
            // Declaración local (por ejemplo: int x;)
            if (statement is LocalDeclarationStatementSyntax localDecl)
            {
                foreach (var variable in localDecl.Declaration.Variables)
                {
                    intermediate.Append($"Declarar {localDecl.Declaration.Type.ToString().Trim()} {variable.Identifier.Text}");
                    if (variable.Initializer != null)
                        intermediate.Append($" = {variable.Initializer.Value.ToString().Trim()}");
                    intermediate.Append(". ");
                }
            }
            // Expresión (por ejemplo: x = x + 5 + 2;)
            else if (statement is ExpressionStatementSyntax expressionStmt)
            {
                if (expressionStmt.Expression is AssignmentExpressionSyntax assignExpr)
                {
                    intermediate.Append($"Asignar: {assignExpr.Left.ToString().Trim()} = {assignExpr.Right.ToString().Trim()}. ");
                }
                else
                {
                    intermediate.Append($"{statement.Kind()} : {statement.ToString().Trim()}. ");
                }
            }
            // Otros tipos de sentencias se procesan de forma genérica
            else
            {
                intermediate.Append($"{statement.Kind()} : {statement.ToString().Trim()}. ");
            }
        }


        private string TranslateToCpp(CompilationUnitSyntax root)
        {
            StringBuilder cppCode = new StringBuilder();
            cppCode.AppendLine("#include <iostream>");
            cppCode.AppendLine("#include <string>");
            cppCode.AppendLine("using namespace std;\n");

            // Procesar clases y namespaces (si existen)
            if (root.Members.Any(m => m is NamespaceDeclarationSyntax))
            {
                foreach (var ns in root.Members.OfType<NamespaceDeclarationSyntax>())
                {
                    foreach (var classDecl in ns.Members.OfType<ClassDeclarationSyntax>())
                    {
                        cppCode.AppendLine(TranslateClassToCpp(classDecl));
                    }
                }
            }
            else
            {
                foreach (var classDecl in root.Members.OfType<ClassDeclarationSyntax>())
                {
                    cppCode.AppendLine(TranslateClassToCpp(classDecl));
                }
            }

            // Procesar sentencias globales (top-level statements)
            var globalStatements = root.Members.OfType<GlobalStatementSyntax>();
            if (globalStatements.Any())
            {
                cppCode.AppendLine("int main() {");
                foreach (var globalStmt in globalStatements)
                {
                    cppCode.AppendLine(TranslateStatementToCpp(globalStmt.Statement));
                }
                cppCode.AppendLine("    return 0;");
                cppCode.AppendLine("}");
            }

            return cppCode.ToString();
        }
        private string TranslateClassToCpp(ClassDeclarationSyntax classDecl)
        {
            StringBuilder classCode = new StringBuilder();
            classCode.AppendLine($"class {classDecl.Identifier.Text} {{");
            classCode.AppendLine("public:");
            foreach (var member in classDecl.Members)
            {
                if (member is MethodDeclarationSyntax methodDecl)
                {
                    classCode.AppendLine(TranslateMethodToCpp(methodDecl));
                }
                // Aquí se pueden agregar traducciones para campos u otros miembros si se requiere
            }
            classCode.AppendLine("};\n");
            return classCode.ToString();
        }

        private string TranslateMethodToCpp(MethodDeclarationSyntax methodDecl)
        {
            string returnType = MapType(methodDecl.ReturnType.ToString());
            string methodName = methodDecl.Identifier.Text;
            string parameters = string.Join(", ", methodDecl.ParameterList.Parameters.Select(p =>
                MapType(p.Type.ToString()) + " " + p.Identifier.Text));

            StringBuilder methodCode = new StringBuilder();
            methodCode.AppendLine($"    {returnType} {methodName}({parameters}) {{");

            // Si el método tiene cuerpo
            if (methodDecl.Body != null)
            {
                foreach (var statement in methodDecl.Body.Statements)
                {
                    methodCode.AppendLine(TranslateStatementToCpp(statement));
                }
            }
            // Si utiliza cuerpo de expresión (=>)
            else if (methodDecl.ExpressionBody != null)
            {
                methodCode.AppendLine("        return " + TranslateExpressionToCpp(methodDecl.ExpressionBody.Expression) + ";");
            }
            methodCode.AppendLine("    }");
            return methodCode.ToString();
        }

        private string TranslateStatementToCpp(StatementSyntax statement)
        {
            // Manejo de sentencias if
            if (statement is IfStatementSyntax ifStmt)
            {
                StringBuilder code = new StringBuilder();
                code.Append("        if (");
                code.Append(TranslateExpressionToCpp(ifStmt.Condition));
                code.AppendLine(") {");
                // Procesa el bloque del if o la sentencia única
                if (ifStmt.Statement is BlockSyntax block)
                {
                    foreach (var stmt in block.Statements)
                    {
                        code.AppendLine(TranslateStatementToCpp(stmt));
                    }
                }
                else
                {
                    code.AppendLine(TranslateStatementToCpp(ifStmt.Statement));
                }
                code.Append("        }");
                // Manejo de la cláusula else, si existe
                if (ifStmt.Else != null)
                {
                    code.AppendLine(" else {");
                    if (ifStmt.Else.Statement is BlockSyntax elseBlock)
                    {
                        foreach (var stmt in elseBlock.Statements)
                        {
                            code.AppendLine(TranslateStatementToCpp(stmt));
                        }
                    }
                    else
                    {
                        code.AppendLine(TranslateStatementToCpp(ifStmt.Else.Statement));
                    }
                    code.Append("        }");
                }
                return code.ToString();
            }
            // Declaración local (por ejemplo: int x;)
            else if (statement is LocalDeclarationStatementSyntax localDecl)
            {
                StringBuilder line = new StringBuilder("        ");
                string tipoCpp = MapType(localDecl.Declaration.Type.ToString());
                foreach (var variable in localDecl.Declaration.Variables)
                {
                    line.Append($"{tipoCpp} {variable.Identifier.Text}");
                    if (variable.Initializer != null)
                    {
                        line.Append(" = " + TranslateExpressionToCpp(variable.Initializer.Value));
                    }
                    line.Append(";");
                }
                return line.ToString();
            }
            // Expresión: puede ser llamada a Console.WriteLine o asignación
            else if (statement is ExpressionStatementSyntax expressionStmt)
            {
                // Caso especial: traducir Console.WriteLine(a, b, ...)
                if (expressionStmt.Expression is InvocationExpressionSyntax invocationExpr)
                {
                    if (invocationExpr.Expression is MemberAccessExpressionSyntax memberAccess &&
                        memberAccess.Expression.ToString().Trim() == "Console" &&
                        memberAccess.Name.ToString().Trim() == "WriteLine")
                    {
                        StringBuilder line = new StringBuilder("        cout << ");
                        var args = invocationExpr.ArgumentList.Arguments;
                        bool first = true;
                        foreach (var arg in args)
                        {
                            if (!first)
                                line.Append(" << ");
                            line.Append(TranslateExpressionToCpp(arg.Expression));
                            first = false;
                        }
                        line.Append(" << endl;");
                        return line.ToString();
                    }
                }
                // Asignación (por ejemplo: x = x + 5;)
                if (expressionStmt.Expression is AssignmentExpressionSyntax assignExpr)
                {
                    string left = assignExpr.Left.ToString().Trim();
                    string right = TranslateExpressionToCpp(assignExpr.Right);
                    return $"        {left} = {right};";
                }
                return "        // [Expresión no traducida]";
            }
            // Sentencia return
            else if (statement is ReturnStatementSyntax returnStmt)
            {
                return "        return " + TranslateExpressionToCpp(returnStmt.Expression) + ";";
            }
            // Caso genérico: sentencia no reconocida
            return "        // [Sentencia no traducida]";
        }

        private string TranslateExpressionToCpp(ExpressionSyntax expr)
        {
            if (expr is BinaryExpressionSyntax binaryExpr)
            {
                string left = TranslateExpressionToCpp(binaryExpr.Left);
                string right = TranslateExpressionToCpp(binaryExpr.Right);
                string op = binaryExpr.OperatorToken.Text;
                return $"{left} {op} {right}";
            }
            else if (expr is LiteralExpressionSyntax literal)
            {
                return literal.Token.Text;
            }
            else if (expr is IdentifierNameSyntax identifier)
            {
                return identifier.Identifier.Text;
            }
            // Caso por defecto: se utiliza el ToString() del nodo
            return expr.ToString();
        }

        private string MapType(string csType)
        {
            if (csType == "int")
                return "int";
            else if (csType == "float")
                return "float";
            else if (csType == "double")
                return "double";
            else if (csType == "bool")
                return "bool";
            else if (csType == "string")
                return "std::string";
            else if (csType == "void")
                return "void";
            else
                return csType;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            string codigoOriginal = entrada.Text;
            string codigoFormateado = await FormatearCodigoRoslynAsync(codigoOriginal);
            entrada.Text = codigoFormateado;
        }

        private async Task<string> FormatearCodigoRoslynAsync(string codigo)
        {
            var tree = CSharpSyntaxTree.ParseText(codigo);
            var root = await tree.GetRootAsync();

            var workspace = new AdhocWorkspace();
            var options = workspace.Options;

            var formattedNode = Formatter.Format(root, workspace, options);

            return formattedNode.ToFullString();
        }

    }
}

