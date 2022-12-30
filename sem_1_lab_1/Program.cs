using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Lab1
{
    class Percolation
    {
        static int[] nodes;
        static bool[,] opened;
        static int topEl;
        static int bottomEl;
        static int size;
        static int openSites;
        static void Init(int n)
        {
            topEl = 0;
            openSites = 0;

            nodes = new int[n * n + 2];
            for (int i = 0; i < n * n + 2; i++)
            {
                nodes[i] = i;
            }
            opened = new bool[n, n];
            bottomEl = n * n + 1;
            size = n;
        }
        static bool InitTest()
        {
            //n=2
            int expected1_1 = 0; // topEl
            int expected1_2 = 0; // openSites
            int[] expected1_3 = {0, 1, 2, 3, 4, 5, 6}; //nodes
            bool[,] expected1_4 = new bool[2, 2]; //opened
            int expected1_5 = 5; //bottomEl
            int expected1_6 = 2; //size
            //n=3
            int expected2_1 = 0; // topEl
            int expected2_2 = 0; // openSites
            int[] expected2_3 = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}; //nodes
            bool[,] expected2_4 = new bool[2, 2]; //opened
            int expected2_5 = 10; //bottomEl
            int expected2_6 = 3; //size

            int actual1 = topEl; // topEl
            int actual2 = openSites; // openSites
            int actual5 = bottomEl; //bottomEl
            int actual6 = size; //size

            Init(2);
            if (expected1_1 != actual1 && expected1_2 != actual2 && compared2(nodes, expected1_3) != true && compared1(opened, expected1_4) != true && expected1_5 != actual5 && expected1_6 != actual6)
            {
                Console.WriteLine("IitTest: case1 FAILED");
                return false;
            }
            
            Init(3);
            
            if (expected1_1 != actual1 && expected1_2 != actual2 && compared2(nodes, expected2_3) != true && compared1(opened, expected2_4) != true && expected1_5 != actual5 && expected1_6 != actual6)
            {
                Console.WriteLine("IitTest: case2 FAILED");
                return false;
            }
            Console.WriteLine("InitTest: all cases Passed");
            return true;
        }
        static bool compared2(int[] system1, int[] system2)
        {
            bool res = true;
            for (int i = 0; i < system1.Length; i++)
            {
                if (system1[i] != system2[i])
                {
                    res = false;
                }
            }
            return res;
        }

        static void Open(int row, int col)
        {
            opened[row - 1, col - 1] = true;
            NumberOfOpenSites();

            if (row == 1)
            {
                union(Index(row, col), topEl);
            }

            if (row == size)
            {
                union(Index(row, col), bottomEl);
            }

            if (row > 1 && IsOpen(row - 1, col))
            {
                union(Index(row, col), Index(row - 1, col));
            }

            if (row < size && IsOpen(row + 1, col))
            {
                union(Index(row, col), Index(row + 1, col));
            }

            if (col > 1 && IsOpen(row, col - 1))
            {
                union(Index(row, col), Index(row, col - 1));
            }

            if (col < size && IsOpen(row, col + 1))
            {
                union(Index(row, col), Index(row, col + 1));
            }
        }
        static bool OpenTest()
        {
            Init(2);
            bool res = true; 
            bool[,] expected1 = 
            { {true, false},
              {false, false} };
            bool[,] expected2 =
            { {true, false},
              {false, true} };
            bool[,] expected3 =
            { {true, false},
              {true, true} };

            Open(1,1);
            if(compared1(expected1, opened) == false)
            {
                Console.WriteLine("OpenTest: case1 FAILED");
                res = false;
            }

            Open(2,2);
            if(compared1(expected2, opened) == false)
            {
                Console.WriteLine("OpenTest: case2 FAILED");
                res = false;
            }

            Open(2,1);
            if(compared1(expected3, opened) == false)
            {
                Console.WriteLine("OpenTest: case3 FAILED");
                res = false;
            }

            Console.WriteLine("OpenTest: all cases Passed");
            return res;
        }
        static bool compared1(bool[,] system1, bool[,] system2)
        {
            bool res = true;
            for(int i = 0; i < system1.GetLength(0); i++)
            {
                for(int j = 0; j < system2.GetLength(1); j++)
                {
                    if (system1[i,j] != system2[i,j])
                    {
                        res = false; break;
                    }
                }
            }
            return res;
        }
        static Boolean IsOpen(int row, int col)
        {
            return opened[row - 1, col - 1];
        }
        static bool IsOpenTest()
        {
            Init(2);

            bool expected1 = true; // row = 1; col = 1
            bool expected2 = false; // row = 1; col = 2
            bool expected3 = true; // row = 2; col = 2

            Open(1,1);
            Open(2,2);

            bool actual1 = IsOpen(1,1);
            bool actual2 = IsOpen(1,2);
            bool actual3 = IsOpen(2,2);

            if (actual1 != expected1)
            {
                Console.WriteLine("IsOpenTest: case1 FAILED");
                return false;
            }
            if (actual2 != expected2)
            {
                Console.WriteLine("IsOpenTest: case2 FAILED");
                return false;
            }
            if (actual3 != expected3)
            {
                Console.WriteLine("IsOpenTest: case3 FAILED");
                return false;
            }
            Console.WriteLine("IsOpenTest: all cases Passed");
            return true;
        }

        static Boolean IsFull(int row, int col)
        {
            return find(topEl) == find(Index(row, col));
        }
        static bool IsFullTest()
        {
            Init(2);
           
            /*bool[,] system1 = new bool[2, 2]
            {
                {true, false},
                {true, false},
            };*/

            nodes[1] = 0;
            nodes[3] = 5;
            nodes[5] = 0;

            bool expected1 = true;
            bool expected2 = false;
            bool expected3 = true;

            bool actual1 = IsFull(1,1);
            bool actual2 = IsFull(1,2);
            bool actual3 = IsFull(2,1);

            if(actual1 != expected1)
            {
                Console.WriteLine("IsFullTest: case1 FAILED");
                return false;
            }
            if(actual2 != expected2)
            {
                Console.WriteLine("IsFullTest: case2 FAILED");
                return false;
            }
            if(actual3 != expected3)
            {
                Console.WriteLine("IsFullTest: case3 FAILED");
                return false;
            }
            Console.WriteLine("IsFullTest: all cases Passed");
            return true;
        }
        static int NumberOfOpenSites()
        {
            return openSites++;
        }
        static bool NumberOfOpenSitesTest()
        {
            Init(2);

            int expected1 = 1;
            int expected2 = 2;
            int expected3 = 3;

            NumberOfOpenSites();
            int actual1 = openSites;
            NumberOfOpenSites();
            int actual2 = openSites;
            NumberOfOpenSites();
            int actual3 = openSites;

            if(actual1 != expected1)
            {
                Console.WriteLine("NumberOfOpenSitesTest: case1 FAILED");
                return false;
            }
            if (actual2 != expected2)
            {
                Console.WriteLine("NumberOfOpenSitesTest: case2 FAILED");
                return false;
            }
            if (actual3 != expected3)
            {
                Console.WriteLine("NumberOfOpenSitesTest: case3 FAILED");
                return false;
            }
            Console.WriteLine("NumberOfOpenSitesTest: all cases Passed");
            return true;
        }
        static Boolean Percolates()
        {
            return find(topEl) == find(bottomEl);
        }
        static bool PercolatesTest()
        {
            bool expected1 = true;
            bool expected2 = false;
            bool expected3 = false;
            Init(2);
            /*bool[,] system1 = new bool[2, 2]
            {
                {true, false},
                {true, false },
            };*/
            nodes[1] = 0;
            nodes[4] = 5;
            nodes[5] = 0;

            bool actual1 = Percolates();

            Init(2);
            /*bool[,] system2 = new bool[2, 2]
            {
                {true, false},
                {false, false},
            };*/
            nodes[1] = 0;

            bool actual2 = Percolates();

            Init(2);
            /*bool[,] system3 = new bool[2, 2]
            {
                {true, false},
                {false, true},
            };*/
            nodes[1] = 0;
            nodes[4] = 5;

            bool actual3 = Percolates();

            if (expected1 != actual1)
            {
                Console.WriteLine("PercolatesTest: case1 FAILED");
                return false;
            }
            if (expected2 != actual2)
            {
                Console.WriteLine("PercolatesTest: case2 FAILED");
                return false;
            }
            if (expected3 != actual3)
            {
                Console.WriteLine("PercolatesTest: case3 FAILED");
                return false;
            }
            Console.WriteLine("PercolatesTest: all cases Passed");
            return true;
        }
        
        static void Print()
        {
            char[,] percolation = new char[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (IsFull(i + 1, j + 1) == true)
                    {
                        percolation[i, j] = '*';
                    }
                    else if (opened[i, j] == false)
                    {
                        percolation[i, j] = '0';
                    }
                    else
                    {
                        percolation[i, j] = '1';
                    }
                    Console.Write(percolation[i, j] + "\t");
                }
                Console.WriteLine();
            }

        }
        static void union(int x, int y)
        {
            int rootX = root(x);
            int rootY = root(y);
            nodes[rootX] = rootY;
        }
        static int root(int x)
        {
            int res = nodes[x];
            while (res != nodes[res])
            {
                nodes[res] = nodes[nodes[res]];
                res = nodes[res];
            }

            return res;
        }
        static int find(int p)
        {
            while (p != nodes[p])
            {
                p = nodes[p];
            }
            return p;
        }
        static int Index(int row, int col)
        {
            return size * (row - 1) + col;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Percolation System");
            Console.WriteLine("------------------");
            int N;
            char choice;
            char testChoice;
            char stopSystem = 'N';
            int open;

            Console.Write("Do you want to test the program? Write Y if yes and N if no:\t");
            testChoice = Convert.ToChar(Console.ReadLine()!);

            while (Char.ToUpper(testChoice) != 'Y' && Char.ToUpper(testChoice) != 'N')
            {
                Console.Write("Please, put correct char! Write Y or N:\t");
                choice = Convert.ToChar(Console.ReadLine()!);
            }

            if (Char.ToUpper(testChoice) == 'Y')
            {
                InitTest();
                OpenTest();
                IsOpenTest();
                IsFullTest();
                NumberOfOpenSitesTest();
                PercolatesTest();
            }

            Console.WriteLine();

            while (true)
            {
                Console.Write("Write the size of percolation system:\t");
                string? isNNumber = Convert.ToString(Console.ReadLine());
                if (uint.TryParse(isNNumber, out uint number))
                {
                    N = int.Parse(isNNumber);
                    break;
                }
                Console.WriteLine("The number must be positive integer, try again\n");
            }

            Init(N);

            Console.Write ("Write R if you want to see a random percolation system, Y if you want to open sites by yourself\t");
            choice = Convert.ToChar(Console.ReadLine()!);
            
            while (Char.ToUpper(choice) != 'R' && Char.ToUpper(choice) != 'Y')
            {
                Console.Write("Please, put correct char! Write R or Y:\t");
                choice = Convert.ToChar(Console.ReadLine()!);
            }

            if (Char.ToUpper(choice) == 'R')
            {
                Random randomRow = new Random();
                Random randomCol = new Random();
                Random num = new Random();

                open = num.Next(N * N - 1, N * N + 1);

                for (int i = 0; i < open; i++)
                {
                    int x = randomRow.Next(1, size + 1);
                    int y = randomCol.Next(1, size + 1);
                    Open(x, y);
                }

                Print();

                if (Percolates() == true)
                {
                    Console.WriteLine("System percolates");
                }
                else
                {
                    Console.WriteLine("System don`t percolate");
                }
            }
            else if (Char.ToUpper(choice) == 'Y')
            {
                Console.WriteLine("Write coordinations, you want to open");

                while (stopSystem == 'N')
                {
                    int i = Convert.ToInt32(Console.ReadLine());
                    if (i > size)
                    {
                        Console.WriteLine("This index must be less than {0} or equal to {0}", size);
                    }
                    int j = Convert.ToInt32(Console.ReadLine());
                    if (j > size)
                    {
                        Console.WriteLine("This index must be less than {0} or equal to {0}", size);
                    }

                    Open(i, j);

                    Console.Write("Do you want to stop? Write Y if yes and N if no:");
                    stopSystem = Convert.ToChar(Console.ReadLine()!);                    
                }
                if (Percolates() == true)
                {
                    Console.WriteLine("Your system percolates");
                }
                else
                {
                    Console.WriteLine("Your system does not percolates");
                }
                Print();
            }
            else
            {
                Console.WriteLine("Try again");
            }           
        }
    }
}