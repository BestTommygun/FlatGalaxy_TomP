using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlatGalaxy.Model;
using System.Drawing;

namespace FlatGalaxy_TomP.Controllers.collisionDetection
{
    public class QuadTreeCollision : ICollision
    {
        private QuadTree _quadTree;

        public List<CelestialBody> Collide(List<CelestialBody> bodies)
        {
            if (bodies.Count > 0)
            {
                double maxRadius = 0;
                foreach (CelestialBody celestialBody in bodies)
                {
                    celestialBody.collision?.doTodo(celestialBody);
                    if(celestialBody.Radius > maxRadius)
                    {
                        maxRadius = celestialBody.Radius;
                    }
                }

                _quadTree = new QuadTree(
                    new System.Drawing.Rectangle(0, 0, 800, 600),
                    bodies,
                    7,
                    4);

                HashSet<CelestialBody> collidingBodies = new HashSet<CelestialBody>();

                foreach(var body in bodies)
                {
                    foreach(var collidingBody in 
                        _quadTree.BodiesInRange(
                            (int)(body.X-(body.Radius+maxRadius)),
                            (int)(body.Y-(body.Radius+maxRadius)),
                            (int)(body.X+(body.Radius+maxRadius)),
                            (int)(body.Y+(body.Radius+maxRadius))
                        ).Where(b => b != body))
                    {
                        double deltaX = body.X - collidingBody.X;
                        double deltaY = body.Y - collidingBody.Y;
                        double distSq = Math.Pow((Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)), 0.5);
                        double sumRad = body.Radius + collidingBody.Radius;
                        if (distSq <= sumRad)
                        { 
                            if (!collidingBodies.Contains(collidingBody))
                            {
                                collidingBodies.Add(collidingBody);
                            }
                        }
                    }
                }
                return collidingBodies.SelectMany(b => b.onCollision()).Where(b => !b.ShouldDissapear).Union(bodies.Where(b => !collidingBodies.Contains(b))).ToList();
            }
            return bodies;
        }

        public List<Rectangle> GetBounds()
        {
            List<Rectangle> bounds = new List<Rectangle>();
            return _quadTree.GetBounds();
        }
    }
}