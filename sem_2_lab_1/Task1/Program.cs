using System;
using System.IO;

class Program
{
    // Write strings into file
    public static void Main(string[] args)
    {
        using (var sw = new StreamWriter("test.txt"))
        {
            sw.WriteLine("It's corn! A big lump with knobs. It has the juice (it has the juice)");
            sw.WriteLine("I can't imagine a more beautiful thing (woo) IT'S CORN!");
        }
        using (StreamReader sr = File.OpenText("test.txt"))
        {
            string s = "";
            while ((s = sr.ReadLine()) != null)
            {
                Console.WriteLine(s);
            }
        }
    }
}