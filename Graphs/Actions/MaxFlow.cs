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
            weightMatrix = new int[nodes, nodes];
            for (int i = 0; i < nodes; ++i)
            {
                for (int j = 0; j < nodes; ++j)
                {
                    weightMatrix[i, j] = g.getWeight(i, j);
                }
            }
        }

        public int findMaxFlow(GraphMatrix g)
        {
            int size = g.nodesNr;
            int max;
            int min;
            List<int> tempList = new List<int>();
            int[,] tempWeightMatrix = new int[nodes, nodes];
            for (int i = 0; i < nodes; ++i)
            {
                for (int j = 0; j < nodes; ++j)
                {
                    tempWeightMatrix[i, j] = weightMatrix[i, j];
                }
            }

            do
            {
                tempList = createFlowRoute(tempWeightMatrix, size);
                if (tempList.Count > 1)
                {
                    min = findMinWeight(tempList);
                    if (tempList.Count > 1)
                        turnRoute(tempList, min);
                    odejmijWageOdTrasy(tempList, min);
                    max += min;
                }
                else
                {
                    break;
                }
            } while (tempList.Count != 0);

            createFlowMatrix(weightMatrix, tempWeightMatrix, size);

            return max;
        }

        public int[,] getFlowMatrix()
        {
            return FlowMatrix;
        }

        public int findMinWeight(List<int> route)
        {
            int min = 1000;
            int j = 1;
            for (int i = 0; i < route.Count - 1; ++i)
            {
                if (weightMatrix[route[i], route[j]] < min)
                    min = weightMatrix[route[i], route[j]];
                ++j;
            }
            return min;
        }

        public void odejmijWageOdTrasy(List<int> trasa, int weight)
        {
            int j = 1;
            for (int i = 0; i < trasa.Count - 1; ++i)
            {
                weightMatrix[trasa[i], trasa[j]] -= weight;
                ++j;
            }
        }

        public void createFlowMatrix(int[,] weights, int[,] weightsResult, int size)
        {
            for(int i = 0; i < size; ++i)
            {
                for(int j = 0; j < size; ++j)
                {
                    FlowMatrix[i, j] = weights[i, j] - weightsResult[i, j];
                }
            }
        }

        public void turnRoute(List<int> trasa, int weight)
        {
            int j = 1;
            for(int i = 0; i < trasa.Count - 1; ++i)
            {
                //trasa[j],trasa[i], weight);
                weightMatrix[trasa[j], trasa[i]] = weight;
                ++j;
            }
        }

        public List<int> createFlowRoute(int[,] weights, int size)
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
                    if (weights[back, i] != 0 && !temp.Contains(i))
                    {
                        temp.Add(i);
                        deque.AddLast(temp);
                        temp.RemoveAt(temp.Count - 1);
                    }
                }
                
            }

            return new List<int>();
        }

        private int[,] FlowMatrix;

        private int[,] weightMatrix;
    }
}
