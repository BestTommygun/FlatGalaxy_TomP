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
        private readonly int MAX_ENTITIES;
        private readonly int MAX_LEVELS;
        private readonly int LEVEL;
        private readonly Rectangle BOUNDS;
        private HashSet<CelestialBody> entityList;
        private HashSet<QuadTree> quadTrees;

        public QuadTree(int level, Rectangle bounds, HashSet<CelestialBody> entities, int maxLevels, int maxEntities)
        {
            this.LEVEL = level;
            this.BOUNDS = bounds;
            this.MAX_LEVELS = maxLevels;
            this.MAX_ENTITIES = maxEntities;

            entityList = new HashSet<CelestialBody>();
            foreach (CelestialBody entity in entities)
            {
                if (isWithinBounds(entity))
                    entityList.Add(entity);
            }
            if (entityList.Count > MAX_ENTITIES && LEVEL < MAX_LEVELS)
                split();
        }

        public void split()
        {
            int width = BOUNDS.Width / 2;
            int height = BOUNDS.Height / 2;
            int xPos = BOUNDS.X;
            int yPos = BOUNDS.Y;

            quadTrees = new HashSet<QuadTree>();
            Point location = new Point(xPos, yPos);
            quadTrees.Add(new QuadTree(LEVEL + 1, new Rectangle(xPos, yPos, width, height),                  entityList, MAX_LEVELS, MAX_ENTITIES));
            quadTrees.Add(new QuadTree(LEVEL + 1, new Rectangle(xPos + width, yPos, width, height),          entityList, MAX_LEVELS, MAX_ENTITIES));
            quadTrees.Add(new QuadTree(LEVEL + 1, new Rectangle(xPos, yPos + height, width, height),         entityList, MAX_LEVELS, MAX_ENTITIES));
            quadTrees.Add(new QuadTree(LEVEL + 1, new Rectangle(xPos + width, yPos + height, width, height), entityList, MAX_LEVELS, MAX_ENTITIES));
        }

        public HashSet<CelestialBody> DetectCollision()
        {
            HashSet<CelestialBody> collidingBodies = new HashSet<CelestialBody>();

            if (quadTrees == null)
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
                foreach (QuadTree quadTree in quadTrees)
                {
                    var bodies = quadTree.DetectCollision();

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
            if (BOUNDS.Contains((int)body.X, (int)body.Y)) return true;
            return false;
        }

        public List<Rectangle> GetBounds()
        {
            List<Rectangle> bounds = new List<Rectangle>();
            if(quadTrees == null)
            {
                bounds.Add(this.BOUNDS);
            }
            else
            {
                foreach (QuadTree quadTree in quadTrees)
                {
                    bounds.AddRange(quadTree.GetBounds());
                }
            }
            return bounds;
        }
    }
}