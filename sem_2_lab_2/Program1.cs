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
    }
}

