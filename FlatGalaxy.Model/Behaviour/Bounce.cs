using System;
using System.Collections.Generic;
using System.Text;
using FlatGalaxy.Model;

namespace FlatGalaxy.Model.Behaviour
{
    public class Bounce : CollisionComponent
    {
        private int bounceCounter = 0;

        public override void Collide(CelestialBody celestialBody)
        {
            celestialBody.VX = -celestialBody.VX;
            celestialBody.VY = - celestialBody.VY;

            bounceCounter++;

            base.Collide(celestialBody);
        }
    }
}
