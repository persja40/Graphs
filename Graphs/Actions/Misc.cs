﻿using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Actions
{
    public class Misc
    {
        /// <summary>
        /// Sprawdza czy graf z danymi stopniami wierzchołków może istnieć
        /// Algorytm : https://en.wikipedia.org/wiki/Havel%E2%80%93Hakimi_algorithm
        /// </summary>
        /// <param name="degrees">lista ze stopniami wierzchołków</param>
        /// <returns>true jesli istnieje</returns>
        public static bool Exists(List<int> degrees)
        {
            int count = 0; // liczba wierzcholkow
            foreach (var list in degrees)
                ++count;
            int sum = 0; // suma stopni, do sprawdzenia czy jest parzysta
            for (int i = 0; i < count; ++i)
                sum += degrees[i];
            if (sum % 2 != 0)
                return false;
            bool b = false; // wartosc logiczna do okreslenia czy lista nie zawiera samych zer
            for (int i = 0; i < count; ++i)
            {
                if (degrees[i] == 0)
                    b = true;
                else
                {
                    b = false;
                    break;
                }
            }
            if (b)
                return true;
            bool c = false; // wartosc logiczna do okreslenia czy ktorys ze stopni nie jest ujemny
            for (int i = 0; i < count; ++i)
            {
                if (degrees[i] >= 0)
                {
                    c = true;
                }
                else
                {
                    c = false;
                    break;
                }
            }
            if (!c)
                return false;
            List<int> result = degrees.OrderByDescending(x => x).ToList(); // result - posortowana malejaco lista
            int d1 = result[0];
            result.RemoveAt(0); // usuniecie pierwszego wierzcholka (o najwiekszym stopniu)
            if (d1 > (count - 1))
                return false;
            else {
                for (int i = 0; i < d1; ++i)
                    result[i] -= 1;
                return Exists(result);// wywolanie rekurencyjne
            }
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Konstruuje graf prosty
        /// </summary>
        /// <param name="degrees">Lista ze stopniami wierzchołków</param>
        /// <returns>Graf</returns>
        public static GraphMatrix Construct(List<int> degrees)
        {
                int nodes = degrees.Count; // liczba wierzcholkow
                int[,] connections = new int[nodes, nodes]; // macierz sasiedztwa
                for(int i = 0; i < nodes; ++i) // wyzerowanie macierzy sasiedztwa
                {
                    for (int j = 0; j < nodes; ++j)
                        connections[i, j] = 0;
                }

                //List<int> max = new List<int>(degrees);
                bool flag = false;
                
                for(int i = 0; i < nodes; ++i)
                {
                    if (degrees[i] != 2)
                    {
                        flag = false;
                        break;
                    }
                    else flag = true;

                }


            if (!flag)
            {

                for (int i = 0; i < nodes; ++i)
                {
                    for (int j = 0; j < nodes; ++j)
                    {
                        if ((degrees[i] > 0) && (degrees[j] > 0)) // sprawdzenie czy jest jeszcze mozliwe przylaczenie krawedzi do danego wierzcholka
                        {
                            if (i == j) // upewnienie sie zeby nie polaczyc wierzcholka ze soba samym
                                connections[i, j] = 0;
                            else
                            {
                                connections[i, j] = 1; // tworzenie polaczenia pomiedzy wierzcholkami o numerach i,j
                                connections[j, i] = 1;
                                degrees[i] -= 1; // po utworzeniu polaczenia miedzy wierzcholkami i,j zmniejszamy ich stopnie o 1.
                                degrees[j] -= 1;
                                /* if (degrees[j] == max[j] - 1)
                                 {
                                     for (int k = 0; k < j - 1; ++j)
                                     {
                                         if (degrees[k] == 0) { flag = false; break; }
                                         else flag = true;
                                     }
                                     if (flag)
                                     {
                                         connections[i, j] = 1; // tworzenie polaczenia pomiedzy wierzcholkami o numerach i,j
                                         connections[j, i] = 1;
                                         degrees[i] -= 1; // po utworzeniu polaczenia miedzy wierzcholkami i,j zmniejszamy ich stopnie o 1.
                                         degrees[j] -= 1;
                                     }
                                     else continue;

                                 }*/




                            }
                        }
                    }
                }
            }

            else
            {
                for(int i = 0; i < nodes; ++i)
                {
                    if(i == nodes - 1)
                    {
                        connections[0, i] = 1;
                        connections[i, 0] = 1;
                    }
                    else
                    {
                        connections[i, i + 1] = 1;
                        connections[i + 1, i] = 1;
                    }
                }



            }
                return new GraphMatrix(nodes, connections);
            
            //throw new NotImplementedException();
        }
        public static GraphMatrix Spojny(GraphMatrix from)
        {
            GraphList kl = from;
            List<int> lista = new List<int>();
            for (int i = 0; i < from.NodesNr; i++)//wszystkie wezly sa poczatkiem
            {
                List<int> q = new List<int>();
                q.Add(i);
                rek(kl, i, q);
                if (lista.Count < q.Count)
                    lista = q;
            }
            //stworz graf z listy największej dlugosci
            GraphList wynik = new GraphList(from.NodesNr);
            for (int i = 0; i < lista.Count; i++)
                for (int j = 0; j < lista.Count; j++)
                    if (from.GetConnection(lista[i], lista[j]))
                        wynik.MakeConnection(lista[i], lista[j]);
            return GraphMatrix.Free( wynik);
        }
        private static void rek(GraphList x, int e, List<int> z)//rekurencyjne przeszukiwanie grafu
        {
            for (int i = 0; i < x.GetConnections(e).Count; i++)
                if (!z.Contains(x.GetConnections(e)[i]))
                {
                    z.Add(x.GetConnections(e)[i]);
                    rek(x, x.GetConnections(e)[i], z);
                }
        }
    }
}
//x.GetConnections(e)[i]