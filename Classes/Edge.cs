using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Classes
{
   public class Edge
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public int Weight { get; set; }

        public Edge(string source, string destination, int weight)
        {
            Source = source;
            Destination = destination;
            Weight = weight;
        }

        public Edge() { }

        
    }
}
