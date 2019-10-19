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
        /// <summary>
        /// Converts the CelestialBodies to Node elements and passes this to the breathfirstsearch class
        /// </summary>
        /// <param name="bodies">A list of all the bodies in the simulations</param>
        /// <param name="root">The starting body</param>
        /// <param name="goal">The ending body</param>
        /// <returns>An adjusted list of all elements in the simulation</returns>
        public List<CelestialBody> GetPath(List<CelestialBody> bodies, CelestialBody root, CelestialBody goal)
        {
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

            //Mark the bodies based on the path
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
