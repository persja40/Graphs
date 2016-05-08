using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs.Actions;
using Graphs.Misc;
using Graphs.Data;


namespace Graphs.Actions
{
    /// <summary>
    /// Klasa reprezentujaca maksymalny przeplyw przez siec
    /// </summary>
    public class MaxFlow1
    {
        public MaxFlow1(DirectedGraphMatrix g)
        {
            int nodes = g.NodesNr;
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
        /// <summary>
        /// Metoda ktora oblicza maksymalny przeplyw w digrafie na podstawie jego przepustowosci(wag-nieujemnych)
        /// </summary>
        /// <param name="g"></param> siec - czyli digraf
        /// <returns></returns> max - maksymalny przeplyw
        public int findMaxFlow(DirectedGraphMatrix g)
        {
            int nodes = g.NodesNr;
            int max = 0;
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
                tempList = createFlowRoute(weightMatrix, nodes);
                if (tempList.Count > 1)
                {
                    int min = findMinWeight(tempList);
                    if (tempList.Count > 1)
                    {
                        turnRoute(tempList, min);
                    }
                    odejmijWageOdTrasy(tempList, min);
                    max += min;
                }
                else
                {
                    break;
                }
            } while (tempList.Count != 0);

            createFlowMatrix(weightMatrix, tempWeightMatrix, nodes);

            return max;
        }

        public int[,] getFlowMatrix()
        {
            return FlowMatrix;
        }
        /// <summary>
        /// Metoda ktora wyznacza minimalna wage w danej sciezce
        /// </summary>
        /// <param name="route"></param> sciezka w ktorej znajdujemy minimalna wage
        /// <returns></returns> min - minimalna waga
        public int findMinWeight(List<int> route)
        {
            int min = 10000;
            int j = 1;
            for (int i = 0; i < route.Count - 1; ++i)
            {
                if (weightMatrix[route[i], route[j]] < min)
                {
                    min = weightMatrix[route[i], route[j]];
                }
                ++j;
            }
            return min;
        }
        /// <summary>
        /// Metoda ktora odejmuje minimalna wage z calej trasy od kazdej krawedzi zawartej w tej trasie
        /// </summary>
        /// <param name="trasa"></param> sciezka od zrodla do ujscia
        /// <param name="weight"></param> minimalna waga w znalezionej sciezce, ktora odejmujemy od calej sciezki(kazda krawedz sciezki zostaje pomniejszona o weight)
        public void odejmijWageOdTrasy(List<int> trasa, int weight)
        {
            int j = 1;
            for (int i = 0; i < trasa.Count - 1; ++i)
            {
                weightMatrix[trasa[i], trasa[j]] -= weight;
                ++j;
            }
        }

        public void createFlowMatrix(int[,] weights, int[,] weightsResult, int siz)
        {
            for (int i = 0; i < siz; ++i)
            {
                for (int j = 0; j < siz; ++j)
                {
                    FlowMatrix[i, j] = weights[i, j] - weightsResult[i, j];
                }
            }
        }
        /// <summary>
        /// Metoda ktora tworzy trase przeciwna do znalezionej sciezki metoda createFlowRoute, przepustowosci w sciezce przeciwnej zostaja zwiekszone o weight
        /// </summary>
        /// <param name="trasa"></param> sciezka od zrodla do ujscia
        /// <param name="weight"></param> minimalna waga danej sciezki od zrodla do ujscia
        public void turnRoute(List<int> trasa, int weight)
        {
            int j = 1;
            for (int i = 0; i < trasa.Count - 1; ++i)
            {
                weightMatrix[trasa[j], trasa[i]] += weight;
                ++j;
            }
        }
        /// <summary>
        /// Metoda ktora tworzy sciezke od zrodla do ujscia na podstawie przepustowosci
        /// </summary>
        /// <param name="weights"></param> macierz wag(przepustowosci) sieci
        /// <param name="siz"></param> liczba wierzcholkow sieci
        /// <returns></returns>
        public List<int> createFlowRoute(int[,] weights, int siz)
        {
            List<List<int>> deque = new List<List<int>>();
            List<int> temp = new List<int>();
            temp.Add(0);
            deque.Add(temp);
            int back = 0;
            while (deque.Count > 0)
            {
                List<int> tempcopy = new List<int>(deque.First());
                deque.RemoveAt(0);
                back = tempcopy.Last();
                if (back == siz - 1)
                {
                    return tempcopy;
                }
                for (int i = 0; i < siz; ++i)
                {
                    if ((weights[back, i] > 0) && (tempcopy.Contains(i) == false))
                    {
                        tempcopy.Add(i);
                        deque.Add(new List<int>(tempcopy));
                        tempcopy.RemoveAt(tempcopy.Count - 1);
                    }
                }
            }

            return new List<int>();

        }

        private int[,] FlowMatrix; // Macierz przeplywu

        private int[,] weightMatrix; // Macierz przepustowosci sieci
    }
}