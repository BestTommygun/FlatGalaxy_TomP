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
        //returns the path taken and null if no path leads to the goal
        //vertex = node
        //edge = pad

        public Path BreathfirstSearch(List<Vertex> bodies, Vertex root, Vertex goal)
        {
            Tuple<Vertex, Path> tuple = new Tuple<Vertex, Path>(root, new Path());
            Queue<Tuple<Vertex, Path>> queue = new Queue<Tuple<Vertex, Path>>();
            queue.Enqueue(tuple);

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
                        Vertex body = bodies.Where(b => b.Name == vertex).FirstOrDefault();
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
