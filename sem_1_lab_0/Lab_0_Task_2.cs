using System;

class Program
{
    public static void Main(string[] args)
    { 
        //Write a program that determines whether the number n is prime.

        /*
         * case 1 num = 1
         * case 2 num = 6
         * case 3 num = 5
         * case 4 num = 8
        */
        int num;
        char restart;
        string positiveResult = "\nThe number is prime\n";
        string negativeResult = "\nThe number is not prime\n";
        string notNumberResult = "\nThe input is not number\n";
        string result;

        Console.WriteLine("Сheck the number to see if it is prime or not\n");

        num = Convert.ToInt32(Console.ReadLine());

        while (true)
        {
            result = positiveResult;
            Console.WriteLine("Enter the number:\n");
            string? inputText = Console.ReadLine();
            bool isInuptNumber = int.TryParse(inputText, out num);
            if (isInuptNumber && num > 0)
            {
                for (int i = 2; i < num; i++)
                {
                    if (num % i == 0)
                    {
                        result = negativeResult;
                        break;
                    }
                }
            }
            else if (isInuptNumber)
            {
                result = negativeResult;
            }
            else
            {
                result = notNumberResult;
            }
            Console.WriteLine(result);

            Console.WriteLine("Would you like to try again? Write Y if yes and N if no:\n");
            restart = Convert.ToChar(Console.ReadLine()!);
            while (Char.ToUpper(restart) != 'Y' && Char.ToUpper(restart) != 'N')
            {
                Console.WriteLine("Please, put correct char! Write Y if yes and N if no:\n");
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
        /*
         * case 1 positiveResult
         * case 2 negativeResult
         * case 3 positiveResult
         * case 4 negativeResult
        */
    }
}