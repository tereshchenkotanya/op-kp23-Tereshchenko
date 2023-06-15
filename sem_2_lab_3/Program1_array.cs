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
class UnitTests
{
    private Deque<int> _deque;
    private bool _isTestSuccess;
    private bool DequeConstructorTest()
    {
        _deque = new Deque<int>();
        _isTestSuccess = true;

        int actualLength = _deque.GetDeque.Length;
        int expectedLength = 5;

        int actualStartIndex = _deque.GetStartIndex;
        int expectedStartIndex = _deque.GetDeque.Length/2;

        int actualEndIndex = _deque.GetEndIndex;
        int expectedEndIndex = _deque.GetDeque.Length / 2;

        if (actualLength != expectedLength)
        {
            Console.WriteLine("DequeConstructorTest: case1 FAILED");
            _isTestSuccess = false;
        }
        if (actualStartIndex != expectedEndIndex)
        {
            Console.WriteLine("DequeConstructorTest: case2 FAILED");
            _isTestSuccess = false;
        }
        if (actualEndIndex != expectedEndIndex)
        {
            Console.WriteLine("DequeConstructorTest: case3 FAILED");
            _isTestSuccess = false;
        }
        if (_isTestSuccess == true)
        {
            Console.WriteLine("DequeConstructorTest: all cases were Passed");
        }

        return _isTestSuccess;
    }
    private bool SizeTest()
    {
        _isTestSuccess = true;
        _deque = new Deque<int>();

        _deque.AddFirst(45);
        int expected1 = 1;
        int actual1 = _deque.Size();

        _deque.AddFirst(32);
        int expected2 = 2;
        int actual2 = _deque.Size();

        _deque.RemoveFirst();
        _deque.RemoveFirst();
        int expected3 = 0;
        int actual3 = _deque.Size();

        if (actual1 != expected1)
        {
            Console.WriteLine("SizeTest: case1 FAILED");
            _isTestSuccess = false;
        }
        if (actual2 != expected2)
        {
            Console.WriteLine("SizeTest: case2 FAILED");
            _isTestSuccess = false;
        }
        if (actual3 != expected3)
        {
            Console.WriteLine("SizeTest: case3 FAILED");
            _isTestSuccess = false;
        }
        if (_isTestSuccess == true)
        {
            Console.WriteLine("SizeTest: all cases were Passed");
        }

        return _isTestSuccess;
    }
    private bool AddFirstTest()
    {
        _isTestSuccess = true;
        _deque = new Deque<int>();

        _deque.AddFirst(45);
        int expected1 = 45;
        int actual1 = _deque.GetFirstElement;

        _deque.AddFirst(23);
        int expected2 = 23;
        int actual2 = _deque.GetFirstElement;

        _deque.AddFirst(89);
        int expected3 = 89;
        int actual3 = _deque.GetFirstElement;

        if (actual1 != expected1)
        {
            Console.WriteLine("AddFirstTest: case1 FAILED");
            _isTestSuccess = false;
        }
        if (actual2 != expected2)
        {
            Console.WriteLine("AddFirstTest: case2 FAILED");
            _isTestSuccess = false;
        }
        if (actual3 != expected3)
        {
            Console.WriteLine("AddFirstTest: case3 FAILED");
            _isTestSuccess = false;
        }
        if (_isTestSuccess == true)
        {
            Console.WriteLine("AddFirstTest: all cases were Passed");
        }

        return _isTestSuccess;
    }
    private bool AddLastTest()
    {
        _deque = new Deque<int>();
        _isTestSuccess = true;

        _deque.AddLast(45);
        int expected1 = 45;
        int actual1 = _deque.GetLastElement;

        _deque.AddLast(23);
        int expected2 = 23;
        int actual2 = _deque.GetLastElement;

        _deque.AddLast(89);
        int expected3 = 89;
        int actual3 = _deque.GetLastElement;

        if (actual1 != expected1)
        {
            Console.WriteLine("AddLastTest: case1 FAILED");
            _isTestSuccess = false;
        }
        if (actual2 != expected2)
        {
            Console.WriteLine("AddLastTest: case2 FAILED");
            _isTestSuccess = false;
        }
        if (actual3 != expected3)
        {
            Console.WriteLine("AddLastTest: case3 FAILED");
            _isTestSuccess = false;
        }
        if (_isTestSuccess == true)
        {
            Console.WriteLine("AddFirstTest: all cases werePassed");
        }
        return _isTestSuccess;
    }
    private bool RemoveFirstTest()
    {
        _deque = new Deque<int>();
        _isTestSuccess = true;

        _deque.AddFirst(34);
        _deque.AddFirst(12);
        _deque.AddFirst(76);

        int actual1 = _deque.RemoveFirst();
        int expected1 = 76;

        int actual2 = _deque.RemoveFirst();
        int expected2 = 12;

        int actual3 = _deque.RemoveFirst();
        int expected3 = 34;

        if (actual1 != expected1)
        {
            Console.WriteLine("RemoveFirstTest: case1 FAILED");
            _isTestSuccess = false;
        }
        if (actual2 != expected2)
        {
            Console.WriteLine("RemoveFirstTest: case2 FAILED");
            _isTestSuccess = false;
        }
        if (actual3 != expected3)
        {
            Console.WriteLine("RemoveFirstTest: case3 FAILED");
            _isTestSuccess = false;
        }
        if (_isTestSuccess == true)
        {
            Console.WriteLine("RemoveFirstTest: all cases were Passed");
        }
        return _isTestSuccess;
    }
    private bool RemoveLastTest()
    {
        _deque = new Deque<int>();
        _isTestSuccess = true;

        _deque.AddLast(34);
        _deque.AddLast(12);
        _deque.AddLast(76);

        int actual1 = _deque.RemoveLast();
        int expected1 = 76;

        int actual2 = _deque.RemoveLast();
        int expected2 = 12;

        int actual3 = _deque.RemoveLast();
        int expected3 = 34;

        if (actual1 != expected1)
        {
            Console.WriteLine("RemoveLastTest: case1 FAILED");
            _isTestSuccess = false;
        }
        if (actual2 != expected2)
        {
            Console.WriteLine("RemoveLastTest: case2 FAILED");
            _isTestSuccess = false;
        }
        if (actual3 != expected3)
        {
            Console.WriteLine("RemoveLastTest: case3 FAILED");
            _isTestSuccess = false;
        }
        if (_isTestSuccess == true)
        {
            Console.WriteLine("RemoveLastTest: all cases were Passed");
        }

        return _isTestSuccess;
    }
    private bool IsEmptyTest()
    {
        _deque = new Deque<int>();
        _isTestSuccess = true;

        _deque.AddFirst(45);
        bool actual1 = _deque.IsEmpty();
        bool expected1 = false;

        _deque.RemoveLast();
        bool actual2 = _deque.IsEmpty();
        bool expected2 = true;

        _deque.AddFirst(67);
        bool actual3 = _deque.IsEmpty();
        bool expected3 = false;

        if (actual1 != expected1)
        {
            Console.WriteLine("IsEmptyTest: case1 FAILED");
            _isTestSuccess = false;
        }
        if (actual2 != expected2)
        {
            Console.WriteLine("IsEmptyTest: case2 FAILED");
            _isTestSuccess = false;
        }
        if (actual3 != expected3)
        {
            Console.WriteLine("IsEmptyTest: case3 FAILED");
            _isTestSuccess = false;
        }
        if (_isTestSuccess == true)
        {
            Console.WriteLine("IsEmptyTest: all cases were Passed");
        }

        return _isTestSuccess;
    }
    private bool Iterator()
    {
        _deque = new Deque<int>();
        _isTestSuccess = true;

        IIterator<int> iterator = _deque.Iterator();
        int currentIndex = _deque.GetCurrentIndex;
        int endIndex = _deque.GetEndIndex;

        if (currentIndex != endIndex || iterator is not Deque<int>)
        {
            Console.WriteLine("IteratorTest was FAILED");
            _isTestSuccess = false;
        }
        else
        {
            Console.WriteLine("ITeratorTest: all tests were Passed");
        }

        return _isTestSuccess;
    }
    private bool MoveNextTest()
    {
        _deque = new Deque<int>();
        _deque.AddLast(10);
        _deque.AddLast(20);
        _deque.AddLast(30);

        IIterator<int> iterator = _deque.Iterator();
        _isTestSuccess = true;

        int expected1 = 10;
        int actual1 = iterator.MoveNext();
        if (actual1 != expected1)
        {
            Console.WriteLine("MoveNextTest: case1 FAILED");
            _isTestSuccess = false;
        }

        int expected2 = 20;
        int actual2 = iterator.MoveNext();
        if (actual2 != expected2)
        {
            Console.WriteLine("MoveNextTest: case2 FAILED");
            _isTestSuccess = false;
        }

        int expected3 = 30;
        int actual3 = iterator.MoveNext();
        if (actual3 != expected3)
        {
            Console.WriteLine("MoveNextTest: case3 FAILED");
            _isTestSuccess = false;
        }

        if (_isTestSuccess)
        {
            Console.WriteLine("MoveNextTest: all tests were Passed");
            _isTestSuccess = true;
        }

        return _isTestSuccess;
    }
    private bool HasNextTest()
    {
        _deque = new Deque<int>();
        _deque.AddLast(10);
        _deque.AddLast(20);

        IIterator<int> iterator = _deque.Iterator();
        _isTestSuccess = true;

        if (!iterator.HasNext)
        {
            Console.WriteLine("HasNextTest: initially false");
            _isTestSuccess = false;
        }

        iterator.MoveNext();
        if (!iterator.HasNext)
        {
            Console.WriteLine("HasNextTest: after consuming one item");
            _isTestSuccess = false;
        }

        iterator.MoveNext();
        if (iterator.HasNext)
        {
            Console.WriteLine("HasNextTest: after consuming all items");
            _isTestSuccess = false;
        }

        if (_isTestSuccess)
        {
            Console.WriteLine("HasNextTest: all tests were Passed");
            _isTestSuccess = true;
        }

        return _isTestSuccess;
    }
    public void CheckDequeueMethods()
    {
        DequeConstructorTest();
        SizeTest();
        AddFirstTest();
        AddLastTest();
        RemoveLastTest();
        RemoveFirstTest();
        IsEmptyTest();
        Iterator();
        MoveNextTest();
        HasNextTest();
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

