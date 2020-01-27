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
using System.Windows.Shapes;
using BE;
using BL;

namespace PLWPF
{
    //
    /// <summary>
    /// Interaction logic for ManagementWindow.xaml
    /// </summary>
    public partial class ManagementWindow : Window
    {
        public ManagementWindow()
        {
            InitializeComponent();
        }


        private void GetAllguestclick(object sender, RoutedEventArgs e)
        {
            //IEnumerable<Guest> ieGuest = bl.getAllGuests();
            //foreach (var item in ieGuest)
            //{
            //    Console.WriteLine("{0}\n\n", item);
            //}
        }

        private void GetguestByOrderclick(object sender, RoutedEventArgs e)
        {

        }

        private void GetguestByIdclick(object sender, RoutedEventArgs e)
        {

        }

        //private void GetguestByAreaclick(object sender, RoutedEventArgs e)
        //{

        //}

        //private void GetguestByPersonclick(object sender, RoutedEventArgs e)
        //{

        //}

        private void GetguestOpenclick(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource guestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guestViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // guestViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource hostingUnitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostingUnitViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // hostingUnitViewSource.Source = [generic data source]
        }

        private void GetAllHostingUnitclick(object sender, RoutedEventArgs e)
        {

        }
        private void GetHostingUnitByIdclick(object sender, RoutedEventArgs e)
        {

        }
        private void GetHostingUnitAvilableclick(object sender, RoutedEventArgs e)
        {

        }

          
    }
}
