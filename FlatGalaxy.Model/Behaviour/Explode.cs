using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy.Model.Behaviour
{
    public class Explode : CollisionComponent
    {
        public async override Task<List<CelestialBody>> Collide(CelestialBody celestialBody)
        {
            List<CelestialBody> explodedRemnants = new List<CelestialBody>();
            if (!celestialBody.ShouldDissapear)
            {
                celestialBody.ShouldDissapear = true;
                explodedRemnants.Add(celestialBody);
                int i = 0;
                while (i < 5)
                {
                    Astroid remnant = new Astroid()
                    {
                        //Name = celestialBody.Name + " remnant-" + i
                        Neighbours = new List<string>(),
                        Radius = celestialBody.Radius / 5 + 1,
                        X = celestialBody.X + i,
                        Y = celestialBody.Y + i,
                        VX = celestialBody.VX + (i / 2),
                        VY = celestialBody.VY + (i / 2),
                        Colour = celestialBody.Colour,
                        collision = new Bounce()
                    };
                    explodedRemnants.Add(remnant);
                    i++;
                }
            }
            return explodedRemnants; //dit moet null returnen om het exploding body weg te halen
        }
    }
}
