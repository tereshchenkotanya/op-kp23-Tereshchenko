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
    }
    static string[] Split(string line)
    {
        return splitData;
    }
}


