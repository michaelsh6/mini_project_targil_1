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

        static public Guest UserInputGuest()
        {
            //
            Guest gst = new Guest();
            //
            gst.GuestRequestKey = configurition.GetGuestRequestKey();
            gst.PrivateName = "Dan";
            gst.FamilyName = "patito";
            gst.MailAddress = "patito@gmail.com";
            gst.Status = enums.GuestStatus.open;
            //
            gst.RegistrationDate = DateTime.Now;
            //
            DateTime entryDate = new DateTime(2020, 08, 09);
            gst.EntryDate = entryDate;
            //
            DateTime releaseDate = new DateTime(2020, 09, 09);
            gst.ReleaseDate = releaseDate;
            //
            gst.Area = enums.CountryAreas.Center;
            gst.Type = enums.HostingUnitType.Camping;
            gst.Adults = 2;
            gst.Children = 0;
            gst.Pool = enums.LuxusOption.necessary;
            gst.Jacuzzi = enums.LuxusOption.notInterested;
            gst.Garden = enums.LuxusOption.necessary;
            gst.ChildrensAttractions = enums.LuxusOption.necessary;
            //
            return gst;
        }


        public GuestWindow()
        {
            InitializeComponent();
            //guest = new Guest();
            guest = UserInputGuest();
            bl = BL.FactoryBL.GetBL();
            guestInfoGrid.DataContext = guest;
            this.areaComboBox.ItemsSource = Enum.GetValues(typeof(enums.CountryAreas));
            this.gardenComboBox.ItemsSource = Enum.GetValues(typeof(enums.LuxusOption));
            this.jacuzziComboBox.ItemsSource = Enum.GetValues(typeof(enums.LuxusOption));
            this.poolComboBox.ItemsSource = Enum.GetValues(typeof(enums.LuxusOption));
            this.childrensAttractionsComboBox.ItemsSource = Enum.GetValues(typeof(enums.LuxusOption));
            this.typeComboBox.ItemsSource = Enum.GetValues(typeof(enums.HostingUnitType));
        }
    }
}
