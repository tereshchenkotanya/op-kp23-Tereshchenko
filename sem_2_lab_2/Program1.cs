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
        // Обчислення значень розрахункових полів
    }

    // Метод для обчислення значення поля "податки"
    public void CalculateTaxes()
    {
        // Обчислення податків - 19,5% від зарплати

    }

    // Метод для виведення відомості на консоль
    public void DisplayTaxes()
    {

    }
    // Метод для отримання суми зарплат
    public double GetSalary()
    {

    }

    // Метод для отримання суми податків
    public double GetTaxes()
    {

    }

    // Метод для отримання суми виплачених заробітків
    public double GetIssuedEarnings()
    {

    }
}

//// Головний клас програми
class Program
{
    // Метод для обчислення суми зарплат
    static double CalculateSalarySum(Data[] data)
    {
        double sum = 0;
        for (int i = 0; i < data.Length; i++)
        {
            sum += Math.Round(data[i].GetSalary());
        }
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
        //tasr cases:
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

        // Створення масиву об'єктів класу "Відомість"
        Data[] data = new Data[n];
        string surname;
        double salary;
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

