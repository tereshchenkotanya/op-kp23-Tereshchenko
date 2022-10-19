using System;
public class Exercise33
{
    public static void Main()
    {
        int row, c = 1, ind, j;

        Console.Write("Display the Pascal's triangle:\n");
        Console.Write("\n\n");

        Console.Write("The number of rows: ");
        row = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < row; i++)
        {
            for (ind = 1; ind <= row - i; ind++)
            {
                Console.Write("    ");
            }
            for (j = 0; j <= i; j++)
            {
                if (j == 0 || i == 0)
                {
                    c = 1;
                }
                else
                {
                    c = c * (i - j + 1) / j;
                }
                Console.Write(c + "      ");
            }
            Console.Write("\n");
        }
    }
}
