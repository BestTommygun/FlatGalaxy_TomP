using FlatGalaxy.Model;
using FlatGalaxy.Model.BreathFirstSSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP.Controllers
{
    public class ALGABfSearch
    {
        //TODO: maybe refactor to return null if not succeeded
        //returns the path taken and if it succeeded or not
        /*
         * public Tuple<Dictionary<string, CelestialBody>, bool> BreathfirstSearch(List<CelestialBody> bodies, string point1, string point2)
        {

            Tuple<Dictionary<string, CelestialBody>, bool> returnTuple;

            if (bodies.Last() != null)
            {
                returnTuple = new Tuple<Dictionary<string, CelestialBody>, bool>(bodies.Last(), true);
            }
            else
                returnTuple = new Tuple<Dictionary<string, CelestialBody>, bool>(bodies, false);


            return returnTuple;
        }
        //recursive
        private Dictionary<string, CelestialBody> search(Dictionary<string, CelestialBody> bodies, string point1, string point2)
        {

            return new Dictionary<string, CelestialBody>();
        }
        */

        public Func<T, List<T>> ShortestPathFunction<T>(BFGraph<T> graph, T start) //should be generic for reusability
        {
            var previous = new Dictionary<T, T>();

            var queue = new Queue<T>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var neighbor in graph.AdjacencyList[vertex])
                {
                    if (previous.ContainsKey(neighbor))
                        continue;

                    previous[neighbor] = vertex;
                    queue.Enqueue(neighbor);
                }
            }

            Func<T, List<T>> shortestPath = v => {
                var path = new List<T> { };

                var current = v;
                while (!current.Equals(start))
                {
                    path.Add(current);
                    current = previous[current];
                };

                path.Add(start);
                path.Reverse();

                return path;
            };

            return shortestPath;
        }
    }
}
