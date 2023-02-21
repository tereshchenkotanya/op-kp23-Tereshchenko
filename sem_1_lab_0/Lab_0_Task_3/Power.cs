using System;
class Program
{
    public static void Main(string[] args)
    {
        // Calculation of the value of x^n

        /*case 1 x_org = 2; n = 2;
        case 2 x_org = 2; n = -1; 
        case 3 x_org = 10; n = 0; 
        case 4 x_org = 1; n = 5; 
        */

        int n;
        int x_org;

        Console.WriteLine("Calculation of the value of x^n:");
        Console.WriteLine("--------------------------------");

        Console.WriteLine("Enter the value of x:");
        x_org = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter the value of n:");
        n = Convert.ToInt32(Console.ReadLine());

        double x = x_org;

        if (n == 0)
        {
            x_org = 1;
            Console.WriteLine("x^n = " + x_org);
        }
        else if (n > 0)
        {
            for (int i = 1; i < n; i++)
            {
                x *= x_org;
            }
            Console.WriteLine("x^n = " + x);
        }
        else
        {
            double rev_x;
            for (int i = -1; i > n; i--)
            {
                x *= x_org;
            }
            rev_x = 1 / (double)x;
            Console.WriteLine("x^n = " + rev_x);
        }
        /*
        case 1 x_org = 4;
        case 2 x_org = 0.5;
        case 3 x_org = 1;
        case 4 x_org = 5;
        */
    }
}
