using System;
using System.Security.Cryptography;

class Program
{
    public static void Main(string[] args)
    {
        //Sorting
        string inputWords = "C:\\Users\\Таня\\OneDrive\\Documents\\KPI\\прога\\lab_2sem\\Lab1\\Task3\\bin\\Debug\\net6.0\\randomWords.txt";
        string outputWords = "C:\\Users\\Таня\\OneDrive\\Documents\\KPI\\прога\\lab_2sem\\Lab1\\Task3\\bin\\Debug\\net6.0\\sortWords.txt";
        string line;
        
        string[] words = File.ReadAllLines(inputWords);

        Sort(words);

        File.WriteAllLines(outputWords, words);
    }
    //InsertionSort
    static void Sort(string[] words)
    {
      
    }
    //Method Compare
    static int Compare(string str1, string str2)
    {
 
    }
}
