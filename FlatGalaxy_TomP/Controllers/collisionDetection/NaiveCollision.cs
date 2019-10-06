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
        public List<CelestialBody> Collide(List<CelestialBody> celestialBodies)
        {
            if (celestialBodies.Count > 0)
            {
                for (int i = 0; i < celestialBodies.Count; i++)
                {
                    var curBody = celestialBodies[i];
                    for (int j = 0; j < celestialBodies.Count; j++)
                    {
                        var nextBody = celestialBodies[j];
                        if (nextBody != curBody)
                        {
                            if (j == 27 && i == 17)
                                j = 27;
                            //pak wortel van C^2 en kijk of dit kleiner is dan de radiussen bij elkaar
                            //opgeteld (via pythagoras krijg je de afstand van een cirkel)
                            int deltaX = (int)curBody.X - (int)nextBody.X;
                            int deltaY = (int)curBody.Y - (int)nextBody.Y;
                            int distSq = (int)Math.Sqrt((int)Math.Pow(deltaX, 2) + (int)Math.Pow(deltaY, 2));
                            int sumRad = (int)curBody.Radius + (int)nextBody.Radius;
                            if (distSq <= sumRad)
                            {
                                var IList = curBody.onCollision();
                                var JList = nextBody.onCollision();

                                if(IList.Count > 1)
                                {
                                    celestialBodies = addRemnants(celestialBodies, IList);
                                }
                                if(JList.Count > 1)
                                {
                                    celestialBodies = addRemnants(celestialBodies, JList);
                                }
                                curBody = IList.FirstOrDefault();
                                nextBody = JList.FirstOrDefault();
                                if (curBody == null) break;
                                if (nextBody == null) j++;
                            }
                        }
                        celestialBodies.RemoveAll(cb => cb == null);
                    }
                }
            }
            return celestialBodies;
        }

        private List<CelestialBody> addRemnants(List<CelestialBody> celestialBodies, List<CelestialBody> remnantList)
        {
            foreach (CelestialBody remnant in remnantList)
            {
                celestialBodies.Add(remnant);
            }

            return celestialBodies;
        }
    }
}
