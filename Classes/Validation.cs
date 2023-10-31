using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Validation 
    {

        public bool EdgeName(string graphName) {

            foreach (char symbol in graphName)
            {
                if (!Char.IsLetterOrDigit(symbol))
                    return false;
                
                
            }

            return true;

        }
        public bool EdgeWeight(int weight)
        {
            if (weight >= 0)
                return true;


            return false;
        }

         


    }
}
