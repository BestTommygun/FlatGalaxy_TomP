using FlatGalaxy.Model;
using FlatGalaxy.Model.BreathFirstSSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP.Controllers.Algorithm
{
    public class BFSearch : IPathingAlgorithm
    {
        public List<CelestialBody> GetPath(List<CelestialBody> bodies, CelestialBody root, CelestialBody goal)
        {
            List<CelestialBody> returnBodies = new List<CelestialBody>();

            ALGABfSearch bfSearch = new ALGABfSearch();
            List<Vertex> vertices = new List<Vertex>();

            Vertex Vroot = new Vertex(root.Name, root.Neighbours);
            Vertex Vgoal = new Vertex(goal.Name, goal.Neighbours);

            foreach (CelestialBody body in bodies)
            {
                body.IsMarked = false;
                vertices.Add(new Vertex(body.Name, body.Neighbours));
            }

            Path path = bfSearch.BreathfirstSearch(vertices, Vroot, Vgoal);
            returnBodies = bodies.ToList();

            foreach (Vertex vertex in path.getPath())
            {
                foreach (CelestialBody celestialBody in bodies)
                {
                    if (celestialBody.Name != null && celestialBody.Name.Equals(vertex.Name))
                        celestialBody.IsMarked = true;
                }
            }
            return bodies;
        }
    }
}
