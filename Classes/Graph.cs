using Graphs.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs.Classes
{


   public class Graph
    {

        public Dictionary<string, List<Edge>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<string, List<Edge>>();
        }

        public void AddEdge(Edge edge)
        {
            if (!adjacencyList.ContainsKey(edge.Source))
                adjacencyList[edge.Source] = new List<Edge>();

           
            if (!adjacencyList.ContainsKey(edge.Destination))
                adjacencyList[edge.Destination] = new List<Edge>();

            adjacencyList[edge.Source].Add(edge);
        }

                public List<List<Edge>> FindAllPaths()
                {
                    List<List<Edge>> paths = new List<List<Edge>>();
                    var allVertices = adjacencyList.Keys.ToList();

                    foreach (var startVertex in allVertices)
                    {
                        foreach (var endVertex in allVertices)
                        {
                            if (startVertex != endVertex)
                            {
                                List<Edge> path = new List<Edge>();
                                HashSet<string> visited = new HashSet<string>();

                                DFS(startVertex, endVertex, path, paths, visited);
                            }
                        }
                    }

                    return paths;
                }

        public List<Edge> FindBestPath(List<List<Edge>> allPaths, int targetWeight)
        {
            List<Edge> bestPath = null;
            int minWeightDifference = int.MaxValue;

            foreach (var path in allPaths)
            {
                int totalWeight = path.Sum(e => e.Weight);
                int weightDifference = Math.Abs(totalWeight - targetWeight);

                if (weightDifference < minWeightDifference)
                {
                    minWeightDifference = weightDifference;
                    bestPath = path.ToList();
                    bestPath.RemoveAt(0); 
                }
            }

            return bestPath;
        }

    

        private void DFS(string current, string end, List<Edge> path, List<List<Edge>> paths, HashSet<string> visited)
        {
            visited.Add(current);

            if (path.Any())
            {
                var lastEdge = path.Last();
              
                if (current != lastEdge.Destination)
                {
                   
                    path.Add(new Edge(current, lastEdge.Destination, adjacencyList[lastEdge.Destination].First().Weight));
                }
            }
            else
            {    
                path.Add(new Edge(current, current, 0));
            }

            if (current == end)
            {              
                paths.Add(new List<Edge>(path));
            }
            else
            {
                foreach (var edge in adjacencyList[current])
                {
                    if (!visited.Contains(edge.Destination))
                    {
                        DFS(edge.Destination, end, path, paths, visited);
                    }
                }
            }

            visited.Remove(current);
            path.RemoveAt(path.Count - 1); 
        }



    }

}