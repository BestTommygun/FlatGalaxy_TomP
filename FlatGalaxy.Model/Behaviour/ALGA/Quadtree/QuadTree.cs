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
        private readonly int _maxEntities;
        private readonly int _maxLevels;
        private readonly int _level;
        private readonly Rectangle _bounds;
        private readonly List<CelestialBody> _entityList;
        private readonly HashSet<QuadTree> _quadTrees;

        public QuadTree(Rectangle bounds, List<CelestialBody> entities, int maxLevels, int maxEntities) : this(0, bounds, entities, maxLevels, maxEntities) { }
        
        private QuadTree(int level, Rectangle bounds, List<CelestialBody> entities, int maxLevels, int maxEntities)
        {
            _level = level;
            _bounds = bounds;
            _maxLevels = maxLevels;
            _maxEntities = maxEntities;

            //create a list of entities that are within the bounds of this QuadTree
            List<CelestialBody> entityList = new List<CelestialBody>();

            foreach (CelestialBody entity in entities)
            {
                if (isWithinBounds(entity)) //check if the entity is within this specific quad's bounds
                    entityList.Add(entity);
            }

            if (entityList.Count > _maxEntities && _level < _maxLevels)
            {
                //if there are more entities than allowed and the maximum depth has not been reached,
                //split the tree up into 4 children
                int width = _bounds.Width / 2;
                int height = _bounds.Height / 2;
                int xPos = _bounds.X;
                int yPos = _bounds.Y;

                _quadTrees = new HashSet<QuadTree>();
                _quadTrees.Add(new QuadTree(_level + 1, new Rectangle(xPos, yPos, width, height),               entities, _maxLevels, _maxEntities)); //linksboven
                _quadTrees.Add(new QuadTree(_level + 1, new Rectangle(xPos + width, yPos, width, height),       entities, _maxLevels, _maxEntities)); //rechtsboven
                _quadTrees.Add(new QuadTree(_level + 1, new Rectangle(xPos, yPos + height, width, height),      entities, _maxLevels, _maxEntities)); //linksonder
                _quadTrees.Add(new QuadTree(_level + 1, new Rectangle(xPos + width, yPos + height, width, height), entities, _maxLevels, _maxEntities)); //rechtsonder
            }
            else //if the max depth has been reached or the entities are less than maximum allowed, add them to this quad's list
                _entityList = entityList;
        }

        private bool isWithinBounds(CelestialBody body) //check if the body is inside the bounds defined by the quadtree
        {
            if (_bounds.Contains((int)(body.X - body.Radius), (int)(body.Y - body.Radius))
                || _bounds.Contains((int)(body.X + body.Radius), (int)(body.Y + body.Radius)))
                return true;
            else
                return false;
        }                       
                                
        public List<Rectangle> GetBounds() //get the rectangles that will be drawn upon the screen
        {
            List<Rectangle> bounds = new List<Rectangle>();
            if(_quadTrees == null)
            {
                bounds.Add(_bounds);
            }
            else
            {
                foreach (QuadTree quadTree in _quadTrees)
                {
                    bounds.AddRange(quadTree.GetBounds());
                }
            }

            return bounds;
        }
        
        public List<CelestialBody> BodiesInRange(int x1, int y1, int x2, int y2)
        {
            List<CelestialBody> returnBodies = new List<CelestialBody>(); //check if the boundary lines are within the circle (range) opposite of withinbouds
            if((_bounds.Left <= x2 || _bounds.Right >= x1) &&
                (_bounds.Top <= y2 || _bounds.Bottom >= y1))
            {
                if (_entityList == null)
                {
                    foreach(var child in _quadTrees)
                    {
                        returnBodies.AddRange(child.BodiesInRange(x1, y1, x2, y2));
                    }
                }
                else
                {
                    returnBodies.AddRange(_entityList);
                }
            }

            return returnBodies;
        }
    }
}