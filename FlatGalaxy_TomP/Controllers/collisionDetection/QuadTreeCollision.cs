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
        private QuadTree quadTree;

        public List<CelestialBody> Collide(List<CelestialBody> celestialBodies)
        {
            foreach (CelestialBody celestialBody in celestialBodies)
            {
                celestialBody.collision.doTodo(celestialBody);
            }

            List<CelestialBody> returnBodies = celestialBodies.ToList();
            HashSet<CelestialBody> set = new HashSet<CelestialBody>();

            foreach (CelestialBody body in celestialBodies)
            {
                set.Add(body);
            }

            quadTree = new QuadTree(
                0,
                new System.Drawing.Rectangle(0, 0, 800, 600),
                set);
            if (celestialBodies.Count > 0)
            {
                var detectedBodies = quadTree.DetectCollision();

                if (detectedBodies.Count > 0)
                {
                    var collidedBodies = _Collide(detectedBodies.ToList(), celestialBodies);

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

        public List<Rectangle> GetBounds()
        {
            List<Rectangle> bounds = new List<Rectangle>();
            return quadTree.GetBounds();
        }

        private List<CelestialBody> _Collide(List<CelestialBody> collidingBodies, List<CelestialBody> allBodies)
        {
            List<CelestialBody> returningBodies = allBodies.ToList();

            foreach (CelestialBody celestialBody in collidingBodies)
            {
                List<CelestialBody> newBodies = celestialBody.onCollision();

                returningBodies.AddRange(newBodies);
            }

            return returningBodies;
        }
    }
}