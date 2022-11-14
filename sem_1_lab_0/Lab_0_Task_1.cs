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

        double x0;
        double xn;
        int n;
        char restart;
        double b = 2.5;

        Console.WriteLine("Function tabulation");
        Console.WriteLine("-------------------\n");

        while (true)
        {
            Console.WriteLine("Enter initial value of x0:");
            x0 = Convert.ToDouble(Console.ReadLine());

            if (x0 < -2.5)
            {
                Console.WriteLine("The entered value does not correspond to the function definition area.Try again!");
                Console.WriteLine("\nEnter initial value of x0:");
                x0 = Convert.ToDouble(Console.ReadLine());
            }

            Console.WriteLine("Enter final value of x:");
            xn = Convert.ToDouble(Console.ReadLine());

            if (xn <= x0)
            {
                Console.WriteLine("The number xn must be greater than the number x0. Try again!");
                Console.WriteLine("\nEnter initial value of xn:");
                x0 = Convert.ToDouble(Console.ReadLine());
            }

            Console.WriteLine("Enter number of intermediate values from x0 to xn:");
            n = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= n; i++)
            {
                double x;
                double y;
                x = x0 + i * (xn - x0) / (double)n;
                y = 9 * (x + 15 * Math.Sqrt(Math.Pow(x, 3) + Math.Pow(b, 3)));
                Console.WriteLine("x{0} = {1}, y(x) = {2}", i, x, y);
            }

            Console.WriteLine("\nWould you like to try again? Write Y if yes and N if no:");
            restart = Convert.ToChar(Console.ReadLine()!);
            while (Char.ToUpper(restart) != 'Y' && Char.ToUpper(restart) != 'N')
            {
                Console.WriteLine("Please, put correct char! Write Y if yes and N if no:");
                restart = Convert.ToChar(Console.ReadLine()!);
            }

            if (Char.ToUpper(restart) == 'Y')
            {
                Console.WriteLine('\n');
                continue;
            }
            else if (Char.ToUpper(restart) == 'N')
            {
                break;
            }
        }
        /*case 1 x0 = 0, y(x) = 533,634355153414
                  x1 = 2, y(x) = 674,1749957137959
           case 2 x0 = 3, y(x) = 908,3856278610402
                  x1 = 4, y(x) = 1240,6433600862954
                  x2 = 5, y(x) = 1645,9030654602418
         */
    }
}
