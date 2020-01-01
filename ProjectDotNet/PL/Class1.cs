using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{

    class Class1
    {
        public void exampel()
        {
            myFunc func = 0;
            switch (func)
            {
                case myFunc.addGuest:
                    break;
                case myFunc.updateGuest:
                    break;
                case myFunc.GetGuest:
                    break;
                case myFunc.getAllGuests:
                    break;
                case myFunc.addHostingUnit:
                    break;
                case myFunc.updateHostingUnit:
                    break;
                case myFunc.deleteHostingUnit:
                    break;
                //case myFunc.HostingUnit:
                //    break;
                case myFunc.GetHostingUnit:
                    break;
                case myFunc.getAllHostingUnits:
                    break;
                case myFunc.addOrder:
                    break;
                case myFunc.updateOrder:
                    break;
                case myFunc.GetOrder:
                    break;
                case myFunc.getAllOrders:
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
                    break;
                case myFunc.GetOrderOldersThen:
                    break;
                case myFunc.GetNumOfDays:
                    break;
                case myFunc.GuestNunOfOrders:
                    break;
                case myFunc.GuestOpenOrSuccessfullyClosedOrders:
                    break;
                case myFunc.getAllBankBranches:
                    break;
                default:
                    break;
            }
        }


    }



    enum myFunc
    {

        addGuest,
        updateGuest,
        GetGuest,
        getAllGuests,


        addHostingUnit,
        updateHostingUnit,
        deleteHostingUnit,
        //HostingUnit
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
}

