using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy.Model
{
    public class Map : ICloneable
    {
        public List<CelestialBody> celestialBodies = new List<CelestialBody>();

        public object Clone() => new Map()
        {
            celestialBodies = this.celestialBodies.Select(b => (CelestialBody)b.Clone()).ToList()
        };
    }
}
