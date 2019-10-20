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
        private List<Rectangle> _bounds;

        public NaiveCollision()
        {
            _bounds = new List<Rectangle>();
            List<Rectangle> rectangles = new List<Rectangle>();
            _bounds.Add(new Rectangle(0, 0, 800, 600));
        }

        public List<CelestialBody> Collide(List<CelestialBody> celestialBodies)
        {
            List<CelestialBody> returnBodies = celestialBodies.ToList();

            foreach (CelestialBody celestialBody in celestialBodies)
            {
                celestialBody.collision?.doTodo(celestialBody);
            }

            if (celestialBodies.Count > 0)
            {
                var detectedBodies = _detectCollision(celestialBodies);

                if (detectedBodies.Count > 0)
                {
                    var collidedBodies = _Collide(detectedBodies, celestialBodies);

                    foreach (CelestialBody collidedBody in collidedBodies.ToList())
                    {
                        int collidedBodyIndex = returnBodies.IndexOf(collidedBody);

                        if (collidedBodyIndex == -1)
                            returnBodies.Add(collidedBody);
                        else
                        {
                            if (collidedBody.ShouldDissapear)
                                returnBodies.Remove(collidedBody);
                            else
                                returnBodies[collidedBodyIndex] = collidedBody;
                            
                        }
                    }
                }
           }

           return returnBodies;
        }

        private List<CelestialBody> _detectCollision(List<CelestialBody> bodies)
        {
            HashSet<CelestialBody> collidingBodies = new HashSet<CelestialBody>();

            foreach (CelestialBody curBody in bodies)
            {
                foreach (CelestialBody nextBody in bodies)
                {
                    if (curBody != nextBody)
                    {
                        double deltaX = curBody.X - nextBody.X;
                        double deltaY = curBody.Y - nextBody.Y;
                        double distSq = Math.Pow((Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)), 0.5);
                        double sumRad = curBody.Radius + nextBody.Radius;
                        if (distSq <= sumRad)
                        {
                            if(!collidingBodies.Contains(curBody))
                                collidingBodies.Add(curBody);
                            if(!collidingBodies.Contains(nextBody))
                                collidingBodies.Add(nextBody);
                        }
                    }
                }
            }
            return collidingBodies.ToList();
        }

        private List<CelestialBody> _Collide(List<CelestialBody> collidingBodies, List<CelestialBody> allBodies)
        {
            List<CelestialBody> returningBodies = allBodies.ToList();

            foreach (CelestialBody celestialBody in collidingBodies)
            {
                returningBodies.AddRange(celestialBody.onCollision());
            }

            return returningBodies;
        }

        public List<Rectangle> GetBounds()
        {
            return _bounds;
        }
    }
}
