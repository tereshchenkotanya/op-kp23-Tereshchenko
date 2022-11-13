using System;

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
        int n = 1;
        double b = 2.5;

        double x = 0;
        int i = 1;
        double y = 559.445842022628;

        Console.WriteLine("x{0} = {1}, y(x) = {2}", i, x, y);
        /* case 1 x0 = 0, y(x) = 533,634355153414
                  x1 = 2, y(x) = 674,1749957137959
           case 2 x0 = 3, y(x) = 908,3856278610402
                  x1 = 4, y(x) = 1240,6433600862954
                  x2 = 5, y(x) = 1645,9030654602418
         */
    }
}
