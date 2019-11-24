using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static mini_project_targil_2.GuestRequest;

namespace mini_project_targil_2
{
   

    class HostingUnit : IComparable
    {
        public static int DAYS = 31, MONTHS = 12;

        private static int stSerialKey = 0;

        public int HostingUnitKey;

        bool[,] Diary;

        public HostingUnit()
        {
            HostingUnitKey = ++stSerialKey;
            Diary = new bool[DAYS, MONTHS];
        }

        public bool ApproveRequest(GuestRequest guestReq)
        {
            //TODO implement
            return true;
        }

        public int GetAnnualBusyDays()
        {
            return 1;
        }
        public float GetAnnualBusyPrecentege()
        {
            //TODO implement
            return 1;
        }

        public int CompareTo(object obj)
        {
            //TODO implement 
            throw new NotImplementedException();
        }


        public override string ToString()
        {
            //TODO implement
            return "";
        }
    }

    
}