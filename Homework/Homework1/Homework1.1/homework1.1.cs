using System;
using System.Transactions;

namespace Practice1
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;

            Console.WriteLine("We're going to search the minimum number");

            Console.WriteLine("Enter value for a: ");
            a = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter value for b: ");
            b = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter value for c: ");
            c = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter value for d: ");
            d = Convert.ToInt32(Console.ReadLine());

            int min = a;
            string variableName = "A";

            if (min > b)
            {
                min = b;
                variableName = "B";
            }

            if (min > c)
            {
                min = c;
                variableName = "C";
            }

            if (min > d)
            {
                min = d;
                variableName = "D";
            }

            Console.WriteLine(variableName + " is the minimum number: " + min);
        }
    }
}
