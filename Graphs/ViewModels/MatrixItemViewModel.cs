using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Graphs.ViewModels
{
    public class MatrixItemViewModel
    {
        public int Connections { get; set; }
        public SolidColorBrush Background { get; set; }
        public Visibility Visibility { get; set; } = Visibility.Visible;
        public int x { get; set; }
        public int y { get; set; }


        public string Hint
        {
            get
            {
                if (x >= 0 && y >= 0)
                    return string.Format("[{0}, {1}] - {2}]", x, y, Connections);
                else
                    return Connections.ToString();
            }
        }
    }
}
