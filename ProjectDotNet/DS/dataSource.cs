using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public class dataSource
    {
        public static List<BE.HostingUnit> hostingUnits = new List<BE.HostingUnit>()
        {
           
          new BE.HostingUnit()
            {
                HostingUnitKey=BE.configurition.GetHostingUnitKey(),
                 Owner=new BE.Host()
                 {
                     HostKey=203376655,
                     PrivateName="shay",
                     FamilyName="patito",
                     FhoneNumber="0544655345",
                     MailAddress="shay@gmail.com",
                     BankBranchDetails=new BE.BankAccunt()
                     {
                         BankNumber=1,
                         BankName="Leumi",
                         BranchNumber=747,
                         BranchAddress="Hayarkot st",
                         BranchCity="Tel Aviv"
                     },
                     BankAccountNumber=456789,
                     CollectionClearance=true
                 },
                HostingUnitName = "negev",
                Diary=new bool[12,31]
            },

            new BE.HostingUnit()
            {
                HostingUnitKey=BE.configurition.GetHostingUnitKey(),
                 Owner=new BE.Host()
                 {
                     HostKey=203378894,
                     PrivateName="dov",
                     FamilyName="hash",
                     FhoneNumber="0544788521",
                     MailAddress="dov@gmail.com",
                     BankBranchDetails=new BE.BankAccunt()
                     {
                         BankNumber=1,
                         BankName="Leumi",
                         BranchNumber=747,
                         BranchAddress="Hayarkot st",
                         BranchCity="Tel Aviv"
                     },
                     BankAccountNumber=123456,
                     CollectionClearance=true
                 },
                HostingUnitName = "The golshim",
                Diary=new bool[12,31]
            },


            new BE.HostingUnit()
            {
                HostingUnitKey=BE.configurition.GetHostingUnitKey(),
                 Owner=new BE.Host()
                 {
                     HostKey=123456789,
                     PrivateName="eav",
                     FamilyName="cohen",
                     FhoneNumber="0546987414",
                     MailAddress="eav@gmail.com",
                     BankBranchDetails=new BE.BankAccunt()
                     {
                         BankNumber=1,
                         BankName="Leumi",
                         BranchNumber=747,
                         BranchAddress="Hayarkot st",
                         BranchCity="Tel Aviv"
                     },
                     BankAccountNumber=321654,
                     CollectionClearance=true
                 },
                HostingUnitName = "mercaz",

                Diary=new bool[12,31]
            }

        };


        public static List<BE.Guest> guests = new List<BE.Guest>()
        {
            new BE.Guest()
            {
                GuestRequestKey= BE.configurition.GetGuestRequestKey(),
                PrivateName="shimon",
                FamilyName="miz",
                MailAddress="shimon@gmail.com",
                Status=BE.enums.GuestStatus.open,
                RegistrationDate=new DateTime(2017,02,12),
                EntryDate=new DateTime(2020,03,17),
                ReleaseDate=new DateTime(2020,03,15),
                Area=BE.enums.CountryAreas.Jerusalem,
                Type=BE.enums.HostingUnitType.Zimmer,
                Adults=2,
                Children=1,
                Pool=BE.enums.LuxusOption.necessary,
                Jacuzzi=BE.enums.LuxusOption.possible,
                Garden=BE.enums.LuxusOption.necessary,
                ChildrensAttractions=BE.enums.LuxusOption.possible
            },

                 new BE.Guest()
            {
                GuestRequestKey= BE.configurition.GetGuestRequestKey(),
                PrivateName="mich",
                FamilyName="ezra",
                MailAddress="mich@gmail.com",
                Status=BE.enums.GuestStatus.open,
                RegistrationDate=new DateTime(2018,02,12),
                EntryDate=new DateTime(2020,05,17),
                ReleaseDate=new DateTime(2020,05,15),
                Area=BE.enums.CountryAreas.Center,
                Type=BE.enums.HostingUnitType.Hotel,
                Adults=2,
                Children=0,
                Pool=BE.enums.LuxusOption.notInterested,
                Jacuzzi=BE.enums.LuxusOption.necessary,
                Garden=BE.enums.LuxusOption.necessary,
                ChildrensAttractions=BE.enums.LuxusOption.possible
            },

                new BE.Guest()
            {
                GuestRequestKey= BE.configurition.GetGuestRequestKey(),
                PrivateName="meira",
                FamilyName="miz",
                MailAddress="meira@gmail.com",
                Status=BE.enums.GuestStatus.open,
                RegistrationDate=new DateTime(2013,02,12),
                EntryDate=new DateTime(2020,01,17),
                ReleaseDate=new DateTime(2020,01,15),
                Area=BE.enums.CountryAreas.Jerusalem,
                Type=BE.enums.HostingUnitType.Zimmer,
                Adults=2,
                Children=1,
                Pool=BE.enums.LuxusOption.necessary,
                Jacuzzi=BE.enums.LuxusOption.notInterested,
                Garden=BE.enums.LuxusOption.notInterested,
                ChildrensAttractions=BE.enums.LuxusOption.notInterested
            }
        };

        public static List<BE.Order> orders = new List<BE.Order>
        {
            new BE.Order()
            {
                HostingUnitKey=10000000,
                GuestRequestKey=10000000,
                OrderKey=BE.configurition.GetOrderKey(),
                Status=BE.enums.OrderStatus.Not_yet_addressed,
                CreateDate=new DateTime(2017,02,12),
                OrderDate=new DateTime(2019,03,12)
            },

             new BE.Order()
            {
                HostingUnitKey=10000001,
                GuestRequestKey=10000001,
                OrderKey=BE.configurition.GetOrderKey(),
                Status=BE.enums.OrderStatus.closed_Request_expired,
                CreateDate=new DateTime(2018,02,12),
                OrderDate=new DateTime(2019,04,12)
            },

              new BE.Order()
            {
                HostingUnitKey=10000002,
                GuestRequestKey=10000002,
                OrderKey=BE.configurition.GetOrderKey(),
                Status=BE.enums.OrderStatus.Not_yet_addressed,
                CreateDate=new DateTime(2013,02,12),
                OrderDate=new DateTime(2019,05,12)
               }
        };

        public static List<BE.BankAccunt> bankAccunt = new List<BE.BankAccunt>
        {
                new BE.BankAccunt()
                {
                         BankNumber=1,
                         BankName="Leumi",
                         BranchNumber=747,
                         BranchAddress="Hayarkot st",
                         BranchCity="Tel Aviv"
                },
                new BE.BankAccunt()
                {
                         BankNumber=2,
                         BankName="Poalim",
                         BranchNumber=123,
                         BranchAddress="lev st",
                         BranchCity="jerusalem"
                },
                new BE.BankAccunt()
                {
                         BankNumber=3,
                         BankName="Mizrahi",
                         BranchNumber=321,
                         BranchAddress="yehuda st",
                         BranchCity="hipfa"
                },
                new BE.BankAccunt()
                {
                         BankNumber=4,
                         BankName="Doar",
                         BranchNumber=222,
                         BranchAddress="ben st",
                         BranchCity="Tel Aviv"
                },
                new BE.BankAccunt()
                {
                         BankNumber=5,
                         BankName="Discount",
                         BranchNumber=111,
                         BranchAddress="via st",
                         BranchCity="eilat"
                }
        };
    }
}

