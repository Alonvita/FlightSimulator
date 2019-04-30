using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
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
        private int first = 1;
        private ObservableDataSource<Point> m_planeLocations = null;
        public FlightBoard()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            m_planeLocations = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            m_planeLocations.SetXYMapping(p => p);

            plotter.AddLineGraph(m_planeLocations, 2, "Route");
        }

        public void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Lat") || e.PropertyName.Equals("Lon")) //set the lon and lat to the map
            {
                switch (first)
                {
                    case 0:
                        var _fbViewModel = sender as FlightBoardVM;
                        Point p1 = new Point(_fbViewModel.Lat, _fbViewModel.Lon);
                        m_planeLocations.AppendAsync(Dispatcher, p1);
                        break;
                    case 1:
                        --first;
                        break;
                }
            }
        }
    }

}