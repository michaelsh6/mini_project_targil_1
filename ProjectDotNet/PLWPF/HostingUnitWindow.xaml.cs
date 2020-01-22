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

        HostingUnit hostingUnit;
        BL.IBL bl;
      

        public HostingUnitWindow()
        {
            InitializeComponent();
            hostingUnit = new HostingUnit();
            bl = BL.FactoryBL.GetBL();
            HostingUnitGrid.DataContext = bl.GetHostingUnit(10000001);
            guestListView.ItemsSource = bl.getAllGuests();
            orderListView.ItemsSource = bl.getAllOrders();
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       
    }
}
