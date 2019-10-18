using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy.Model.Behaviour
{
    public class Grow : CollisionComponent
    {
        public override List<CelestialBody> Collide(CelestialBody celestialBody)
        {
            celestialBody.Radius++;

           if(celestialBody.Radius > 20)
                celestialBody.collision = new Explode();

            return base.Collide(celestialBody);
        }
    }
}
