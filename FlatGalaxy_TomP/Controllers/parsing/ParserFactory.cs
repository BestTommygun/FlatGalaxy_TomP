using FlatGalaxy.Model;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlatGalaxy_TomP_JohanW.Controllers.parsing
{
    public class ParserFactory
    {
        private Dictionary<string, Func<IParser>> _parsers = typeof(ParserFactory).Assembly.GetTypes()       //get all types in the assembly
            .Where(t => t.GetInterfaces().Contains(typeof(IParser)))                                         //get all classes that implement the IParser interface
            .Where(t => !t.IsAbstract && !t.IsArray && !t.IsGenericType && !t.IsInterface && !t.IsValueType) //get all classes that are not abstract, array, generic, interface or struct
            .Where(t => t.GetConstructors().Any(c => c.GetParameters().Length == 0))                         //get all classes with a default constructor
            .SelectMany(t => t.GetCustomAttributes<ParserAttribute>()                                        //get all attributes of type parserAttribute on the classes
                .Select(a => new Tuple<string, Func<IParser>>(a.Name, () =>                                  //create a new tuple with the name of the parser and its constructor
                { return (IParser)t.GetConstructors().Single(c => c.GetParameters().Length == 0).Invoke(new object[0]); })))
            .ToDictionary(t => t.Item1, t => t.Item2);                                                       //convert this IEnumerable to Dictionary

        public IParser returnParser(string file)
        {
            if (file.Length > 1)
            {
                Uri uri = new Uri(file);
                return _create(Path.GetExtension(uri.AbsolutePath));
            }
            else throw new ArgumentNullException();
        }

        private IParser _create(string fileEnding)
        {
            return _parsers[fileEnding]();
        }
    }
}
