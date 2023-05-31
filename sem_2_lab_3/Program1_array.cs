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
        
    }
    private void Compress()
    {
        
    }
    public bool IsEmpty()
    {
       
    }

    // return the number of items on the deque
    public int Size()
    {
       
    }

    private bool CheckIfCompessNeeded()
    {
       
    }
    private bool CheckIfExpandNeeded()
    {
        
    }

    // add the item to the front
    public void AddFirst(Item item)
    {
       
    }

    // add the item to the back
    public void AddLast(Item item)
    {

    }

    // remove and return the item from the front
    public Item RemoveFirst()
    {
        
    }

    // remove and return the item from the back
    public Item RemoveLast()
    {
       
    }

    public IIterator<Item> Iterator()
    {
        
    }

    public bool HasNext
    {
        
    }

    public Item MoveNext()
    {
        
    }
}

class Program
{
    public static void PrintQueue(Deque<string> deque)
    {

    }

    public static void Main(String[] args)
    {
        
    }
}

