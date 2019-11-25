using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mini_project_targil_2
{
    class GuestRequest
    {
        
        public const int YEAR  = 2019;
        //public static DateTime  FIRST_DATE = new DateTime(YEAR, 1, 1);
        //public static DateTime  LAST_DATE = new DateTime(YEAR, 12, 31);

        public DateTime EntryDate { get => EntryDate; set => EntryDate = value; }
        public DateTime ReleaseDate { get => ReleaseDate; set => ReleaseDate = value; }

        public bool IsApproved
        {
            get => IsApproved;
            set => IsApproved = value;
        }
        public GuestRequest()
        {
            IsApproved = false;
        }

        public int num_of_day()
        {
            return (ReleaseDate - EntryDate).Days;
        }

        public override string ToString()
        {
            return $"Entry Date: {EntryDate.ToString("dd/mm")}, Release Date: {ReleaseDate.ToString("dd/mm")}";
        }

       
    }
}