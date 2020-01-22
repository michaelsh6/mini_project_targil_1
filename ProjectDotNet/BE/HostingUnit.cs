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



        [XmlIgnoreAttribute]
        public bool[,] Diary { get; set; }
        public String TempDiary
        {
            get
            {
                if (Diary == null)
                    return null;
                string result = "";

                int sizeA = Diary.GetLength(0);
                int sizeB = Diary.GetLength(1);
                result += "" + sizeA + "," + sizeB;
                for (int i = 0; i < sizeA; i++)
                    for (int j = 0; j < sizeB; j++)
                        result += "," + Diary[i, j].ToString();

                return result;
            }
            set
            {
                 if (value != null && value.Length > 0)
                {
                    string[] values = value.Split(',');
                    int sizeA = int.Parse(values[0]);
                    int sizeB = int.Parse(values[1]);
                    Diary = new bool[sizeA, sizeB];
                    int index = 2;
                    for (int i = 0; i < sizeA; i++)
                        for (int j = 0; j < sizeB; j++)
                            Diary[i, j] = bool.Parse(values[index++]);
                } 
            }
        }



    //public enums.CountryAreas Area { get; set; }
    //public enums.HostingUnitType Type { get; set; }
    //public enums.LuxusOption Pool { get; set; }
    //public enums.LuxusOption Jacuzzi { get; set; }
    //public enums.LuxusOption Garden { get; set; }
    //public enums.LuxusOption ChildrensAttractions { get; set; }




    public bool this[DateTime date]
        {
            set => Diary[date.Day - 1, date.Month - 1] = value;
            get => Diary[date.Day - 1, date.Month - 1];
        }

        //public override string ToString()
        //{
        //    return string.Format("HostingUnitKey={0}, HostingUnitName={1}, Area={2}, Type={3},  Pool={4}, Jacuzzi={5},Garden={6},ChildrensAttractions={7}", HostingUnitKey, HostingUnitName, Area, Type, Pool, Jacuzzi, Garden, ChildrensAttractions) + this.Owner.ToString();
        //}

    }
}
