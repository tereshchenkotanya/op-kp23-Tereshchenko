using System;
class Program
{
    public static void Main(string[] args)
    {
        //Calculation of the factorial
        /*case 1 n = 1;
        case 2 n = 2;
        case 3 n = 3;
        case 4 n = 4;
        */
        //Calculation of the factorial
        int num;

        Console.WriteLine("Calculation of the factorial");
        Console.WriteLine("----------------------------\n");

        int factorial = 1;
        string negativeResult = "The number cannot be negative. Try again!";
        Console.WriteLine("Enter the number:");
        num = Convert.ToInt32(Console.ReadLine());

        if (num < 0)
        {
            Console.WriteLine(negativeResult);
        }

        for (int i = 1; i <= num; i++)
        {
            factorial *= i;
        }

        Console.WriteLine("factorial = " + factorial);
        /*case 1 factorial = 1;
        case 2 factorial = 2;
        case 3 factorial = 6;
        case 4 factorial = 24;
        */
    }
}