using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    public interface IDAL
    {
        void addGuest(Guest guest);
        void updateGuest(Guest guest);
        void addHostingUnit(HostingUnit hostingUnit);
        void updateHostingUnit(HostingUnit hostingUnit);
        void deleteHostingUnit(int hostingUnit);
        void addOrder(Order order);
        void updateOrder(Order order);

        Guest GetGuest(int GuestRequestKey);
        HostingUnit GetHostingUnit(int HostingUnitKey);
        Order GetOrder(int OrderKey);

        IEnumerable<HostingUnit> getAllHostingUnits(Func<HostingUnit, bool> predicat = null);
        IEnumerable<Guest> getAllGuest(Func<Guest, bool> predicat = null);
        IEnumerable<Order> getAllOrders(Func<Order, bool> predicat = null);
        IEnumerable<BankAccunt> getAllBankBranches(); 
    }
}
