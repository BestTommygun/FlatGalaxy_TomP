using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy.Model.Behaviour
{
    public class Dissapear : CollisionComponent
    {
        public async override Task<List<CelestialBody>> Collide(CelestialBody celestialBody)
        {
            celestialBody.ShouldDissapear = true; //check if this works

            return await base.Collide(celestialBody);
        }
    }
}
