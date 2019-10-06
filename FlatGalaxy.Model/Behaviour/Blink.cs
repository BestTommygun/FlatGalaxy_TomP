using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlatGalaxy.Model;

namespace FlatGalaxy.Model
{
    public class Blink : CollisionComponent
    {
        public override List<CelestialBody> Collide(CelestialBody celestialBody) //maybe list?
        {
            celestialBody.Colour = "Yellow";

            return base.Collide(celestialBody);
        }
    }
}
