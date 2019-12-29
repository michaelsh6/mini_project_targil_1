using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace BL
{
    interface IBL
    {
        #region Guest Function
        void addGuest(Guest guest);
        void updateGuest(Guest guest);
        Guest GetGuest(int GuestRequestKey);
        IEnumerable<HostingUnit> getAllGuests(Func<Guest, bool> predicat = null);
        #endregion

        #region HostingUnit Function
        void addHostingUnit(HostingUnit hostingUnit);
        void updateHostingUnit(HostingUnit hostingUnit);
        void deleteHostingUnit(int HostingUnitKey);
        HostingUnit GetHostingUnit (int HostingUnitKey);
        IEnumerable<HostingUnit> getAllHostingUnits(Func<HostingUnit, bool> predicat = null);
        #endregion

        #region Order Function
        void addOrder(Order order);
        void updateOrder(Order order);
        Order GetOrder(int OrderKey);
        IEnumerable<HostingUnit> getAllOrders(Func<Order, bool> predicat = null);
        #endregion

        int GetNumOfDays(DateTime dateFrom,DateTime? dateTo=null);


        #region Grouping Function
        IGrouping<enums.CountryAreas, Guest> GetGroupingGuestByCountryAreas();
        IGrouping<int, Guest> GetGroupingGuestByNumOdPeoples();
        IGrouping<int, Host> GetGroupingHostByNumOfHostingUnit();
        IGrouping<enums.CountryAreas, HostingUnit> GetGroupingHostingUnitByCountryAreas();
        #endregion


        #region others
        IEnumerable<HostingUnit> getAllAvailableHostingUnits(DateTime date,int num_of_dats);
        IEnumerable<Order> GetOrderOldersThen(int num_of_days);
        int GuestNunOfOpenOrders(int GuestRequestKeyt);
        int GuestOpenOrSuccessfullyClosedOrders(int GuestRequestKeyt);
        #endregion




        void getAllBankBranches();
    }
}
