using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using BE;
using DAL;
namespace BL
{



    public class BL_Imp : IBL
    {
        IDAL dal = DalFactory.GetDal(); 

       internal BL_Imp() //implementaion DalFactory
        {
            updateMatrixs();
            if (configurition.LastApdateDaily.AddDays(1) < DateTime.Now) // Sync those date that passed.
            {
                
                Thread threadDaily = new Thread(UpdateOldOrder);
                threadDaily.Start();
            }

        }


        private void UpdateOldOrder() //Update old order function.
        {
            IEnumerable<Order> oldOrders = GetOrderOldersThen(31);
            foreach(var order in oldOrders)
            {
                order.Status = enums.OrderStatus.closed_Request_expired;//Update Status.
                updateOrder(order);
            }
            configurition.LastApdateDaily = DateTime.Now; //Update date to DateTime=now.
        }


      public void addGuest(Guest guest)//addGuest the function get guest object and input it to data base
        {
            // תאריך תחילת הנופש קודם לפחות ביום אחד לתאריך סיום הנופש
            //if date distance more than 11 month, or EntryDate earler than today.
            if (!ValidDate(guest.EntryDate,guest.ReleaseDate))
                throw new DateMismatchException("Date mismatch");//TODO DateMismatchException
            //incorrect mail                                  
            if (!MailValidition(guest.MailAddress))
                throw new MailValiditionException("incorrect mail");//TODO DateMismatchException }
            //
            dal.addGuest(guest);
        }

        public void addHostingUnit(HostingUnit hostingUnit) //addHostingUnit the  function get HostingUnit object and input it to data base
        {
            //incorrect mail                                  
            if (!MailValidition(hostingUnit.Owner.MailAddress))
                throw new MailValiditionException("incorrect mail");//TODO DateMismatchException }
            //
            //incorrect Phon                                  
            if (!IsDigitsOnly(hostingUnit.Owner.phoneNumber))
                throw new IncorrectPhoneException("incorrect phone");//TODO DateMismatchException }
            //
            dal.addHostingUnit(hostingUnit);
        }

        private bool UnitIsAvailabl(HostingUnit hostingUnit, DateTime EntryDate,DateTime ReleaseDate) //UnitIsAvailabl function.
        { // the function return true when Unit Is Availabl
            int day = EntryDate.Day;
            int month = EntryDate.Month;
            //hostingUnit[new DateTime(2020,3,2)] = true;
            for (DateTime CurrentDay = EntryDate; CurrentDay < ReleaseDate; CurrentDay = CurrentDay.AddDays(1))   
                if (hostingUnit[CurrentDay] ==true)
                    return false;
            return true;
        }

        public void addOrder(Order order) //addOrder function. the function get order and add it into data base
        {
            //יש לוודא בעת יצירת הזמנה ללקוח, שהתאריכים המבוקשים פנויים ביחידת האירוח שמוצעת לו
            HostingUnit unit = dal.GetHostingUnit(order.HostingUnitKey);
            Guest guest = dal.GetGuest(order.GuestRequestKey);

            if (!UnitIsAvailabl(unit, guest.EntryDate, guest.ReleaseDate)) //check if Unit Availabl
                throw new UnitIsAvailablException("Unit not Availabl"); //TODO UnitIsAvailablException
		
            if (order.Status != enums.OrderStatus.Not_yet_addressed) //check if status start with Not_yet_addressed
                throw new StartStatusException("status mast start with Not_yet_addressed"); //TODO  Exception
		
            if (guest.Status != enums.GuestStatus.open) //check if guest status open
                throw new GuestStatusException("Guest status mast by open"); //TODO  Exception
		
            if (unit.Owner.CollectionClearance ==false) //check if bank have credit
                throw new NoBankCreditException("אין אפשרות לפתוח הזמנה שכן אין הרשאה לחיוב חשבון הבנק שלך"); //TODO  Exception


            dal.addOrder(order);
            //sendMail(order);

        }

        public void deleteHostingUnit(int HostingUnitKey) //deleteHostingUnit function the function get HostingUnitKey and delete the HostingUnit
        {
            //לא ניתן למחוק יחידת אירוח כל עוד יש הצעה הקשורה אליה במצב פתוח
            Func<Order, bool> func = x => x.HostingUnitKey == HostingUnitKey &&
             (x.Status == enums.OrderStatus.mail_has_been_sent || x.Status == enums.OrderStatus.Not_yet_addressed);
            int count = dal.getAllOrders(func).Count();
            if (count != 0)
                throw new NoOpenOrderException("there is open orders to this unit"); //TODO OpenOrdersUnitException
            dal.deleteHostingUnit(HostingUnitKey);



        }

        public IEnumerable<HostingUnit> getAllAvailableHostingUnits(DateTime date, int num_of_dats) //getAllAvailableHostingUnits function
        { //the function return all AvailableHostingUnit objects 
            DateTime EntryDate = date;
            DateTime ReleaseDate = date.AddDays(num_of_dats);
            return dal.getAllHostingUnits(unit => UnitIsAvailabl(unit, EntryDate, ReleaseDate));
        }

        public IEnumerable<BankAccunt> getAllBankBranches() //getAllBankBranches function.
        { //the function return all Bank Branches
            return dal.getAllBankBranches();
        }

        public IEnumerable<Guest> getAllGuests(Func<Guest, bool> predicat = null) //getAllGuests function.
        { //the function return All Guests
            return dal.getAllGuest(predicat);
            throw new NotImplementedException();
        }

        public IEnumerable<HostingUnit> getAllHostingUnits(Func<HostingUnit, bool> predicat = null) //getAllHostingUnits function.
        { //the function return all Hosting Units
            return dal.getAllHostingUnits(predicat);
        }

        public IEnumerable<Order> getAllOrders(Func<Order, bool> predicat = null) //getAllOrders function.
        { //the function All Orders.
            return dal.getAllOrders(predicat);
        }

        public IEnumerable<IGrouping<enums.CountryAreas, Guest>> GetGroupingGuestByCountryAreas() //Grouping function.
        { //the function return Guest object by Grouping Country Areas.
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
// לפי אזור )Grouping )ע"פ מספר יחידות האירוח שהם מחזיקים
        public IEnumerable<IGrouping<enums.CountryAreas, HostingUnit>> GetGroupingHostingUnitByCountryAreas()
        {

            //var hostingUnits = dal.getAllHostingUnits();
            //return from hostingUnit in hostingUnits group hostingUnit by hostingUnit.Area;
            var orders = dal.getAllOrders();
            return from order in orders group GetHostingUnit(order.HostingUnitKey) by GetGuest(order.GuestRequestKey).Area;
        }
 
        public Guest GetGuest(int GuestRequestKey) //GetGuest the function get Guest Request Key and return the object.
        {
            return dal.GetGuest(GuestRequestKey);
        }

        public HostingUnit GetHostingUnit(int HostingUnitKey) //GetHostingUnit the function get Hosting Unit Key and return the object.
        {
            return dal.GetHostingUnit(HostingUnitKey);
        }

        public Order GetOrder(int OrderKey) //GetOrder the function get Order Key and return the object.
        {
            return dal.GetOrder(OrderKey);
        }

        public int GetNumOfDays(DateTime dateFrom, DateTime? dateTo = null) //GetNumOfDays the function  get date From and date To and return the num of days
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

        public int GuestNunOfOrders(int GuestRequestKey) //GuestNunOfOrders the function get Guest Request Key and retrun Nun Of Orders
        {
            return getAllOrders(x => x.GuestRequestKey == GuestRequestKey).Count();
        }
        
        public int GuestOpenOrSuccessfullyClosedOrders(int GuestRequestKey)//GuestOpenOrSuccessfullyClosedOrders the function get Guest Request Key
        { // and retrun the number of Guest Open Or Successfully Closed Orders
            return getAllOrders(x => x.GuestRequestKey == GuestRequestKey &&
            (x.Status != enums.OrderStatus.closed_Request_expired)).Count();
            //(x.Status == enums.OrderStatus.mail_has_been_sent || x.Status == enums.OrderStatus.closed_Order_accepted)).Count();
        }

        public void updateGuest(Guest guest) //the function get guest obj and  update Guest into data base
        {
            dal.updateGuest(guest);
        }

        public void updateHostingUnit(HostingUnit hostingUnit) //the function get HostingUnit obj and  update HostingUnit into data base
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
                    throw new IncorrentStatusException("There is an open offer for the owner so account authorization cannot be revoked");
            }
            //get(x=>x.Owner.HostKey == hostKey)
            dal.updateHostingUnit(hostingUnit);
        }

        public void updateOrder(Order order) //the function get Order obj and update order into data base  
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
                        if(guest.Status == enums.GuestStatus.open)
                        {
                            guest.Status = enums.GuestStatus.closed_Request_expired;
                            updateGuest(guest);
                        }
                       
                        
                        foreach (var ord in orders)
                        {
                            ord.Status = enums.OrderStatus.closed_Request_expired;
                            dal.updateOrder(ord);
                        }

                        break;
                    case enums.OrderStatus.closed_Order_accepted:
                        int num_of_days = GetNumOfDays(guest.EntryDate, guest.ReleaseDate);
                        int commission = configurition.commission * num_of_days;
                        configurition.commissionAll += commission;
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



        public void insertDates(HostingUnit hostingUnit, DateTime from,DateTime To,bool val =true) //the function get HostingUnit obj and insert Dates into
        {
            for (DateTime corrent = from; corrent < To; corrent = corrent.AddDays(1))
            {
                hostingUnit[corrent] = val;
            }
        }

         //mail validition function    
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

        public Order guestToOrder(Guest guest, HostingUnit hostingUnit) //the function get guest obj and HostingUnit obj and creat Order
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
            DateTime startDate = configurition.LastApdateMonthly;
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

                
                configurition.LastApdateMonthly = now;
            }
        }
        //הפונקציה מחזירה את מספר החודשים שבין שני התאריכים שמתקבלים
        private int diffrenceOfMonths(DateTime time2, DateTime time1)
        {
            return 12 * (time1.Year - time2.Year) + (time1.Month - time2.Month);
        }

        


    }
}
