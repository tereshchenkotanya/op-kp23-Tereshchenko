using System;

class Program
{
    public static void Main(string[] args)
    {
        //Function tabulation
        double x0 = 0;
        double xn = 2;
        int x_number = 0;
        int n = 1;
        double b = 2.5;

        for (int i = 0; i <= n; i++)
        {
            double x;
            double y;
            x = x0 + i * (xn - x0) / n;
            y = 9 * (x + 15 * Math.Sqrt(Math.Pow(x, 3) + Math.Pow(b, 3)));
            Console.WriteLine("x" + x_number + " = " + x + ", " + "y(x) = " + y);
            x_number++;
        }
    }
}
