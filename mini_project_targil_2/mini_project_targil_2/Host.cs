﻿using System;
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
            this.HostKey = HostKey;
            for(int i=0;i< numOfUnit;i++)
            {
                HostingUnitCollection.Add(new HostingUnit());
            }
            //TODO init units  
        }

        public void AssignRequests(params GuestRequest[] gs1)
        {
            //TODO implement
            throw new NotImplementedException();
        }

        public void SortUnits()
        {
            HostingUnitCollection.Sort();
        }


        private long SubmitRequest(GuestRequest guestReq)
        {
            //TODO implement
            return 1;
        }
        public int GetHostAnnualBusyDays()
        {
            //TODO implement
            return 1;
        }

        public override string ToString()
        {
            //TODO implement
            return "";
        }

        public HostingUnit this[int i]
        {
            set => HostingUnitCollection[i] = value;
            get => HostingUnitCollection[i];
        }

        public IEnumerator GetEnumerator()
        {
            //TODO implement
            throw new NotImplementedException();
            //return ;
        }
    }

}
