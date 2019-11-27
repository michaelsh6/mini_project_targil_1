using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mini_project_targil_2
{
    //
    //GuestRequest class:the class presents customer hosting requirement.
    //
    class GuestRequest
    {
        
        public static int YEAR = DateTime.Today.Year;
        //public static DateTime  FIRST_DATE = new DateTime(YEAR, 1, 1);
        //public static DateTime  LAST_DATE = new DateTime(YEAR, 12, 31);

        public DateTime EntryDate; //{ get => EntryDate; set => EntryDate = value; }
        public DateTime ReleaseDate;//{ get => ReleaseDate; set => ReleaseDate = value; }

        private bool m_IsApproved;
        
        /// <summary>
        /// Property get\set of m_IsApproved field.
        /// </summary>
        public bool IsApproved
        {
            get => m_IsApproved;
            set => m_IsApproved = value;
        }
         /// <summary>
        ///GuestRequest defult ctor.
        /// </summary>
        public GuestRequest()
        {
            IsApproved = false;
        }
        /// <summary>
        /// num_of_day function.
        /// </summary>
        /// <returns>the function returns requested vacation term.</returns>
        public int num_of_day()
        {
            return (ReleaseDate - EntryDate).Days;
        }

        /// <summary>
        /// ToString function.
        /// </summary>
        /// <returns>the function returns string entry and exit vacation.</returns>
        public override string ToString()
        {
            return $"Entry Date: {EntryDate}, Release Date: {ReleaseDate}";
        }

       
    }
}
