﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graphs.UserControls
{
    /// <summary>
    /// Interaction logic for GraphListControl.xaml
    /// </summary>
    public partial class GraphListControl : UserControl
    {
        public EventHandler<RoutedEventArgs> OnChange { get; set; }
        public GraphListControl()
        {
            InitializeComponent();
        }

        private void ChildChanged(object sender, RoutedEventArgs args)
        {
            if (OnChange != null)
                OnChange(sender, args);
        }
    }
}
