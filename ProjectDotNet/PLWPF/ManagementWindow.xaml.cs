using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// 
           
    public partial class ManagementWindow : Window
    {
        IBL bl;
        Guest filterGuest;
        ObservableCollection<Guest> guests;
        public ManagementWindow()
        {

            bl = FactoryBL.GetBL();

            InitializeComponent();
            hostingUnitListView.ItemsSource = bl.getAllHostingUnits();
            guests = new ObservableCollection<Guest>(bl.getAllGuests());
            guestListView.ItemsSource = guests;
            orderListView.ItemsSource = bl.getAllOrders();

            this.filterStatusGuest.ItemsSource = Enum.GetValues(typeof(enums.GuestStatus));
            this.filterAreaGuest.ItemsSource = Enum.GetValues(typeof(enums.CountryAreas));
            this.filterJacuzziGuest.ItemsSource = Enum.GetValues(typeof(enums.LuxusOption));
            this.filterPoolGuest.ItemsSource = Enum.GetValues(typeof(enums.LuxusOption));
            filterGuest = new Guest();
            filterGuestStackPanel.DataContext = filterGuest;
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

     

        private void GetAllHostingUnitclick(object sender, RoutedEventArgs e)
        {

        }
        private void GetHostingUnitByIdclick(object sender, RoutedEventArgs e)
        {

        }
        private void GetHostingUnitAvilableclick(object sender, RoutedEventArgs e)
        {

        }

        private Func<Guest, bool> getGuestFilter(Guest guest)
        {

            return x => (x.Pool == guest.Pool || filterPoolGuest.SelectedIndex == -1) &&
            (x.Jacuzzi == guest.Jacuzzi || filterJacuzziGuest.SelectedIndex == -1) &&
            (x.Area == guest.Area || filterAreaGuest.SelectedIndex == -1) &&
            (x.Status == guest.Status || filterStatusGuest.SelectedIndex == -1);


        }

        private void FilterGuestButton_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Guest> newGuests = bl.getAllGuests(getGuestFilter(filterGuest));
            guests.Clear();
            newGuests.ToList().ForEach(guests.Add);
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            filterPoolGuest.SelectedIndex = -1;
            filterJacuzziGuest.SelectedIndex = -1;
            filterAreaGuest.SelectedIndex = -1;
            filterStatusGuest.SelectedIndex = -1;
            guests.Clear();
            bl.getAllGuests().ToList().ForEach(guests.Add);

        }

    }
}
