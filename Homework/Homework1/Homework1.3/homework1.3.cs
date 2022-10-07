using System;
using System.Transactions;

namespace Practice1
{
    class Program
    {
        static void Main(string[] args)
        {
            double x1 = 0;
            double y1 = 0;
            double x2 = 0;
            double y2 = 0;
            double a1 = 0;
            double a2 = 0;
            double m = 0;

            Console.WriteLine("We are going to find the modulus of the vector with the coordinates of the ends (x1;y1) (x2;y2)");
            Console.WriteLine("Enter value for x1");
            x1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter value for y1");
            y1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter value for x2");
            x2 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter value for y2");
            y2 = Convert.ToDouble(Console.ReadLine());

            a1 = x2 - x1;
            a2 = y2 - y1;
            m = Math.Sqrt(a1 * a1 + a2 * a2);

            Console.WriteLine("Modulus of the vector is " + m);
        }
    }
}