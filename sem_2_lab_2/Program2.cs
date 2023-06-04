using System;
using System.Runtime.CompilerServices;

// Клас, що представляє один запис відомості
class Record
{
    // Поля запису
    private string surname;
    private double salary;
    private double taxes;
    private double issuedEarnings;
    public const double taxRate = 0.195;

    // Конструктор для встановлення початкових значень полів
    public Record(string surname, double salary)
    {
        this.surname = surname;
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

    // Метод для виведення запису на консоль
    public void DisplayRecord()
    {
        double taxesRounded = Math.Round(taxes, 2);
        double issuedEarningsRounded = Math.Round(issuedEarnings, 2);

        Console.WriteLine(surname + ":     " + salary + "   " + taxesRounded + "   " + issuedEarningsRounded);
    }

    // Метод для отримання зарплати
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

// Клас, що представляє відомість
class Data
{
    // Поля відомості
    private Record[] records;

    // Конструктор для встановлення початкових значень полів
    public Data(int n)
    {
        // Створення масиву об'єктів класу "Record"
        records = new Record[n];       
    }
    public void CreateRecordData(int i, string surname, double salary)
    {
        // Створення об'єкту "Record" з введеними даними
        records[i] = new Record(surname, salary);
    }
    // Метод для виведення заголовка відомості на консоль
    public void DisplayHeader()
    {
        Console.WriteLine("Surname: Salary: Taxes: Issued Earnings:");
    }

    // Метод для виведення відомості на консоль
    public void DisplayData()
    {
        // Виведення заголовка
        DisplayHeader();

        // Виведення записів
        for (int i = 0; i < records.Length; i++)
        {
            records[i].DisplayRecord();
        }
    }

    // Метод для розрахунку загальної суми зарплат
    public double CalculateTotalSalary()
    {
        double totalSalary = 0;

        // Обчислення загальної суми зарплат
        for (int i = 0; i < records.Length; i++)
        {
            totalSalary += records[i].GetSalary();
        }

        return totalSalary;
    }

    // Метод для розрахунку загальної суми податків
    public double CalculateTotalTaxes()
    {
        double sum = 0;

        // Обчислення загальної суми податків
        for (int i = 0; i < records.Length; i++)
        {
            sum += Math.Round(records[i].GetTaxes(), 2);
        }

        return Math.Round(sum, 2);
    }

    // Метод для розрахунку загальної суми виплачених заробітків
    public double CalculateTotalIssuedEarnings()
    {
        double sum = 0;

        // Обчислення загальної суми виплачених заробітків
        for (int i = 0; i < records.Length; i++)
        {
            sum += Math.Round(records[i].GetIssuedEarnings(), 2);
        }
        return Math.Round(sum, 2);
    }
}

class Program
{
    static void Main(string[] args)
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

        int n = 0;
        bool validInput = false;
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
        Data data = new Data(n); // Створення об'єкту "Data" з введеними даними

        Console.WriteLine();

        // Уведення вихідних даних з консолі і створення об'єктів "Record"
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter a surname:");
            string surname = Console.ReadLine();

            double salary = 0;

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

            Console.WriteLine("-------------------------------------------");
            data.CreateRecordData(i, surname, salary);
        }

        data.DisplayData(); // Виведення відомості на консоль

        double salarySum = data.CalculateTotalSalary();
        double taxesSum = data.CalculateTotalTaxes();
        double issuedEarnіngsSum = data.CalculateTotalIssuedEarnings();

        // Розрахунок та виведення загальної суми зарплат, податків та виплачених заробітків
        Console.WriteLine("-------------------------------------------");
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