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
        public string Text { get; set; }
        public SolidColorBrush Background { get; set; }
        public Visibility Visibility { get; set; } = Visibility.Visible;
        public string Hint { get; set; }

    }
}
