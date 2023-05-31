using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

public interface IIterator<T>
{
    bool HasNext { get; }
    T MoveNext();
}

public class RandomizedQueue<Item> : IIterator<Item>
{
    private LinkedListNode<Item> _current;
    private LinkedList<Item> _items;
    private bool _isFirstAccess;
    private Item[] _arrayFromList;
    private int _iteratorIndex = 0;
    Random rnd = new Random();
    // construct an empty deque
    // construct an empty randomized queue
    public RandomizedQueue()
    {
        _items = new LinkedList<Item>();
    }

    // is the randomized queue empty?
    public bool IsEmpty()
    {
        return _items.Count == 0;
    }

    // return the number of items on the randomized queue
    public int Size()
    {
        return _items.Count;
    }

    // add the item
    public void Enqueue(Item item)
    {
        _items.AddLast(item);
    }

    // remove and return a random item
    public Item Dequeue()
    {
        if (IsEmpty())
        {
            throw new NullReferenceException("The queque is already empty");
        }

        Item item;

        item = Sample();
        _items.Remove(item);

        return item;
    }

    // return a random item (but do not remove it)
    public Item Sample()
    {
        int endIndex = Size() + 1;
        int randomIndex = rnd.Next(1, endIndex);
        Item item;

        _current = _items.First;

        for (int i = 1; i < randomIndex; i++)
        {
            _current = _current?.Next;
        }

        item = _current.Value;

        return item;
    }

    // return an independent iterator over items in random order
    public IIterator<Item> Iterator()
    {
        _isFirstAccess = true;
        _arrayFromList = _items.ToArray();

        _iteratorIndex = 0;

        for (int i = 0; i < Size(); ++i)
        {
            int r = rnd.Next(i, Size());
            Item tmp = _arrayFromList[r];
            _arrayFromList[r] = _arrayFromList[i];
            _arrayFromList[i] = tmp;
        }

        return this;
    }

    // return an iterator over items in order from front to back

    public bool HasNext
    {
        get
        {
            return _iteratorIndex < _arrayFromList.Length;
        }
    }

    public Item MoveNext()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("No more items in the queue");
        }

        Item item = _arrayFromList[_iteratorIndex];
        _iteratorIndex++;

        return item;
    }
    public LinkedList<Item> GetQueue { get { return _items; } }
    public bool GetIsFirstAccess { get { return _isFirstAccess; } }
    public int GetIteratorIndex { get { return _iteratorIndex; } }
    public  Item GetElement(int i)
    {
        return _arrayFromList[i];
    }
}
class Program
{
    public static void PrintQueue(RandomizedQueue<string> randomizedQueuedeque)
    {
        string currentEl;
        IIterator<string> dequeIterator = randomizedQueuedeque.Iterator();
        if (randomizedQueuedeque.IsEmpty())
        {
            Console.WriteLine("Your deque is empty now");
        }
        else
        {
            while (dequeIterator.HasNext)
            {
                currentEl = dequeIterator.MoveNext();
                Console.Write(currentEl + " ");
            }
            Console.WriteLine();
        }
    }
    public static void Main(String[] args)
    {
        RandomizedQueue<string> deque = new RandomizedQueue<string>();
        UnitTests unitTests = new UnitTests();
        int answer = 0;
        string element;
        string currentEl;

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
                    Console.Write("Enter the element, you want to add: ");
                    element = Console.ReadLine();
                    Console.WriteLine();
                    deque.Enqueue(element);
                    break;
                case 2:
                    deque.Dequeue();
                    break;
                case 3:
                    currentEl = deque.Sample();
                    Console.WriteLine(currentEl);
                    break;
                case 4:
                    unitTests.CheckUnitTests();
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
