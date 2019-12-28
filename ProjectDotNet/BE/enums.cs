using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class enums
    {
        public enum OrderStatus
        {
            Not_yet_addressed,//טרם טופל
            mail_has_been_sent,//נשלח מייל
            closed_Request_expired,//נסגר מחוסר הענות של הלקוח
            closed_Order_accepted,//נסגר בהיענות של הלקוח
        }
        public enum CountryAreas
        {
            All,
            North,
            South,
            Center,
            Jerusalem
        }
        public enum HostingUnitType
        {
            Zimmer,
            Hotel,
            Camping,
            Etc
        }

        public enum GuestStatus
        {
            open,//פתוחה
            closed_Request_expired,//נסגרה כי פג תוקפה
            closed_Order_accepted//נסגרה עסקה דרך האת
        }
        
        
         public enum LuxusOption
        {
            necessary,
            possible,
            notInterested
        }

        

    }
}
