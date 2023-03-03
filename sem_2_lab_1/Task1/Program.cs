using System;
using System.IO;

class Program
{
    // Write strings into file
    public static void Main(string[] args)
    {
        using (var sw = new StreamWriter("test.txt"))
        {
 
        }
        using (StreamReader sr = File.OpenText("test.txt"))
        {

        }
    }
}