using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Guest
    {
        public int GuestRequestKey{ get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string MailAddress { get; set; }
        public enums.GuestStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public enums.CountryAreas Area { get; set; } 
        //SubArea;
        public enums.HostingUnitType Type { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public enums.LuxusOption Pool { get; set; }
        public enums.LuxusOption Jacuzzi { get; set; }
        public enums.LuxusOption Garden { get; set; }
        public enums.LuxusOption ChildrensAttractions { get; set; }
        //Etc

        public override string ToString()
        {
            return string.Format("GuestRequestKey={0}, PrivateName={1}, FamilyName={2}, MailAddress={3}, Status={4}, RegistrationDate={5}, EntryDate={6},ReleaseDate={7},Area={8},Type={9},Adults={10},Children={11},Pool={12},Jacuzzi={13},Garden={14},ChildrensAttractions={15}", GuestRequestKey, PrivateName, FamilyName, MailAddress, Status, RegistrationDate, EntryDate, ReleaseDate, Area, Type, Adults, Children, Pool, Jacuzzi, Garden, ChildrensAttractions);
        }
    }
}
