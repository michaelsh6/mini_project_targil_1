using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static mini_project_targil_2.GuestRequest;

namespace mini_project_targil_2
{

    /// <summary>
    /// class HostingUnit: the class represents hosting units.
    /// </summary>
    class HostingUnit : IComparable
    {
        public static int DAYS = 31, MONTHS = 12;

        private static int stSerialKey = 0;

        public int HostingUnitKey;

        bool[,] Diary;

        /// <summary>
        /// HostingUnit defult ctor.
        /// </summary>
        public HostingUnit()
        {
            //creating per obj.
            HostingUnitKey = ++stSerialKey;
            Diary = new bool[DAYS, MONTHS];
        }

        /// <summary>
        /// ApproveRequest function: the function chech if the request day ate free.
        /// </summary>
        /// <param name="guestReq">serching dates by this obj.</param>
        /// <returns>the function return true if the action done.</returns>
        public bool ApproveRequest(GuestRequest guestReq)
        {
            for (DateTime CurrentDay = guestReq.EntryDate; CurrentDay <= guestReq.ReleaseDate; CurrentDay = CurrentDay.AddDays(1))//runing at the request dates.
                if (this[CurrentDay] == true) return false;
            for (DateTime CurrentDay = guestReq.EntryDate; CurrentDay <= guestReq.ReleaseDate; CurrentDay = CurrentDay.AddDays(1))//if dates are free so we put in matrix true.
                this[CurrentDay] = true;
            guestReq.IsApproved = true;//Will be marked as true.
            return true;
        }

        /// <summary>
        /// GetAnnualBusyDays function: the function returns the total number of occupied days per year.
        /// </summary>
        /// <returns>num of occupied days per year</returns>
        public int GetAnnualBusyDays()
        {
            int year = GuestRequest.YEAR;//get current year.

            int numOfBusyDays = 0;
            for (DateTime date = new DateTime(year, 1 ,1); date.Year == year; date = date.AddDays(1))//run from first day at the current year untill the end of year.
                if (this[date] == true) numOfBusyDays++;//count occupied days.
            return numOfBusyDays;
        }
        /// <summary>
        /// GetAnnualBusyPrecentege function: the function returns the percentage of annual occupancy.
        /// </summary>
        /// <returns></returns>
        public float GetAnnualBusyPrecentege()
        {
            int DayOfYear = new DateTime(GuestRequest.YEAR, 12, 31).DayOfYear;//DayOfYear= num of days in current year.
            int numOfBusyDays = this.GetAnnualBusyDays();//numOfBusyDays=num of occupied days.
            return ((float)numOfBusyDays) / DayOfYear;//return the precent.
        }

        /// <summary>
        /// CompareTo function: the function comparison of accommodation units by total occupied days per year.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;//if obj not exist.
            HostingUnit otherHostingUnit = obj as HostingUnit;
            if (otherHostingUnit != null)
                return this.GetAnnualBusyDays().CompareTo(otherHostingUnit.GetAnnualBusyDays());
            else
                throw new NotImplementedException();
        }

        /// <summary>
        /// index function: realization of operator [] for user.
        /// </summary>
        /// <param name="date">DateTime class</param>
        /// <returns>param get.</returns>
        public bool this[DateTime date]
        {
            private set => Diary[date.Day - 1, date.Month - 1] = value;
            get => Diary[date.Day - 1, date.Month - 1];
        }

        /// <summary>
        /// ToString function: the function printing the unit its serial number, and The list of periods in which it is occupied.
        /// </summary>
        /// <returns></returns>
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
                    if (flag == true)
                    {
                        //string EntryDate = firstDate.ToString("dd/MM");
                        //string ReleaseDate = date.AddDays(-1).ToString("dd/MM");
                        result += $"Entry Date: {firstDate}, Release Date: {date.AddDays(-1)}\n";
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
