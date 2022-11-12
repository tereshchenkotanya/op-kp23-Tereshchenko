using System;
using System.Runtime.InteropServices;

class Program
{
    public static void Main(string[] args)
    {
        //Function tabulation
        /*
         * case 1 x0 = 0, xn = 2, n = 1;
         * case 2 x0 = 3, xn = 5, n = 2;
         */
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
        /*
         * case 1 x0 = 0, y(x) = 533,634355153414
                  x1 = 2, y(x) = 674,1749957137959
           case 2 x0 = 3, y(x) = 908,3856278610402
                  x1 = 4, y(x) = 1240,6433600862954
                  x2 = 5, y(x) = 1645,9030654602418
         */
    }
}
