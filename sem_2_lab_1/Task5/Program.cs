using System;
using System.Reflection;
using System.Runtime.ConstrainedExecution;

class Program
{
    public static void Main(string[] args)
    {
        string text = "C:\\Users\\Таня\\OneDrive\\Documents\\KPI\\прога\\lab_2sem\\Lab1\\Task5\\bin\\Debug\\net6.0\\text.txt";

        string[] lines = File.ReadAllLines(text);
        string[,] list = new string[1000, 2];
        int rowIndex = 0;

        for (int i = 0; i < rowIndex; i++)
        {
            Console.WriteLine(list[i, 0] + ": " + list[i, 1]);
        }
    }
    static void SplitAndCount(string[,] list, string line, int rowIndex)
    {
       
    }
}