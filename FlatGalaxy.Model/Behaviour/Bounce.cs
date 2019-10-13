using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FlatGalaxy.Model;

namespace FlatGalaxy.Model.Behaviour
{
    public class Bounce : CollisionComponent
    {
        private int bounceCounter;

        public Bounce()
        {
            bounceCounter = 0;
        }

        public async override Task<List<CelestialBody>> Collide(CelestialBody celestialBody)
        {
            if(bounceCounter < 5 && !celestialBody.ShouldDissapear)
            {
                celestialBody.VX = -celestialBody.VX;
                celestialBody.VY = -celestialBody.VY;

                bounceCounter++;
            }
            else
            {
               celestialBody.collision = new Blink();
            }

            return await base.Collide(celestialBody);
        }
    }
}
