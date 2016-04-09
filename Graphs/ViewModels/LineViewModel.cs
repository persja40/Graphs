using Graphs.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graphs.ViewModels
{
    public class LineViewModel
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }

        public double Width { get { return X2 - X1; } }
        public double Height { get { return Y2 - Y1; } }

        public double Length { get { return Math.Sqrt(Math.Pow(Width, 2) + Math.Pow(Height, 2)); } }

        public double Angle
        {
            get
            {
                var arcsin = Math.Asin(Height / Length);
                var arccos = Math.Acos(Width / Length);

                if(arcsin < 0 && arccos < 0)
                {
                    return arcsin + Math.PI / 2;
                }
                else if(arcsin < 0)
                {
                    return Math.PI - arccos;
                }
                else if(arccos < 0)
                {
                    return Math.PI * 2 - arcsin;
                }

                return arcsin;

            }
        }

        public Color Color { get; set; } = Colors.Black;
        public int Thickness { get; set; } = 1;

        public int Node1 { get; set; }
        public int Node2 { get; set; }
        public string Hint { get; set; }

        public LineViewModel() { }
        public LineViewModel(LineViewModel other)
        {
            this.X1 = other.X1;
            this.Y1 = other.Y1;
            this.X2 = other.X2;
            this.Y2 = other.Y2;

            this.Color = other.Color;
            this.Thickness = other.Thickness;

            this.Node1 = other.Node1;
            this.Node2 = other.Node2;
            this.Hint = other.Hint;
        }
    }
}
