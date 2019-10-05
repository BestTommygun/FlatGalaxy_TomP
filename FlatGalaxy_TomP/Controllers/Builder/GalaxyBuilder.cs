using FlatGalaxy_TomP_JohanW.Controllers.parsing;
using FlatGalaxy.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP_JohanW.Controllers
{
    public class GalaxyBuilder
    {
        CelestialBodyBuilder celestialBodyBuilder = new CelestialBodyBuilder();

        public Map buildGalaxy(List<ParserData> ParserDataList)
        {
            Stopwatch timer = new Stopwatch();

            timer.Start();

            Map returnMap = new Map();
            foreach (ParserData parserData in ParserDataList)
            {
                returnMap.celestialBodies.Add(celestialBodyBuilder.BuildCelestialBody(parserData));
            }
            timer.Stop();
            var elapsedTime = timer.Elapsed;
            return returnMap;
        }
    }
}
