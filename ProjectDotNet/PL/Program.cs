//203375563
//206197733

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL
{
    class Program
    {

        enum myFunc
        {
            Exit,
            addGuest,
            updateGuest,
            GetGuest,
            getAllGuests,

            addHostingUnit,
            updateHostingUnit,
            deleteHostingUnit,
            GetHostingUnit,
            getAllHostingUnits,


            addOrder,
            updateOrder,
            GetOrder,
            getAllOrders,


            GetGroupingGuestByCountryAreas,
            GetGroupingGuestByNumOfPeoples,
            GetGroupingHostByNumOfHostingUnit,
            GetGroupingHostingUnitByCountryAreas,

            getAllAvailableHostingUnits,

            GetOrderOldersThen,
            GetNumOfDays,
            GuestNunOfOrders,
            GuestOpenOrSuccessfullyClosedOrders,
            getAllBankBranches
        }


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

        static public HostingUnit UserInputHostingUnit()
        {
            HostingUnit hUnit = new HostingUnit();
            Host Owner = new Host();
            BankAccunt bank = new BankAccunt();
            bool[,] diary = new bool[31, 12];
            //
            hUnit.HostingUnitKey = configurition.GetHostingUnitKey();
            //
            
            Owner.HostKey = 123456874;
            Owner.PrivateName = "david";
            Owner.FamilyName = "miz";
            Owner.phoneNumber = "0544644141";
            Owner.MailAddress = "hh@ss.nn";
            //
            bank.BankNumber = 1;
            bank.BankName = "Leumi";
            bank.BranchNumber = 747;
            bank.BranchAddress = "Hayarkot st";
            bank.BranchCity = "Tel aviv";
            //
            Owner.BankAccountNumber = 98754;
            Owner.BankBranchDetails = bank;
            Owner.CollectionClearance = true;
            //
            hUnit.Owner = Owner;
 
            hUnit.HostingUnitName = "kk";
            hUnit.Diary = diary;
            hUnit.Area = enums.CountryAreas.Center;
            hUnit.Type = enums.HostingUnitType.Etc;
            hUnit.Pool = enums.LuxusOption.notInterested;
            hUnit.Jacuzzi = enums.LuxusOption.notInterested;
            hUnit.Garden = enums.LuxusOption.necessary;
            hUnit.ChildrensAttractions = enums.LuxusOption.necessary;
            //
            return hUnit;
        }

        static public Order addOrder()
        {
            Order ord = new Order();
            DateTime cDate = new DateTime(2020, 08, 09);
            DateTime oDate = new DateTime(2020, 04, 12);
            //
            ord.HostingUnitKey = 10000002;
            ord.GuestRequestKey = 10000002;
            ord.OrderKey = configurition.GetOrderKey();
            ord.Status = enums.OrderStatus.Not_yet_addressed;
            //
            ord.CreateDate = cDate;
            ord.OrderDate = oDate;
            return ord;
        }

        static public void PrintOpenScreen()
        {
            Console.WriteLine("For get to Client operations area press 1");
            Console.WriteLine("For get to Hosting unit area press 2");
            Console.WriteLine("For get to Webmaster area press 3");
            Console.WriteLine("For Exit press 0");
        }

        static public void PrintClientOperations()
        {
            Console.WriteLine("For adding guest request press 1");
            Console.WriteLine("For update guest request press 2");
            Console.WriteLine("For get to the previous screen press 0");
        }

        static public void PrintHostingUnitOperations()
        {
            Console.WriteLine("For adding hosting unit press 1");
            Console.WriteLine("For get to Personal Area press 2");
            Console.WriteLine("For get to the previous screen press 0");
        }

        static public void PrintPersonalArea()
        {
            Console.WriteLine("For update hosting unit press 1");
            Console.WriteLine("For delete unit press 2");
            Console.WriteLine("For adding order press 3");
            Console.WriteLine("For update order press 4");
            Console.WriteLine("For get to the previous screen press 0");
        }

        static public void PrintWebmaster()
        {
            Console.WriteLine("For get guest by Id press 1");
            Console.WriteLine("For get all guests press 2");
            Console.WriteLine("For get hosting units by Id press 3");
            Console.WriteLine("For get all hosting units press 4");
            Console.WriteLine("For get order by Id press 5");
            Console.WriteLine("For get all orders press 6");
            Console.WriteLine("For get all available hosting units press 7");
            Console.WriteLine("For get order that olders then date press 8");
            Console.WriteLine("For get num of days press 9");
            Console.WriteLine("For get guest nun of orders press 10");
            Console.WriteLine("For get guest open or successfully closed orders press 11");
            Console.WriteLine("For get all bank branches press 12");
            Console.WriteLine("For get get grouping guest by country areas press 13");
            Console.WriteLine("For get  get grouping guest by num of peoples 14");
            Console.WriteLine("For get grouping host by num of hosting unit 15");
            Console.WriteLine("For get grouping hosting unit by country areas  16");
            Console.WriteLine("For get to the previous screen press 0");
        }

        static void Main(string[] args)
        {
            IBL bl = FactoryBL.GetBL();
            //
            Console.WriteLine("Hello and wellcome to our project!");
            string openScreen = "";
            string clientOperations = "";
            string hostingUnitOperations = "";
            string PersonalArea = "";
            string printWebmaster = "";
            //
            do
            {
                try
                {
                PrintOpenScreen();
                openScreen = Console.ReadLine();
                //
                switch (openScreen)
                {
                    case "0":
                        break;
                    case "1":
                        do
                        {
                            PrintClientOperations();
                            //
                            clientOperations = Console.ReadLine();
                            //
                            switch (clientOperations)
                            {
                                case "0":
                                    break;
                                case "1":
                                    bl.addGuest(UserInputGuest());
                                    break;
                                case "2":
                                    Guest gst = bl.GetGuest(10000003);
                                    gst.Children = 16;
                                    bl.updateGuest(gst);
                                    Console.WriteLine( gst);
                                    break;
                                default:
                                    Console.WriteLine("not an option");
                                    break;
                            }

                        } while (clientOperations != "0");
                        break;
                    case "2":
                        do
                        {
                            PrintHostingUnitOperations();
                            //
                            hostingUnitOperations = Console.ReadLine();
                            //
                            switch (hostingUnitOperations)
                            {
                                case "0":
                                    break;
                                case "1":
                                    bl.addHostingUnit(UserInputHostingUnit());
                                    break;
                                case "2":
                                    do
                                    {
                                        PrintPersonalArea();
                                        //
                                        PersonalArea = Console.ReadLine();
                                        //
                                        switch (PersonalArea)
                                        {
                                            case "0":
                                                break;
                                            case "1":
                                                HostingUnit hUnit = bl.GetHostingUnit(10000002);
                                                hUnit.HostingUnitName = "normal name";
                                                bl.updateHostingUnit(hUnit);
                                                Console.WriteLine(hUnit);
                                                break;
                                            case "2":
                                                bl.deleteHostingUnit(10000001);
                                                break;
                                            case "3":
                                                bl.addOrder(addOrder());
                                                Console.WriteLine(addOrder());
                                                break;
                                            case "4":
                                                Order ord = bl.GetOrder(10000002);
                                                ord.Status = enums.OrderStatus.mail_has_been_sent;
                                                //ord.GuestRequestKey = 10000002;


                                                bl.updateOrder(ord);
                                                Console.WriteLine(ord);
                                                break;
                                            default:
                                                Console.WriteLine("not an option");
                                                break;
                                        }

                                    } while (PersonalArea != "0");
                                    break;
                                default:
                                    Console.WriteLine("not an option");
                                    break;
                            }

                        } while (hostingUnitOperations != "0");
                        break;
                    case "3":
                        do
                        {
                            PrintWebmaster();
                            //
                            printWebmaster = Console.ReadLine();
                            //
                            switch (printWebmaster)
                            {
                                case "0":
                                    break;
                                case "1":
                                    Guest gst1 = bl.GetGuest(10000002);
                                    Console.WriteLine(gst1);
                                    break;
                                case "2":
                                    IEnumerable<Guest> ieGuest = bl.getAllGuests();
                                    foreach(var item in ieGuest)
                                    {
                                        Console.WriteLine("{0}\n\n",item);
                                    }
                                    break;
                                case "3":
                                    HostingUnit hUnit1 = bl.GetHostingUnit(10000002);
                                    Console.WriteLine(hUnit1);
                                    break;
                                case "4":
                                    DateTime hUnitGetAll = new DateTime(2020, 09, 08);
                                    IEnumerable<HostingUnit> iehUnit = bl.getAllAvailableHostingUnits(hUnitGetAll, 7);
                                    foreach (var item in iehUnit)
                                    {
                                        Console.WriteLine("{0}\n\n", item);
                                    }
                                    break;
                                case "5":
                                    Order ord1 = bl.GetOrder(10000002);
                                    Console.WriteLine(ord1);
                                    break;
                                case "6":
                                    IEnumerable<Order> ieOrd = bl.getAllOrders();
                                    foreach (var item in ieOrd)
                                    {
                                        Console.WriteLine("{0}\n\n", item);
                                    }
                                    break;
                                case "7":
                                    DateTime iehUnitDate = new DateTime(2019, 07, 08);
                                    IEnumerable<HostingUnit> iehUnit2 = bl.getAllAvailableHostingUnits(iehUnitDate, 4);
                                    foreach (var item in iehUnit2)
                                    {
                                        Console.WriteLine("{0}\n\n", item);
                                    }
                                    break;
                                case "8":
                                    IEnumerable<Order> ieOrder2 = bl.GetOrderOldersThen(5);
                                    foreach (var item in ieOrder2)
                                    {
                                        Console.WriteLine("{0}\n\n", item);
                                    }
                                    break;
                                case "9":
                                    DateTime numOfDayDate = new DateTime(2020, 01, 08);
                                    int numOfDay = bl.GetNumOfDays(numOfDayDate);
                                    Console.WriteLine(numOfDay);
                                    break;
                                case "10":
                                    int numOfOrders = bl.GuestNunOfOrders(10000002);
                                    Console.WriteLine(numOfOrders);
                                    break;
                                case "11":
                                    int SuccessfullyClosedOrders = bl.GuestOpenOrSuccessfullyClosedOrders(10000002);
                                    Console.WriteLine(SuccessfullyClosedOrders);
                                    break;
                                case "12":
                                    IEnumerable<BankAccunt> ieAllBankBranches = bl.getAllBankBranches();
                                    Console.WriteLine(ieAllBankBranches);
                                    break;
                                case "13":
                                    var Grouping0 = bl.GetGroupingGuestByCountryAreas(); //todo.

                                    foreach (var group in Grouping0)
                                    {
                                        Console.WriteLine("Users from " + group.Key + ":");
                                        foreach (var item in group)
                                            Console.WriteLine("{0}\n",item);
                                    }
                                    break;
                                case "14":
                                    var Grouping1 = bl.GetGroupingGuestByNumOfPeoples();// todo.
                                    foreach (var group in Grouping1)
                                    {
                                        Console.WriteLine("Users from " + group.Key + ":");
                                        foreach (var item in group)
                                            Console.WriteLine("{0}\n", item);
                                    }
                                    break;
                                case "15":
                                    var Grouping2 = bl.GetGroupingHostByNumOfHostingUnit();// todo.
                                    foreach (var group in Grouping2)
                                    {
                                        Console.WriteLine("Users from " + group.Key + ":");
                                        foreach (var item in group)
                                            Console.WriteLine("{0}\n", item);
                                    }
                                    break;
                                case "16":
                                    var Grouping3 = bl.GetGroupingHostingUnitByCountryAreas();// todo.
                                    foreach (var group in Grouping3)
                                    {
                                        Console.WriteLine("Users from " + group.Key + ":");
                                        foreach (var item in group)
                                            Console.WriteLine("{0}\n", item);
                                    }
                                    break;

                                default:
                                    Console.WriteLine("not an option");
                                    break;
                            }

                        } while (printWebmaster != "0");
                        break;
                    default:
                        Console.WriteLine("not an option");
                        break;
                }
                    
              }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }

            } while (openScreen != "0");

           
        }
    }
}
