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

namespace PLWPF
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


        private void AddGuestButton_Click(object sender, RoutedEventArgs e)
        {
            new GuestWindow().ShowDialog();
        }

        private void HostingUnitsButton_click(object sender, RoutedEventArgs e)
        {
            new HostingUnitWindow().ShowDialog();
        }

        private void CreateHostingUnitButton_click(object sender, RoutedEventArgs e)
        {
            HostingUnitWindow hst = new HostingUnitWindow();
            hst.HostingUnitGrid.IsEnabled = true;
            hst.DeleteHostingUnit.IsEnabled = false;
            hst.upDateHostingUnit.Content = "שלח";
            hst.orderTabItem.IsEnabled = false;
            hst.guestTabItem.IsEnabled = false;
            hst.ShowDialog();



        }
    }
}
