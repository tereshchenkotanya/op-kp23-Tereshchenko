using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;

public class HashTable<KItem>
{
    private LinkedList<KItem>[] buckets;
    private readonly int capacity;
    private int count;
    private readonly string dictionaryFile;

    public HashTable()
    {
        capacity = 16; // Default initial capacity
        buckets = new LinkedList<KItem>[capacity];
        count = 0;
        dictionaryFile = "Words.txt";

        ReadDictionaryFromFile();
    }

    public HashTable(int initialCapacity)
    {
        capacity = initialCapacity;
        buckets = new LinkedList<KItem>[capacity];
        count = 0;
        dictionaryFile = "Words.txt";

        ReadDictionaryFromFile();
    }

    public void Add(KItem? key)
    {
        int bucketIndex = Math.Abs(key.GetHashCode() % capacity);

        if (buckets[bucketIndex] == null)
        {
            buckets[bucketIndex] = new LinkedList<KItem>();
        }

        LinkedList<KItem> bucket = buckets[bucketIndex];
        if (!bucket.Contains(key))
        {
            bucket.AddLast(key);
            count++;

            UpdateDictionaryFile();
        }
    }

    public void Remove(KItem key)
    {
        int bucketIndex = Math.Abs(key.GetHashCode() % capacity);
        LinkedList<KItem> bucket = buckets[bucketIndex];
        if (bucket != null)
        {
            if (bucket.Remove(key))
            {
                count--;

                UpdateDictionaryFile();
            }
        }
    }

    public bool Contains(KItem? key)
    {
        int bucketIndex = Math.Abs(key.GetHashCode() % capacity);
        LinkedList<KItem> bucket = buckets[bucketIndex];
        return bucket != null && bucket.Contains(key);
    }

    public void Clear()
    {
        buckets = new LinkedList<KItem>[capacity];
        count = 0;

        UpdateDictionaryFile();
    }

    public int Size()
    {
        return count;
    }

    private void ReadDictionaryFromFile()
    {
        string[] lines = File.ReadAllLines(dictionaryFile);
        foreach (string line in lines)
        {
            KItem key = (KItem)Convert.ChangeType(line, typeof(KItem));
            Add(key);
        }
    }

    private void UpdateDictionaryFile()
    {
        List<string>? lines = new List<string>();

        foreach (LinkedList<KItem> bucket in buckets)
        {
            if (bucket != null)
            {
                foreach (KItem key in bucket)
                {
                    lines.Add(key.ToString());
                }
            }
        }

        File.WriteAllLines(dictionaryFile, lines.ToArray());
    }
}

class Program
{
    static void Main(string[] args)
    {
        HashTable<string>? dictionary = new();

        int selectedOption = 0;

        string[] menuOptions =
        {
            "Add new word",
            "Delete word",
            "Check if word exist",
            "Delete all words"
        };

        bool exitOptionMenu = false;

        while (!exitOptionMenu)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            string text = "Choose function you need:";
            Console.WriteLine(text);
            Console.ResetColor();
            Console.WriteLine();
            Console.CursorVisible = false;

            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine($"{menuOptions[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{menuOptions[i]}");
                }
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedOption = (selectedOption - 1 + menuOptions.Length) % menuOptions.Length;
                    break;
                case ConsoleKey.DownArrow:
                    selectedOption = (selectedOption + 1) % menuOptions.Length;
                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    Console.CursorVisible = true;
                    HandleOption(selectedOption, dictionary);
                    break;
                default:
                    break;
            }
        }
    }
    private static void HandleOption(int selectedOption, HashTable<string> dictionary)
    {
        string? word;
        switch (selectedOption)
        {
            case 0:
                Console.Clear();
                Console.Write("Write word you want to add: ");
                word = Console.ReadLine(); 
                dictionary.Add(word);

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Word was added");
                Console.ResetColor();
                Escape();
                break;
            case 1:
                Console.Clear();
                Console.Write("Write word you want to remove: ");
                word = Console.ReadLine();

                if(dictionary.Contains(word))
                {
                    dictionary.Remove(word);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Word was removed");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Word does not exist");
                    Console.ResetColor();
                }
                Escape();
                break;
            case 2:
                Console.Clear();
                Console.Write("Write word you want to check: ");
                word = Console.ReadLine();

                if(dictionary.Contains(word))
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Word exist");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Word does not exist");
                    Console.ResetColor();
                }
                Escape();
                break;
            case 3:
                Console.Clear();
                dictionary.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Dictionary has been cleared");
                Console.ResetColor();
                Escape();
                break;
            case 4:
                Environment.Exit(0);
                break;
            default:
                break;
        }
    }
    private static void Escape()
    {
        Console.CursorVisible = false;
        while (true)
        {
            ConsoleKeyInfo key;
            key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
        }
    }
}
