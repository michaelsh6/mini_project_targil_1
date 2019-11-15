//michael shachor 206197733
//Shimon Mizrahi 203375563

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace part2
{
    class Program
    {
        const int MONTH = 12, DAY = 31;
        static bool[,] calander = new bool[MONTH, DAY];

        static void Main(string[] args)
        {
            string user_input;
            //
            do
            {
                PrintMenu();
                user_input = Console.ReadLine();
                //
                switch (user_input)
                {
                    case "v"://add new vection.
                        AddVection();
                        break;

                    case "s": //year list.
                        PrintYearList();
                        break;

                    case "d"://total occupied number of days per year, and the percentage of annual occupancy.
                        PrintCalculation();
                        break;

                    case "e"://exit.
                        break;

                    default:
                        Console.WriteLine("Eror: not option");
                        break;
                }

            } while (user_input != "e");
        }
        //print menu function.
        public static void PrintMenu()
        {
            Console.WriteLine("Please entr your choice:");
            Console.WriteLine("To add new vection press v");
            Console.WriteLine("To show the year list press s");
            Console.WriteLine("To displays the total occupied number of days per year, and the percentage of annual occupancy press d");
            Console.WriteLine("For exit press e \n");
        }
        //the function add vection.

        public static bool ValidDate(string [] date)
        {
            if(date.Length != 2)
                return false;

            int day; //= Int32.Parse(date[0]);
            bool isNumeric = int.TryParse(date[0], out day);
            if (!isNumeric)
                return false;

            int month;// = Int32.Parse(date[1]);
            isNumeric = int.TryParse(date[1], out month);
            if (!isNumeric)
                return false;



            
            return ValidDate(day,month);
        }

        public static bool ValidDate(int day,int month)
        {
            if (day < 1 || day > 31 || month < 1 || month > 12) //if date not correct.
            {
                return false;
            }
            return true;
        }
            
            public static void AddVection()
        {
            bool validLastDate = false;
            bool validFirstDate = false;
            //
            String[] first_date, last_date;
            int first_day, last_day;
            int first_month, last_month;
            //
            do
            {
                Console.WriteLine("enter first date as format DD/MM");
                first_date = Console.ReadLine().Split('/');
                validFirstDate = ValidDate(first_date); //if date not correct.
                if (validFirstDate)
                {
                    first_day = Int32.Parse(first_date[0]);
                    first_month = Int32.Parse(first_date[1]);
                }
                else
                {
                    Console.WriteLine("Erorr date");
                    return;
                }
                //
                //if ((first_day > 0 && first_day < 32) && (first_month > 0 && first_month < 13))//if date not correct.
                //{
                //    validFirstDate = true;
                //}
            } while (!validFirstDate);
            //

            do
            {
                Console.WriteLine("enter last date as format DD/MM");
                last_date = Console.ReadLine().Split('/');
                validLastDate = ValidDate(last_date); //if date not correct.
                if (validFirstDate)
                {
                    last_day = Int32.Parse(last_date[0]);
                    last_month = Int32.Parse(last_date[1]);
                    if ((last_month < first_month) || ((last_month == first_month) && (last_day < first_day)))//if last date is bigger than first date.
                    {
                        Console.WriteLine("Erorr range date");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Erorr date");
                    return;
                }

            } while (!validLastDate);

            //
            //bool flag = false;
            //for (int i = first_month - 1; i < last_month; i++)
            //{
            //    for (int j = first_day; j < last_day; j++)
            //    {
            //        if (calander[i, j] == true)//check if date is free.
            //            flag = true;
            //    }
            //}
            ////
            //if (!flag)//add new vection.
            //{

            int sum = (last_month - first_month) * DAY + (last_day + 1) - first_day;//sum of days.
            int year_day = first_day+ (first_month-1)*DAY;
            bool valid = true;
            //
            for (int i = 1; i < sum-1; i++)
            {
                //if (calander[(first_month - 1) + (i / 31), (first_day - 1) + (i % 31)])//if vection is fullybook.
                //if (calander[((i+ first_day-1)/31 +first_month)-1, ((i + first_day-1) % 31)])//if vection is fullybook.
                int day = (year_day + i-1) % DAY;
                int month = (year_day + i -1) / DAY;
                if (calander[month, day])//if vection is fullybook.
                {
                   
                    valid = false;
                    Console.WriteLine("The application was rejected ");
                    break;
                }
            }

            if (valid)
            {

                for (int i = 0; i < sum; i++)
                {
                    int day = (year_day + i - 1) % DAY;
                    int month = (year_day + i - 1) / DAY;
                    calander[month, day] = true;
                    //calander[((i + first_day - 1) / 31 + first_month) - 1, ((i + first_day - 1) % 31)] = true;//add tha range into calander.
                }
                Console.WriteLine("The application was approved");
            }

        }
        //the function print all year list.


        public static void PrintYearList()
        {
            bool flag = false;
            int day = 0, month = 0;
            int lastDay = 0, lastMonth = 0;
            //
            for (int i = 0; i < MONTH; i++)
            {
                for (int j = 0; j < DAY; j++)
                {
                    if ((calander[i, j]))//if find a booking.
                    {
                        if (!flag)
                        {
                            day = j;
                            month = i;
                        }
                        flag = true;
                        //
                        lastDay = j;
                        lastMonth = i;
                    }
                    //
                    if ((flag) && (!calander[i, j]))//print all the range.
                    {
                        Console.WriteLine("first date: " + (day + 1).ToString() + "/" + (month + 1).ToString() + " last date: " + (lastDay + 1).ToString() + "/" + (lastMonth + 1).ToString());
                        flag = false;
                    }
                }
            }
        }
        //the function print the total occupied number of days per year, and the percentage of annual occupancy.
        public static void PrintCalculation()
        {
            int TotalNumber = 0;
            double Percentage = 0;
            //
            for (int i = 0; i < MONTH; i++)
            {
                for (int j = 0; j < DAY; j++)
                {
                    if (calander[i, j])
                    {
                        TotalNumber++;//count all invition.
                    }
                }
            }
            //
            if (TotalNumber != 0)//if there are not invition.
                Percentage = ((double)TotalNumber / 372);
            //
            Console.WriteLine("Total number of occupied days per year: " + TotalNumber.ToString());
            Console.WriteLine("Percentage of annual occupancy: " + Percentage.ToString());
        }

    }
}
