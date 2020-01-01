using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public class BL_Imp : IBL
    {
         IDAL dal = DalFactory.GetDal();

        public void addGuest(Guest guest)
        {
            // תאריך תחילת הנופש קודם לפחות ביום אחד לתאריך סיום הנופש
            if (guest.EntryDate >= guest.ReleaseDate)
                throw new Exception("Date mismatch");//TODO DateMismatchException
            dal.addGuest(guest);
        }

        public void addHostingUnit(HostingUnit hostingUnit)
        {
            dal.addHostingUnit(hostingUnit);
        }

        private bool UnitIsAvailabl(HostingUnit unit,DateTime EntryDate,DateTime ReleaseDate)
        {
            for (DateTime CurrentDay = EntryDate; CurrentDay <= ReleaseDate; CurrentDay = CurrentDay.AddDays(1))
                if (unit[CurrentDay] ==true)
                    return false;
            return true;
        }

        public void addOrder(Order order)
        {
            //יש לוודא בעת יצירת הזמנה ללקוח, שהתאריכים המבוקשים פנויים ביחידת האירוח שמוצעת לו
            HostingUnit unit = dal.GetHostingUnit(order.HostingUnitKey);
            Guest guest = dal.GetGuest(order.GuestRequestKey);

            if (!UnitIsAvailabl(unit, guest.EntryDate, guest.ReleaseDate))
                throw new Exception("Unit not Availabl"); //TODO UnitIsAvailablException
            if (order.Status != enums.OrderStatus.Not_yet_addressed)
                throw new Exception("status mast start with Not_yet_addressed"); //TODO  Exception
            dal.addOrder(order);

        }

        public void deleteHostingUnit(int HostingUnitKey)
        {
            //לא ניתן למחוק יחידת אירוח כל עוד יש הצעה הקשורה אליה במצב פתוח
            Func<Order, bool> func = x => x.HostingUnitKey == HostingUnitKey &&
             (x.Status == enums.OrderStatus.mail_has_been_sent || x.Status == enums.OrderStatus.Not_yet_addressed);
            int count = dal.getAllOrders(func).Count();
            if (count != 0)
                throw new Exception("there is open orders to this unit"); //TODO OpenOrdersUnitException
            dal.deleteHostingUnit(HostingUnitKey);



        }

        public IEnumerable<HostingUnit> getAllAvailableHostingUnits(DateTime date, int num_of_dats)
        {
            DateTime EntryDate = date;
            DateTime ReleaseDate = date.AddDays(num_of_dats);
            return dal.getAllHostingUnits(unit => UnitIsAvailabl(unit, EntryDate, ReleaseDate));
        }

        public IEnumerable<BankAccunt> getAllBankBranches()
        {
            return dal.getAllBankBranches();
        }

        public IEnumerable<Guest> getAllGuests(Func<Guest, bool> predicat = null)
        {
            return dal.getAllGuest(predicat);
            throw new NotImplementedException();
        }

        public IEnumerable<HostingUnit> getAllHostingUnits(Func<HostingUnit, bool> predicat = null)
        {
            return dal.getAllHostingUnits(predicat);
        }

        public IEnumerable<Order> getAllOrders(Func<Order, bool> predicat = null)
        {
            return dal.getAllOrders(predicat);
        }

        public IEnumerable<IGrouping<enums.CountryAreas, Guest>> GetGroupingGuestByCountryAreas()
        {
            var guests =  dal.getAllGuest();
            return from guest in guests group guest by guest.Area;
        }


        //רשימת דרישות לקוח מקובצת )Grouping )ע"פ מספר הנופשים.
        public IEnumerable<IGrouping<int, Guest>> GetGroupingGuestByNumOfPeoples()
        {
            var guests = dal.getAllGuest();
            return from guest in guests group guest by guest.Adults + guest.Children;
        }
        // רשימת מארחים מקובצת )Grouping )ע"פ מספר יחידות האירוח שהם מחזיקים
        public IEnumerable<IGrouping<int, Host>> GetGroupingHostByNumOfHostingUnit()
        {
            var hostingUnits = dal.getAllHostingUnits();

            return  from Unit in hostingUnits
                    group Unit by Unit.Owner.HostKey into Units
                    let Owner = Units.FirstOrDefault().Owner
                    let num_of_Unit = Units.Count()
                    group Owner by num_of_Unit;
        }

        public IEnumerable<IGrouping<enums.CountryAreas, HostingUnit>> GetGroupingHostingUnitByCountryAreas()
        {
            var hostingUnits = dal.getAllHostingUnits();
            return from hostingUnit in hostingUnits group hostingUnit by hostingUnit.Area;
        }

        public Guest GetGuest(int GuestRequestKey)
        {
            return dal.GetGuest(GuestRequestKey);
        }

        public HostingUnit GetHostingUnit(int HostingUnitKey)
        {
            return dal.GetHostingUnit(HostingUnitKey);
        }

        public Order GetOrder(int OrderKey)
        {
            return dal.GetOrder(OrderKey);
        }

        public int GetNumOfDays(DateTime dateFrom, DateTime? dateTo = null)
        {
            dateTo = dateTo == null ? DateTime.Today : dateTo; 
            return ((DateTime)dateTo - dateFrom).Days;
        }

        //פונקציה שמקבלת מספר ימים, ומחזירה את כל ההזמנות שמשך הזמן שעבר מאז שנוצרו / מאז שנשלח מייל ללקוח גדול או שווה למספר הימים שהפונקציה קיבלה.
        public IEnumerable<Order> GetOrderOldersThen(int num_of_days)
        {
            Func<Order, bool> OrderOlderThen = order => new DateTime(Math.Min(order.OrderDate.Ticks, order.CreateDate.Ticks)).AddDays(num_of_days)
              <=DateTime.Today;
            return getAllOrders(OrderOlderThen);
        }

        public int GuestNunOfOrders(int GuestRequestKey)
        {
            return getAllOrders(x => x.GuestRequestKey == GuestRequestKey).Count();
        }
        
        public int GuestOpenOrSuccessfullyClosedOrders(int GuestRequestKey)
        {
            return getAllOrders(x => x.GuestRequestKey == GuestRequestKey && 
            (x.Status == enums.OrderStatus.mail_has_been_sent || x.Status == enums.OrderStatus.closed_Order_accepted)).Count();
        }

        public void updateGuest(Guest guest)
        {
            dal.updateGuest(guest);
        }

        public void updateHostingUnit(HostingUnit hostingUnit)
        {
            // לא ניתן לבטל הרשאה לחיוב חשבון כאשר יש הצעה הקשורה אליה במצב פתוח
            //int hostKey = hostingUnit.Owner.HostKey;
            //get(x=>x.Owner.HostKey == hostKey)
            throw new NotImplementedException();
        }

        public void updateOrder(Order order)
        {
            //בעל יחידת אירוח יוכל לשלוח הזמנה ללקוח  (שינוי הסטטוס ל "נשלח מייל")  רק אם חתם על הרשאה לחיוב חשבון בנק.
            //לאחר שסטטוס ההזמנה השתנה לסגירת עיסקה – לא ניתן לשנות יותר את הסטטוס שלה

            // כאשר סטטוס ההזמנה משתנה בגלל סגירת עסקה – יש לבצע חישוב עמלה בגובה של 10 ₪ ליום אירוח
            //כאשר סטטוס הזמנה משתנה עקב סגירת עסקה – יש לשנות את הסטטוס של דרישת הלקוח בהתאם, וכן לשנות את הסטטוס של כל ההזמנות האחרות של אותו לקוח
            //כאשר סטטוס ההזמנה משתנה ל"נשלח מייל" – המערכת תשלח באופן אוטומטי מייל  עם פרטי ההזמנה
            //כאשר סטטוס ההזמנה משתנה בגלל סגירת עסקה – יש לסמן במטריצה את התאריכים הרלוונטיים

            bool CollectionClearance = GetHostingUnit(order.HostingUnitKey).Owner.CollectionClearance;
            if (!CollectionClearance)
                throw new Exception("Collection Clearance not given"); //TODO CollectionClearanceException
            Order OldOrder = GetOrder(order.OrderKey);
            if (OldOrder == null)
                addOrder(order);
            if ((OldOrder.Status == enums.OrderStatus.closed_Order_accepted ||
                OldOrder.Status == enums.OrderStatus.closed_Request_expired) &&
                order.Status != OldOrder.Status)
                throw new Exception("Status cannot change after closing");//TODO StatusChangeException



            throw new NotImplementedException();
        }
    }
}
