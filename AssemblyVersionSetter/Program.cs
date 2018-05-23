using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace AssemblyVersionSetter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(args[0]);
                var version = Assembly.ReflectionOnlyLoadFrom(args[0]).GetName().Version.ToString();
                Console.WriteLine($"Version of {args[0]} is {version}");

                var xdoc = XDocument.Load(args[1]);

                xdoc.XPathSelectElement(args[2]).Value = version;

                xdoc.Save(args[1]);

                Console.WriteLine($"Updated {args[2]} of {args[1]}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Usage();
            }
        }

        private static void Usage()
        {
            Console.WriteLine(@"
Sets the value of the xpath in the xml file to the assembly version of the specified assembly.

AssemblyVersionSetter assembly xmlfile xpath
");
        }
    }
}
