using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    public static void Main(string[] args)
    {
        //The max number of line

        string allNumbersFilePath = "C:\\Users\\Таня\\OneDrive\\Documents\\KPI\\прога\\lab_2sem\\Lab1\\Task2\\bin\\Debug\\net6.0\\numbers.txt";
        string maxNumberFilePath = "C:\\Users\\Таня\\OneDrive\\Documents\\KPI\\прога\\lab_2sem\\Lab1\\Task2\\bin\\Debug\\net6.0\\max.txt";
        int maxNumber = 0;
        Random rand = new Random();

        using (StreamWriter sw = File.CreateText(allNumbersFilePath))
        {
            
        }

        using (StreamWriter sw = File.CreateText(maxNumberFilePath))
        {
        }

    }
}