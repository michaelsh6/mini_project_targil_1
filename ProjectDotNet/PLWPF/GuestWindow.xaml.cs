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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for GuestWindow.xaml
    /// </summary>
    public partial class GuestWindow : Window
    {
        Guest guest;
        BL.IBL bl;

        static public Guest DefultValueGuest()
        {
            //
            Guest gst = new Guest();
            //
            gst.GuestRequestKey = configurition.GetGuestRequestKey();
            gst.PrivateName = "";
            gst.FamilyName = "";
            gst.MailAddress = "";
            gst.Status = enums.GuestStatus.open;
            //
            gst.RegistrationDate = DateTime.Today;
            //

            gst.EntryDate = DateTime.Today;
            //
            gst.ReleaseDate = DateTime.Today;
            //
            gst.Area = enums.CountryAreas.All;
            //gst.Type = enums.HostingUnitType;
            //gst.Adults = 2;
            //gst.Children = 0;
            gst.Pool = enums.LuxusOption.possible;
            gst.Jacuzzi = enums.LuxusOption.possible;
            gst.Garden = enums.LuxusOption.possible;
            gst.ChildrensAttractions = enums.LuxusOption.possible;
            //
            return gst;
        }


        public GuestWindow()
        {
            InitializeComponent();
            //guest = new Guest();
            bl = BL.FactoryBL.GetBL();
            guest = DefultValueGuest();
            guestInfoGrid.DataContext = guest;
            this.areaComboBox.ItemsSource = Enum.GetValues(typeof(enums.CountryAreas));
            this.gardenComboBox.ItemsSource = Enum.GetValues(typeof(enums.LuxusOption));
            this.jacuzziComboBox.ItemsSource = Enum.GetValues(typeof(enums.LuxusOption));
            this.poolComboBox.ItemsSource = Enum.GetValues(typeof(enums.LuxusOption));
            this.childrensAttractionsComboBox.ItemsSource = Enum.GetValues(typeof(enums.LuxusOption));
            this.typeComboBox.ItemsSource = Enum.GetValues(typeof(enums.HostingUnitType));
        }

        private void addGuestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.addGuest(guest);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
