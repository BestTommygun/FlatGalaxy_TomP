using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy.Model.Behaviour
{
    public class Explode : CollisionComponent
    {
        private Random random;

        public Explode()
        {
            random = new Random();
        }

        public async override Task<List<CelestialBody>> Collide(CelestialBody celestialBody) //TODO: misschien Collision = nextCollision doen ipv new object()
        {
            List<CelestialBody> explodedRemnants = new List<CelestialBody>();

            if (!celestialBody.ShouldDissapear)
            {
                celestialBody.ShouldDissapear = true;
                explodedRemnants.Add(celestialBody);
                int i = 0;

                while (i < 5)
                {
                    double newX = random.NextDouble() * 2 + 0.5; //random number between 0.5 and 2.5
                    double newY = random.NextDouble() * 2 + 0.5;

                    Astroid remnant = new Astroid()
                    {
                        Neighbours = new List<string>(),
                        Radius = celestialBody.Radius / 5 + 1,
                        X = celestialBody.X + i * 5.5,
                        Y = celestialBody.Y + i * 5.5,
                        VX = (celestialBody.VX + -i) * newX,
                        VY = (celestialBody.VY + -i) * newY,
                        Colour = celestialBody.Colour,
                        collision = new Bounce()
                    };

                    explodedRemnants.Add(remnant);
                    i++;
                }
            }
            return explodedRemnants;
        }
    }
}
