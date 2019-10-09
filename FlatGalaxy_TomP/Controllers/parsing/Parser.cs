using FlatGalaxy.Model;
using FlatGalaxy_TomP_JohanW.Controllers.parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP.Controllers.parsing
{
    public class Parser
    {
        private readonly Dictionary<Parsers, ParserFactory> _factories;

        private Parser()
        {
            _factories = new Dictionary<Parsers, ParserFactory>();

            foreach (Parsers action in Enum.GetValues(typeof(Parsers)))
            {
                var factory = (ParserFactory)Activator.CreateInstance(Type.GetType("FactoryMethod." + Enum.GetName(typeof(Parser), action) + "Factory"));
                _factories.Add(action, factory);
            }
        }

        public static Parser InitializeFactories() => new Parser();

        public IParser ExecuteCreation(Parsers parser, string fileEnding) => _factories[parser].Create(fileEnding);
    }
}
