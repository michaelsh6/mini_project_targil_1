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
            bool[,] diary = new bool[12, 31];
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
            ord.HostingUnitKey = 10000003;
            ord.GuestRequestKey = 10000003;
            ord.OrderKey = configurition.GetOrderKey();
            ord.Status = enums.OrderStatus.Not_yet_addressed;
            //
            ord.CreateDate = cDate;
            ord.OrderDate = oDate;
            return ord;
        }

        static void menu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("For exit press 0");
            Console.WriteLine("For adding guest request press 1");
            Console.WriteLine("For update guest request press 2");
            Console.WriteLine("For getting guest by id press 3");
            Console.WriteLine("For getting all guests press 4");
            //
            Console.WriteLine("For adding hosting unit press 5");
            Console.WriteLine("For update hosting unit press 6");
            Console.WriteLine("For getting hosting unit by id press 7");
            Console.WriteLine("For getting all hosting unit 8");
            //
            Console.WriteLine("For adding order press 9");
            Console.WriteLine("For update order press 10");
            Console.WriteLine("For getting order by id press 11");
            Console.WriteLine("For getting all order 12");
            //



        }
        static void Main(string[] args)
        {
            IBL bl = FactoryBL.GetBL();
            //
            menu();


            myFunc func = 0;

            while (func != myFunc.Exit)
            {

                switch (func)
                {
                    case myFunc.addGuest:
                        bl.addGuest(UserInputGuest());
                        break;
                    case myFunc.updateGuest:
                        Guest gst = bl.GetGuest(10000003);
                        gst.Children = 16;
                        bl.updateGuest(gst);
                        break;
                    case myFunc.GetGuest:
                        Guest gst1 = bl.GetGuest(10000003);
                        break;
                    case myFunc.getAllGuests:
                        IEnumerable<Guest> ieGuest = bl.getAllGuests();
                        break;
                    case myFunc.addHostingUnit:
                        bl.addHostingUnit(UserInputHostingUnit());
                        break;
                    case myFunc.updateHostingUnit:
                        HostingUnit hUnit = bl.GetHostingUnit(10000003);
                        hUnit.HostingUnitName = "normal name";
                        bl.updateHostingUnit(hUnit);
                        break;
                    case myFunc.deleteHostingUnit:
                        bl.deleteHostingUnit(10000000);
                        break;
                    case myFunc.GetHostingUnit:
                        HostingUnit hUnit1 = bl.GetHostingUnit(10000003);
                        break;
                    case myFunc.getAllHostingUnits:
                        DateTime hUnitGetAll = new DateTime(2020, 09, 08);
                        IEnumerable<HostingUnit> iehUnit = bl.getAllAvailableHostingUnits(hUnitGetAll, 7);
                        break;
                    case myFunc.addOrder:
                        bl.addOrder(addOrder());
                        break;
                    case myFunc.updateOrder:
                        Order ord = bl.GetOrder(10000003);
                        ord.GuestRequestKey = 10000003;
                        bl.updateOrder(ord);
                        break;
                    case myFunc.GetOrder:
                        Order ord1 = bl.GetOrder(10000003);
                        break;
                    case myFunc.getAllOrders:
                        IEnumerable<Order> ieOrd = bl.getAllOrders();
                        break;

                    case myFunc.GetGroupingGuestByCountryAreas:
                        break;
                    case myFunc.GetGroupingGuestByNumOfPeoples:
                        break;
                    case myFunc.GetGroupingHostByNumOfHostingUnit:
                        break;
                    case myFunc.GetGroupingHostingUnitByCountryAreas:
                        break;

                    case myFunc.getAllAvailableHostingUnits:
                        DateTime iehUnitDate = new DateTime(2020, 07, 08);
                        IEnumerable<HostingUnit> iehUnit2 = bl.getAllAvailableHostingUnits(iehUnitDate, 4);
                        break;
                    case myFunc.GetOrderOldersThen:
                        IEnumerable<Order> ieOrder2 = bl.GetOrderOldersThen(5);
                        break;
                    case myFunc.GetNumOfDays:
                        DateTime numOfDayDate = new DateTime(2020, 07, 08);
                        int numOfDay = bl.GetNumOfDays(numOfDayDate);
                        break;
                    case myFunc.GuestNunOfOrders:
                        int numOfOrders = bl.GuestNunOfOrders(10000003);
                        break;
                    case myFunc.GuestOpenOrSuccessfullyClosedOrders:
                        int SuccessfullyClosedOrders = bl.GuestOpenOrSuccessfullyClosedOrders(5);
                        break;
                    case myFunc.getAllBankBranches:
                        IEnumerable<BankAccunt> ieAllBankBranches = bl.getAllBankBranches();
                        break;
                    default:
                        break;
                }
            }

        }
    }
}