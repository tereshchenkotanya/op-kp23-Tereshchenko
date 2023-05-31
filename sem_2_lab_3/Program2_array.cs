using System;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

public interface IIterator<T>
{
    bool HasNext { get; }
    T MoveNext();
}

public class RandomizedQueue<Item> : IIterator<Item>
{
    private Item[] _items;
    private int _endIndex;
    private const int minSize = 5;
    private const double percentToExpand = 0.75;
    private const double percentToCompress = 0.25;
    private int _iteratorIndex;
    private Random rnd = new Random();

    // construct an empty randomized queue
    public RandomizedQueue()
    {
        _items = new Item[minSize];
        _endIndex = -1;
    }

    // is the randomized queue empty?
    public bool IsEmpty()
    {
        if (_endIndex == -1)
        {
            return true;
        }
        return false;
    }
    public bool IsFull()
    {
        if (_endIndex == (_items.Length - 1))
        {
            return true;
        }

        return false;
    }

    // return the number of items on the randomized queue
    public int Size()
    {
        int size = _endIndex + 1;
        return size;
    }
    private void Expand()
    {
        Item[] resizedArray = new Item[_items.Length * 2];
        Array.Copy(_items, 0, resizedArray, 0, Size());

        _items = resizedArray;
    }
    private void Compress()
    {
        Item[] resizedArray = new Item[_items.Length / 2];
        Array.Copy(_items, 0, resizedArray, 0, Size());

        _items = resizedArray;
    }
    public bool CheckIfCompessNeeded()
    {
        return Size() < Math.Round(percentToCompress * _items.Length) & _items.Length > minSize;
    }
    public bool CheckIfExpandNeeded()
    {
        return _endIndex == _items.Length - 1 || Size() > percentToExpand * _items.Length;
    }
    // add the item
    public void Enqueue(Item item)
    {
        if (CheckIfExpandNeeded())
        {
            Expand();
        }

        _endIndex++;
        _items[_endIndex] = item;
    }

    // remove and return a random item
    public Item Dequeue()
    {
        int randomIndex = rnd.Next(0, _endIndex);
        Item randomElement;

        if (IsEmpty())
        {
            throw new NullReferenceException("The queque is empty");
        }
        else
        {
            randomElement = _items[randomIndex];
            _items[randomIndex] = _items[_endIndex];
            _endIndex--;
        }

        if (CheckIfCompessNeeded())
        {
            Compress();
        }

        return randomElement;
    }

    // return a random item (but do not remove it)
    public Item Sample()
    {
        int randomIndex = rnd.Next(0, _endIndex);
        Item randomElement;

        if (IsEmpty())
        {
            throw new NullReferenceException("The queque is empty");
        }
        else
        {
            randomElement = _items[randomIndex];
        }
        return randomElement;
    }

    // return an independent iterator over items in random order
    public IIterator<Item> Iterator()
    {
        _iteratorIndex = 0;

        for (int i = 0; i < Size(); ++i)
        {
            int r = rnd.Next(i, _endIndex);
            Item tmp = _items[r];
            _items[r] = _items[i];
            _items[i] = tmp;
        }
        return this;
    }

    public bool HasNext
    {
        get
        {
            return _iteratorIndex < Size();
        }
    }

    public Item MoveNext()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("No more items in the queue");
        }

        Item item = _items[_iteratorIndex];
        _iteratorIndex++;

        return item;
    }
    public Item[] GetQueue { get { return _items; } }
    public int GetEndIndex { get { return _endIndex; } }
    public Item GetLastElement { get { return _items[_endIndex]; } }
    public int GetIteratorIndex { get { return _iteratorIndex; } }
    public Item GetElement(int i) 
    { 
        return _items[i]; 
    }
}

class Program
{
    public static void PrintQueue(RandomizedQueue<string> deque)
    {
        if (deque.IsEmpty())
        {
            Console.WriteLine("Your deque is empty now");
        }
        else
        {
            IIterator<string> dequeIterator = deque.Iterator();
            while (dequeIterator.HasNext)
            {
                string currentEl = dequeIterator.MoveNext();
                Console.Write(currentEl + " ");
            }
        }
    }

    public static void Main(String[] args)
    {
        RandomizedQueue<string> deque = new RandomizedQueue<string>();
        UnitTests unitTests = new UnitTests();
        int answer = 0;
        string element;
        string currentElement;

        while (answer != 5)
        {
            PrintQueue(deque);
            Console.WriteLine();
            Console.WriteLine("What do you want to do with your deque? Choose the number");
            Console.WriteLine("1. Add a random element.");
            Console.WriteLine("2. Remove a random element");
            Console.WriteLine("3. Show a random element");
            Console.WriteLine("4. Show unit tests");
            Console.WriteLine("5. Exit");
            Console.WriteLine();

            try
            {
                Console.Write("Write your answer: ");
                answer = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("Your answer is not correct");
            }

            switch (answer)
            {
                case 1:
                    Console.Write("Enter the element you want to add: ");
                    element = Console.ReadLine();
                    deque.Enqueue(element);
                    Console.WriteLine();
                    break;
                case 2:
                    Console.WriteLine();
                    deque.Dequeue();
                    Console.WriteLine();
                    break;
                case 3:
                    currentElement = deque.Sample();
                    Console.WriteLine(currentElement);
                    Console.WriteLine();
                    break;
                case 4:
                    unitTests.CheckQueueMethods();
                    Console.WriteLine();
                    break;
                case 5:
                    break;
                default:
                    Console.WriteLine("Your answer is not correct.");
                    break;
            }
        }
    }
}
