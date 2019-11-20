using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mini_project_targil_2
{
    class GuestRequest: IComparable
    {
        DateTime EntryDate, ReleaseDate;
        bool IsApproved;

        public override string ToString()
        {
            return "";
        }

        public bool ApproveRequest(GuestRequest guestReq)
        {
            return true;
        }

        public int GetAnnualBusyDays()
        {
            return 1;
        }
        public float GetAnnualBusyPercentage()
        {
            return 1;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}