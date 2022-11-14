using System;
class Program
{
    static void Main(string[] args)
    {
        //Calculate the trigonometric function sin(x)
        /*
         * case 1 x = 0, precision = 1;
         * case 2 x = 0.5, precision = 0,0000001;
         * case 3 x = 0, precision = 0,0000001;
         * case 4 x = 0.5, precision = 1;
        */

        double x = 0;
        double precision = 1;
        double sin_x = 0;

        Console.WriteLine("sin(x) = {0}", sin_x);
        /*
          * case 1 sin_x = 0;
          * case 2 sin_x = 0,479425538604203;
          * case 3 sin_x = 0;
          * case 4 sin_x = 0,5;
         */
    }
}
