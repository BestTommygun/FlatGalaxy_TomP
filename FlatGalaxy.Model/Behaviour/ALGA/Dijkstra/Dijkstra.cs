using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FlatGalaxy.Model.Behaviour.ALGA.Dijkstra
{
    public class Dijkstra
    {
        private List<Node> _nodes;
        public Node Root { get; set; }
        public Node Goal { get; set; }

        public HashSet<Node> ExecuteDijkstra(List<Node> nodes, Node root, Node goal)
        {
            _nodes = nodes;
            Root = root;
            Goal = goal;
            Node goalNode = DijkstraSearch();

            HashSet<Node> returnNodes = new HashSet<Node>();
            returnNodes.Add(root);
            returnNodes.Add(goal);
            List<Node> todo = new List<Node>();
            while (goalNode.From != null)
            {
                todo.AddRange(goalNode.From);
                foreach (Node fromNode in goalNode.From)
                {
                    returnNodes.Add(fromNode);
                }
                goalNode = todo.FirstOrDefault();
                todo.Remove(todo.FirstOrDefault());
            }
            return returnNodes;
        }

        private Node DijkstraSearch()
        {
            Root.Weight = 0;
            Node currentNode;
            List<Node> visited = new List<Node>();
            List<Node> todo = new List<Node>();

            //add all nodes to the todo list to iterate over
            foreach (var node in _nodes)
            {
                if (!node.Name.Equals(Root.Name))
                    todo.Add(node);
                else
                {
                    //because it is root and root has no travel time, weight is 0
                    node.Weight = 0;
                    todo.Add(node);
                }
            }

            //while theres nodes to be visited
            while (todo.Count > 0)
            {
                currentNode = todo.Where(n => n.Weight == todo.Min(m => m.Weight)).FirstOrDefault();

                //if the node doesn't exist start with the next one
                if (currentNode == null)
                    break;
                if (currentNode.Name.Equals(Goal.Name)) // if you have reached the goal, return the node
                    return todo.Single(n => n.Name.Equals(Goal.Name));
                
                todo.Remove(currentNode);
                visited.Add(currentNode);

                foreach (string node in currentNode.connectedNodes)
                {
                    var connectedNode = todo.Find(n => n.Name.Equals(node));
                    if (connectedNode != null)
                    {
                        double deltaX = currentNode.X - connectedNode.X;
                        double deltaY = currentNode.Y - connectedNode.Y;
                        double weight = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)) + currentNode.Weight;

                        if (connectedNode.Weight == weight)
                            connectedNode.From.Add(currentNode);
                        if (connectedNode.Weight > weight)
                        {
                            connectedNode.Weight = weight;
                            List<Node> fromNode = new List<Node>();
                            fromNode.Add(currentNode);
                            connectedNode.From = fromNode;
                        }
                    }
                }
            }
            return null;
        }
    }
}