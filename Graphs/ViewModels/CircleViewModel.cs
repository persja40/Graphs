using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graphs.ViewModels
{
    public class CircleViewModel
    {
        double _x, _y;

        public double Radius { get; set; }
        public Color Color { get; set; } = Colors.Black;
        public Color BorderColor { get; set; } = Colors.Red;
        public double X
        {
            get
            {
                return _x - Radius / 2;
            }
            set
            {
                _x = value;
            }
        }
        public double Y
        {
            get
            {
                return _y - Radius / 2;
            }
            set
            {
                _y = value;
            }
        }

    }
}
