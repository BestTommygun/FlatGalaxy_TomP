using System;
using System.Collections.Generic;
using System.Text;

namespace FlatGalaxy.Model.Behaviour
{
    public class Explode : CollisionComponent
    {
        public override List<CelestialBody> Collide(CelestialBody celestialBody)
        {
            //TODO: do collision stuff here
            List<CelestialBody> explodedRemnants = new List<CelestialBody>();
            explodedRemnants.Add(null);
            int i = 0;
            while (i < 5)
            {
                Astroid remnant = new Astroid()
                {
                    Name = celestialBody.Name + " remnant-" + i,
                    Neighbours = new List<string>(),
                    Radius = celestialBody.Radius,
                    X = celestialBody.X,
                    Y = celestialBody.Y,
                    VX = celestialBody.VX + (i / 10),
                    VY = celestialBody.VY + (i / 10),
                    Colour = celestialBody.Colour,
                    collision = new Bounce()
                };
                explodedRemnants.Add(remnant);
                i++;
            }
            
            return explodedRemnants; //dit moet null returnen om het exploding body weg te halen
        }
    }
}
