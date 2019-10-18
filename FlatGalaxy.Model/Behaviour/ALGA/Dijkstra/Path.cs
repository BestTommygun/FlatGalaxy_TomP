using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FlatGalaxy.Model.Behaviour.ALGA.Dijkstra
{
    public class Path
    {
        private List<Node> _Path;
        public double weight { get; set; }


        public Path(double weight)
        {
            _Path = new List<Node>();
            this.weight = weight;
        }

        public Path(List<Node> path)
        { 
            _Path = path.ToList();
        }

        public Path Add(Node newNode)
        {
            _Path = new List<Node>();
            _Path.Add(newNode);
            weight = 0;
            foreach (Node node in _Path)
            {
                weight += node.Weight;
            }
            return this;
        }

        public Node getEndNode()
        {
            return _Path.LastOrDefault();
        }
    }
}
