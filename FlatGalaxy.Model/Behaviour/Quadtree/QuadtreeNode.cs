using System;
using System.Collections.Generic;
using System.Text;

namespace FlatGalaxy.Model.Behaviour.Quadtree
{
    public class QuadtreeNode
    {
        public QuadtreeNode North, East, South, West;

        QuadtreeNode(List<CelestialBody> celestialBodies)
        {
            if(celestialBodies.Count > 4)
            {
                //North = new QuadtreeNode();
                //East = new QuadtreeNode();
                //South = new QuadtreeNode();
                //West = new QuadtreeNode();
            }
        }
    }
}
