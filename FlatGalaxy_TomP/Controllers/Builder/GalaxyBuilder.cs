using FlatGalaxy_TomP_JohanW.Controllers.parsing;
using FlatGalaxy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlatGalaxy_TomP_JohanW.Controllers
{
    public class GalaxyBuilder
    {
        private CelestialBodyBuilder celestialBodyBuilder;

        public GalaxyBuilder()
        {
            celestialBodyBuilder = new CelestialBodyBuilder();
        }

        public Map buildGalaxy(List<ParserData> ParserDataList)
        {
            Map returnMap = new Map();

            foreach (ParserData parserData in ParserDataList)
            {
                returnMap.celestialBodies.Add(celestialBodyBuilder.BuildCelestialBody(parserData));
            }

            return returnMap;
        }
    }
}
