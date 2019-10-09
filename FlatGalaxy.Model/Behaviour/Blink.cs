using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlatGalaxy.Model;

namespace FlatGalaxy.Model
{
    public class Blink : CollisionComponent
    {
        string originalColour;
        public async override Task<List<CelestialBody>> Collide(CelestialBody celestialBody) //TODO: IT DOESNT FUCKING BLINK
        {
            if (originalColour == null) originalColour = celestialBody.Colour;

            celestialBody.Colour = "Yellow";
            //await Task.Delay(100);
            celestialBody.Colour = originalColour;

            return await base.Collide(celestialBody);
        }
    }
}
