using System;
using System.Security.Cryptography;

class Program
{
    public static void Main(string[] args)
    {
        /*test cases:
         case1:randomWords: Whimsical
                            Enigmatic
                            Radiant
         case2:randomWords: Blissful
                            Nostalgic
                            Fanciful
         case3:randomWords: Quirky
                            Serendipity
                            Enchanting
         */
        string inputWords = "C:\\Users\\Таня\\OneDrive\\Documents\\KPI\\прога\\lab_2sem\\Lab1\\Task3\\bin\\Debug\\net6.0\\randomWords.txt";
        string outputWords = "C:\\Users\\Таня\\OneDrive\\Documents\\KPI\\прога\\lab_2sem\\Lab1\\Task3\\bin\\Debug\\net6.0\\sortWords.txt";
        string line;
        
        string[] words = File.ReadAllLines(inputWords);

        Sort(words);

        File.WriteAllLines(outputWords, words);
        /*
         * case1: sortWords: Enigmatic
                             Radiant
                             Whimsical
           case1: sortWords: Blissful
                             Fanciful
                             Nostalgic
           case1: sortWords: Enchanting
                             Quirky
                             Serendipity
        */
    }
    //InsertionSort
    static void Sort(string[] words)
    {
        for (int i = 1; i < words.Length; i++)
        {
            string str = words[i];
            int j = i - 1;
            while(j >= 0 && Compare(words[j], str) > 0)
            {
                words[j + 1] = words[j];
                j--;
            }
            words[j + 1] = str;
        }
    }
    //Method Compare
    static int Compare(string str1, string str2)
    {
        int len1 = str1.Length;
        int len2 = str2.Length;

        int length = len2;

        if (len1 < len2)
        {
            length = len1;
        }
        else
        {
            length = len2;
        }

        for (int i = 0; i < length; i++)
        {
            //str1[i] == str2[i];
            if (str1[i] == str2[i])
            {
                continue;
            }

            if (str1[i] > str2[i])
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        if (len1 > len2)
        {
            return 1;
        }
        else if (len1 < len2)
        {
            return -1;
        }

        return 0;
    }
}
