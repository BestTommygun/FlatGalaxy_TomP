using System;
using System.Collections.Generic;
using System.Text;

namespace FlatGalaxy.Model.BreathFirstSSearch
{
    //nodig? neighbours is al een edge
    public class BFGraph<T> //TODO: this should be generic since it's a seperate library
    {
        //class should have edges and vertexes maybe?
        //Helper for my creator: Vertex = "Knooppunt", Edge = pad van knooppunt naar knooppunt

        public BFGraph(List<T> vertices, List<Tuple<T, T>> edges)
        {
            AdjacencyList = new Dictionary<T, HashSet<T>>();

            foreach (var vertex in vertices)
            {
                AddVertex(vertex);
            }

            foreach (var edge in edges)
            {
                AddEdge(edge);
            }
        }

        public Dictionary<T, HashSet<T>> AdjacencyList { get; }

        public void AddVertex(T vertex)
        {
            AdjacencyList[vertex] = new HashSet<T>();
        }

        public void AddEdge(Tuple<T, T> edge)
        {
            if (AdjacencyList.ContainsKey(edge.Item1) && AdjacencyList.ContainsKey(edge.Item2))
            {
                AdjacencyList[edge.Item1].Add(edge.Item2);
                AdjacencyList[edge.Item2].Add(edge.Item1);
            }
        }
    }
}
