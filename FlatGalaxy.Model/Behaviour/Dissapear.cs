using System;
using System.Collections.Generic;
using System.Text;

namespace FlatGalaxy.Model.Behaviour
{
    public class Dissapear : CollisionComponent
    {
        public override void Collide(CelestialBody celestialBody)
        {
            celestialBody = null; //check if this works

            base.Collide(celestialBody);
        }
    }
}
