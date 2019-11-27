using System;
using System.Collections;
using System.Collections.Generic;
using static mini_project_targil_2.HostingUnit;

namespace mini_project_targil_2
{
    ///
    ///class Host, the class represent owned accommodation units. 
    ///
    class Host : IEnumerable
    {
        int HostKey;
        public List<HostingUnit> HostingUnitCollection;
        //{
        //    get => HostingUnitCollection;
        //    set => HostingUnitCollection = value;
        //}
    //Host ctor.
    public Host(int HostKey, int numOfUnit)
        {
            HostingUnitCollection = new List<HostingUnit>();
            this.HostKey = HostKey;
            for(int i=0;i< numOfUnit;i++)//runing all HostingUnit obj.
            {
                HostingUnitCollection.Add(new HostingUnit());
            }
        }
        //AssignRequests function: the function get GuestRequest obj and rutern the true when the request is done.
        public bool AssignRequests(params GuestRequest[] gs)
        {
            bool flag = true;
            for(int i = 0; i<gs.Length;i++)// run all GuestRequest.
                if (SubmitRequest(gs[i]) == -1) flag = false;//if all SubmitRequest are true.
            return flag;
        }
        //SortUnits function: the function sort the units by compering obj by invations. 
        public void SortUnits()
        {
            HostingUnitCollection.Sort();
        }
        //SubmitRequest functions: the function get GuestRequest obj and return positive number when request is done.
        private long SubmitRequest(GuestRequest guestReq)
        {
            for (int i = 0; i < HostingUnitCollection.Count; i++)//runnig all over the hosting unit collection.
            {
                if (HostingUnitCollection[i].ApproveRequest(guestReq))//if request is aprove.
                    return HostingUnitCollection[i].HostingUnitKey;
            }
            return -1;
        }
        //GetHostAnnualBusyDays function: the function return sum of busy days on host.
        public int GetHostAnnualBusyDays()
        {
            int sum = 0;
            for (int i = 0; i < HostingUnitCollection.Count; i++)//runnig on all HostingUnitCollection.
                sum +=  HostingUnitCollection[i].GetAnnualBusyDays();//sum days.
            return sum;
        }
        //ToString functions: the function return hosting unit collection string.
        public override string ToString()
        {
            String result = $"Host Key: {HostKey}\n";
            for (int i = 0; i < HostingUnitCollection.Count; i++)
                result += HostingUnitCollection[i].ToString();
            //TODO implement
            return result;// + $"\n{this.GetHostAnnualBusyDays()}";
        }
        //HostingUnit function: the function get index, and using the indexer for get/set on this obj.
        public HostingUnit this[int i]
        {
            set => HostingUnitCollection[i] = value;
            get => HostingUnitCollection[i];
        }
        //GetEnumerator function: the function using and return GetEnumerator for user to using foreach.  
        public IEnumerator GetEnumerator()
        {
            return HostingUnitCollection.GetEnumerator();
        }
    }

}
