using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP_JohanW.Controllers.parsing
{
    public interface IParser //TODO: implement http calls, make factory generic somehow
    {
        List<ParserData> Parse(Stream file); //this method returns the data from an input in string format
    }
}
