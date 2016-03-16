using Graphs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Actions
{
    public static class Converter
    {
        public static GraphMatrix ConvertToMatrix(GraphMatrixInc from)
        {
            GraphMatrix x = new GraphMatrix(from.NodesNr);
            for (int i = 0; i < from.NodesNr; i++)
                for (int j = 0; j < from.NodesNr; j++)
                    if (from.GetConnection(i, j))
                        x.MakeConnection(i, j);
            return x;
        }
        public static GraphMatrix ConvertToMatrix(GraphList from)
        {
            return ConvertToMatrix(ConvertToMatrixInc(from));
        }

        public static GraphMatrixInc ConvertToMatrixInc(GraphMatrix from)
        {
            return ConvertToMatrixInc(ConvertToList(from));
        }

        public static GraphMatrixInc ConvertToMatrixInc(GraphList from)
        {
            int sumc = 0;
            for (int i = 0; i < from.NodesNr; i++)
                sumc += from.CountElem(i);
            sumc = sumc / 2;
            GraphMatrixInc q = new GraphMatrixInc(from.NodesNr, sumc);
            int c = 0;
            for (int i = 0; i < from.NodesNr; i++)//pobiera po kolei elementy, dodaje do matrixinc i usuwa z listy
                for (int j = 0; j < from.NodesNr; j++)
                {
                    if (from.GetConnection(i, j))
                    {
                        q.MakeConnection(i, j, c);
                        c++;
                        from.RemoveConnection(i, j);
                    }
                }
            return q;
        }

        public static GraphList ConvertToList(GraphMatrix from)
        {
            GraphList q = new GraphList(from.NodesNr);
            for (int i = 0; i < from.NodesNr; i++)
                for (int j = 0; j < i; j++)
                    if (from.GetConnection(i, j))
                        q.MakeConnection(i, j);
            return q;
        }

        public static GraphList ConvertToList(GraphMatrixInc from)
        {
            return Converter.ConvertToList(Converter.ConvertToMatrix(from));
        }
        /// <summary>
        /// SKIEROWANE KONWERSJE
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public static SGraphMatrix ConvertToSMatrix(SGraphMatrixInc from)
        {
            SGraphMatrix x = new SGraphMatrix(from.NodesNr);
            for (int i = 0; i < from.NodesNr; i++)
                for (int j = 0; j < from.NodesNr; j++)
                    if (from.GetConnection(i, j))
                        x.MakeConnection(i, j);
            return x;
        }
        public static SGraphList ConvertToSList(SGraphMatrix from)
        {
            SGraphList x = new SGraphList(from.NodesNr);
            for (int i = 0; i < from.NodesNr; i++)
                for (int j = 0; j < from.NodesNr; j++)
                    if (from.GetConnection(i, j))
                        x.MakeConnection(i, j);
            return x;
        }
        public static SGraphMatrixInc ConvertToSMatrixInc(SGraphList from)
        {
            int sumc = 0;
            for (int i = 0; i < from.NodesNr; i++)
                sumc += from.CountElem(i);
            int c = 0;
            SGraphMatrixInc x = new SGraphMatrixInc(from.NodesNr,sumc);
            for (int i = 0; i < from.NodesNr; i++)//pobiera po kolei elementy, dodaje do matrixinc i usuwa z listy
                for (int j = 0; j < from.NodesNr; j++)               
                    if (from.GetConnection(i, j))
                    {
                        x.MakeConnection(i, j, c);
                        c++;
                        from.RemoveConnection(i, j);
                    }
            return x;
        }
    }
}
