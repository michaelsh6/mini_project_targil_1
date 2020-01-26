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
namespace PLWPF
{



    /// <summary>
    /// Interaction logic for HostingUnitWindow.xaml
    /// </summary>
    public partial class HostingUnitWindow : Window
    {



        public HostingUnit hostingUnit;
        BL.IBL bl;

        ObservableCollection<Guest> guests;
        ObservableCollection<Order> orders;

        public HostingUnitWindow(HostingUnit Unit)
        {
            InitializeComponent();
            this.hostingUnit = Unit;
            bl = BL.FactoryBL.GetBL();
            //hostingUnit = new HostingUnit();
            //hostingUnit = getHostingUnit();
            //bl.addHostingUnit(hostingUnit);

            guests = new ObservableCollection<Guest>(bl.getAllGuests());
            orders = new ObservableCollection<Order>(bl.getAllOrders());

            List<string> bankNames = (from bank in bl.getAllBankBranches() select bank.BankName).Distinct().ToList();
            List<int> BankNumbers = (from bank in bl.getAllBankBranches() select bank.BankNumber).Distinct().ToList();

            BankNameComboBox.ItemsSource = bankNames;
            BankNumberComboBox.ItemsSource = BankNumbers;

            //   Enum.GetValues(typeof(enums.HostingUnitType));

            SetBlackOutDates();
            
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
                //order.Status = enums.OrderStatus.closed_Order_accepted;
                //bl.updateOrder(order);
                orders.Add(order);
                //orderListView.ItemsSource = orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpDateHostingUnit(object sender, RoutedEventArgs e)
        {
            if (HostingUnitGrid.IsEnabled == false)
            {
                HostingUnitGrid.IsEnabled = true;
                upDateHostingUnit.Content = "שלח"
;
            }
            else
            {
                MessageBox.Show("הפעולה בוצעה בהצלחה", "עידכון יחידת אירוח");
                upDateHostingUnit.Content = "ערוך יחידת אירוח";
                bl.updateHostingUnit(hostingUnit);
                HostingUnitGrid.IsEnabled = false;
                if (DeleteHostingUnit.IsEnabled == false)
                {
                    DeleteHostingUnit.IsEnabled = true;
                    orderTabItem.IsEnabled = true;
                    guestTabItem.IsEnabled = true;
                }
            }

        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("?" + "האם ברצונך למחוק יחידת אירוח", "מחיקת יחידת אירוח", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("הפעולה בוצעה בהצלחה", "מחיקת יחידת אירוח");
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("!" + "הפעולה לא בוצעה", "מחיקת יחידת אירוח");
                    break;
                case MessageBoxResult.Cancel:
                    MessageBox.Show("!" + "הפעולה לא בוצעה", "מחיקת יחידת אירוח");
                    break;
            }
        }


        private void SetBlackOutDates()
        {
            //MyCalendar.DisplayDate = DateTime.Today;
            DateTime from = DateTime.Today;
            DateTime To = new DateTime(from.Year + 1, from.Month, 1).AddDays(-1);
            for (DateTime corrent = DateTime.Today; corrent <To; corrent = corrent.AddDays(1))
            {
                if(hostingUnit[corrent] ==true)
                    MyCalendar.BlackoutDates.Add(new CalendarDateRange(corrent));
            }
        }

        private void close_window(object sender, EventArgs e)
        {
            new MainWindow().Show();
        }
    }
}
