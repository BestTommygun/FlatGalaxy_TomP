using System;
using System.Collections.Generic;
using System.Text;

namespace FlatGalaxy.Model.Behaviour
{
    public class Dissapear : CollisionComponent
    {
        public override List<CelestialBody> Collide(CelestialBody celestialBody)
        {
            celestialBody = null; //check if this works

            return base.Collide(celestialBody);
        }
    }
}
