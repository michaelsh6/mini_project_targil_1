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
        IEnumerable<Order> getAllOrders(Func<Order, bool> predicat = null);
        #endregion

        


        #region Grouping Function
        IEnumerable<IGrouping<enums.CountryAreas, Guest>> GetGroupingGuestByCountryAreas();
        IEnumerable<IGrouping<int, Guest>> GetGroupingGuestByNumOfPeoples();
        IEnumerable<IGrouping<int, Host>> GetGroupingHostByNumOfHostingUnit();
        IEnumerable<IGrouping<enums.CountryAreas, HostingUnit>> GetGroupingHostingUnitByCountryAreas();
        #endregion


        #region others
        //פונקציה שמקבלת תאריך ומספר ימי נופש ומחזירה את רשימת כל יחידות האירוח הפנויות בתאריך זה.
        IEnumerable<HostingUnit> getAllAvailableHostingUnits(DateTime date,int num_of_dats);

        IEnumerable<Order> GetOrderOldersThen(int num_of_days);
        int GetNumOfDays(DateTime dateFrom, DateTime? dateTo = null);
        int GuestNunOfOrders(int GuestRequestKey);
        int GuestOpenOrSuccessfullyClosedOrders(int GuestRequestKey);
        #endregion




        IEnumerable<BankAccunt> getAllBankBranches();
    }
}
