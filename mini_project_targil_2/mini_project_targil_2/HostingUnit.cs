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
            for (DateTime CurrentDay = guestReq.EntryDate; CurrentDay <= guestReq.ReleaseDate; CurrentDay = CurrentDay.AddDays(1))
                if (this[CurrentDay] == true) return false;
            for (DateTime CurrentDay = guestReq.EntryDate; CurrentDay <= guestReq.ReleaseDate; CurrentDay = CurrentDay.AddDays(1))
                this[CurrentDay] = true;
            guestReq.IsApproved = true;
            return true;
        }

        public int GetAnnualBusyDays()
        {
            int year = GuestRequest.YEAR;
            DateTime CurrentDay = new DateTime(year, 1, 1);
            DateTime lastDay = new DateTime(year, 12, 31);
            int numOfBusyDays = 0;
            for (; CurrentDay <= lastDay; CurrentDay = CurrentDay.AddDays(1))
                if (this[CurrentDay] == true) numOfBusyDays++;
            return numOfBusyDays;
        }
        public float GetAnnualBusyPrecentege()
        {
            int DayOfYear = new DateTime(GuestRequest.YEAR, 12, 31).DayOfYear;
            int numOfBusyDays = this.GetAnnualBusyDays();
            return ((float)numOfBusyDays )/DayOfYear;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            HostingUnit otherHostingUnit = obj as HostingUnit;
            if (otherHostingUnit != null)
                return this.GetAnnualBusyDays().CompareTo(otherHostingUnit.GetAnnualBusyDays());
            else
                throw new NotImplementedException();
        }

        public bool this[DateTime date]
        {
            private set => Diary[date.Day-1,date.Month-1] = value;
            get => Diary[date.Day-1, date.Month-1];
        }

        public override string ToString()
        {
            //TODO implement
            return "";
        }
    }

    
}