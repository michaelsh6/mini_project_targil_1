using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;
using static BE.Tools;

namespace DAL
{
    class imp_XML_Dal : IDAL
    {
        //Thread bankAccunDownload = new Thread(DownloadBankXml);

        XElement OrderRoot;
        XElement bankAccuntsRoot;

        public static List<Guest> guests;
        public static List<HostingUnit> hostingUnits;
        //public static List<Order> orders;
        public static List<BankAccunt> bankAccunts;


        

       


        internal imp_XML_Dal()
        {
            //DownloadBankXml();
            //Thread threadBanks = new Thread(DownloadBankXmlLoop);
            //threadBanks.Start();

           
            /// config file
            if (!File.Exists(configPath))
            {
                SaveConfigToXml();
            }
            else
            {
                ConfigRoot = XElement.Load(configPath);
                configurition.GuestRequestKey = Convert.ToInt32(ConfigRoot.Element("GuestRequestKey").Value);
                configurition.HostingUnitKey = Convert.ToInt32(ConfigRoot.Element("HostingUnitKey").Value);
                configurition.OrderKey = Convert.ToInt32(ConfigRoot.Element("OrderKey").Value);
                configurition.commission = Convert.ToInt32(ConfigRoot.Element("commission").Value);
                configurition.commissionAll = Convert.ToInt32(ConfigRoot.Element("commissionAll").Value);
                configurition.LastApdateMonthly = Convert.ToDateTime(ConfigRoot.Element("LastApdateMonthly").Value);
                configurition.LastApdateDaily = Convert.ToDateTime(ConfigRoot.Element("LastApdateDaily").Value);
            }

            if (!File.Exists(OrderPath))
            {
                OrderRoot = new XElement("Orders");
                OrderRoot.Save(OrderPath);
            }
            if (!File.Exists(GuestPath))
            {
                SaveToXML(new List<Guest>(), GuestPath);
            }
            if (!File.Exists(HostingUnitPath))
            {
                SaveToXML(new List<HostingUnit>(), HostingUnitPath);
            }

            OrderRoot = XElement.Load(OrderPath);
            guests = LoadFromXML<List<Guest>>(GuestPath);
            hostingUnits = LoadFromXML<List<HostingUnit>>(HostingUnitPath);
           

        }


        ~imp_XML_Dal()
        {
            SaveConfigToXml();

            //SaveToXML<List<Guest>>(guests, GuestPath);
            //SaveToXML<List<HostingUnit>>(hostingUnits, GuestPath);

           // OrderRoot.Save(OrderPath);
        }


         //addGuest function get Guest for Save To XML
        public void addGuest(Guest guest)
        {
            if (!guests.Any(x => x.GuestRequestKey == guest.GuestRequestKey))
            {
                guests.Add(guest.Clone());
                SaveToXML<List<Guest>>(guests, GuestPath);
            }
            else
            {
                throw new ExceptionException("DuplicateIdExceptionGuest"); //TODO // DuplicateIdException()
            }
        }
        //updateGuest function get Guest for Save To XML
        public void updateGuest(Guest guest)
        {
            guests.RemoveAll(x => x.GuestRequestKey == guest.GuestRequestKey);
            addGuest(guest);
        }
        //addHostingUnit function get HostingUnit for Save To XML
        public void addHostingUnit(HostingUnit hostingUnit)
        {
            if (!hostingUnits.Any(x => x.HostingUnitKey == hostingUnit.HostingUnitKey))
            {
                hostingUnits.Add(hostingUnit.Clone());
                SaveToXML<List<HostingUnit>>(hostingUnits, HostingUnitPath);
            }
            else
            {
                throw new ExceptionException("DuplicateIdExceptionHostinUnit"); //TODO // DuplicateIdException()
            }
        }
           //updateHostingUnit function get HostingUnit for Save To XML 
        public void updateHostingUnit(HostingUnit hostingUnit)
        {
            hostingUnits.RemoveAll(x => x.HostingUnitKey == hostingUnit.HostingUnitKey);
            addHostingUnit(hostingUnit);
        }
         //deleteHostingUnit function get hosting Unit Id  and delete it from XML
        public void deleteHostingUnit(int hostingUnitId)
        {
            int count = hostingUnits.RemoveAll(x => x.HostingUnitKey == hostingUnitId);
            if (count == 0)
                throw new HostinUnitNotExistException("HostinUnit not exist"); //TODO // DuplicateIdException()
            SaveToXML<List<HostingUnit>>(hostingUnits, HostingUnitPath);

        }
        // GetGuest function get Guest Request Key and return Guest obj from Xml data
        public Guest GetGuest(int GuestRequestKey)
        {
            return guests.FirstOrDefault(x => x.GuestRequestKey == GuestRequestKey).Clone();
        }
        // HostingUnit function get  Hosting Unit Key and return HostingUnit obj from Xml data
        public HostingUnit GetHostingUnit(int HostingUnitKey)
        {
            return hostingUnits.FirstOrDefault(x => x.HostingUnitKey == HostingUnitKey).Clone();
        }
      // getAllHostingUnits function   return all HostingUnit obj from Xml data
        public IEnumerable<HostingUnit> getAllHostingUnits(Func<HostingUnit, bool> predicat = null)
        {
            return from hostingUnit in hostingUnits
                   where predicat == null ? true : predicat(hostingUnit)
                   select hostingUnit.Clone();
        }
 // getAllGuest function   return all Guest obj from Xml data
        public IEnumerable<Guest> getAllGuest(Func<Guest, bool> predicat = null)
        {
            return from guest in guests
                   where predicat == null ? true : predicat(guest)
                   select guest.Clone();
        }






  // addOrder function   add Order obj to Xml data
        public void addOrder(Order order)
        {
            if (GetOrder(order.OrderKey) != null)
            {
                throw new IdOrderException("DuplicateIdExceptionOrder"); //TODO // DuplicateIdException()
            }
            try
            {
                XElement orderXml = new XElement("Order");
                orderXml.Add(
                    new XElement("OrderKey", order.OrderKey),
                    new XElement("GuestRequestKey", order.GuestRequestKey),
                    new XElement("HostingUnitKey", order.HostingUnitKey),

                    new XElement("OrderDate", order.OrderDate),
                    new XElement("Status", order.Status),
                    new XElement("CreateDate", order.CreateDate)
                    );
                OrderRoot.Add(orderXml);
                OrderRoot.Save(OrderPath);
            }
            catch (Exception ex)
            {
                throw new problem_OrderException("file_problem_Order");
            }

        }
        
          // updateOrder function   add update Order  to Xml data
         public void updateOrder(Order order)
        {
            order = order.Clone();
            XElement oldorder = (from orderXml in OrderRoot.Elements()
                                 where orderXml.Element("OrderKey").Value == order.OrderKey.ToString()
                                 select orderXml).FirstOrDefault();

            oldorder.Element("OrderKey").Value = order.OrderKey.ToString();
            oldorder.Element("OrderDate").Value = order.OrderDate.ToString();
            oldorder.Element("GuestRequestKey").Value = order.GuestRequestKey.ToString();
            oldorder.Element("HostingUnitKey").Value = order.HostingUnitKey.ToString();
            oldorder.Element("Status").Value = order.Status.ToString();
            oldorder.Element("CreateDate").Value = order.CreateDate.ToString();

            OrderRoot.Save(OrderPath);

        }

       // GetOrder function get Order Key and return Order obj from xml data.
        public Order GetOrder(int OrderKey)
        {
            //OrderRoot = XElement.Load(OrderPath);
            return (from order in OrderRoot.Elements().Where(x => x.Element("OrderKey").Value == OrderKey.ToString())
                    select new Order()
                    {
                        OrderKey = Convert.ToInt32(order.Element("OrderKey").Value),
                        GuestRequestKey = Convert.ToInt32(order.Element("GuestRequestKey").Value),
                        HostingUnitKey = Convert.ToInt32(order.Element("HostingUnitKey").Value),
                        Status = (enums.OrderStatus)Enum.Parse(typeof(enums.OrderStatus), order.Element("Status").Value),
                        CreateDate = DateTime.Parse(order.Element("CreateDate").Value),
                        OrderDate = DateTime.Parse(order.Element("OrderDate").Value)
                    }).FirstOrDefault().Clone();
        }


       // getAllOrders function  return all Order obj from xml data.
        public IEnumerable<Order> getAllOrders(Func<Order, bool> predicat = null)
        {
            try
            {
                return (from order in OrderRoot.Elements()
                        let orderObj = new Order()
                        {
                            OrderKey = Convert.ToInt32(order.Element("OrderKey").Value),
                            GuestRequestKey = Convert.ToInt32(order.Element("GuestRequestKey").Value),
                            HostingUnitKey = Convert.ToInt32(order.Element("HostingUnitKey").Value),

                            Status = (enums.OrderStatus)Enum.Parse(typeof(enums.OrderStatus), order.Element("Status").Value),
                            CreateDate = DateTime.Parse(order.Element("CreateDate").Value),
                            OrderDate = DateTime.Parse(order.Element("OrderDate").Value)
                        }
                        where predicat == null ? true : predicat(orderObj)
                        select orderObj).ToList().Clone();
            }
            catch (Exception ex)
            {
               // throw new Exception("file_problem_Order");
               throw new problem_OrderException("file_problem_Order");
            }
        }

        //getAllBankBranches function  return all All Bank Branches from flie BankAccuntPath or atm1.xml.
        public IEnumerable<BankAccunt> getAllBankBranches()
        {

            string BankAccuntPath_temp = BankAccuntPath;
            if (!File.Exists(BankAccuntPath) || configurition.BanksXmlFinish == false)
            {
                BankAccuntPath_temp = "atm1.xml";
            }
            else
            {
                long length = new FileInfo(BankAccuntPath).Length;
                if (length < 10000)
                    BankAccuntPath_temp = "atm1.xml";
            }
            bankAccuntsRoot = XElement.Load(BankAccuntPath_temp);
            bankAccunts = XmlToBankAccunt(bankAccuntsRoot);
            //SaveToXML(bankAccunts, "banks.xml");
            return from BankAccunt in bankAccunts

                   select BankAccunt.Clone();
           // throw new Exception("banks file problem");

            

        }
    }



     //return new List<BE.BankAccunt>
     //       {
     //           new BE.BankAccunt()
     //           {
     //                    BankNumber=1,
     //                    BankName="Leumi",
     //                    BranchNumber=747,
     //                    BranchAddress="Hayarkot st",
     //                    BranchCity="Tel Aviv"
     //           },
     //           new BE.BankAccunt()
     //           {
     //                    BankNumber=2,
     //                    BankName="Poalim",
     //                    BranchNumber=123,
     //                    BranchAddress="lev st",
     //                    BranchCity="jerusalem"
     //           },
     //           new BE.BankAccunt()
     //           {
     //                    BankNumber=3,
     //                    BankName="Mizrahi",
     //                    BranchNumber=321,
     //                    BranchAddress="yehuda st",
     //                    BranchCity="hipfa"
     //           },
     //           new BE.BankAccunt()
     //           {
     //                    BankNumber=4,
     //                    BankName="Doar",
     //                    BranchNumber=222,
     //                    BranchAddress="ben st",
     //                    BranchCity="Tel Aviv"
     //           },
     //           new BE.BankAccunt()
     //           {
     //                    BankNumber=5,
     //                    BankName="Discount",
     //                    BranchNumber=111,
     //                    BranchAddress="via st",
     //                    BranchCity="eilat"
     //           }
     //       };


}
