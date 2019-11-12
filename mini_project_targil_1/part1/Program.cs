using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace part1
{
    class Program
    {
        static Random r = new Random();
        const int ARR_LEN = 20, LOW = 18, HIGH = 122;

        static void Print(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write("{0,-4}", arr[i]);
            }
            Console.WriteLine();
        }




        static void Main(string[] args)
        {
            int[] A = new int[ARR_LEN], B = new int[ARR_LEN], C = new int[ARR_LEN];
            for (int i = 0; i < ARR_LEN; i++)
            {
                A[i] = r.Next(LOW, HIGH);
                B[i] = r.Next(LOW, HIGH);
                C[i] = A[i] > B[i] ? A[i] - B[i] : B[i] - A[i];
            }
            Print(A);
            Print(B);
            Print(C);
            Console.ReadKey();
        }
    }
}
