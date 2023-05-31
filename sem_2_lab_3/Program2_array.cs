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
class UnitTests
{
    private RandomizedQueue<int> _randomQueue = new RandomizedQueue<int>();
    private bool _isTestSuccess;
    private bool RandomizedQueueTest()
    {
        _randomQueue = new RandomizedQueue<int>();
        _isTestSuccess = true;

        int[] expectedQueue = new int[5];
        int[] actualQueue = _randomQueue.GetQueue;

        int expectedEndIndex = -1;
        int actualEndIndex = _randomQueue.GetEndIndex;

        if (!Compared(actualQueue, expectedQueue))
        {
            Console.WriteLine("RandomizedQueueTest: case1 was FAILED");
            _isTestSuccess = false;
        }
        if (actualEndIndex != expectedEndIndex)
        {
            Console.WriteLine("RandomizedQueueTest: case2 was FAILED");
            _isTestSuccess = false;
        }
        if (_isTestSuccess == true)
        {
            Console.WriteLine("RandomizedQueueTest: all cases were Passed");
            _isTestSuccess = true;
        }

        return _isTestSuccess;
    }
    private bool IsEmptyTest()
    {
        _randomQueue = new RandomizedQueue<int>();
        _isTestSuccess = true;

        bool expected1 = true;
        bool actual1 = _randomQueue.IsEmpty();

        _randomQueue.Enqueue(20);
        bool expected2 = false;
        bool actual2 = _randomQueue.IsEmpty();

        if (!(expected1 == actual1))
        {
            Console.WriteLine("IsEmptyTest: case1 was FAILED");
            _isTestSuccess = false;
        }
        if (!(expected2 == actual2))
        {
            Console.WriteLine("IsEmptyTest: case2 was FAILED");
            _isTestSuccess = false;
        }
        if (_isTestSuccess)
        {
            Console.WriteLine("IsEmptyTest: all tests were Passed");
            _isTestSuccess = false;
        }

        return _isTestSuccess;
    }
    private bool SizeTest()
    {
        _randomQueue = new RandomizedQueue<int>();
        _isTestSuccess = true;

        _randomQueue.Enqueue(1);
        _randomQueue.Enqueue(2);

        int expected1 = 2;
        int actual1 = _randomQueue.Size();

        _randomQueue.Enqueue(3);
        int expected2 = 3;
        int actual2 = _randomQueue.Size();

        _randomQueue.Enqueue(3);
        int expected3 = 4;
        int actual3 = _randomQueue.Size();

        if (!(expected1 == actual1))
        {
            Console.WriteLine("SizeTest: case1 Failed");
            _isTestSuccess = false;
        }
        if (!(expected2 == actual2))
        {
            Console.WriteLine("SizeTest: case2 was Failed");
            _isTestSuccess = false;
        }
        if (!(expected3 == actual3))
        {
            Console.WriteLine("SizeTest: case3 was Failed");
            _isTestSuccess = false;
        }
        if (_isTestSuccess)
        {
            Console.WriteLine("SizeTest: all tests were Passed");
            _isTestSuccess = true;
        }

        return _isTestSuccess;
    }
    private bool EnqueueTest()
    {
        _randomQueue = new RandomizedQueue<int>();
        _isTestSuccess = true;

        _randomQueue.Enqueue(1);
        _randomQueue.Enqueue(2);
        _randomQueue.Enqueue(3);

        int expectedSize = 3;
        int actualSize = _randomQueue.Size();

        int[] expectedQueue = new int[expectedSize];
        for (int i = 0; i < expectedSize; i++)
        {
            expectedQueue[i] = i+1;
        }
        int[] actualQueue = _randomQueue.GetQueue;

        if (actualSize != expectedSize)
        {
            Console.WriteLine("EnqueueTest: Size incorrect");
            _isTestSuccess = false;
        }
        if (!Compared(expectedQueue, actualQueue))
        {
            Console.WriteLine("EnqueueTest: incorrect filling of the array");
            _isTestSuccess = false;
        }
        if(_isTestSuccess == true)
        {
            Console.WriteLine("SizeTest: all test were Passed");
            _isTestSuccess = true;
        }


        return _isTestSuccess;
    }
    private bool Compared(int[] array1, int[] array2)
    {
        bool isTestSuccess = true;
        for (int i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
            {
                isTestSuccess = false;
            }
        }
        return isTestSuccess;
    }
    private bool DequeueTest()
    {
        _randomQueue = new RandomizedQueue<int>();
        _isTestSuccess = true;

        _randomQueue.Enqueue(1);
        _randomQueue.Enqueue(2);
        _randomQueue.Enqueue(3);

        int initialSize = _randomQueue.Size();
        int dequeuedItem = _randomQueue.Dequeue();
        int expectedSize = initialSize - 1;
        int actualSize = _randomQueue.Size();

        if (actualSize != expectedSize)
        {
            Console.WriteLine("DequeueTest: Size incorrect");
            _isTestSuccess = false;
        }
        if (_randomQueue.GetQueue.Contains(dequeuedItem))
        {
            Console.WriteLine("DequeueTest: Dequeued item still present in the queue");
            _isTestSuccess = false;
        }
        if (_isTestSuccess == true)
        {
            Console.WriteLine("DequeueTest: all test were Passed");
            _isTestSuccess = true;
        }

        return _isTestSuccess;
    }
    private bool SampleTest()
    {
        _randomQueue = new RandomizedQueue<int>();
        _isTestSuccess = true;

        _randomQueue.Enqueue(1);
        _randomQueue.Enqueue(2);
        _randomQueue.Enqueue(3);

        int sample = _randomQueue.Sample();

        if (!_randomQueue.GetQueue.Contains(sample))
        {
            Console.WriteLine("SampleTest: Sample item not present in the queue");
            _isTestSuccess = false;
        }
        if (_isTestSuccess == true)
        {
            Console.WriteLine("SampleTest: all tests were Passed");
            _isTestSuccess = true;
        }

        return _isTestSuccess;
    } 
    private bool Iterator()
    {
        _randomQueue = new RandomizedQueue<int>();
        _isTestSuccess = true;

        IIterator<int> iterator = _randomQueue.Iterator();
        int iteratorIndex = _randomQueue.GetIteratorIndex;

        if (iteratorIndex != 0 || iterator is not RandomizedQueue<int>)
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
    private bool HasNextTest()
    {
        _randomQueue = new RandomizedQueue<int>();
        _randomQueue.Enqueue(10);
        _randomQueue.Enqueue(20);

        IIterator<int> iterator = _randomQueue.Iterator();
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
    private bool MoveNextTest()
    {
        _randomQueue = new RandomizedQueue<int>();
        _randomQueue.Enqueue(10);
        _randomQueue.Enqueue(20);
        _randomQueue.Enqueue(30);

        IIterator<int> iterator = _randomQueue.Iterator();
        _isTestSuccess = true;

        int expected1 = _randomQueue.GetElement(0);
        int actual1 = iterator.MoveNext();
        if (actual1 != expected1)
        {
            Console.WriteLine("MoveNextTest: case1 FAILED");
            _isTestSuccess = false;
        }

        int expected2 = _randomQueue.GetElement(1);
        int actual2 = iterator.MoveNext();
        if (actual2 != expected2)
        {
            Console.WriteLine("MoveNextTest: case2 FAILED");
            _isTestSuccess = false;
        }

        int expected3 = _randomQueue.GetElement(2); ;
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
   
    public void CheckQueueMethods()
    {
        RandomizedQueueTest();
        IsEmptyTest();
        SizeTest();
        EnqueueTest();
        DequeueTest();
        SampleTest();
        Iterator();
        HasNextTest();
        MoveNextTest();
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
