using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

public interface IIterator<T>
{

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

    }

    // is the randomized queue empty?
    public bool IsEmpty()
    {

    }

    // return the number of items on the randomized queue
    public int Size()
    {
        return _items.Count;
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

    // return an iterator over items in order from front to back

    public bool HasNext
    {
        
    }

    public Item MoveNext()
    {
       
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
        
    }
    
    public static void Main(String[] args)
    {
       


    }
}
