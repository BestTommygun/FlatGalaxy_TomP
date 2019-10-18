using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlatGalaxy.Model;
using System.Drawing;

namespace FlatGalaxy_TomP.Controllers.collisionDetection
{
    public class QuadTree
    {
        private readonly int MAX_ENTITIES = 4;
        private readonly int MAX_LEVELS = 5;
        private readonly int level = 0;
        private readonly Rectangle bounds;
        private readonly HashSet<CelestialBody> entityList;
        private HashSet<QuadTree> leafs;

        public QuadTree(int level, Rectangle bounds, HashSet<CelestialBody> entities)
        {
            this.level = level;
            this.bounds = bounds;

            entityList = new HashSet<CelestialBody>();
            foreach (CelestialBody entity in entities)
            {
                if (isWithinBounds(entity))
                    entityList.Add(entity);
            }
            if (entityList.Count > MAX_ENTITIES && level < MAX_LEVELS)
                split();
        }

        public void split()
        {
            int width = bounds.Width / 2;
            int height = bounds.Height / 2;
            int xPos = bounds.X;
            int yPos = bounds.Y;

            leafs = new HashSet<QuadTree>();
            Point location = new Point(xPos, yPos);
            leafs.Add(new QuadTree(level + 1, new Rectangle(xPos, yPos, width, height), entityList));
            leafs.Add(new QuadTree(level + 1, new Rectangle(xPos + width, yPos, width, height), entityList));
            leafs.Add(new QuadTree(level + 1, new Rectangle(xPos, yPos + height, width, height), entityList));
            leafs.Add(new QuadTree(level + 1, new Rectangle(xPos + width, yPos + height, width, height), entityList));
        }

        public HashSet<CelestialBody> DetectCollision()
        {
            HashSet<CelestialBody> collidingBodies = new HashSet<CelestialBody>();

            if (leafs == null)
            {
                foreach (CelestialBody curBody in entityList)
                {
                    foreach (CelestialBody nextBody in entityList)
                    {
                        if (curBody != nextBody)
                        {
                            int deltaX = (int)curBody.X - (int)nextBody.X;
                            int deltaY = (int)curBody.Y - (int)nextBody.Y;
                            int distSq = (int)Math.Sqrt((int)Math.Pow(deltaX, 2) + (int)Math.Pow(deltaY, 2));
                            int sumRad = (int)curBody.Radius + (int)nextBody.Radius;
                            if (distSq <= sumRad)
                            {
                                if (!collidingBodies.Contains(curBody))
                                    collidingBodies.Add(curBody);
                                if (!collidingBodies.Contains(nextBody))
                                    collidingBodies.Add(nextBody);
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (QuadTree leaf in leafs)
                {
                    var bodies = leaf.DetectCollision();

                    foreach (CelestialBody body in bodies)
                    {
                        collidingBodies.Add(body);
                    }
                }
            }

            return collidingBodies;
        }

        private bool isWithinBounds(CelestialBody body)
        {
            if (bounds.Contains((int)body.X, (int)body.Y)) return true;
            return false;
        }

        public List<Rectangle> GetBounds()
        {
            List<Rectangle> bounds = new List<Rectangle>();
            if(leafs == null)
            {
                bounds.Add(this.bounds);
            }
            else
            {
                foreach (QuadTree leaf in leafs)
                {
                    bounds.AddRange(leaf.GetBounds());
                }
            }
            return bounds;
        }
    }
}