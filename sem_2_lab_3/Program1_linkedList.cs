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
