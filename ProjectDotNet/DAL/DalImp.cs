using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class DalImp :IDAL
    {
        public void addGuest(Guest guest)
        {
            //IEnumerable<Guest> result = from item in DS.dataSource.guests
            //             where item.GuestRequestKey == guest.GuestRequestKey
            //             select item;
            //    if(DS.dataSource.guests.Count(x => x.GuestRequestKey == guest.GuestRequestKey)==0)
            //    {
            //        throw new Exception("dssf"); //TODO // DuplicateIdException()
            //    }

            //    throw new NotImplementedException();
            //
        }

        public void updateGuest(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void addHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public void updateHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public void deleteHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public void addOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void updateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void getAllHostingUnits()
        {
            throw new NotImplementedException();
        }

        public void getAllGuests()
        {
            throw new NotImplementedException();
        }

        public void getAllOrders()
        {
            throw new NotImplementedException();
        }

        public void getAllBankBranches()
        {
            throw new NotImplementedException();
        }

        public 
    }
}
