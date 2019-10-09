using FlatGalaxy.Model;
using FlatGalaxy_TomP.Controllers.parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP_JohanW.Controllers.parsing
{
    public class ParserFactory
    {
        public IParser returnParser(String file) //TODO: use the factory
        {
            if (file.Length > 1)
            {
                file.ToLower();
                string[] splitText = file.Split('.');
                string fileEnding = splitText.LastOrDefault().Substring(0, 3);

                if (fileEnding == "xml") return xmlParser();
                else if (fileEnding == "csv") return csvParser();
                else throw new ArgumentException();
            }
            else throw new ArgumentNullException();
        }

        internal IParser Create(string fileEnding)
        {
            if (fileEnding.Length > 1)
            {
                if (fileEnding == "xml") return xmlParser();
                else if (fileEnding == "csv") return csvParser();
                else throw new ArgumentException();
            }
            else throw new ArgumentNullException();
        }

        public IParser xmlParser ()
        {
            return new XMLParser();
        }

        public IParser csvParser()
        {
            return new CSVParser();
        }
    }
}
