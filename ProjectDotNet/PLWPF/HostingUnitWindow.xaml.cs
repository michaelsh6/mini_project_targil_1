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
    /// Interaction logic for HostingUnitWindow.xaml
    /// </summary>
    public partial class HostingUnitWindow : Window
    {
        private HostingUnit getHostingUnit()
        {
            return new BE.HostingUnit()
            {
                HostingUnitKey = configurition.GetHostingUnitKey(),
                Owner = new BE.Host()
                {
                    HostKey = 203376655,
                    PrivateName = "shay",
                    FamilyName = "patito",
                    phoneNumber = "0544655345",
                    MailAddress = "shay@gmail.com",
                    BankBranchDetails = new BE.BankAccunt()
                    {
                        BankNumber = 1,
                        BankName = "Leumi",
                        BranchNumber = 747,
                        BranchAddress = "Hayarkot st",
                        BranchCity = "Tel Aviv"
                    },
                    BankAccountNumber = 456789,
                    CollectionClearance = true
                },
                HostingUnitName = "negev",
                Diary = new bool[12, 31]
            };
        }

        HostingUnit hostingUnit;
        BL.IBL bl;
        IEnumerable<Guest> guests;
        IEnumerable<Order> orders;

        public HostingUnitWindow()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            //hostingUnit = new HostingUnit();
            hostingUnit = getHostingUnit();
            bl.addHostingUnit(hostingUnit);

            guests = bl.getAllGuests();
            orders = bl.getAllOrders();

            

            HostingUnitGrid.DataContext = hostingUnit;
            guestListView.ItemsSource = guests;
            orderListView.ItemsSource = orders;
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void createOrder_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guest selectedGuest = guests.ElementAtOrDefault(guestListView.SelectedIndex);
                Order order = bl.guestToOrder(selectedGuest, hostingUnit);
                bl.addOrder(order);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
