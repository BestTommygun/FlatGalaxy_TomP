using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy.Model.Behaviour
{
    public class Dissapear : CollisionComponent
    {
        public override List<CelestialBody> Collide(CelestialBody celestialBody)
        {
            celestialBody.ShouldDissapear = true;

            return base.Collide(celestialBody);
        }
    }
}
