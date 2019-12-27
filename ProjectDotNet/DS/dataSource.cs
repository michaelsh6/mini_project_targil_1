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

                HostingUnitKey= 1,
                HostingUnitName = "",
                //Diary =
                Owner=new BE.Host()
                {

                }
                
            }
            //new BE.HostingUnit();
            //new BE.HostingUnit();
            //new BE.HostingUnit();
        };
            
        //public static List<BE.Host> hosts;
        public static List<BE.Guest> guests;
        public static List<BE.Order> orders;
        //TODO add info for testing
    }
}
