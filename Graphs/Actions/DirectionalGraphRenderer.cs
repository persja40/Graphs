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
    public class DirectionalGraphRenderer
    {
        internal DirectedGraphMatrix _graph;
        IDirectedDisplayer _displayer = new DirectedCircleDisplayer();
        public IDirectedDisplayer Displayer { get { return _displayer; } set { _displayer = value; Render(); } }
        public DirectedGraphMatrix Graph
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

        internal DirectedWindowViewModel DirectedWindowVM = null;

        public DirectionalGraphRenderer(DirectedGraphMatrix Graph, GraphControl GraphControl, DirectedWindowViewModel vm)
        {
            this.GraphControl = GraphControl;
            this.Graph = Graph;
            this.DirectedWindowVM = vm;
        }
        
        private void onGraphChange()
        {
            Render();
        }
        
        public void Render()
        {
            Displayer.Display(this);
        }
    }
}
