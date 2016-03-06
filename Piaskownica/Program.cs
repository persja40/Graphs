using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs.Data;
using Graphs.Actions;

namespace Piaskownica
{
    class Program
    {
        static void Main(string[] args)
        {
            int cc = 3;
            GraphMatrix q = GraphGenerator.generatorGER(10,cc);

            for (int i = 0; i < q.NodesNr; i++)
            {//wpisywanie wylosowanych liczb
                for (int j = 0; j < q.NodesNr; j++)
                {
                    if (q.GetConnection(i, j))
                        Console.Write(1);
                    else
                        Console.Write(0);
                    Console.Write(" ; ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("-------------------------------------------------------------");
            
            for (int i = 0; i < 1000; i++)
            {
                if (q.ConnectionCount != cc)
                {
                    Console.WriteLine("Blad");
                    Console.Read();
                }
                q= q.Randomize();
            }
            if (q.ConnectionCount == cc)
                Console.WriteLine("OK");
            for (int i = 0; i < q.NodesNr; i++)
            {//wpisywanie wylosowanych liczb
                for (int j = 0; j < q.NodesNr; j++)
                {
                    if (q.GetConnection(i, j))
                        Console.Write(1);
                    else
                        Console.Write(0);
                    Console.Write(" ; ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("-------------------------------------------------------------");

            //GraphMatrix w = Converter.ConvertToMatrix(Converter.ConvertToMatrixInc(Converter.ConvertToList(q)));
            //Console.WriteLine(por(q,w));
            //int[,] connect = new int[5, 6];
            /*
            for (int i = 0; i < q.NodesNr; i++) {//wpisywanie wylosowanych liczb
                for (int j = 0; j < q.NodesNr; j++)
                {
                    if(q.GetConnection(i, j))
                        Console.Write(1);
                    else
                        Console.Write(0);
                    Console.Write(" ; ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("-------------------------------------------------------------");
            GraphList w = Converter.ConvertToList(q);
            for (int i = 0; i < w.NodesNr; i++)
            {
                Console.Write(i + ":");
                for (int j = 0; j < w.NodesNr; j++)
                {
                    if (w.GetConnection(i, j))
                    {
                        Console.Write(j);
                        Console.Write(" ; ");
                    }
                }
                Console.WriteLine("");
            }
            Console.WriteLine("-------------------------------------------------------------");

            GraphMatrixInc e= Converter.ConvertToMatrixInc(w);
            for (int i = 0; i < e.NodesNr; i++)
            {//wpisywanie wylosowanych liczb
                for (int j = 0; j < e.ConnectNr; j++)
                {
                    if (e.GetConnectionArray(i, j))
                        Console.Write(1);
                    else
                        Console.Write(0);
                    Console.Write(" ; ");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("-------------------------------------------------------------");
            GraphMatrix r = Converter.ConvertToMatrix(e);
            for (int i = 0; i < r.NodesNr; i++)
            {//wpisywanie wylosowanych liczb
                for (int j = 0; j < r.NodesNr; j++)
                {
                    if (r.GetConnection(i, j))
                        Console.Write(1);
                    else
                        Console.Write(0);
                    Console.Write(" ; ");
                }
                Console.WriteLine("");
            }
            */
            /*
            q = q.Randomize();
            Console.WriteLine("jest");
            for (int i = 0; i < 5; i++)
            {//wpisywanie wylosowanych liczb
                for (int j = 0; j < 5; j++)
                {
                    if (q.GetConnection(i, j))
                        Console.Write(1);
                    else
                        Console.Write(0);
                    Console.Write(" ; ");
                }
                Console.WriteLine("");
            }
            */
            //Console.Write(5>3);
            Console.Read();
        }
        static bool por(GraphMatrix a, GraphMatrix b) {
            for (int i = 1; i < b.NodesNr; i++)//wpisywanie wylosowanych liczb
                for (int j = 1; j < i; j++)
                    if (a.GetConnection(i, j) != b.GetConnection(i, j))
                        return false;            
            return 5<3;
        }
    }
}
