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
using BE;
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<HostingUnit> hostingUnits;
        public MainWindow()
        {
            IBL bl = FactoryBL.GetBL();
            hostingUnits = bl.getAllHostingUnits().ToList();
            InitializeComponent();
            hostingUnitCB.ItemsSource = hostingUnits;
            hostingUnitCB.DisplayMemberPath = "HostingUnitKey";
            hostingUnitCB.SelectedIndex = 0;

        }


        private void AddGuestButton_Click(object sender, RoutedEventArgs e)
        {

            new GuestWindow().ShowDialog();
        }

        private void HostingUnitsButton_click(object sender, RoutedEventArgs e)
        {
            HostingUnitWindow hostingUnitWindow = new HostingUnitWindow(hostingUnits[hostingUnitCB.SelectedIndex]);
            //hostingUnitWindow.hostingUnit = hostingUnits[hostingUnitCB.SelectedIndex];
            hostingUnitWindow.ShowDialog();
        }

        private void CreateHostingUnitButton_click(object sender, RoutedEventArgs e)
        {
            HostingUnitWindow hst = new HostingUnitWindow(getHostingUnit());
            hst.HostingUnitGrid.IsEnabled = true;
            hst.DeleteHostingUnit.IsEnabled = false;
            hst.upDateHostingUnit.Content = "שלח";
            hst.orderTabItem.IsEnabled = false;
            hst.guestTabItem.IsEnabled = false;
            hst.ShowDialog();




        }

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
                Diary = new bool[31, 12]
            };
        }
    }
}
