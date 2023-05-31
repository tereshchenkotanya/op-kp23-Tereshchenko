using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

public interface IIterator<T>
{
    bool HasNext { get; }
    T MoveNext();
}

public class Deque<Item> : IIterator<Item>
{
    private LinkedListNode<Item> current;
    private bool _isFirstAccess = false;
    private LinkedList<Item> _items;
    // construct an empty deque
    public Deque()
    {
        _items = new LinkedList<Item>();
    }

    public bool IsEmpty()
    {
        if (_items.Count == 0)
        {
            return true;
        }
        return false;
    }

    // return the number of items on the deque
    public int Size()
    {
        return _items.Count;
    }

    // add the item to the front
    public void AddFirst(Item item)
    {
        _items.AddFirst(item);
    }

    // add the item to the back
    public void AddLast(Item item)
    {
        _items.AddLast(item);
    }

    // remove and return the item from the front
    public Item RemoveFirst()
    {
        if (_items.Count == 0)
        {
            throw new NullReferenceException("The dequeque is already empty");
        }

        Item item = _items.First.Value;
        _items.RemoveFirst();

        return item;
    }
   
    // remove and return the item from the back
    public Item RemoveLast()
    {
        if (_items.Count == 0)
        {
            throw new NullReferenceException("The dequeque is already empty");
        }

        Item item = _items.Last.Value;
        _items.RemoveLast();

        return item;
    }
   
    // return an iterator over items in order from front to back
    public IIterator<Item> Iterator()
    {
        _isFirstAccess = true;
        return this;
    }

    public bool HasNext
    {
        get
        {
            if (_isFirstAccess)
            {
                return _items.Count > 0;
            }
            return current?.Next != null;
        }
    }

    public Item MoveNext()
    {
        if (_isFirstAccess)
        {
            _isFirstAccess = false;
            current = _items.First;
        }
        else
        {
            current = current?.Next;
        }

        if (current == null)
        {
            throw new InvalidOperationException("No more items in the deque.");
        }

        return current.Value;
    }
    public Item GetFirstElement { get { return _items.First.Value; }}
    public Item GetLastElement  { get { return _items.Last.Value; }}
    public bool GetIsFirstAccess { get { return _isFirstAccess; }}
}

class UnitTests
{
    private Deque<int> _deque ;
    private bool _isTestSuccess;
    public bool SizeTest()
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
            Console.WriteLine("SizeTest: all cases Passed");
        }

        return _isTestSuccess;
    }
    public bool AddFirstTest()
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
            Console.WriteLine("AddFirstTest: all cases Passed");
        }

        return _isTestSuccess;
    }
    public bool AddLastTest()
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
            Console.WriteLine("AddFirstTest: all cases Passed");
        }
        return _isTestSuccess;
    }
    public bool RemoveFirstTest()
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
            Console.WriteLine("RemoveFirstTest: all cases Passed");
        }
        return _isTestSuccess;
    }
    public bool RemoveLastTest()
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
            Console.WriteLine("RemoveLastTest: all cases Passed");
        }

        return _isTestSuccess;
    }
    public bool IsEmptyTest()
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
            Console.WriteLine("IsEmptyTest: all cases Passed");
        }

        return _isTestSuccess;
    }
    public bool HasNextTest()
    {
        // Arrange
        _deque = new Deque<int>();
        _isTestSuccess = true;

        // Act
        _deque.AddLast(45);
        bool expected1 = true;
        bool actual1 = _deque.Iterator().HasNext;

        _deque.AddLast(23);
        bool expected2 = true;
        bool actual2 = _deque.Iterator().HasNext;

        _deque.RemoveFirst();
        bool expected3 = true;
        bool actual3 = _deque.Iterator().HasNext;

        _deque.RemoveLast();
        bool expected4 = false;
        bool actual4 = _deque.Iterator().HasNext;

        // Assert
        if (actual1 != expected1)
        {
            Console.WriteLine("HasNextTest: case1 FAILED");
            _isTestSuccess = false;
        }
        if (actual2 != expected2)
        {
            Console.WriteLine("HasNextTest: case2 FAILED");
            _isTestSuccess = false;
        }
        if (actual3 != expected3)
        {
            Console.WriteLine("HasNextTest: case3 FAILED");
            _isTestSuccess = false;
        }
        if (actual4 != expected4)
        {
            Console.WriteLine("HasNextTest: case4 FAILED");
            _isTestSuccess = false;
        }
        if (_isTestSuccess)
        {
            Console.WriteLine("HasNextTest: all cases Passed");
        }

        return _isTestSuccess;
    }
    public bool MoveNextTest()
    {
        // Arrange
        _deque = new Deque<int>();
        _isTestSuccess = true;

        // Act
        _deque.AddLast(45);
        _deque.AddLast(23);
        _deque.AddLast(89);

        IIterator<int> iterator = _deque.Iterator();

        int expected1 = 45;
        int actual1 = iterator.MoveNext();

        int expected2 = 23;
        int actual2 = iterator.MoveNext();

        int expected3 = 89;
        int actual3 = iterator.MoveNext();

        // Assert
        if (actual1 != expected1)
        {
            Console.WriteLine("MoveNextTest: case1 FAILED");
            _isTestSuccess = false;
        }
        if (actual2 != expected2)
        {
            Console.WriteLine("MoveNextTest: case2 FAILED");
            _isTestSuccess = false;
        }
        if (actual3 != expected3)
        {
            Console.WriteLine("MoveNextTest: case3 FAILED");
            _isTestSuccess = false;
        }
        if (_isTestSuccess)
        {
            Console.WriteLine("MoveNextTest: all cases Passed");
        }

        return _isTestSuccess;
    }
    public bool IteratorTest()
    {
        _deque = new Deque<int>();
        _isTestSuccess = true;

        IIterator<int> iterator = _deque.Iterator();

        if (!_deque.GetIsFirstAccess || iterator is not Deque<int>)
        {
            Console.WriteLine("IteratorTest was FAILED");
            _isTestSuccess = false;
        }
        else
        {
            Console.WriteLine("ITeratorTest was Passed");
        }

        return _isTestSuccess;
    }
    public void CheckDequeueMethods()
    {
        SizeTest();
        AddFirstTest();
        AddLastTest();
        RemoveLastTest();
        RemoveFirstTest();
        IsEmptyTest();
        HasNextTest();
        MoveNextTest();
        IteratorTest();
    }
}
class Program
{
    public static void PrintQueue(Deque<string> deque)
    {
        string currentEl;
        IIterator<string> dequeIterator = deque.Iterator();
        if (deque.IsEmpty())
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
        Deque<string> deque = new Deque<string>();
        UnitTests unitTests = new UnitTests();

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
            Console.WriteLine("5. Show unit tests");
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
                    Console.Write("Enter the element, you want to add: ");
                    element = Console.ReadLine();
                    Console.WriteLine();
                    deque.AddFirst(element);
                    Console.WriteLine();
                    break;
                case 2:
                    Console.Write("Enter the element, you want to add: ");
                    element = Console.ReadLine();
                    Console.WriteLine();
                    deque.AddLast(element);
                    Console.WriteLine();
                    break;
                case 3:
                    deque.RemoveFirst();
                    Console.WriteLine();
                    break;
                case 4:
                    deque.RemoveLast();
                    Console.WriteLine();
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
