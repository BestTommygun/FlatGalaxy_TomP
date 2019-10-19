using FlatGalaxy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP_JohanW.Controllers.parsing
{
    public class ParserFactory
    {
        public IParser returnParser(string file)
        {
            if (file.Length > 1)
            {
                string[] splitText = file.Split('.');
                string fileEnding = splitText.LastOrDefault().Substring(0, 3);

                return _create(fileEnding);
            }
            else throw new ArgumentNullException();
        }

        private IParser _create(string fileEnding)
        {
            switch (fileEnding)
            {
                case "xml":
                    return xmlParser();
                case "csv":
                    return csvParser();
                default:
                    return null;
            }
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
