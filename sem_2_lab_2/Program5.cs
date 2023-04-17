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

        for (int i = 0; i < length1; i++)
        {
            if (v1.elements[i] >= 0)
            {
                continue;
            }
            else
            {
                sum += v1.elements[i];
            }
        }
        for (int i = 0; i < length2; i++)
        {
            if (v2.elements[i] >= 0)
            {
                continue;
            }
            else
            {
                sum += v2.elements[i];
            }
        }
        if (sum == 0) // Перевірка, чи сума від'ємних елементів дорівнює 0
        {
            Console.WriteLine("У векторах немає від'ємних елементів.");
        }
        return sum;
    }
    public static int operator *(Vector v1, Vector v2)
    {
        int product = 1;
        for (int i = 1; i < v1.elements.Length; i += 2)
        {
            product *= v1.elements[i];
        }
        for (int i = 1; i < v2.elements.Length; i += 2)
        {
            product *= v2.elements[i];
        }
        return product;
    }
    public static int CountZeroElements(Vector v)
    {
        int count = 0;
        for (int i = 0; i < v.elements.Length; i++)
        {
            if (v.elements[i] == 0)
            {
                count++;
            }
        }
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
        //test cases
        //case 1: n1 = 2
        //        n2 = 2
        //        v1 = {1, -2}
        //        v2 = {2, -3}
        //        num = 1
        //case 2: n1 = 3
        //        n2 = 2
        //        v1 = {1, 2, -5}
        //        v2 = {2, -3}
        //        num = 2
        //case 2: n1 = 2
        //        n2 = 2
        //        v1 = {1, 0}
        //        v2 = {0, -3}
        //        num = 3

        Console.WriteLine("Enter number of vectors` elements");
        int n1 = Convert.ToInt32(Console.ReadLine());
        int n2 = Convert.ToInt32(Console.ReadLine());

        int[] v1 = new int[n1];
        int[] v2 = new int[n2];

        Console.WriteLine("Write elements for v1");
        for (int i = 0; i < n1; i++)
        {
            v1[i] = Convert.ToInt32(Console.ReadLine());
        }
        Vector v1_1 = new Vector(v1);

        Console.WriteLine("Write elements for v1");
        for (int i = 0; i < n2; i++)
        {
            v2[i] = Convert.ToInt32(Console.ReadLine());
        }
        Vector v2_1 = new Vector(v2);

        Console.WriteLine("What do you want to know?");
        Console.WriteLine("1) Sum\n2) Product\n3) Count of 0-elements");
        int num = Convert.ToInt32(Console.ReadLine());

        if(num == 1)
        {
            int sum = v1_1 + v2_1;
            Console.WriteLine("The sum of the vectors elements: " + sum);
        }
        else if(num == 2)
        {
            int product = v1_1 * v2_1;
            Console.WriteLine("The product of the vectors elements: " + product);
        }
        else if(num == 3)
        {
            int zeroElementsCount = v1_1 & v2_1;
            Console.WriteLine("Number of zero elements of vectors: " + zeroElementsCount);
        }

        //test cases:
        //case 1: -5
        //case 2: -6
        //case 2: 2
    }
}