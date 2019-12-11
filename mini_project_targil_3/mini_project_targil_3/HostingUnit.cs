using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mini_project_targil_3
{
    //HostingUnit class presentation:
    public class HostingUnit
    {
        //propertis:
        public string UnitName { get; set; }
        public int Rooms { get; set; }
        public bool IsSwimmimgPool { get; set; }
        public List<DateTime> AllOrders { get; set; }
        public List<string> Uris { get; set; } 


    }
}
