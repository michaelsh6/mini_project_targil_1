using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDAL
    {
        void addGuest(BE.Guest guest);
        void updateGuest(BE.Guest guest);

        void addHostingUnit(BE.HostingUnit hostingUnit);
        void updateHostingUnit(BE.HostingUnit hostingUnit);
        void deleteHostingUnit(BE.HostingUnit hostingUnit);

        void addOrder(BE.Order order);
        void updateOrder(BE.Order order);


        void getAllHostingUnits();
        void getAllGuests();
        void getAllOrders();


        void getAllBankBranches();

        




    }

}
