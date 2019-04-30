using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class FlightBoard : UserControl
    {
        List<Window> _children = null;
        ObservableDataSource<Point> planeLocations = null;
        public Window ParentControl { get; set; }

        public FlightBoard(Window parent)
        {
            InitializeComponent();

            ParentControl = parent;

            _children = new List<Window>();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planeLocations = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            planeLocations.SetXYMapping(p => p);

            plotter.AddLineGraph(planeLocations, 2, "Route");
        }

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals("Lat") || e.PropertyName.Equals("Lon"))
            {
                Point p1 = new Point(0,0);            // Fill here!
                planeLocations.AppendAsync(Dispatcher, p1);
            }
        }

        private void Open_Settings_Window(object sender, RoutedEventArgs e)
        {
            // add a new settings window ptr to children
            _children.Add(new Settings(this));

            // show the window
            _children.Last().Show();
        }

        public void Back_To_Main_Window(object sender, RoutedEventArgs e)
        {
            // close all children if there's exist any
            foreach(Window w in _children)
            {
                w.Close();
            }

            // need to clear the content of this window


            ParentControl.Show();
        }
    }

}

