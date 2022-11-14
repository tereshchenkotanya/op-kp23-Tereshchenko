using System;
class Program
{
    static double Factor(int a)
    {
        double factorial = 1;

        for (int i = 1; i <= a; i++)
        {
            factorial *= i;
        }
        return factorial;
    }

    static double Pow(int degree, double x_org)
    {
        double x = x_org;

        if (degree == 0)
        {
            return 1;
        }
        for (int i = 1; i < degree; i++)
        {
            x *= x_org;
        }

        return x;
    }
    static double Abs(double num)
    {
        if (num >= 0)
        {
            return num;
        }

        return -num;
    }
    static void Main(string[] args)
    {
        //Calculate the trigonometric function sin(x)
        /*
         * case 1 x = 0, precision = 1;
         * case 2 x = 0.5, precision = 0,0000001;
         * case 3 x = 0, precision = 0,0000001;
         * case 4 x = 0.5, precision = 1;
        */

        double x;
        double t;
        double precision;

        Console.WriteLine("Calculate the trigonometric function sin(x)");
        Console.WriteLine("-------------------------------------------\n");
        double sin_x = 0;
        int n = 0;
        int x_degree;

        Console.WriteLine("Enter value for x:");
        x = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter value for precision:");
        precision = Convert.ToDouble(Console.ReadLine());

        while (true)
        {
            x_degree = 2 * n + 1;
            double sign = (double)Pow(n, -1);
            double power = (double)Pow(x_degree, x);
            double factorial = (double)Factor(x_degree);

            t = sign * power / factorial;
            sin_x += t;
            n++;

            if (Abs(t) < precision)
            {
                break;
            }
        }

        Console.WriteLine("sin(x) = {0}", sin_x);
        
        /*
          * case 1 sin_x = 0;
          * case 2 sin_x = 0,479425538604203;
          * case 3 sin_x = 0;
          * case 4 sin_x = 0,5;
         */
    }
}
