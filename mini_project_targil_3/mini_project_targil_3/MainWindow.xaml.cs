
  
//michael shachor 206197733
//Shimon Mizrahi 203375563

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

namespace mini_project_targil_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Host> hostsList = new List<Host>()
            {
                new Host()//new host.
                {
                    HostName="micahel",
                    Units = new List<HostingUnit>()
                    {
                        new HostingUnit()//new hosting unit.
                        {
                            UnitName = "הצימר של שמעון",
                            Rooms = 4,
                            IsSwimmimgPool = true,
                            AllOrders = new List<DateTime>(),
                            Uris = new List<string>()
                            {
                                "https://img.mako.co.il/2015/11/17/hamelech_david_i.jpg",
                                "https://www.zimertop.co.il/gallery/151921876755.jpg",
                                "https://www.zimmer.co.il/9823724/3%20and%204%20(9).jpg"
                            }

                        },
                         new HostingUnit()//new hosting unit.
                        {
                            UnitName = "הצימר של מיכאל",
                            Rooms = 5,
                            IsSwimmimgPool = false,
                            AllOrders = new List<DateTime>(),
                            Uris = new List<string>()
                            {
                                "https://img.mako.co.il/2015/11/17/hamelech_david_i.jpg",
                                "https://www.zimertop.co.il/gallery/151921876755.jpg",
                                "https://www.zimmer.co.il/9823724/3%20and%204%20(9).jpg"
                            }

                        },
                         new HostingUnit()//new hosting unit.
                        {
                            UnitName = "הצימר של מיכאל",
                            Rooms = 5,
                            IsSwimmimgPool = false,
                            AllOrders = new List<DateTime>(),
                            Uris = new List<string>()
                            {
                                "https://img.mako.co.il/2015/11/17/hamelech_david_i.jpg",
                                "https://www.zimertop.co.il/gallery/151921876755.jpg",
                                "https://www.zimmer.co.il/9823724/3%20and%204%20(9).jpg"
                            }

                        }
                    }

                },
                new Host()//new hosting unit.
                {
                    HostName="shimon",
                    Units = new List<HostingUnit>()
                    {
                        new HostingUnit()//new hosting unit.
                        {
                            UnitName = "הצימר של שמעון1",
                            Rooms = 7,
                            IsSwimmimgPool = true,
                            AllOrders = new List<DateTime>(),
                            Uris = new List<string>()
                            {
                                "https://img.mako.co.il/2015/11/17/hamelech_david_i.jpg",
                                "https://www.zimertop.co.il/gallery/151921876755.jpg",
                                "https://www.zimmer.co.il/9823724/3%20and%204%20(9).jpg"
                            }

                        },
                         new HostingUnit()//new h osting unit.
                        {
                            UnitName = "1הצימר של מיכאל",
                            Rooms = 2,
                            IsSwimmimgPool = true,
                            AllOrders = new List<DateTime>(),
                            Uris = new List<string>()
                            {
                                "https://img.mako.co.il/2015/11/17/hamelech_david_i.jpg",
                                "https://www.zimertop.co.il/gallery/151921876755.jpg",
                                "https://www.zimmer.co.il/9823724/3%20and%204%20(9).jpg"
                            }

                        }
                    }

                },
                new Host()//new host.
                {
                    HostName="micahel2",
                    Units = new List<HostingUnit>()
                    {
                        new HostingUnit()//new hosting unit.
                        {
                            UnitName = "2הצימר של שמעון",
                            Rooms = 4,
                            IsSwimmimgPool = true,
                            AllOrders = new List<DateTime>(),
                            Uris = new List<string>()
                            {
                                "https://img.mako.co.il/2015/11/17/hamelech_david_i.jpg",
                                "https://www.zimertop.co.il/gallery/151921876755.jpg",
                                "https://www.zimmer.co.il/9823724/3%20and%204%20(9).jpg"
                            }

                        },
                         new HostingUnit()//new hosting unit.
                        {
                            UnitName = "2הצימר של מיכאל",
                            Rooms = 5,
                            IsSwimmimgPool = false,
                            AllOrders = new List<DateTime>(),
                            Uris = new List<string>()
                            {
                                "https://img.mako.co.il/2015/11/17/hamelech_david_i.jpg",
                                "https://www.zimertop.co.il/gallery/151921876755.jpg",
                                "https://www.zimmer.co.il/9823724/3%20and%204%20(9).jpg"
                            }

                        },
                          new HostingUnit()//new hosting unit.
                        {
                            UnitName = "2שחור הצימר של מיכאל",
                            Rooms = 5,
                            IsSwimmimgPool = false,
                            AllOrders = new List<DateTime>(),
                            Uris = new List<string>()
                            {
                                "https://img.mako.co.il/2015/11/17/hamelech_david_i.jpg",
                                "https://www.zimertop.co.il/gallery/151921876755.jpg",
                                "https://www.zimmer.co.il/9823724/3%20and%204%20(9).jpg"
                            }

                        }
                    }

                }
            };
        private Host currentHost;
        
        public MainWindow()//main window.
        {
            InitializeComponent();
            cbHostList.ItemsSource = hostsList;
            cbHostList.DisplayMemberPath = "HostName";
            cbHostList.SelectedIndex = 0;
            //tbHostName.Text =  "abc";
        }

        private void CbHostList_SelectionChanged(object sender, SelectionChangedEventArgs e)//HostList selection.
        {
            InitializeHost(cbHostList.SelectedIndex);
        }
        private void InitializeHost(int index)//Initialize Host grid.
        {
            MainGrid.Children.RemoveRange(1, 3);
            currentHost = hostsList[index];
            UpGrid.DataContext = currentHost;
            for (int i = 0; i < currentHost.Units.Count; i++)//run on total units for creating main grid (user control).
            {
                HostingUnitUserControl a = new HostingUnitUserControl(currentHost.Units[i]);
                MainGrid.Children.Add(a);
                Grid.SetRow(a, i + 1);
            }
        }
     }
}
