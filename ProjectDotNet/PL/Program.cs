using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL
{
    class Program
    {
        public Guest UserInputGuest()
        {
            //string name = Console.ReadLine();
            //string familyName= Console.ReadLine();
            //string mailAddress = Console.ReadLine();
            //int status= Convert.ToInt32(Console.ReadLine());
            ////
            //int EntryDateDay= Convert.ToInt32(Console.ReadLine());
            //int EntryDateMonth = Convert.ToInt32(Console.ReadLine());
            //int EntryDateYear = Convert.ToInt32(Console.ReadLine());
            ////
            //int ReleaseDateDay = Convert.ToInt32(Console.ReadLine());
            //int ReleaseDateMonth = Convert.ToInt32(Console.ReadLine());
            //int ReleaseDateYear = Convert.ToInt32(Console.ReadLine());
            ////
            //int Area = Convert.ToInt32(Console.ReadLine());
            //int Type = Convert.ToInt32(Console.ReadLine());
            //int Adults = Convert.ToInt32(Console.ReadLine());
            //int Children = Convert.ToInt32(Console.ReadLine());
            //int Pool = Convert.ToInt32(Console.ReadLine());
            //int Jacuzzi = Convert.ToInt32(Console.ReadLine());
            //int Garden = Convert.ToInt32(Console.ReadLine());
            //int ChildrensAttractions = Convert.ToInt32(Console.ReadLine());
            //
            Guest gst = new Guest();
            //
            gst.GuestRequestKey = configurition.GetGuestRequestKey();
            gst.PrivateName = "Dan";
            gst.FamilyName ="patito";
            gst.MailAddress = "patito@gmail.com";
            gst.Status = enums.GuestStatus.open;
            //
            gst.RegistrationDate= DateTime.Now;
            //
            DateTime entryDate = new DateTime(2020,08,09);
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



        static void Main(string[] args)
        {
            IBL bl = FactoryBL.GetBL();
            Console.WriteLine("Menu:");
            Console.WriteLine("For adding guest request press 1");

            Console.WriteLine("For exit press e");

            string choice = "";

            while (choice != "e")
            {
                Console.Write("Please enter your selection: ");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                       
                        break;
                    case "2":
                        break;
                    case "e":
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please select from menu");
                        break;
                }

            }

        }
    }

    
}
