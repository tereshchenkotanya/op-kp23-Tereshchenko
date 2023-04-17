using System;
class Vector
{
    private int[] elements;

    public Vector(int[] elements)
    {
        this.elements = elements;
    }

    public static int operator +(Vector v1, Vector v2)
    {
        int length1 = v1.elements.Length;
        int length2 = v2.elements.Length;

        int sum = 0;

        return sum;
    }
    public static int operator *(Vector v1, Vector v2)
    {
        int product = 1;

        return product;
    }
    public static int CountZeroElements(Vector v)
    {
        int count = 0;

        return count;
    }

    public static int operator &(Vector v1, Vector v2)
    {
        int count1 = CountZeroElements(v1);
        int count2 = CountZeroElements(v2);
        return count1 + count2;
    }
}
class Program
{
    public static void Main(string[] args)
    {
        int n1 = Convert.ToInt32(Console.ReadLine());
        int n2 = Convert.ToInt32(Console.ReadLine());

        int[] v1 = new int[n1];
        int[] v2 = new int[n2];

        Vector v1_1 = new Vector(v1);

        Vector v2_1 = new Vector(v2);

        int num;

        if(num == 1)
        {

        }
        else if(num == 2)
        {

        }
        else if(num == 3)
        {

        }
    }
}