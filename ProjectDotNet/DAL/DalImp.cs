using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    public class DalImp :IDAL
    {

        private bool CheckGuest(int id)
        {
            return dataSource.guests.Any(x => x.GuestRequestKey == id);
        }

        private bool CheckHostingUnit(int id)
        {
            return dataSource.hostingUnits.Any(x => x.HostingUnitKey == id);
        }

        private bool CheckOrder(int id)
        {
            return dataSource.orders.Any(x => x.OrderKey == id);
        }


        public void addGuest(Guest gst)
        {
            //IEnumerable<Guest> result = from item in DS.dataSource.guests
            //                            where item.GuestRequestKey == guest.GuestRequestKey
            //                            select item;
            //if (DS.dataSource.guests.Any(x => x.GuestRequestKey == guest.GuestRequestKey))
            //{
            //    throw new Exception("a"); 
            //}
            if(!CheckGuest(gst.GuestRequestKey))
            {
                dataSource.guests.Add(gst.Clone());
            }
            else
            {
                throw new Exception("a"); //TODO // DuplicateIdException()
            }
        }

        public void updateGuest(Guest gst)
        {
            //int count = dataSource.guests.RemoveAll(x=>x.GuestRequestKey == gst.GuestRequestKey);
            //if (count == 0)
            //    throw new Exception("a"); 
            dataSource.guests.RemoveAll(x => x.GuestRequestKey == gst.GuestRequestKey);
            addGuest(gst);
        }

        public void addHostingUnit(HostingUnit hstUnit)
        {
            if (!CheckHostingUnit(hstUnit.HostingUnitKey))
            {
                dataSource.hostingUnits.Add(hstUnit.Clone());
            }
            else
            {
                throw new Exception("a"); //TODO // DuplicateIdException()
            }
        }

        public void updateHostingUnit(HostingUnit hstUnit)
        {
           dataSource.hostingUnits.RemoveAll(x => x.HostingUnitKey == hstUnit.HostingUnitKey);
            addHostingUnit(hstUnit);
        }

        public void deleteHostingUnit(int hstUnitId)
        {
            int count = dataSource.hostingUnits.RemoveAll(x => x.HostingUnitKey == hstUnitId);
            if (count == 0)
                throw new Exception("a"); //TODO // DuplicateIdException()
        }

        public void addOrder(Order ord)
        {
            if (!CheckOrder(ord.OrderKey))
            {
                dataSource.orders.Add(ord.Clone());
            }
            else
            {
                throw new Exception("a"); //TODO // DuplicateIdException()
            }
        }

        public void updateOrder(Order ord)
        {
            //int count = dataSource.orders.RemoveAll(x => x.OrderKey == ord.OrderKey);
            //if (count == 0)
            //    throw new Exception("a");  
            dataSource.orders.RemoveAll(x => x.OrderKey == ord.OrderKey);
            addOrder(ord);
        }

        public IEnumerable<Order> getAllOrders()//TODO Func<Guest,bool> predicat=null
        {
            return from allArd in dataSource.orders
                   select allArd.Clone();
        }

        public IEnumerable<Guest> getAllGuest()//TODO Func<Guest,bool> predicat=null
        {
            return from gst in dataSource.guests 
                   select gst.Clone();
        }
        
        public IEnumerable<HostingUnit> getAllHostingUnits()//TODO Func<Guest,bool> predicat=null
        {
            return from hstUnit in dataSource.hostingUnits
                   select hstUnit.Clone();
        }
        public IEnumerable<BankAccunt> getAllBankBranches() 
        {
            return from Baccunt in dataSource.bankAccunt
                   select Baccunt.Clone();
        }  
    }
}

