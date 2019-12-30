using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace BE
{
    public class HostingUnit
    {
        

        public int HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }
        public bool[,] Diary { get; set; }



        public enums.CountryAreas Area { get; set; }
        public enums.HostingUnitType Type { get; set; }
        public enums.LuxusOption Pool { get; set; }
        public enums.LuxusOption Jacuzzi { get; set; }
        public enums.LuxusOption Garden { get; set; }
        public enums.LuxusOption ChildrensAttractions { get; set; }




        public bool this[DateTime date]
        {
            set => Diary[date.Day - 1, date.Month - 1] = value;
            get => Diary[date.Day - 1, date.Month - 1];
        }

        public override string ToString()
        {
            return "";
        }

    }
}
