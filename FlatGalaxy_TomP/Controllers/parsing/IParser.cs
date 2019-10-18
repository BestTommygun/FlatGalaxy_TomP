using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP_JohanW.Controllers.parsing
{
    public interface IParser
    {
        /// <summary>
        /// Returns the data from an input in string format
        /// </summary>
        /// <param name="file">The Stream</param>
        List<ParserData> Parse(Stream file); 
    }
}
