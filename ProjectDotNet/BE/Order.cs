using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class Order
    {
        public int HostingUnitKey{get;set;}
        public int GuestRequestKey { get;set;}
        public int OrderKey { get;set;}
        public enums.OrderStatus Status { get;set;}
        public DateTime CreateDate { get;set;}
        public DateTime OrderDate { get;set;}


        public override string ToString()
        {
            return string.Format("HostingUnitKey={0}, GuestRequestKey={1}, OrderKey={2}, Status={3}, CreateDate={4}, OrderDate={5}", HostingUnitKey, GuestRequestKey, OrderKey, Status, CreateDate, OrderDate);

        }


    }
}
