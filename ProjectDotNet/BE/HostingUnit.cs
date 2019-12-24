using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HostingUnit
    {
        int HostingUnitKey { get; set; }
        Host Owner { get; set; }
        string HostingUnitName { get; set; }
        bool[,] Diary { get; set; }

        public override string ToString()
        {
            return "";
        }

    }
}
