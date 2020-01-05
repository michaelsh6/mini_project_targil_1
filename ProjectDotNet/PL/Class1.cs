using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
namespace PL
{

    class Class1
    {
        public static Guest UserInputGuest()
        {
            //
            Guest gst = new Guest();
            //
            gst.GuestRequestKey = configurition.GetGuestRequestKey();
            gst.PrivateName = "Dan";
            gst.FamilyName = "patito";
            gst.MailAddress = "patito@gmail.com";
            gst.Status = enums.GuestStatus.open;
            //
            gst.RegistrationDate = DateTime.Now;
            //
            DateTime entryDate = new DateTime(2020, 08, 09);
            gst.EntryDate = entryDate;
            //
            DateTime releaseDate = new DateTime(2020, 09, 09);
            gst.ReleaseDate = releaseDate;
            //
            gst.Area = enums.CountryAreas.Center;
            gst.Type = enums.HostingUnitType.Camping;
            gst.Adults = 2;
            gst.Children = 0;
            gst.Pool = enums.LuxusOption.necessary;
            gst.Jacuzzi = enums.LuxusOption.notInterested;
            gst.Garden = enums.LuxusOption.necessary;
            gst.ChildrensAttractions = enums.LuxusOption.necessary;
            //
            return gst;
        }


        
        static void Main1(string[] args)
        {
            IDAL dal = DalFactory.GetDal();

            Guest guest = UserInputGuest();
            dal.addGuest(guest);
            Console.WriteLine( dal.GetGuest(guest.GuestRequestKey));
            //Console.WriteLine(typeof(Guest).IsSerializable);
            Console.WriteLine("hi");
        }


    }
}

