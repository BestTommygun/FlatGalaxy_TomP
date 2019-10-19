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

        /// <summary>
        /// Finds the shortest root between a root and goal Node based on the X and Y values
        /// </summary>
        /// <param name="nodes">All the nodes in the simulation</param>
        /// <param name="root">The starting Node</param>
        /// <param name="goal">The Goal Node</param>
        /// <returns>A HashSet containing all nodes that the shortest path between root and goal contains</returns>
        public HashSet<Node> ExecuteDijkstra(List<Node> nodes, Node root, Node goal)
        {
            _nodes = nodes;
            Root = root;
            Goal = goal;
            Node goalNode = DijkstraSearch();

            //converts the linked list to a HashSet of nodes, because From is a List<Node> multiple paths are possible
            HashSet<Node> returnNodes = new HashSet<Node>();
            returnNodes.Add(root);
            returnNodes.Add(goal);
            List<Node> todo = new List<Node>();
            while (goalNode.From != null)
            {
                //Add all the from nodes to the todo list
                todo.AddRange(goalNode.From);
                foreach (Node fromNode in goalNode.From)
                {
                    returnNodes.Add(fromNode); //add all from nodes to the returning list
                }
                goalNode = todo.FirstOrDefault(); //change node to the first on the todo list
                todo.Remove(todo.FirstOrDefault()); //remove current node from the todo list since it will be handled in the next iteration
            }
            return returnNodes;
        }

        /// <summary>
        /// Finds the shortest path between root and goal
        /// </summary>
        /// <returns>Returns the ending node with updated weight and a linked list back to root</returns>
        private Node DijkstraSearch()
        {
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

            //while there are nodes to be visited
            while (todo.Count > 0)
            {
                currentNode = todo.Where(n => n.Weight == todo.Min(m => m.Weight)).FirstOrDefault();

                //if the node doesn't exist start with the next one
                if (currentNode == null)
                    break;
                if (currentNode.Name.Equals(Goal.Name)) // if you have reached the goal, return the node
                    return todo.Single(n => n.Name.Equals(Goal.Name));
                
                //this node has been visited so remove it.
                todo.Remove(currentNode);
                visited.Add(currentNode);

                foreach (string node in currentNode.connectedNodes)
                {
                    var connectedNode = todo.Find(n => n.Name.Equals(node));
                    if (connectedNode != null)
                    {
                        //calculates the weight
                        double deltaX = currentNode.X - connectedNode.X;
                        double deltaY = currentNode.Y - connectedNode.Y;
                        double weight = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)) + currentNode.Weight;

                        if (connectedNode.Weight == weight) //If the weight is equal add this to the from list
                            connectedNode.From.Add(currentNode);
                        if (connectedNode.Weight > weight) //If the weight is smaller replace the from list
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