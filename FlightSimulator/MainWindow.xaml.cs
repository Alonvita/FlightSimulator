using System;
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
using FlightSimulator.Views;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start_Clicked(object sender, RoutedEventArgs e)
        {
            FlightBoard f = new FlightBoard(this);
            this.AddChild(f);
        }

        private void Exit_Clicked(object sender, RoutedEventArgs e)
        {
            // Close the window
            Close();
        }
    }
}
