using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP_JohanW.Controllers.parsing
{
    public class ParserFactory
    {
        public IParser returnParser(String file) //TODO: VERY UGLY CLEAN THIS UP (temp solution)
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
