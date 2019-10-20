using FlatGalaxy.Model;
using FlatGalaxy_TomP.Controllers.collisionDetection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<CelestialBody> bodies = new List<CelestialBody>()
            {
                new Planet(){X = 0, Y=0, Radius = 10},
                new Planet(){X = 299, Y=399, Radius = 10},
                new Planet(){X = 299, Y=399, Radius = 10},
                new Planet(){X = 299, Y=399, Radius = 10},
                new Planet(){X = 299, Y=399, Radius = 10},
            };


            QuadTree quadTree = new QuadTree(new System.Drawing.Rectangle(0, 0, 599, 799), bodies, 4, 4);
            Console.WriteLine("────────────[ running debugger ]──────────");
            Console.ReadKey();

        }
    }
}
