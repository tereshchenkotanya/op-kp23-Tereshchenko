using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;

public class HashTable<KItem, VItem>
{
    private readonly LinkedList<KeyValuePair<KItem, VItem>>[] buckets;
    private readonly int capacity;
    private int count;
    private readonly string dictionaryFile;

    public HashTable()
    {
        capacity = 1023;
        buckets = new LinkedList<KeyValuePair<KItem, VItem>>[capacity];
        count = 0;
        dictionaryFile = "Words.txt";

        ReadDictionaryFromFile();
    }

    public HashTable(int initialCapacity)
    {
        capacity = initialCapacity;
        buckets = new LinkedList<KeyValuePair<KItem, VItem>>[capacity];
        count = 0;
        dictionaryFile = "Words.txt";

        ReadDictionaryFromFile();
    }

    private int GetBucketIndex(KItem? key)
    {
        int hashCode = key.GetHashCode();
        return (hashCode & 0x7FFFFFFF) % capacity;
    }
    
    public void Add(KItem? key, VItem? value)
    {
        int bucketIndex = GetBucketIndex(key);

        if (buckets[bucketIndex] == null)
        {
            buckets[bucketIndex] = new LinkedList<KeyValuePair<KItem, VItem>>();
        }

        LinkedList<KeyValuePair<KItem, VItem>> bucket = buckets[bucketIndex];
        foreach (KeyValuePair<KItem, VItem> pair in bucket)
        {
            if (pair.Key.Equals(key))
            {
                KeyValuePair<KItem, VItem> updatedPair = new KeyValuePair<KItem, VItem>(key, value);
                bucket.Remove(pair);
                bucket.AddLast(updatedPair);
                UpdateDictionaryFile();
                return;
            }
        }

        bucket.AddLast(new KeyValuePair<KItem, VItem>(key, value));
        count++;

        UpdateDictionaryFile();
    }

    public void Remove(KItem? key)
    {
        int bucketIndex = GetBucketIndex(key);
        LinkedList<KeyValuePair<KItem, VItem>> bucket = buckets[bucketIndex];

        if (bucket != null)
        {
            LinkedListNode<KeyValuePair<KItem, VItem>> current = bucket.First;
            while (current != null)
            {
                if (current.Value.Key.Equals(key))
                {
                    bucket.Remove(current);
                    count--;

                    UpdateDictionaryFile();
                    return;
                }

                current = current.Next;
            }
        }
    }


    public bool Contains(KItem? key)
    {
        int bucketIndex = GetBucketIndex(key);
        LinkedList<KeyValuePair<KItem, VItem>> bucket = buckets[bucketIndex];
        if (bucket != null)
        {
            foreach (KeyValuePair<KItem, VItem> pair in bucket)
            {
                if (pair.Key.Equals(key))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void Clear()
    {
        Array.Clear(buckets, 0, buckets.Length);
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
            Add(key, default(VItem));
        }
    }

    private void UpdateDictionaryFile()
    {
        List<string> lines = new List<string>();

        foreach (LinkedList<KeyValuePair<KItem, VItem>> bucket in buckets)
        {
            if (bucket != null)
            {
                foreach (KeyValuePair<KItem, VItem> pair in bucket)
                {
                    lines.Add(pair.Key.ToString());
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
        HashTable<string, string> dictionary = new HashTable<string, string>();

        int selectedOption = 0;

        string[] menuOptions =
        {
            "Add new word",
            "Delete word",
            "Check if word exist",
            "Delete all words",
            "Show size"
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
    private static void HandleOption(int selectedOption, HashTable<string, string> dictionary)
    {
        string? word;
        switch (selectedOption)
        {
            case 0:
                Console.Clear();
                Console.Write("Write word you want to add: ");
                word = Console.ReadLine();
                dictionary.Add(word, word);

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

                if (dictionary.Contains(word))
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

                if (dictionary.Contains(word))
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
                Console.Clear();
                Console.WriteLine("The size of dictionary is: " + dictionary.Size());
                Escape();
                break;
            case 5:
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