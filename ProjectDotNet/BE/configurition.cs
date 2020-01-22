using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class configurition
    {
        public static int GuestRequestKey = 10000000;
        public static int GetGuestRequestKey() { return GuestRequestKey++; }

        public static int HostingUnitKey = 10000000;
        public static int GetHostingUnitKey() { return HostingUnitKey++; }

        public static int OrderKey = 10000000;
        public static int GetOrderKey() { return OrderKey++; }

        public static int commission = 10; 
    }
}
