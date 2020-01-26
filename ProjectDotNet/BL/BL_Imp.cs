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
       internal BL_Imp()
        {
            updateMatrixs();
            

        }

         IDAL dal = DalFactory.GetDal();

      public void addGuest(Guest guest)
        {
            // תאריך תחילת הנופש קודם לפחות ביום אחד לתאריך סיום הנופש
            //if date distance more than 11 month, or EntryDate earler than today.
            if (!ValidDate(guest.EntryDate,guest.ReleaseDate))
                throw new Exception("Date mismatch");//TODO DateMismatchException
            //incorrect mail                                  
            if (!MailValidition(guest.MailAddress))
                throw new Exception("incorrect mail");//TODO DateMismatchException }
            //
            dal.addGuest(guest);
        }

        public void addHostingUnit(HostingUnit hostingUnit)
        {
            //incorrect mail                                  
            if (!MailValidition(hostingUnit.Owner.MailAddress))
                throw new Exception("incorrect mail");//TODO DateMismatchException }
            //
            //incorrect Phon                                  
            if (!IsDigitsOnly(hostingUnit.Owner.phoneNumber))
                throw new Exception("incorrect phone");//TODO DateMismatchException }
            //
            dal.addHostingUnit(hostingUnit);
        }

        private bool UnitIsAvailabl(HostingUnit hostingUnit, DateTime EntryDate,DateTime ReleaseDate)
        {
            int day = EntryDate.Day;
            int month = EntryDate.Month;
            //hostingUnit[new DateTime(2020,3,2)] = true;
            for (DateTime CurrentDay = EntryDate; CurrentDay < ReleaseDate; CurrentDay = CurrentDay.AddDays(1))
                if (hostingUnit[CurrentDay] ==true)
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
            sendMail(order);

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

            //var hostingUnits = dal.getAllHostingUnits();
            //return from hostingUnit in hostingUnits group hostingUnit by hostingUnit.Area;
            var orders = dal.getAllOrders();
            return from order in orders group GetHostingUnit(order.HostingUnitKey) by GetGuest(order.GuestRequestKey).Area;
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
            (x.Status != enums.OrderStatus.closed_Request_expired)).Count();
            //(x.Status == enums.OrderStatus.mail_has_been_sent || x.Status == enums.OrderStatus.closed_Order_accepted)).Count();
        }

        public void updateGuest(Guest guest)
        {
            dal.updateGuest(guest);
        }

        public void updateHostingUnit(HostingUnit hostingUnit)
        {
            // לא ניתן לבטל הרשאה לחיוב חשבון כאשר יש הצעה הקשורה אליה במצב פתוח
            HostingUnit OldhostingUnit = GetHostingUnit(hostingUnit.HostingUnitKey);
            if (OldhostingUnit == null)
            {
                addHostingUnit(hostingUnit);
                return;
            }
            bool newOwnerCollectionClearance = hostingUnit.Owner.CollectionClearance;
            bool oldOwnerCollectionClearance = OldhostingUnit.Owner.CollectionClearance;
            if (oldOwnerCollectionClearance == true && hostingUnit.Owner.CollectionClearance == false)
            {
                int hostKey = hostingUnit.Owner.HostKey;
                IEnumerable<Order> orderOrders = getAllOrders(x => GetHostingUnit(x.HostingUnitKey).Owner.HostKey == hostKey);
                if (orderOrders.Any(x => x.Status == enums.OrderStatus.mail_has_been_sent || x.Status == enums.OrderStatus.Not_yet_addressed))
                    throw new Exception("There is an open offer for the owner so account authorization cannot be revoked");
            }
            //get(x=>x.Owner.HostKey == hostKey)
            dal.updateHostingUnit(hostingUnit);
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
            {
                addOrder(order);
                return;
            }

            if ((OldOrder.Status == enums.OrderStatus.closed_Order_accepted ||
                OldOrder.Status == enums.OrderStatus.closed_Request_expired) &&
                order.Status != OldOrder.Status)
                throw new Exception("Status cannot change after closing");//TODO StatusChangeException

            Guest guest = GetGuest(order.GuestRequestKey);
            if(OldOrder.Status != order.Status)
            {
                IEnumerable<Order> orders = getAllOrders(x => x.GuestRequestKey == order.GuestRequestKey && x.OrderKey!=order.OrderKey);
                switch (order.Status)
                {
                    case enums.OrderStatus.Not_yet_addressed:
                        break;
                    case enums.OrderStatus.mail_has_been_sent:
                        //sendMail(order);
                        Console.WriteLine("mail_has_been_sent");
                        break;
                    case enums.OrderStatus.closed_Request_expired:
                        guest.Status = enums.GuestStatus.closed_Request_expired;
                        updateGuest(guest);
                        
                        foreach (var ord in orders)
                        {
                            ord.Status = enums.OrderStatus.closed_Request_expired;
                            updateOrder(ord);
                        }

                        break;
                    case enums.OrderStatus.closed_Order_accepted:
                        int num_of_days = GetNumOfDays(guest.EntryDate, guest.ReleaseDate);
                        int commission = configurition.commission * num_of_days;

                        guest.Status = enums.GuestStatus.closed_Order_accepted;
                        updateGuest(guest);
                        foreach(var ord in orders)
                        {
                            ord.Status = enums.OrderStatus.closed_Request_expired;
                            updateOrder(ord);
                        }

                        HostingUnit hostingUnit = GetHostingUnit(order.HostingUnitKey);
                        insertDates(hostingUnit, guest.EntryDate, guest.ReleaseDate);

                        
                        updateHostingUnit(hostingUnit);
                        break;
                    default:
                        break;

                    
                }
            }
            dal.updateOrder(order);
        }

        public void insertDates(HostingUnit hostingUnit, DateTime from,DateTime To,bool val =true)
        {
            for (DateTime corrent = from; corrent < To; corrent = corrent.AddDays(1))
            {
                hostingUnit[corrent] = val;
            }
        }

         //mail validition    
        public bool MailValidition(string email)
        {         
             var addr = new System.Net.Mail.MailAddress(email);
              return addr.Address == email;
        }
        //phon digit only
        public bool IsDigitsOnly(string str)
        {
             foreach (char c in str)
              {
                 if (c < '0' || c > '9')
                    return false;
               }
            return true;
         }
	//if date distance more than 11 month, or EntryDate earler than today.
        public static bool ValidDate(DateTime first, DateTime end)
        {
            DateTime localDate = DateTime.Today;
            //
            if (end <= first)
                return false;
            if (first < localDate)
                return false;
            if (end.Year - first.Year > 1)
                return false;
            if (end.Year - first.Year == 1)
                if (end.Month >= first.Month)
                    return false;
            //
            return true;
        }

        public Order guestToOrder(Guest guest, HostingUnit hostingUnit)
        {
            Order order = new Order();
            order.HostingUnitKey = hostingUnit.HostingUnitKey;
            order.GuestRequestKey = guest.GuestRequestKey;
            order.OrderKey = configurition.GetOrderKey();
            order.Status = enums.OrderStatus.Not_yet_addressed;
            order.CreateDate = DateTime.Today;
            order.OrderDate = DateTime.Today;
            return order;
            
        }

       





        //עדכון המטריצות של כל יחידות האירוח ל11 חודשים קדימה
        private void updateMatrixs()
        {
            
            DateTime now = DateTime.Today;
            DateTime startDate = configurition.LastApdate;
            int diffrence = diffrenceOfMonths(startDate, now);
            if (diffrence > 0)
            {
                if (diffrence > 12)
                    diffrence = 12;
                DateTime from = new DateTime(startDate.Year, startDate.Month, 1);
                DateTime to = from.AddMonths(diffrence).AddDays(-1);
                List<HostingUnit> hostingUnits = getAllHostingUnits().ToList();
                foreach(var unit in hostingUnits)
                {
                    //HostingUnit newHostingUnit = unit.Clone();
                    insertDates(unit, from, to, false);
                    updateHostingUnit(unit);
                }

                
                configurition.LastApdate = now;
            }
        }
        //הפונקציה מחזירה את מספר החודשים שבין שני התאריכים שמתקבלים
        private int diffrenceOfMonths(DateTime time2, DateTime time1)
        {
            return 12 * (time1.Year - time2.Year) + (time1.Month - time2.Month);
        }

        public void sendMail(Order order)
        {
            

            HostingUnit hostingUnit = GetHostingUnit(order.HostingUnitKey);
            Guest guest = GetGuest(order.GuestRequestKey);
            string To = guest.MailAddress;
            string Subject = string.Format(": {0} הצעת חופשה ביחידת האירוח ", hostingUnit.HostingUnitName);
            string Body = string.Format("שלום {0} מייל נשלח אילך בהמשך לבקשתך לחופשה דרך האתר שלנו. יחידת האירוח {1} שלחה אילך הצעת אירוח. להמשך טיפוך ניתן לפנות למייל {2}. יום טוב ", guest.PrivateName + " " + guest.FamilyName, hostingUnit.HostingUnitName, hostingUnit.Owner.MailAddress);

            if (Tools.sendMail(To, Subject, Body, false))
            {
                order.Status = enums.OrderStatus.mail_has_been_sent;
                updateOrder(order);
            }


        }


    }
}
