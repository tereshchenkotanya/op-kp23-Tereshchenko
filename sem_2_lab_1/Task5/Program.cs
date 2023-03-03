using System;
using System.Reflection;
using System.Runtime.ConstrainedExecution;

class Program
{
    public static void Main(string[] args)
    {
        /*test cases:
        *case1: aa bb
                bb aa
        *case2: The first line
                The second line
        *case3: cat dog parrot
                dog cat parrot
                parrot dog cat
        */
        string text = "C:\\Users\\Таня\\OneDrive\\Documents\\KPI\\прога\\lab_2sem\\Lab1\\Task5\\bin\\Debug\\net6.0\\text.txt";

        string[] lines = File.ReadAllLines(text);
        string[,] list = new string[1000, 2];
        int rowIndex = 0;

        for (int i = 0; i < rowIndex; i++)
        {
            Console.WriteLine(list[i, 0] + ": " + list[i, 1]);
        }
        /*test cases:
        *case1: aa: 2
                bb: 2
        *case2: The first line
                The second line
                the: 2
                first: 1
                line: 2
                second: 1
                line: 2
        *case3: cat: 3
                dog: 3
                parrot: 3
        */
    }
    static void SplitAndCount(string[,] list, string line, int rowIndex)
    {
       
    }
}