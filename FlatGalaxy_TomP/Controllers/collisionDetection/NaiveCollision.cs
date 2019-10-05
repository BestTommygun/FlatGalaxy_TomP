using FlatGalaxy.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP.Controllers.collisionDetection
{
    public class NaiveCollision : ICollision
    {
        public void Collide(List<CelestialBody> celestialBodies)
        {
            if (celestialBodies.Count > 0)
            {
                foreach (CelestialBody curBody in celestialBodies)
                {
                    foreach (CelestialBody nextBody in celestialBodies)
                    {
                        if (nextBody != curBody)
                        {
                            //pak wortel van C^2 en kijk of dit kleiner is dan de radiussen bij elkaar
                            //opgeteld (via pythagoras krijg je de afstand van een cirkel)
                            int deltaX = (int)curBody.X - (int)nextBody.X;
                            int deltaY = (int)curBody.Y - (int)nextBody.Y;
                            int distSq = (int)Math.Sqrt((int)Math.Pow(deltaX, 2) + (int)Math.Pow(deltaY, 2));
                            int sumRad = (int)curBody.Radius + (int)nextBody.Radius;
                            if (distSq <= sumRad)
                            {
                                curBody.onCollision();
                                nextBody.onCollision();
                            }
                        }
                    }
                }
            }
        }
    }
}
