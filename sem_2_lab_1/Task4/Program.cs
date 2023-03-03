using System;
class Program
{
    public static void Main(string[] args)
    {
        string csvFilePath = "C:\\Users\\Таня\\OneDrive\\Documents\\KPI\\прога\\lab_2sem\\Lab1\\task4\\bin\\Debug\\net6.0\\CSV.txt";
         
         /*test cases:
         * case1: John,Smith,87
                  Emily,Jones,92
                  Daniel,Lee,50
         * case2: Ava,Johnson,95
                  Michael,Miller,83
                  Olivia,Davis,89
         * case3: Ethan,Wilson,40
                  Sophia,Garcia,70
                  William,Anderson,86 
         */

        string[] lines = File.ReadAllLines(csvFilePath);
        string[] splitData = new string[3];

        bool flag = false;

        if (!flag)
        {

        }
        
         /* case1: All students have more then 60 points
         * case2: Daniel,Lee,50
         * case3: Ethan,Wilson,40
         */
    }
    static string[] Split(string line, string[] splitData)
    {        
        
    }
}