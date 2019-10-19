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
        /// <summary>
        /// Finds the shortest path based on the edges off a vertex
        /// </summary>
        /// <param name="bodies">A list off all the vertices in the simulation</param>
        /// <param name="root">The starting vertice</param>
        /// <param name="goal">The ending vertice</param>
        /// <returns>A Path object which contains all the vertices that the search needs to reach goal from root</returns>
        public Path BreathfirstSearch(List<Vertex> vertices, Vertex root, Vertex goal)
        {
            Path startingPath = new Path();
            startingPath.add(root);

            Tuple<Vertex, Path> startingTuple = new Tuple<Vertex, Path>(root, startingPath);
            Queue<Tuple<Vertex, Path>> queue = new Queue<Tuple<Vertex, Path>>();
            queue.Enqueue(startingTuple);

            while(queue.Count > 0)
            {
                Tuple<Vertex, Path> queueItem = queue.Dequeue();
                Vertex curBody = queueItem.Item1;
                Path path = queueItem.Item2;

                if (curBody.Name.Equals(goal.Name))
                    return path;

                foreach (string vertex in curBody.Vertices)
                {
                    if (!path.getPath().Select(p => p.Name).Contains(vertex))
                    {
                        //visited this vertex
                        Vertex body = vertices.Where(b => b.Name == vertex).FirstOrDefault();
                        Path newPath = new Path(path);
                        newPath.add(body);
                        queue.Enqueue(new Tuple<Vertex, Path>(body, newPath));
                    }
                }
            }
            return null;
        }
    }
}
