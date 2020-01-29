using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    [Serializable]
    public class HostingUnit 
    {


        public int HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }



        private bool[,] m_diary = new bool[12, 31];

        [XmlIgnore]
        public bool[,] Diary
        {
            get { return m_diary; }
            set { m_diary = value; }
        }

        [XmlArray("Diary")]
        public bool[] TempDiary
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(31); }
        }



    //public enums.CountryAreas Area { get; set; }
    //public enums.HostingUnitType Type { get; set; }
    //public enums.LuxusOption Pool { get; set; }
    //public enums.LuxusOption Jacuzzi { get; set; }
    //public enums.LuxusOption Garden { get; set; }
    //public enums.LuxusOption ChildrensAttractions { get; set; }




    public bool this[DateTime date]
        {
            set => Diary[date.Month - 1, date.Day - 1] = value;
            get => Diary[date.Month - 1, date.Day - 1];
        }

        //public override string ToString()
        //{
        //    return string.Format("HostingUnitKey={0}, HostingUnitName={1}, Area={2}, Type={3},  Pool={4}, Jacuzzi={5},Garden={6},ChildrensAttractions={7}", HostingUnitKey, HostingUnitName, Area, Type, Pool, Jacuzzi, Garden, ChildrensAttractions) + this.Owner.ToString();
        //}

    }
}
