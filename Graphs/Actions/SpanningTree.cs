using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Actions
{
    public class EdgeWage
    {
        public int Length { get; private set; }
        public int Point1 { get; private set; }
        public int Point2 { get; private set; }
        public EdgeWage(int pt1, int pt2, int length)
        {
            Point1 = pt1;
            Point2 = pt2;
            Length = length;
        }
        public override string ToString()
        {
            return String.Format("({0}-{1})={2:0.00}", Point1, Point2, Length);
        }
    }
    public class Kruskal
    {
        public static void SpanTree(int[][] matrixwage){//interfejs
            var kruskal = new Kruskal(matrixwage);
            for (int i = 0; i < kruskal.Result.Length; i++)
                Console.WriteLine(kruskal.Result[i]);
            Console.WriteLine(String.Format("Rozpiętość {0:0.00}", kruskal.Span));
        }
        public EdgeWage[] Result { get; private set; }
        public double Span { get; private set; }
        public Kruskal(int[][] matrixwage)
        {
            int edgesArrayLength = 0;

            for (int i = matrixwage[0].Length - 1; i > 0; i--)
                edgesArrayLength += i;
            EdgeWage[] edges = new EdgeWage[edgesArrayLength];

            for (int i = 0, index = 0; i < matrixwage[0].Length; i++)
                for (int j = i + 1; j < matrixwage[i].Length; j++)
                {
                    if(matrixwage[i][j]==0)
                        edges[index] = new EdgeWage(i, j, 9999);
                    else edges[index] = new EdgeWage(i, j, matrixwage[i][j]);
                    index++;
                }
            

            var sortEdges = edges.OrderBy(a => a.Length);

            int[] sets = new int[matrixwage[0].Length];
            Result = new EdgeWage[matrixwage[0].Length - 1];
            int processedEdges = 0;
            foreach (var edge in sortEdges)
            {

                if (processedEdges == matrixwage[0].Length - 1)
                    break;


                if (sets[edge.Point1] == 0 || sets[edge.Point1] != sets[edge.Point2])
                {
                    Result[processedEdges] = edge;
                    Span += edge.Length;
                    processedEdges++;

                    if (sets[edge.Point1] != 0 || sets[edge.Point2] != 0)
                    {
                        //Te zbiory będą łączone w jeden!
                        int set1 = sets[edge.Point1];
                        int set2 = sets[edge.Point2];

                        for (int i = 0; i < matrixwage[0].Length; i++)
                            if (sets[i] != 0 && (sets[i] == set1 || sets[i] == set2))
                                sets[i] = processedEdges;
                    }

                    sets[edge.Point1] = processedEdges;
                    sets[edge.Point2] = processedEdges;
                }
            }
        }
    }
}
