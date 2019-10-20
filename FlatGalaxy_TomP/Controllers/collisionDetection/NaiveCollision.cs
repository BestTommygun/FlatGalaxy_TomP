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

        public NaiveCollision() //creates a Rectangle to represent the bounds of the algorithm (entire screen)
        {
            _bounds = new List<Rectangle>();
            List<Rectangle> rectangles = new List<Rectangle>();
            _bounds.Add(new Rectangle(0, 0, 800, 600));
        }

        public List<CelestialBody> Collide(List<CelestialBody> bodies)
        {
            if (bodies.Count > 0)
            {
                //do the todo queue
                foreach (CelestialBody celestialBody in bodies)
                {
                    celestialBody.collision?.doTodo(celestialBody);
                }

                //detect for collisions (all elements with all elements)
                HashSet<CelestialBody> collidingBodies = new HashSet<CelestialBody>();

                foreach (CelestialBody curBody in bodies)
                {
                    foreach (CelestialBody nextBody in bodies.Where(b => b != curBody))
                    {
                        double deltaX = curBody.X - nextBody.X;
                        double deltaY = curBody.Y - nextBody.Y;
                        double distSq = Math.Pow((Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)), 0.5);
                        double sumRad = curBody.Radius + nextBody.Radius;
                        if (distSq <= sumRad)
                        {
                            if (!collidingBodies.Contains(curBody))
                                collidingBodies.Add(curBody);
                        }
                    }
                }
                //merge the colliding bodies with the existing bodies, ignore elements that have ShouldDissapear set to true
                return collidingBodies.SelectMany(b => b.onCollision()).Where(b => !b.ShouldDissapear).Union(bodies.Where(b => !collidingBodies.Contains(b))).ToList();
            }

            return bodies;
        }

        public List<Rectangle> GetBounds() //returns the rectangle representing the bounds of the algorithm (the entire screen)
        {
            return _bounds;
        }
    }
}
