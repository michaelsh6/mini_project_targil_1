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

            int numOfBusyDays = 0;
            for (DateTime date = new DateTime(year, 1, 1); date.Year == year; date = date.AddDays(1))
                if (this[date] == true) numOfBusyDays++;
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
            string result = $"Hosting Unit Key: {HostingUnitKey}\n";

            int year = GuestRequest.YEAR;
            DateTime firstDate = new DateTime(year, 1, 1);
            bool flag = false;
            for (DateTime date = new DateTime(year, 1, 1); date.Year == year; date = date.AddDays(1))
            {
                if (this[date] == true)
                {
                    if (flag == false)
                        firstDate = date;
                    flag = true;
                }
                else
                {
                    if(flag == true)
                    {
                        string EntryDate = firstDate.ToString("dd/MM");
                        string ReleaseDate = date.AddDays(-1).ToString("dd/MM");
                        result += $"Entry Date: {EntryDate}, Release Date: {ReleaseDate}\n";
                        // {(date - firstDate).Days}
                        flag = false;
                    }
                }
            }
            // + $"{this.GetAnnualBusyDays()}\n"
            return result;
        }
    }

    
}