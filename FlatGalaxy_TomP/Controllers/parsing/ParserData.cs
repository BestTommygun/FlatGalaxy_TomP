using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP_JohanW.Controllers.parsing
{
    public class ParserData
    {
        public string Type { get; set; }

        public string Name { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public int Radius { get; set; }

        public double VX { get; set; }
        public double VY { get; set; }

        public List<string> Neighbours { get; set; }

        public string Colour { get; set; }

        public string OnCollision { get; set; }

    }
}
