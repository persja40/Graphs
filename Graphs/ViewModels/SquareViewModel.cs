using Graphs.Data;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graphs.ViewModels
{
    public class SquareViewModel
    {
        public Color Color { get; set; }
        public int Number { get; set; }

        public Brush Brush
        {
            get
            {
                return new SolidColorBrush(Color);
            }
        }

        public override string ToString()
        {
            return Number.ToString();
        }

        public SquareViewModel() { }

        public SquareViewModel(int number)
        {
            if (number >= GraphColors.Values.Length)
                Color =  GraphColors.Random(number);
            else
                Color = GraphColors.Values[number];

            Number = number;
        }
    }
}
