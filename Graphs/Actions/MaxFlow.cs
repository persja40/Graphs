using Graphs.Data;
using Graphs.Extensions;
using Graphs.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Graphs.Actions
{
    public class MaxFlow
    {
        public MaxFlow(GraphMatrix g)
        {
            int nodes = g.nodesNr;
            FlowMatrix = new int[nodes, nodes];
        }
        public static int findMaxFlow(GraphMatrix g);
        int[,] getFlowMatrix()
        {
            return getFlowMatrix;
        }
        public static int findMinWeight(List<int> lista, int[,] weights)
        {
            int max;
            return max;
        }
        public static void odejmijWageOdTrasy(List<int> trasa, int[,] weights, int weight);
        public static void createFlowMatrix(int[,] weights, int[,] weightsResult);
        public static void turnRoute(List<int> trasa, int[,] weights, int weight);
        public static List<int> createFlowRoute(int[,] weights, int size)
        {
            LinkedList<List<int>> deque = new LinkedList<List<int>>();
            List<int> temp = new List<int>();
            temp.Add(0);
            deque.AddLast(temp);
            int back;
            while (deque.Count != 0)
            {
                temp = deque.First;
                deque.RemoveFirst();
                back = temp.Last;
                if (back == size - 1)
                    return temp;
                for(int i = 0; i < size; ++i)
                {
                    if (weights[back, i] != 0 && temp.Find(i) == temp.Last)
                    {
                        temp.Add(i);
                        deque.AddLast(tmep);
                        temp.RemoveAt(temp.Count - 1);
                    }
                }
                
            }

            return new List<int>();
        }

        private int[,] FlowMatrix;
    }
}
