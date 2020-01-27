using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        BackgroundWorker sendMailWorker;

        public HostingUnit hostingUnit;
        BL.IBL bl;

        ObservableCollection<Guest> guests;
        ObservableCollection<Order> orders;
        IEnumerable<BankAccunt> bankAccunts;

        Guest filterGuest;
        public HostingUnitWindow(HostingUnit Unit)
        {
            sendMailWorker = new BackgroundWorker();
            sendMailWorker.DoWork += sendMail;
            InitializeComponent();
            this.hostingUnit = Unit;
            bl = BL.FactoryBL.GetBL();
            //hostingUnit = new HostingUnit();
            //hostingUnit = getHostingUnit();
            //bl.addHostingUnit(hostingUnit);

            guests = new ObservableCollection<Guest>(bl.getAllGuests());
            orders = new ObservableCollection<Order>(bl.getAllOrders(x => x.HostingUnitKey == hostingUnit.HostingUnitKey));
            bankAccunts = bl.getAllBankBranches();


            List<string> bankNames = (from bank in bankAccunts select bank.BankName).Distinct().ToList();
            BankNameComboBox.ItemsSource = bankNames;

            this.filterStatusCb.ItemsSource = Enum.GetValues(typeof(enums.OrderStatus));

            this.filterStatusGuest.ItemsSource = Enum.GetValues(typeof(enums.GuestStatus));
            this.filterAreaGuest.ItemsSource = Enum.GetValues(typeof(enums.CountryAreas));
            this.filterJacuzziGuest.ItemsSource = Enum.GetValues(typeof(enums.LuxusOption));
            this.filterPoolGuest.ItemsSource = Enum.GetValues(typeof(enums.LuxusOption));
            filterGuest = new Guest();
            filterGuestStackPanel.DataContext = filterGuest;

            //   Enum.GetValues(typeof(enums.HostingUnitType));

            SetBlackOutDates();

            HostingUnitGrid.DataContext = hostingUnit;
            guestListView.ItemsSource = guests;
            orderListView.ItemsSource = orders;
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void BankNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string bamkname = BankNameComboBox.SelectedValue.ToString();
            // BankNumberComboBox.Text = ((from bank in bankAccunts where bank.BankName == bamkname select bank.BankNumber).FirstOrDefault()).ToString();
            BankNumberComboBox.Text = ((from bank in bankAccunts where bank.BankName == BankNameComboBox.SelectedValue.ToString() select bank.BankNumber).FirstOrDefault()).ToString();
            List<int> BranchNumbers = (from bank in bankAccunts where bank.BankName == bamkname select bank.BranchNumber).Distinct().ToList();
            BranchNumberComboBox.IsReadOnly = false;
            BranchNumberComboBox.ItemsSource = BranchNumbers;
        }

        private void BranchNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BranchNumberComboBox.SelectedIndex != -1)
            {
                // int BranchNumber = BranchNumbers[BranchNumberComboBox.SelectedIndex];

                string bamkname = BankNameComboBox.SelectedValue.ToString();
                int BranchNumber = Convert.ToInt32(BranchNumberComboBox.SelectedValue.ToString());
                BranchCityTextBox.Text = ((from bank in bankAccunts where bank.BankName == bamkname && bank.BranchNumber == BranchNumber select bank.BranchCity).FirstOrDefault());
                BranchAddressTextBox.Text = ((from bank in bankAccunts where bank.BankName == bamkname && bank.BranchNumber == BranchNumber select bank.BranchAddress).FirstOrDefault());

            }

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
                sendMailWorker.RunWorkerAsync(order);
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
                    bl.deleteHostingUnit(hostingUnit.HostingUnitKey);
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
            for (DateTime corrent = DateTime.Today; corrent < To; corrent = corrent.AddDays(1))
            {
                if (hostingUnit[corrent] == true)
                    MyCalendar.BlackoutDates.Add(new CalendarDateRange(corrent));
            }
        }

        private void close_window(object sender, EventArgs e)
        {
            new MainWindow().Show();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text); ;
        }

        public void sendMail(object sender, DoWorkEventArgs e)
        {
            Order order = (Order)e.Argument;
            HostingUnit hostingUnit = bl.GetHostingUnit(order.HostingUnitKey);
            Guest guest = bl.GetGuest(order.GuestRequestKey);
            string To = guest.MailAddress;
            string Subject = string.Format(": {0} הצעת חופשה ביחידת האירוח ", hostingUnit.HostingUnitName);
            string Body = string.Format("שלום {0} מייל נשלח אילך בהמשך לבקשתך לחופשה דרך האתר שלנו. יחידת האירוח {1} שלחה אילך הצעת אירוח. להמשך טיפוך ניתן לפנות למייל {2}. יום טוב ", guest.PrivateName + " " + guest.FamilyName, hostingUnit.HostingUnitName, hostingUnit.Owner.MailAddress);


            for (int i = 0; i < 10; i++)
            {
                if (Tools.sendMail(To, Subject, Body, false))
                {
                    order.Status = enums.OrderStatus.mail_has_been_sent;
                    bl.updateOrder(order);
                    return;
                }
            }
            MessageBox.Show("לא הצליח לשלוח מייל", "בעיה");
        }


        //public Func<Guest, bool> getFilter(Guest guest)
        //{
        //    bool statusSelected = filterStatusCb.SelectedIndex != -1;
        //    return x => x.Status == (enums.OrderStatus)filterStatusCb.SelectedIndex;
        //}


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
