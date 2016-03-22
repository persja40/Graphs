using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Actions
{

    class Vertice // klasa reprezentujaca wierzcholek
    {
        public Vertice(int id, int deg)
        {
            _id = id;
            _deg = deg;
        }
        public int _id; // id wierzcholka
        public int _deg; // stopien wierzcholka
    }

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
            for (int i = 0; i < nodes; ++i) // wyzerowanie macierzy sasiedztwa
            {
                for (int j = 0; j < nodes; ++j)
                    connections[i, j] = 0;
            }

            bool flag = false;

            for (int i = 0; i < nodes; ++i)
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
                List<Vertice> lista = new List<Vertice>(); //lista zawierajaca obiekty(wierzcholki)
                degrees = degrees.OrderByDescending(x => x).ToList();
                for (int i = 0; i < nodes; ++i) // tworzenie obiektow wraz z przypisaniem id i stopnia
                    lista.Add(new Vertice(i, degrees[i]));
                int deg = 0;
                int id = 0;
                while (lista.Count != 0)
                {
                    deg = lista[0]._deg; // przypisanie zmiennej deg stopnia wierzcholka(o najwyzszym stopniu)
                    id = lista[0]._id; // przypisanie zmiennej id - _id wierzcholka o najwyzszym stopniu
                    lista.RemoveAt(0); // usuwamy pierwszy element listy
                    for (int i = 0; i < deg; ++i)
                    {
                        connections[id, lista[i]._id] = 1; // tworzymy polaczenie pomiedzy usunietym juz wierzcholkiem a pozostałymi na liscie
                        connections[lista[i]._id, id] = 1;
                        lista[i]._deg -= 1; // zmniejszamy stopien wierzcholka z listy
                    }
                    lista = lista.OrderByDescending(x => x._deg).Cast<Vertice>().ToList(); // ponowne sortowanie listy     
                }
            }
            else
            {
                for (int i = 0; i < nodes; ++i)
                {
                    if (i == nodes - 1)
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
        }
        /// <summary>
        /// Generacja listy zawierajacej nr maksymalnie spojnych wierzcholkow
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public static List<int> ListaSpojny(GraphMatrix from)
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
            return lista;
            //stworz graf z listy największej dlugosci        
        }
        /// <summary>
        /// Generuje maksymalnie spojny graf uzuwajac ListaSpojny()
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public static GraphMatrix MakeSpojny(GraphMatrix from) {
            List<int> lista = ListaSpojny(from);
            GraphList wynik = new GraphList(from.NodesNr);
            for (int i = 0; i < lista.Count; i++)
                for (int j = 0; j < lista.Count; j++)
                    if (from.GetConnection(lista[i], lista[j]))
                        wynik.MakeConnection(lista[i], lista[j]);
            return GraphMatrix.Free(wynik);
        }

        /// <summary>
        /// przeszukiwanie grafu w glab operacje dokonujemy na liscie
        /// </summary>
        /// <param name="x">Graf ktory przeszukujemy</param>
        /// <param name="e">element w ktorym jestesmy</param>
        /// <param name="z">referencyna lista do zapisywania wierzcholkow spojnych</param>
        private static void rek(GraphList x, int e, List<int> z)//rekurencyjne przeszukiwanie grafu
        {
            for (int i = 0; i < x.GetConnections(e).Count; i++)
                if (!z.Contains(x.GetConnections(e)[i]))
                {
                    z.Add(x.GetConnections(e)[i]);
                    rek(x, x.GetConnections(e)[i], z);
                }
        }
        /// <summary>
        /// Tworzenie macierzy odleglosci pomiedzy wszystkimi parami wierzcholkow w grafie spojnym
        /// </summary>
        /// <param name="from"></param> Graf, w ktorym liczymy odleglosci miedzy wszystkimi parami wierzcholkow
        /// <returns></returns> Macierz odleglosci miedzy wszystkimi parami wierzcholkow
        public static int[,] distancesMatrix(GraphMatrix from)
        {
            GraphMatrix g = MakeSpojny(from);
            int nodes = from.NodesNr;
            int[,] distances = new int[nodes, nodes];
            for (int i = 0; i < nodes; ++i)
            {
                for (int j = 0; j < nodes; ++j)
                    distances[i, j] = 0;
            }
            for (int i = 0; i < nodes; ++i)
            {
                for(int j = 0; j < nodes; ++j)
                {
                    if (i == j) distances[i, j] = 0;
                    else if (distances[i, j] != 0) continue;
                    else
                    {
                        List<int> path = PathFinding.Dijkstra(g, i, j);
                        distances[i, j] = distances[j, i] = path.Sum(x => x);
                        path.Clear();
                    }
                }
            }
            return distances;
                

        }




    }
}



//x.GetConnections(e)[i]