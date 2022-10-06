using System;
using System.Transactions;

namespace Practice1
{
    class Program
    {
        static void Main(string[] args)
        {
            double a = 0;
            double b = 0;
            double c = 0;
            double p = 0;
            double s = 0;
            double d = 0;

            Console.WriteLine("We are going to find S of a triangle using the Heron formula");

            Console.WriteLine("Enter value for a");
            a = Convert.ToDouble(Console.ReadLine());

            string negativeErrorMessage = "You cannot use negative numbers";
            string ErrorMessage = "The specified numbers do not correspond to the properties of the triangle";

            if (a <= 0)
            {
                Console.WriteLine(negativeErrorMessage);
            }
            else
            {
                Console.WriteLine("Enter value for b");
                b = Convert.ToDouble(Console.ReadLine());

                if (b <= 0)
                {
                    Console.WriteLine(negativeErrorMessage);
                }
                else
                {
                    Console.WriteLine("Enter value for c");
                    c = Convert.ToDouble(Console.ReadLine());

                    if (c <= 0)
                    {
                        Console.WriteLine(negativeErrorMessage);
                    }
                    else
                    { 
                        if ((a + b) <= c)
                        {
                            Console.WriteLine(ErrorMessage);
                        }
                        else
                        {
                            if ((b + c) <= a)
                            {
                                Console.WriteLine(ErrorMessage);
                            }
                            else
                            {
                                if ((c + a) <= b)
                                {
                                    Console.WriteLine(ErrorMessage);
                                }
                                else
                                {
                                    p = (a + b + c) / 2;
                                    s = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

                                    Console.WriteLine("S = " + s);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}