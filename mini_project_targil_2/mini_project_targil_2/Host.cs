using System;
using System.Collections;
using System.Collections.Generic;
using static mini_project_targil_2.HostingUnit;

namespace mini_project_targil_2
{
    class Host : IEnumerable
    {
        int HostKey;
        public List<HostingUnit> HostingUnitCollection;
        //{
        //    get => HostingUnitCollection;
        //    set => HostingUnitCollection = value;
        //}

    public Host(int HostKey, int numOfUnit)
        {
            HostingUnitCollection = new List<HostingUnit>();
            this.HostKey = HostKey;
            for(int i=0;i< numOfUnit;i++)
            {
                HostingUnitCollection.Add(new HostingUnit());
            }
        }

        public bool AssignRequests(params GuestRequest[] gs)
        {
            bool flag = true;
            for(int i = 0; i<gs.Length;i++)
                if (SubmitRequest(gs[i]) == -1) flag = false;
            return flag;
        }

        public void SortUnits()
        {
            HostingUnitCollection.Sort();
        }


        private long SubmitRequest(GuestRequest guestReq)
        {
            for (int i = 0; i < HostingUnitCollection.Count; i++)
            {
                if (HostingUnitCollection[i].ApproveRequest(guestReq))
                    return HostingUnitCollection[i].HostingUnitKey;
            }
            return -1;
        }
        public int GetHostAnnualBusyDays()
        {
            int sum = 0;
            for (int i = 0; i < HostingUnitCollection.Count; i++)
                sum +=  HostingUnitCollection[i].GetAnnualBusyDays();
            return sum;
        }

        public override string ToString()
        {
            String result = $"Host Key: {HostKey}\n";
            for (int i = 0; i < HostingUnitCollection.Count; i++)
                result += HostingUnitCollection[i].ToString();
            //TODO implement
            return result;// + $"\n{this.GetHostAnnualBusyDays()}";
        }

        public HostingUnit this[int i]
        {
            set => HostingUnitCollection[i] = value;
            get => HostingUnitCollection[i];
        }

        public IEnumerator GetEnumerator()
        {
            return HostingUnitCollection.GetEnumerator();
        }
    }

}
