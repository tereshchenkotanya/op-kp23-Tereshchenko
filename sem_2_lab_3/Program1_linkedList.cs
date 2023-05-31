using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

public interface IIterator<T>
{

}

public class Deque<Item> : IIterator<Item>
{
    private LinkedListNode<Item> current;
    private bool _isFirstAccess = false;
    private LinkedList<Item> _items;
    // construct an empty deque
    public Deque()
    {

    }

    public bool IsEmpty()
    {

    }

    // return the number of items on the deque
    public int Size()
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
   
    // return an iterator over items in order from front to back
    public IIterator<Item> Iterator()
    {

    }

    public bool HasNext
    {

    }

    public Item MoveNext()
    {

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
 
    }
    public bool AddFirstTest()
    {
    }
    public bool AddLastTest()
    {
      
    }
    public bool RemoveFirstTest()
    {
        
    }
    public bool RemoveLastTest()
    {
       
    }
    public bool IsEmptyTest()
    {
        
    }
    public bool HasNextTest()
    {
        
    }
    public bool MoveNextTest()
    {

    }
    public bool IteratorTest()
    {

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
 
    }

    public static void Main(String[] args)
    {
     
    }
}
