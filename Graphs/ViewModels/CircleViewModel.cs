using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graphs.ViewModels
{
    public class CircleViewModel : INotifyPropertyChanged
    {
        double _x, _y;

        public event PropertyChangedEventHandler PropertyChanged;

        public void AllChanged()
        {
            notifyChanged("Selected");
        }

        private void notifyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public double Radius { get; set; }
        public int Number { get; set; }
        public SolidColorBrush Color { get; set; } = new SolidColorBrush(Colors.Black);
        public SolidColorBrush BorderColor { get; set; } = new SolidColorBrush(Colors.Red);
        public bool Selected { get; set; } = false;
        public int NodeNumber { get; set; }
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
