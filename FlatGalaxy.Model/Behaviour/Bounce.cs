using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FlatGalaxy.Model;

namespace FlatGalaxy.Model.Behaviour
{
    public class Bounce : CollisionComponent
    {
        private int bounceCounter = 0;

        public async override Task<List<CelestialBody>> Collide(CelestialBody celestialBody)
        {
            if(bounceCounter < 5)
            {
                celestialBody.VX = -celestialBody.VX;
                celestialBody.VY = -celestialBody.VY;

                bounceCounter++;
            }
            celestialBody.collision = new Blink();

            return await base.Collide(celestialBody);
        }
    }
}
