using Graphs.Data;
using Graphs.UserControls;
using Graphs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graphs.Actions
{
    public class GraphRenderer
    {

        internal GraphMatrix _graph;
        IDisplayer _displayer = new CircleDisplayer();

        public IDisplayer Displayer
        {
            get { return _displayer; }
            set
            {
                _displayer = value; Render(); _displayer = new CircleDisplayer();
            }
        }
        public GraphMatrix Graph
        {
            get
            {
                return _graph;
            }
            set
            {
                if (_graph != null)
                    _graph.OnChange -= onGraphChange;
                _graph = value;
                _graph.OnChange += onGraphChange;
            }
        }
        public GraphControl GraphControl { get; set; }

        internal MainWindowViewModel mainWindowVM = null;

        public GraphRenderer(GraphMatrix Graph, GraphControl GraphControl, MainWindowViewModel vm)
        {
            this.GraphControl = GraphControl;
            this.Graph = Graph;
            this.mainWindowVM = vm;
        }

        private void onGraphChange()
        {
            Render();
        }

        public void Render()
        {
            _displayer.Display(this);

        }

    }
}
