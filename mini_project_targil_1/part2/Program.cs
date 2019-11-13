using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace part2
{
    class Program
    {

        bool[,] calander = new bool[12, 31];
        static void Main(string[] args)
        {
            
            Console.WriteLine("entr your choice:");
            string user_input = Console.ReadLine();
            switch(user_input)
            {
                case "REQUEST":
                    AddVection();
                    break;

                case "LIST":
                    break;

                case "NUM_OF_DAYS":
                    break;

                case "EXIT":
                    break;

                default:
                    Console.WriteLine("ERROR");
                    break;

            }

        }

        public static void AddVection()
        {
            Console.WriteLine("enter first date as format DD/MM");
            String[] first_date = Console.ReadLine().Split('/');
            Console.WriteLine("enter last date as format DD/MM");
            String[] last_date = Console.ReadLine().Split('/');
            int first_day = Int32.Parse(first_date[0]);
            int first_month = Int32.Parse(first_date[1]);
            int last_day = Int32.Parse(last_date[0]);
            int last_month = Int32.Parse(last_date[1]);
            for(int month = first_month-1; month < last_month; month++)
            {
                for(int day = first_month - 1; day < last_month; day++)
                {

                }
            }
                // throw new NotImplementedException();
        }
    }
}
