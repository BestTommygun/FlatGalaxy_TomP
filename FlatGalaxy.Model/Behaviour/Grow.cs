using System;
using System.Collections.Generic;
using System.Text;

namespace FlatGalaxy.Model.Behaviour
{
    public class Grow : CollisionComponent
    {
        public override void Collide(CelestialBody celestialBody)
        {
            celestialBody.Radius++;

            //TODO: this is ugly fix this
            if(celestialBody.Radius > 20)
                celestialBody.collision = new Explode();
        }
    }
}
