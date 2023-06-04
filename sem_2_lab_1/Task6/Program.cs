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
                string[] fields = Split(line);
                string name = fields[0];
                string surname = fields[1];
                int score = int.Parse(fields[2]);

                writer.Write(name + "," + surname + "," + score);
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
                    string stringData = reader.ReadString();
                    string[] fields = Split(stringData);

                    string name = fields[0];
                    string surname = fields[1];
                    int score = int.Parse(fields[2]);

                    if (score > 95)
                    {
                        count++;

                        writer.Write(name + ',' + score);
                    }
                }
            }
        }
        if (count > 0)
        {
            Console.WriteLine("Number of students with score greater than 95: " + count);
        }
        else
        {
            Console.WriteLine("Anyone has a great score");
        }

        /*test cases:
         * case1:Number of students with score greater than 95:1 
         * case2:Anyone has a great score
         * case3:Number of students with score greater than 95:2 
         */
    }
    static string[] Split(string line)
    {
        string substring = "";
        int number = 0;
        string[] splitData = new string[3];

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] != ',')
            {
                substring += line[i];
            }
            else
            {
                splitData[number] = substring;
                number++;
                substring = "";
            }
        }
        splitData[number] = substring;

        return splitData;
    }
}

