using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FlatGalaxy.Model.BreathFirstSSearch
{
    public class Path
    {
        private List<Vertex> _vertices;

        public Path()
        {
            _vertices = new List<Vertex>();
        }

        public Path(Path path)
        {
            _vertices = path.getPath().ToList();
        }

        public void add(Vertex vertex)
        {
            _vertices.Add(vertex);
        }

        public List<Vertex> getPath()
        {
            return _vertices;
        }
    }
}
