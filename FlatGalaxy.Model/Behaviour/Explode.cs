using System;
using System.Collections.Generic;
using System.Text;

namespace FlatGalaxy.Model.Behaviour
{
    public class Explode : CollisionComponent
    {
        public override void Collide(CelestialBody celestialBody)
        {
            celestialBody = null;
        }
    }
}
