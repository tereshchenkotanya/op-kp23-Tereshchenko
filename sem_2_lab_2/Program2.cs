using System;
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
    }

    // Метод для обчислення значення поля "податки"
    public void CalculateTaxes()
    {
        // Податки - 19,5% від зарплати

    }

    // Метод для виведення відомості на консоль
    public void DisplayTaxes()
    {

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
    static double CalculateSalarySum(Data[] data)
    {
        double sum = 0;

        return sum;
    }

    // Метод для обчислення суми податків
    static double CalculateTaxesSum(Data[] data)
    {
        double sum = 0;

        return sum;
    }

    // Метод для обчислення суми виплачених заробітків
    static double CalculateIssuedEarningsSum(Data[] data)
    {
        double sum = 0;

        return sum;
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
        string input;
        string surname;
        double salary;

        // Створення масиву об'єктів класу "Відомість"
        Data[] data = new Data[n];

        // Уведення вихідних даних з консолі і створення об'єктів

        int num;

        // Виведення відомостей на консоль

        double salarySum = CalculateSalarySum(data);
        double taxesSum = CalculateTaxesSum(data);
        double issuedEarnіngsSum = CalculateIssuedEarningsSum(data);
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

