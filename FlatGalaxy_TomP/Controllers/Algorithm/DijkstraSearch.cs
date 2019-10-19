using FlatGalaxy.Model;
using FlatGalaxy.Model.Behaviour.ALGA.Dijkstra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP.Controllers.Algorithm
{
    public class DijkstraSearch : IPathingAlgorithm
    {
        /// <summary>
        /// Finds the shortest path based on X and Y values between 2 CelestialBodies
        /// </summary>
        /// <param name="bodies">All the bodies in the simulation</param>
        /// <param name="root">The starting body</param>
        /// <param name="goal">The ending body</param>
        /// <returns>A list off all the bodies it needs to reach goal from root</returns>
        public List<CelestialBody> GetPath(List<CelestialBody> bodies, CelestialBody root, CelestialBody goal)
        {
            List<Node> nodes = new List<Node>();

            foreach (CelestialBody body in bodies)
            {
                if(body.Name != null)
                {
                    body.IsMarked = false;
                    nodes.Add(new Node(body.Name, body.Neighbours, body.X, body.Y));
                }
            }

            Node Vroot = new Node(root.Name, root.Neighbours, root.X, root.Y);
            Node Vgoal = new Node(goal.Name, goal.Neighbours, goal.X, goal.Y);

            Dijkstra dijkstra = new Dijkstra();
            HashSet<Node> markedNodes = dijkstra.ExecuteDijkstra(nodes, Vroot, Vgoal);
            foreach (Node node in markedNodes)
            {
                bodies.Single(b => b.Name != null && b.Name.Equals(node.Name)).IsMarked = true;
            }
            return bodies;
        }
    }
}
