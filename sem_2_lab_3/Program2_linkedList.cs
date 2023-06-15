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
class UnitTests
{
        private RandomizedQueue<int> _randomQueue = new RandomizedQueue<int>();
        private bool _isTestSuccess;
        private bool RandomizedQueueTest()
        {
            _randomQueue = new RandomizedQueue<int>();
            _isTestSuccess = true;

            if(_randomQueue is not RandomizedQueue<int>)
            {
                Console.WriteLine("RandomizedQueueTest: _items has the other type");
                _isTestSuccess = false;
            }
            else
            {
                Console.WriteLine("RandomizedQueueTest: all tests are passed");
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
            if(_isTestSuccess)
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
            if(_isTestSuccess)
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

            int actualSize = _randomQueue.Size();

            LinkedList<int> actualQueue = _randomQueue.GetQueue;

            bool actual1 = actualQueue.Any(item => item == 1);
            bool actual2 = actualQueue.Any(item => item == 2);
            bool actual3 = actualQueue.Any(item => item == 3);
            actualQueue.Find(2);
            actualQueue.Find(3);

            if (!actual1)
            {
                Console.WriteLine("EnqueueTest: incorrect filling of the array");
                _isTestSuccess = false;
            }
            if (!actual2)
            {
                Console.WriteLine("EnqueueTest: incorrect filling of the array");
                _isTestSuccess = false;
            }
            if (!actual3)
            {
                Console.WriteLine("EnqueueTest: incorrect filling of the array");
                _isTestSuccess = false;
            }
            if (_isTestSuccess == true)
            {
                Console.WriteLine("SizeTest: all test were Passed");
                _isTestSuccess = true;
            }

            return _isTestSuccess;
        }
        private bool DequeueTest()
        {
            _randomQueue = new RandomizedQueue<int>();
            _isTestSuccess = true;

            _randomQueue.Enqueue(1);
            _randomQueue.Enqueue(2);
            _randomQueue.Enqueue(3);
            _randomQueue.Enqueue(4);
            _randomQueue.Enqueue(5);

            int num1 = _randomQueue.Dequeue();
            int num2 = _randomQueue.Dequeue();
            int num3 = _randomQueue.Dequeue();

            int actualSize = _randomQueue.Size();

            LinkedList<int> actualQueue = _randomQueue.GetQueue;

            bool actual1 = actualQueue.Any(item => item == num1);
            bool actual2 = actualQueue.Any(item => item == num2);
            bool actual3 = actualQueue.Any(item => item == num3);

            if (actual1)
            {
                Console.WriteLine("EnqueueTest: EnqueueTest: case1 was FAILED");
                _isTestSuccess = false;
            }
            if (actual2)
            {
                Console.WriteLine("EnqueueTest: EnqueueTest: case2 was FAILED");
                _isTestSuccess = false;
            }
            if (actual3)
            {
                Console.WriteLine("EnqueueTest: case3 was FAILED");
                _isTestSuccess = false;
            }
            if (_isTestSuccess == true)
            {
                Console.WriteLine("SizeTest: all test were Passed");
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
            _randomQueue.Enqueue(4);
            _randomQueue.Enqueue(5);

            int num1 = _randomQueue.Sample();
            int num2 = _randomQueue.Sample();
            int num3 = _randomQueue.Sample();

            int actualSize = _randomQueue.Size();

            LinkedList<int> actualQueue = _randomQueue.GetQueue;

            bool actual1 = actualQueue.Any(item => item == num1);
            bool actual2 = actualQueue.Any(item => item == num2);
            bool actual3 = actualQueue.Any(item => item == num3);

            if (!actual1)
            {
                Console.WriteLine("EnqueueTest: EnqueueTest: case1 was FAILED");
                _isTestSuccess = false;
            }
            if (!actual2)
            {
                Console.WriteLine("EnqueueTest: EnqueueTest: case2 was FAILED");
                _isTestSuccess = false;
            }
            if (!actual3)
            {
                Console.WriteLine("EnqueueTest: case3 was FAILED");
                _isTestSuccess = false;
            }
            if (_isTestSuccess == true)
            {
                Console.WriteLine("SizeTest: all test were Passed");
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
            bool isFirstAccess = _randomQueue.GetIsFirstAccess;

            if (iteratorIndex != 0)
            {
                Console.WriteLine("IteratorTest: iterator index is not 0");
                _isTestSuccess = false;
            }
            if (iterator is not RandomizedQueue<int>)
            {
                Console.WriteLine("IteratorTest: queue has incorect type");
                _isTestSuccess = false;
            }
            if (!isFirstAccess)
            {
                Console.WriteLine("IteratorTest was FAILED");
                _isTestSuccess = false;
            }
            if(_isTestSuccess)
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
            _randomQueue.Enqueue(40);
            _randomQueue.Enqueue(50);

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

            int expected3 = _randomQueue.GetElement(2);
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
        public void CheckUnitTests()
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
