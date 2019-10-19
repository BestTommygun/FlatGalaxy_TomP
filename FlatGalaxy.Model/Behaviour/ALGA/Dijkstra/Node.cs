using System;
using System.Collections.Generic;
using System.Text;

namespace FlatGalaxy.Model.Behaviour.ALGA.Dijkstra
{
    public class Node
    {
        public List<string> connectedNodes { get; set; }
        public List<Node> From { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }

        public Node() { }

        public Node(string name, List<string> nodes, double x, double y)
        {
            Name = name;
            connectedNodes = nodes;
            this.X = x;
            this.Y = y;
            Weight = double.MaxValue;
        }
    }
}
