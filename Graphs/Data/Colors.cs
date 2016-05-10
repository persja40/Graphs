using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graphs.Data
{
    public class GraphColors
    {
        public static Color[] Values = new Color[]
        {
            Colors.White,
            Colors.Red,
            Colors.Blue,
            Colors.Green,
            Colors.Yellow,
            Colors.Purple,
            Colors.Pink,
            Colors.Brown,
            Colors.Gray,
            Colors.DarkBlue,
            Colors.DarkRed,
            Colors.DarkGreen,
            Colors.LightBlue,
            Colors.LightGreen,
            Colors.Black
        };

        private static Random rand = new Random();

        public static Color Random(int number)
        {
            int index = number - Values.Count();

            while (Randoms.Count <= index)
                Randoms.Add(null);
            if (Randoms[index] == null)
                Randoms[index] = Color.FromArgb(255, (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255));

            return Randoms[index].Value;
        }

        public static  List<Color?> Randoms { get; set; } = new List<Color?>();
    }
}
