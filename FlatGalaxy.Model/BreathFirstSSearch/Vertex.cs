using System;
using System.Collections.Generic;
using System.Text;

namespace FlatGalaxy.Model.BreathFirstSSearch
{
    public class Vertex
    {
        public Vertex() { }
        public Vertex(string name, List<string> vertices)
        {
            this.Name = name;
            this.Vertices = vertices;
        }

        public string Name { get; set; }

        public List<string> Vertices { get; set; }
    }
}
