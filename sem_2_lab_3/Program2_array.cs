using System;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

public interface IIterator<T>
{

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
 
    }

    // is the randomized queue empty?
    public bool IsEmpty()
    {
     
    }
    public bool IsFull()
    {
      
    }

    // return the number of items on the randomized queue
    public int Size()
    {
    }
    private void Expand()
    {
      
    }
    private void Compress()
    {
      
    }
    public bool CheckIfCompessNeeded()
    {
        
    }
    public bool CheckIfExpandNeeded()
    {
        
    }
    // add the item
    public void Enqueue(Item item)
    {
       
    }

    // remove and return a random item
    public Item Dequeue()
    {
       
    }

    // return a random item (but do not remove it)
    public Item Sample()
    {
       
    }

    // return an independent iterator over items in random order
    public IIterator<Item> Iterator()
    {
      
    }

    public bool HasNext
    {
       
    }

    public Item MoveNext()
    {
        
    }
    public Item[] GetQueue { get { return _items; } }
    public int GetEndIndex { get { return _endIndex; } }
    public Item GetLastElement { get { return _items[_endIndex]; } }
    public int GetIteratorIndex { get { return _iteratorIndex; } }
    public Item GetElement(int i) 
    { 
      
    }
}

class Program
{
    public static void PrintQueue(RandomizedQueue<string> deque)
    {
        
    }

    public static void Main(String[] args)
    {
        
    }
}
