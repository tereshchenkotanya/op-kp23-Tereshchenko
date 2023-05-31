using System;
using System.Data;

public interface IIterator<T>
{
    bool HasNext { get; }
    T MoveNext();
}

public class Deque<Item> : IIterator<Item>
{
    private Item[] _items;
    private int _startIndex;
    private int _endIndex;
    private int _currentIndex;
    private const int minSize = 5;
    private const double percentToExpand = 0.75;
    private const double percentToCompress = 0.25;

    // construct an empty deque
    public Deque()
    {
        _items = new Item[minSize];
        _startIndex = minSize / 2;
        _endIndex = minSize / 2;
    }
    public Item[] GetDeque { get {return _items; } }
    public int GetStartIndex { get { return _startIndex; } }
    public int GetCurrentIndex { get { return _currentIndex; } }
    public int GetEndIndex { get { return _endIndex; } }
    public Item GetFirstElement{ get { return _items[_startIndex]; }}
    public Item GetLastElement{get { return _items[_endIndex - 1]; }}
    private void Expand()
    {
        Item[] resizedArray = new Item[_items.Length * 2];
        int startIndex = (resizedArray.Length - Size()) / 2;
        int endIndex = startIndex + Size();
        int index = startIndex;

        for (int i = _startIndex; i < _endIndex; i++)
        {
            resizedArray[index] = _items[i];
            index++;
        }

        _startIndex = startIndex;
        _endIndex = endIndex;
        _items = resizedArray;
    }
    private void Compress()
    {
        Item[] resizedArray = new Item[_items.Length / 2];
        int startIndex = (resizedArray.Length - Size()) / 2;
        int endIndex = startIndex + Size();
        int index = startIndex;

        for (int i = _startIndex; i < _endIndex; i++)
        {
            resizedArray[index] = _items[i];
            index++;
        }

        _startIndex = startIndex;
        _endIndex = endIndex;
        _items = resizedArray;
    }
    public bool IsEmpty()
    {
        if ((_startIndex - _endIndex) == 0)
        {
            return true;
        }
        return false;
    }

    // return the number of items on the deque
    public int Size()
    {
        int size = _endIndex - _startIndex;
        return size;
    }

    private bool CheckIfCompessNeeded()
    {
        return Size() < Math.Round(percentToCompress * _items.Length) & _items.Length > minSize;
    }
    private bool CheckIfExpandNeeded()
    {
        return _startIndex == 0 || _endIndex == Size() || Size() > percentToExpand * _items.Length;
    }

    // add the item to the front
    public void AddFirst(Item item)
    {
        if (CheckIfExpandNeeded())
        {
            Expand();
        }

        _startIndex--;
        _items[_startIndex] = item;
    }

    // add the item to the back
    public void AddLast(Item item)
    {

        if (CheckIfExpandNeeded())
        {
            Expand();
        }

        _items[_endIndex] = item;
        _endIndex++;
    }

    // remove and return the item from the front
    public Item RemoveFirst()
    {
        if (IsEmpty())
        {
            throw new NullReferenceException("The dequeque is already empty");
        }

        Item removedEl = _items[_startIndex];
        _startIndex++;

        if (CheckIfCompessNeeded())
        {
            Compress();
        }

        return removedEl;
    }

    // remove and return the item from the back
    public Item RemoveLast()
    {
        if (IsEmpty())
        {
            throw new NullReferenceException("The dequeque is already empty");
        }

        _endIndex--;
        Item removedEl = _items[_endIndex];

        if (CheckIfCompessNeeded())
        {
            Compress();
        }

        return removedEl;
    }

    public IIterator<Item> Iterator()
    {
        _currentIndex = _startIndex;
        return this;
    }

    public bool HasNext
    {
        get
        {
            return _endIndex > _currentIndex;
        }
    }

    public Item MoveNext()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("No more items in the deque");
        }

        Item item = _items[_currentIndex];
        _currentIndex++;

        return item;
    }
}

class Program
{
    public static void PrintQueue(Deque<string> deque)
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
            Console.WriteLine();
        }
    }

    public static void Main(String[] args)
    {
        Deque<string> deque = new Deque<string>();
        UnitTests unitTests = new();
        int answer = 0;
        string element;

        while (answer != 6)
        {
            PrintQueue(deque);
            Console.WriteLine();
            Console.WriteLine("What do you want to do with your deque? Choose the number");
            Console.WriteLine("1. Add the first element.");
            Console.WriteLine("2. Add the last element");
            Console.WriteLine("3. Remove the first element");
            Console.WriteLine("4. Remove the last element");
            Console.WriteLine("5. Check unit tests");
            Console.WriteLine("6. Exit");
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
                    Console.WriteLine();
                    deque.AddFirst(element);
                    break;
                case 2:
                    Console.Write("Enter the element you want to add: ");
                    element = Console.ReadLine();
                    Console.WriteLine();
                    deque.AddLast(element);
                    break;
                case 3:
                    deque.RemoveFirst();
                    break;
                case 4:
                    deque.RemoveLast();
                    break;
                case 5:
                    unitTests.CheckDequeueMethods();
                    Console.WriteLine();
                    break;
                case 6:
                    break;
                default:
                    Console.WriteLine("Your answer is not correct.");
                    break;
            }
        }
    }
}

