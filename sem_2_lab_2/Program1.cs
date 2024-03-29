﻿using System;
using System.Runtime.CompilerServices;

// Клас, що представляє відомість
class Data
{
    // Поля відомості
    private string surname;
    private double salary;
    private double taxes;
    private double issuedEarnings;
    public const double taxRate = 0.195;
    // Конструктор для встановлення початкових значень полів
    public Data(string name, double salary)
    {
        this.surname = name;
        this.salary = salary;
        // Обчислення значень розрахункових полів
        this.CalculateTaxes();
    }

    // Метод для обчислення значення поля "податки"
    public void CalculateTaxes()
    {
        taxes = salary * taxRate; // Податки - 19,5% від зарплати
        issuedEarnings = salary - taxes;
    }

    // Метод для виведення відомості на консоль
    public void DisplayTaxes()
    {
        double taxesRounded = Math.Round(taxes, 2);
        double issuedEarningsRounded = Math.Round(issuedEarnings, 2);

        Console.WriteLine(surname + ":     " + salary + "   " + taxesRounded + "   " + issuedEarningsRounded);
    }
    public double GetSalary()
    {
        return salary;
    }

    // Метод для отримання суми податків
    public double GetTaxes()
    {
        return taxes;
    }

    // Метод для отримання суми виплачених заробітків
    public double GetIssuedEarnings()
    {
        return issuedEarnings;
    }
}

//// Головний клас програми
class Program
{
    // Метод для обчислення зарплати
    static double CalculateSalarySum(Data[] data)
    {
        double sum = 0;
        for (int i = 0; i < data.Length; i++)
        {
            sum += Math.Round(data[i].GetSalary(), 2);
        }

        return sum;
    }

    // Метод для обчислення суми податків
    static double CalculateTaxesSum(Data[] data)
    {
        double sum = 0;

        for (int i = 0; i < data.Length; i++)
        {
            sum += data[i].GetIssuedEarnings();
        }

        return Math.Round(sum, 2);
    }

    // Метод для обчислення суми виплачених заробітків
    static double CalculateIssuedEarningsSum(Data[] data)
    {
        double sum = 0;

        for (int i = 0; i < data.Length; i++)
        {
            sum += data[i].GetIssuedEarnings();
        }

        return Math.Round(sum, 2);
    }
    static void Main()
    {
        //tast cases:
        //input = 2
        //case1: obj1: surname = Sidorova
        //       salary = 12345
        //       obj2: surname = Koneyv
        //       salary = 56789
        //case3: obj1: surname = Nikolaenko
        //       salary = 67890
        //       obj2: surname = Clymenko
        //       salary: 34567
        
        bool validInput = false;
        int n = 0;
        //Перевірка
        while (!validInput)
        {
            Console.WriteLine("Write a number of rows");
            string input = Console.ReadLine();

            if (int.TryParse(input, out n))
            {
                validInput = true;
            }
            else
            {
                Console.WriteLine("It is not correct entering data. Please enter an integer number.");
            }
        }
        validInput = false;

        // Створення масиву об'єктів класу "Відомість"
        Data[] data = new Data[n];
        Console.WriteLine();
        // Уведення вихідних даних з консолі і створення об'єктів
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter a surname:");
            string surname = Console.ReadLine();

            double salary = 0;
            //Перевірка
            while (!validInput)
            {
                Console.WriteLine("Enter a salary:");
                string input = Console.ReadLine();

                if (double.TryParse(input, out salary))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("It is not correct entering data. Please enter an integer number.");
                }
            }
            validInput = false;
            Console.WriteLine("-------------------------------");

            data[i] = new Data(surname, salary);
        }

        Console.WriteLine();
        int num;
        Console.WriteLine("                Salary     Taxes   Issued Earnings");

        // Виведення відомостей на консоль
        for (int i = 0; i < n; i++)
        {
            num = i + 1;
            Console.Write(num + ") ");
            data[i].DisplayTaxes();
        }

        double salarySum = CalculateSalarySum(data);
        double taxesSum = CalculateTaxesSum(data);
        double issuedEarnіngsSum = CalculateIssuedEarningsSum(data);


        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("Total: salary " + salarySum + "   taxes " + taxesSum + "   issued earnongs " + issuedEarnіngsSum);
        //test cases:
        //case1:          Salary     Taxes   Issued Earnings
        //1) Sidorova:     12345   2407,28   9937,72
        //2) Koneyv:     56789   11073,86   45715,14
        //--------------------------------------------------
        //Together: salary 69134   taxes 13481   issued earnongs 55653

        //case2:                 Salary     Taxes   Issued Earnings
        //1) Nikolaenko:     67890   13238,55   54651,45
        //2) Clymenko:     34567   6740,56   27826,44
        //--------------------------------------------------
        //Together: salary 102457   taxes 19980   issued earnongs 82477
    }
}

