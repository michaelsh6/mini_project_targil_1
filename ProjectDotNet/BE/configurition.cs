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
        public static int GetGuestRequestKey()
        {
            GuestRequestKey++;
            Tools.SaveConfigToXml();
            return GuestRequestKey;

        }

        public static int HostingUnitKey = 10000000;
        public static int GetHostingUnitKey()
        {
            HostingUnitKey++;
            Tools.SaveConfigToXml();
            return HostingUnitKey;
        }

        public static int OrderKey = 10000000;
        public static int GetOrderKey()
        {
            OrderKey++;
            Tools.SaveConfigToXml();
            return OrderKey;
        }

        public static int commission = 10; 

        public static DateTime LastApdateMonthly;
        public static DateTime LastApdateDaily;

        public static bool BanksXmlFinish = true;
        public static bool mailFinish;
    }
}
