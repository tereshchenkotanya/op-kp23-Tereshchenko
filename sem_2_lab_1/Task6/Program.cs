using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Read the CSV file
        string csvFilePath = "students.txt";
        string binaryFilePath = "students.bin";
        string binaryFilePath2 = "students2.bin";

        string[] lines = File.ReadAllLines(csvFilePath);
        
        /*test cases:
         * case1: John,Smith,87
                  Emily,Jones,96
                  Daniel,Lee,50
         * case2: Ava,Johnson,94
                  Michael,Miller,83
                  Olivia,Davis,89
         * case3: Ethan,Wilson,99
                  Sophia,Garcia,98
                  William,Anderson,86 
         */

        // Create the binary file based on the CSV file

        using (BinaryWriter writer = new BinaryWriter(File.Open(binaryFilePath, FileMode.Create)))
        {
            foreach (string line in lines)
            {
                
            }
        }

        // Read the binary file and create another binary file containing the number of students
        // whose score is greater than 95 and all the records for those students
        int count = 0;
        using (BinaryReader reader = new BinaryReader(File.Open(binaryFilePath, FileMode.Open)))
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(binaryFilePath2, FileMode.Create)))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    
                }
            }
        }
        if (count > 0)
        {

        }
        else
        {

        }
        /*test cases:
         * case1:Number of students with score greater than 95:1 
         * case2:Anyone has a great score
         * case3:Number of students with score greater than 95:2 
         */
    }
    static string[] Split(string line)
    {
        return splitData;
    }
}


