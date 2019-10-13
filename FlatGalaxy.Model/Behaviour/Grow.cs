using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy.Model.Behaviour
{
    public class Grow : CollisionComponent
    {
        public async override Task<List<CelestialBody>> Collide(CelestialBody celestialBody)
        {
            celestialBody.Radius++;

           if(celestialBody.Radius > 20)
                celestialBody.collision = new Explode();

            return await base.Collide(celestialBody);
        }
    }
}
