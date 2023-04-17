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
    }
}

