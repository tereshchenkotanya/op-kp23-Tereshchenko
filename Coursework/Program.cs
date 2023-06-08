using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

class Constants
{
    public const char Separator = '$';
}

class School
{
    public School(string id, string schoolNumber, string phone, string email)
    {
        Id = id;
        Phone = phone;
        Email = email;
        SchoolNumber = schoolNumber;

    }
    public string Id { get; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string SchoolNumber { get; }
    public string ConvertToStringForFile()
    {
        return $"{Id}{Constants.Separator}{SchoolNumber}{Constants.Separator}{Phone}{Constants.Separator}{Email}";
    }
}
class SchoolManager : Manager
{
    protected override string FileName
    {
        get
        {
            return "Schools.txt";
        }
    }

    private List<School> _schools;
    public SchoolManager()
    {
        string[] schoolsData = ReadFromFile();
        _schools = new List<School>();
        string id;
        string phone;
        string email;
        string schoolNumber;

        for (int i = 0; i < schoolsData.Length; i++)
        {
            string[] splitData = schoolsData[i].Split(Constants.Separator);
            id = splitData[0];
            schoolNumber = splitData[1];
            phone = splitData[2];
            email = splitData[3];
            
            _schools.Add(new School(id, schoolNumber, phone, email));
        }
    }

    protected override string[] GenerateDataForFile()
    {
        string[] newData = new string[_schools.Count];
        for (int i = 0; i < _schools.Count; i++)
        {
            newData[i] = _schools[i].ConvertToStringForFile();
        }
        return newData;
    }

    public void AddSchool(string schoolNumber, string phone, string email)
    {
        School school = _schools.Last();
        int id = Convert.ToInt32(school.Id) + 1;
        _schools.Add(new School(Convert.ToString(id), schoolNumber, phone, email));
        SaveToFile();
    }

    public void UpdateSchoolContact(string schoolNumber, string phone, string email)
    {
        School? school = GetSchoolByNumber(schoolNumber);
        if (school != null)
        {
            school.Phone = phone;
            school.Email = email;
            SaveToFile();
        }
        else
        {
            Console.WriteLine("School was not found");
        }

    }

    public bool CheckIfSchoolExistByNumber(string enterSchoolNumber)
    {
        School? school = _schools.Find(school => school.SchoolNumber == enterSchoolNumber);
        if(school == null)
        {
            return false;
        }
        return true;
    }
    public School? GetSchoolByNumber(string? schoolNumber)
    {
        for (int i = 0; i < _schools.Count; i++)
        {
            if (schoolNumber == _schools[i].SchoolNumber)
            {
                return _schools[i];
            }
        }
        return null;
    }
}
abstract class Reader
{
    public string Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string BirthDay { get; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public abstract string ConvertToStringForFile();

    public Reader(string id, string lastName, string firstName, string birthDay, string phone, string email)
    {
        Id = id;
        LastName = lastName;
        FirstName = firstName;
        BirthDay = birthDay;
        Phone = phone;
        Email = email;
    }
}
class Student : Reader
{
    public Student(string id, string lastName, string firstName, string birthDay, string phone, string email, string schoolNumber) : base(id, lastName, firstName, birthDay, phone, email)
    {
        SchoolNumber = schoolNumber;
    }

    public string SchoolNumber { get; }

    public override string ConvertToStringForFile()
    {
        return $"{Id}{Constants.Separator}{LastName}{Constants.Separator}{FirstName}{Constants.Separator}{BirthDay}{Constants.Separator}{Phone}{Constants.Separator}{Email}{Constants.Separator}{SchoolNumber}";
    }
}
class Adult : Reader
{
    public Adult(string id, string lastName, string firstName, string birthDay, string phone, string email) : base(id, lastName, firstName, birthDay, phone, email)
    {
    }

    public override string ConvertToStringForFile()
    {
        return $"{Id}{Constants.Separator}{LastName}{Constants.Separator}{FirstName}{Constants.Separator}{BirthDay}{Constants.Separator}{Phone}{Constants.Separator}{Email}";
    }
}
abstract class Manager
{
    protected abstract string FileName { get; }
    protected abstract string[] GenerateDataForFile();
    public virtual string[] ReadFromFile()
    {
        string[] fileData = File.ReadAllLines(FileName);
        return fileData;
    }
    public virtual void SaveToFile()
    {
        string[] newData = GenerateDataForFile();
        File.WriteAllLines(FileName, newData);
    }
}
class ReaderManager : Manager
{
    protected override string FileName
    {
        get
        {
            return "Users.txt";
        }
    }

    public override void SaveToFile()
    {
        Console.WriteLine("Reader saving was started");
        base.SaveToFile();
        Console.WriteLine("Reader saving done");
    }

    private List<Reader> _readers;
    public List<Student> GetListOfAllStudents
    {
        get
        {
            return _readers.FindAll(student => student is Student).Cast<Student>().ToList();
        }
    }
    public List<Adult> GetListOfAllAdults
    {
        get
        {
            return _readers.FindAll(student => student is Adult).Cast<Adult>().ToList();
        }
    }
    public ReaderManager()
    {
        string[] readerData = ReadFromFile();
        _readers = new List<Reader>();
        string id;        
        string lastName;
        string firstName;
        string birthday;
        string phone;
        string email;
        string schoolNumber;

        for (int i = 0; i < readerData.Length; i++)
        {
            string[] splitData = readerData[i].Split(Constants.Separator);
            id = splitData[0];          
            lastName = splitData[1];
            firstName = splitData[2];
            birthday = splitData[3];
            phone = splitData[4];
            email = splitData[5];
            if (splitData.Length == 7)
            {
                schoolNumber = splitData[6];
                _readers.Add(new Student(id, lastName, firstName, birthday, phone, email, schoolNumber));
            }
            if (splitData.Length == 6)
            {
                _readers.Add(new Adult(id, lastName, firstName, birthday, phone, email));
            }
        }
    }

    protected override string[] GenerateDataForFile()
    {
        string[] newData = new string[_readers.Count];
        for (int i = 0; i < _readers.Count; i++)
        {
            newData[i] = _readers[i].ConvertToStringForFile();
        }
        return newData;
    }

    public void AddReader(string lastName, string firstName, string birthday, string phone, string email, string? schoolNumber)
    {
        Func<int, int>? getNextId = id => id + 1;

        Reader reader = _readers.Last();

        int id = getNextId(Convert.ToInt32(reader.Id));
        if (schoolNumber == null)
        {
            _readers.Add(new Adult(Convert.ToString(id), lastName, firstName, birthday, phone, email));
        }
        else
        {
            _readers.Add(new Student(Convert.ToString(id), lastName, firstName, birthday, phone, email, schoolNumber));
        }
        SaveToFile();
    }

    public void UpdateReaderContact(string readerId, string phone, string email)
    {
        Reader? reader = GetReaderById(readerId);
        if (reader != null)
        {
            reader.Phone = phone;
            reader.Email = email;
            SaveToFile();
        }
        else
        {
            Console.WriteLine("Reader does not exist");
        }
    }
    public Reader? GetReaderById(string? readerId)
    {
        return _readers.Find(reader => reader.Id == readerId);
    }

    public Reader? GetReaderByName(string lastName, string firstName)
    {
        return _readers.Find(reader =>  (reader.LastName == lastName && reader.FirstName == firstName));
    }
}
class BookDetail
{
    public BookDetail(string bookId, string bookTitile, string bookAuthor, string bookCreateDate, string bookType)
    {
        BookId = bookId;
        BookTitle = bookTitile;
        BookAuthor = bookAuthor;
        BookCreateDate = bookCreateDate;
        BookType = bookType;
    }
    public string BookId { get; }
    public string BookTitle { get; }
    public string BookAuthor { get; }
    public string BookCreateDate { get; }
    public string BookType { get; }

    public string ConvertToStringForFile()
    {
        return $"{BookId}{Constants.Separator}{BookTitle}{Constants.Separator}{BookAuthor}{Constants.Separator}{BookCreateDate}{Constants.Separator}{BookType}";
    }
}
class BookCopy
{
    public BookCopy(string bookCopyId, string bookCopyDetailId, string? readerId)
    {
        BookCopyId = bookCopyId;
        BookDetailId = bookCopyDetailId;
        ReaderId = readerId;
    }
    public string BookCopyId { get; }
    public string BookDetailId { get; }
    public string? ReaderId { get; set; }

    public string ConvertToStringForFile()
    {
        return $"{BookCopyId}{Constants.Separator}{BookDetailId}{Constants.Separator}{ReaderId}";
    }
}
class BookDetailsManager : Manager
{
    protected override string[] GenerateDataForFile()
    {
        string[] newData = new string[_bookDetails.Count];
        for (int i = 0; i < _bookDetails.Count; i++)
        {
            newData[i] = _bookDetails[i].ConvertToStringForFile();
        }
        return newData;
    }

    protected override string FileName
    {
        get
        {
            return "BookDetails.txt";
        }
    }

    private List<BookDetail> _bookDetails;
    public BookDetailsManager()
    {
        _bookDetails = new List<BookDetail>();
        string[] bookDetailsData = ReadFromFile();

        for (int i = 0; i < bookDetailsData.Length; i++)
        {
            string[] splitData = bookDetailsData[i].Split('$');

            string bookId = splitData[0];
            string bookTitile = splitData[1];
            string bookAuthor = splitData[2];
            string bookCreateDate = splitData[3];
            string bookType = splitData[4];

            _bookDetails.Add(new BookDetail(bookId, bookTitile, bookAuthor, bookCreateDate, bookType));
        }
    }
    public List<string> FindBooksByType(string bookType)
    {
        List<string> booksByType = new();
        foreach(BookDetail bookDetail in _bookDetails)
        {
            if(bookDetail.BookType == bookType)
            {
                booksByType.Add(bookDetail.BookTitle);
            }
        }
        return booksByType;
    }
    public void AddBookDetails(string bookTitile, string bookAuthor, string bookCreateDate, string bookType)
    {
        Func<int, int>? getNextId = id => id + 1;
        BookDetail bookDetail = _bookDetails.Last();
        int id = getNextId(Convert.ToInt32(bookDetail.BookId));
        _bookDetails.Add(new BookDetail(Convert.ToString(id), bookTitile, bookAuthor, bookCreateDate, bookType));
        SaveToFile();
    }


    public BookDetail? GetBookDetailsById(string? bookDetailsId)
    {
        return _bookDetails.Find(x => x.BookId == bookDetailsId);
    }

    public BookDetail? GetBookDetailsByName(string bookName)
    {
        return _bookDetails.Find(x => x.BookTitle == bookName);
    }

    public List<string> GetAllPossibleTypes()
    {
        List<string> possibleTypes = new List<string>();

        foreach(BookDetail bookDetail in _bookDetails)
        {
            if(!possibleTypes.Contains(bookDetail.BookType))
            {
                possibleTypes.Add(bookDetail.BookType);
            }
        }

        return possibleTypes;
    }

    public List<BookDetail> GetBooksByType(string bookType)
    {
        List<BookDetail> books = new List<BookDetail>();

        foreach (BookDetail bookDetail in _bookDetails)
        {
            if(bookDetail.BookType == bookType)
            {
                books.Add(bookDetail);
            }
            
        }
        return books;
    }
}
class BookCopiesManager : Manager
{
    protected override string[] GenerateDataForFile()
    {
        List<string> newData = new List<string>();
        foreach (KeyValuePair<string, List<BookCopy>> entry in _bookCopiesDictionary)
        {
            string key = entry.Key;
            List<BookCopy> bookCopies = entry.Value;
            foreach (BookCopy bookCopy in bookCopies)
            {
                string dataForFile = bookCopy.ConvertToStringForFile();
                newData.Add(dataForFile);
            }
        }
        return newData.ToArray();
    }

    protected override string FileName
    {
        get
        {
            return "BookCopies.txt";
        }
    }

    private HistoryManager _historyManager;
    private Dictionary<string, List<BookCopy>> _bookCopiesDictionary;
    private int nextId = 0;
    public Dictionary<string, List<BookCopy>> BookCopiesDictionary
    {
        get
        {
            return _bookCopiesDictionary;
        }
    }
    public BookCopiesManager()
    {
        _historyManager = new HistoryManager();
        string[] bookCopiesData = ReadFromFile();
        _bookCopiesDictionary = new Dictionary<string, List<BookCopy>>();

        for (int i = 0; i < bookCopiesData.Length; i++)
        {
            string[] splitData = bookCopiesData[i].Split($"{Constants.Separator}");
            string bookCopyId = splitData[0];
            string bookCopyDetailId = splitData[1];
            string readerId = splitData[2];
            if (_bookCopiesDictionary.ContainsKey(bookCopyDetailId) == false)
            {
                _bookCopiesDictionary[bookCopyDetailId] = new List<BookCopy>();
            }
            _bookCopiesDictionary[bookCopyDetailId].Add(new BookCopy(bookCopyId, bookCopyDetailId, readerId));
        }
        foreach (KeyValuePair<string, List<BookCopy>> entry in _bookCopiesDictionary)
        {
            foreach(BookCopy bookCopy in entry.Value)
            {
                if(nextId < Convert.ToInt32(bookCopy.BookCopyId))
                {
                    nextId = Convert.ToInt32(bookCopy.BookCopyId);
                }
            }
        }
        nextId++;
    }
    public string? FindWhoBorrowBook(string bookCopyId)
    {
        Dictionary<string, List<BookCopy>>.KeyCollection keyColl = _bookCopiesDictionary.Keys;

        foreach (string key in keyColl)
        {
            List<BookCopy> bookCopies = _bookCopiesDictionary[key];
            BookCopy? bookCopy = bookCopies.FirstOrDefault(bookCopy => bookCopy.BookCopyId == bookCopyId);
            if (bookCopy != null)
            {
                return bookCopy.ReaderId;
            }
        }

        return null;
    }
    public BookCopy? GetBookCopyById(string bookCopyId)
    {
        Dictionary<string, List<BookCopy>>.KeyCollection keyColl = _bookCopiesDictionary.Keys;

        foreach (string key in keyColl)
        {
            List<BookCopy> bookCopies = _bookCopiesDictionary[key];

            for (int i = 0; i < bookCopies.Count; i++)
            {
                if (bookCopyId == bookCopies[i].BookCopyId)
                {
                    return bookCopies[i];
                }
            }
        }
        return null;
    }
    public void AddBookCopy(string bookDetailId)
    {
        bool doesCopyWasAdded = false;
        foreach (KeyValuePair<string, List<BookCopy>> entry in _bookCopiesDictionary)
        {
            if (entry.Key == bookDetailId)
            {
                List<BookCopy> bookCopies = entry.Value;
                string bookCopyId = Convert.ToString(nextId);
                nextId++;
                BookCopy newBookCopy = new(bookCopyId, entry.Key, null);
                bookCopies.Add(newBookCopy);
                doesCopyWasAdded = true;
                break;
            }
        }
        if(!doesCopyWasAdded)
        {
            _bookCopiesDictionary.Add(bookDetailId, new List<BookCopy>());
            string bookCopyId = Convert.ToString(nextId);
            nextId++;
            BookCopy newBookCopy = new(bookCopyId, bookDetailId, null);
            _bookCopiesDictionary.LastOrDefault().Value.Add(newBookCopy);
        }
        SaveToFile();
    }
    public void ResereveBookCopyToReader(string bookCopyId, string readerId, string date)
    {
        BookCopy? bookCopy = GetBookCopyById(bookCopyId);
        if (bookCopy == null) return;
        bookCopy.ReaderId = readerId;
        _historyManager.AddHistoryRecord(readerId, bookCopyId, date);
        SaveToFile();
    }
    public void DropBookCopyFromReader(string bookCopyId)
    {
        BookCopy? bookCopy = GetBookCopyById(bookCopyId);
        if (bookCopy == null) return;
        bookCopy.ReaderId = null;
        SaveToFile();
    }
    public bool DeleteBookCopy(string bookCopyId)
    {
        BookCopy? bookCopy = GetBookCopyById(bookCopyId);
        if (bookCopy == null)
        {
            return false;
        }
        _bookCopiesDictionary[bookCopy.BookDetailId]?.Remove(bookCopy);
        SaveToFile();
        return true;
    }
    public HistoryManager GetHistoryManager
    {
        get { return _historyManager; }
    }
    public List<BookCopy> GetBooksByReaderId(string readerId)
    {
        Dictionary<string, List<BookCopy>>.KeyCollection keyColl = _bookCopiesDictionary.Keys;
        List<BookCopy> bookCopyList = new();

        foreach (string key in keyColl)
        {
            foreach (BookCopy bookCopy in _bookCopiesDictionary[key])
            {
                if (bookCopy.ReaderId == readerId)
                {
                    bookCopyList.Add(bookCopy);
                }
            }
        }
        return bookCopyList;
    }
    public List<BookCopy> GetReservedBooks()
    {
        Dictionary<string, List<BookCopy>>.KeyCollection keyColl = _bookCopiesDictionary.Keys;
        List<BookCopy> bookCopyList = new();

        foreach (string key in keyColl)
        {
            foreach (BookCopy bookCopy in _bookCopiesDictionary[key])
            {
                if (bookCopy.ReaderId != null)
                {
                    bookCopyList.Add(bookCopy);
                }
            }
        }
        return bookCopyList;
    }
    public List<BookCopy> GetAvailableBooksByBookDetailId(string bookDetailId)
    {
        List<BookCopy>? bookCopyList = _bookCopiesDictionary[bookDetailId];
        List<BookCopy> availableBookCopyList = new();


        foreach (BookCopy bookCopy in bookCopyList)
        {
            if (bookCopy.ReaderId == null)
            {
                availableBookCopyList.Add(bookCopy);
            }
        }
        return bookCopyList;
    }
    public bool CheckIfReaderHasBorrowingBooks(string readerId)
    {
        List<BookCopy> borrowingBooks = GetReservedBooks();
        foreach (BookCopy bookCopy in borrowingBooks)
        {
            if(bookCopy.ReaderId == readerId)
            {
                return true;
            }
        }
        return false;
    }
}
class HistoryRecord
{
    public HistoryRecord(string bookCopyId, string readerId, string date)
    {
        ReaderId = readerId;
        BookCopyId = bookCopyId;
        Date = date;
    }
    public string ReaderId { get; }
    public string BookCopyId { get; }
    public string Date { get; }

    public string ConvertToStringForFile()
    {
        return $"{ReaderId}{Constants.Separator}{BookCopyId}{Constants.Separator}{Date}";
    }
}
class HistoryManager : Manager
{
    public HistoryManager()
    {
        string[] historyData = ReadFromFile();
        _histories = new List<HistoryRecord>();

        for (int i = 0; i < historyData.Length; i++)
        {
            string[] splitData = historyData[i].Split($"{Constants.Separator}");
            string bookCopyId = splitData[0];
            string readerId = splitData[1];
            string date = splitData[2];
            _histories.Add(new HistoryRecord(readerId, bookCopyId, date));
        }
    }
    protected override string[] GenerateDataForFile()
    {
        string[] newData = new string[_histories.Count];
        for (int i = 0; i < _histories.Count; i++)
        {
            newData[i] = _histories[i].ConvertToStringForFile();
        }
        return newData;
    }
    public void AddHistoryRecord(string readerId, string bookCopyId, string date)
    {
        HistoryRecord record = new(readerId, bookCopyId, date);
        _histories.Add(record);
        SaveToFile();
    }
    protected override string FileName
    {
        get
        {
            return "History.txt";
        }
    }

    private List<HistoryRecord> _histories;
   
    public List<HistoryRecord> GetHistoryByReader(string readerId)
    {
        List<HistoryRecord> historyByReader = _histories.FindAll(historyRecord => historyRecord.ReaderId == readerId);
        return historyByReader;
    }
    public List<HistoryRecord> GetHistoryByBook(string bookCopyId)
    {
        List<HistoryRecord> historyByBook = _histories.FindAll(historyRecord => historyRecord.BookCopyId == bookCopyId);
        return historyByBook;
    }
}
class Library
{
    private ReaderManager _readerManager;
    private BookDetailsManager _bookDetailsManager;
    private BookCopiesManager _bookCopiesManager;
    private SchoolManager _schoolManager;
    public Library()
    {
        _readerManager = new ReaderManager();
        _bookDetailsManager = new BookDetailsManager();
        _bookCopiesManager = new BookCopiesManager();
        _schoolManager = new SchoolManager();
    }
    
    public void AddNewReader()
    {
        string? lastName = null;
        string? firstName = null;
        string? birthday = null;
        string? phone = null;
        string? email = null;
        string? schoolNumber = null;
        string? isReaderStudent = null;

        string text = "Add New Reader";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine();

        ConsoleKeyInfo key;
        Console.Write("Is new reader is student or adult? Write S if student and A if adult: ");
        while(true)
        {
            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
                else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    isReaderStudent += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && isReaderStudent.Length > 0)
                {
                    isReaderStudent = lastName.Substring(0, isReaderStudent.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter && (isReaderStudent == "s" || isReaderStudent == "a"));
            if (isReaderStudent == "s" || isReaderStudent == "a")
            {
                break;
            }
            else
            {
                Console.WriteLine("Try again");
            }
        }
        
        Console.Write("\nWrite lastname: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                lastName += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && lastName.Length > 0)
            {
                lastName = lastName.Substring(0, lastName.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.Write("\nWrite firstName: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                firstName += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && firstName.Length > 0)
            {
                firstName = firstName.Substring(0, firstName.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.Write("\nWrite birthday: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                birthday += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && birthday.Length > 0)
            {
                birthday = birthday.Substring(0, birthday.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.Write("\nWrite phone: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                phone += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && phone.Length > 0)
            {
                phone = phone.Substring(0, phone.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.Write("\nWrite email: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                email += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && email.Length > 0)
            {
                email = email.Substring(0, email.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter );

        if (string.Equals(isReaderStudent, "s", StringComparison.OrdinalIgnoreCase))
        {
            Console.Write("\nWrite school number: ");
            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
                else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    schoolNumber += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && schoolNumber.Length > 0)
                {
                    schoolNumber = schoolNumber.Substring(0, schoolNumber.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
        }

        _readerManager.AddReader(lastName, firstName, birthday, phone, email, schoolNumber);
        _bookCopiesManager.GetHistoryManager.SaveToFile();

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Reader was added");
        Console.ResetColor();

        Escape();
    }
    public void UpgradeReaderContacts()
    {
        string? lastName = null;
        string? firstName = null;
        string? phone = null;
        string? email = null;

        string text = "Update Reader's Contacts";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine();

        ConsoleKeyInfo key;
        Console.Write("Write lastname: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                lastName += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && lastName.Length > 0)
            {
                lastName = lastName.Substring(0, lastName.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.Write("\nWrite firstName: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                firstName += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && firstName.Length > 0)
            {
                firstName = firstName.Substring(0, firstName.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Reader? reader = _readerManager.GetReaderByName(lastName, firstName);
        if (reader == null)
        {
            Console.WriteLine("Reader was not found");
        }
        else
        {
            Console.Write("\nWrite new phone: ");
            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
                else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    phone += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && phone.Length > 0)
                {
                    phone = phone.Substring(0, phone.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.Write("\nWrite new email: ");
            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
                else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    email += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && email.Length > 0)
                {
                    email = email.Substring(0, email.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            _readerManager.UpdateReaderContact(reader.Id, phone, email);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Reader's contacts were updated");
            Console.ResetColor();
        }
        
        Escape();
    }
    public void ShowInformationAboutReader()
    {
        string? lastName = null;
        string? firstName = null; 

        string text = "Show Information About Reader";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine();

        ConsoleKeyInfo key;
        Console.Write("Write lastname: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                lastName += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && lastName.Length > 0)
            {
                lastName = lastName.Substring(0, lastName.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.Write("\nWrite firstname: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                firstName += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && firstName.Length > 0)
            {
                firstName = firstName.Substring(0, firstName.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Reader? reader = _readerManager.GetReaderByName(lastName, firstName);
        if (reader == null)
        {
            Console.WriteLine("Reader was not found");
        }
        else
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\nAll information");
            Console.ResetColor();
            Console.WriteLine("{0,-15}{1,-20}", "Name:", $"{lastName} {firstName}");
            Console.WriteLine("{0,-15}{1,-20}", "Birthday:", $"{reader.BirthDay}");
            Console.WriteLine("{0,-15}{1,-20}", "Phone:", $"{reader.Phone}");
            Console.WriteLine("{0,-15}{1,-20}", "Email:", $"{reader.Email}");
            if (reader is Student student)
            {
                Console.WriteLine("{0,-15}{1,-20}", "School #:", $"{student.SchoolNumber}");
            }
        }     

        Escape();
    }
    public void AddBookCopy()
    {
        string? title = null;

        string text = "Add New Book Copy";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine();

        ConsoleKeyInfo key;
        Console.Write("Write title: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                title += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && title.Length > 0)
            {
                title = title.Substring(0, title.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();

        BookDetail? book = _bookDetailsManager.GetBookDetailsByName(title);
        if(book == null)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nThere is any book with this title");
            Console.ResetColor();
        }
        else
        {
            _bookCopiesManager.AddBookCopy(_bookDetailsManager.GetBookDetailsByName(title).BookId);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nNew book copy was added");
            Console.ResetColor();
        }

        Escape();
    }
    public void DeleteBookCopy()
    {
        string? bookCopyId = null;

        string text = "Delete Book Copy";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine();

        ConsoleKeyInfo key;
        Console.Write("Enter ID of the book: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                bookCopyId += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && bookCopyId.Length > 0)
            {
                bookCopyId = bookCopyId.Substring(0, bookCopyId.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        bool doesBookWasDleted = _bookCopiesManager.DeleteBookCopy(bookCopyId);
        if(!doesBookWasDleted)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n\nID is not correct");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nBook copy was deleted");
            Console.ResetColor();
        }
        
        Escape();
    }
    public void AddDetailsForNewBook()
    {
        string? bookTitle = null;
        string? bookAuthor = null; 
        string? bookCreateDate = null;
        string? bookType = null;

        string text = "Add Details For New Book";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine();

        ConsoleKeyInfo key;
        Console.Write("Write book title: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                bookTitle += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && bookTitle.Length > 0)
            {
                bookTitle = bookTitle.Substring(0, bookTitle.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.Write("\nWrite author: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                bookAuthor += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && bookAuthor.Length > 0)
            {
                bookAuthor = bookAuthor.Substring(0, bookAuthor.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.Write("\nWrite a year of creating: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                bookCreateDate += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && bookCreateDate.Length > 0)
            {
                bookCreateDate = bookCreateDate.Substring(0, bookCreateDate.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.Write("\nWrite book type: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                bookType += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && bookType.Length > 0)
            {
                bookType = bookType.Substring(0, bookType.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        _bookDetailsManager.AddBookDetails(bookTitle, bookAuthor, bookCreateDate, bookType);
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("\n\nNew book was added");
        Console.ResetColor();
        Escape();
    }
    public void ShowInformationAboutBook()
    {
        string? bookTitle = null;

        string text = "Show Details of The Book";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();

        ConsoleKeyInfo key;
        Console.Write("Write book title: ");

        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                bookTitle += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && bookTitle.Length > 0)
            {
                bookTitle = bookTitle.Substring(0, bookTitle.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        BookDetail? bookDetail = _bookDetailsManager.GetBookDetailsByName(bookTitle);

        if (bookDetail == null)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nBook was not found");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n\nAll information: ");
            Console.ResetColor();

            Console.WriteLine("{0,-15}{1,-20}", "Title:", $"{bookTitle}");
            Console.WriteLine("{0,-15}{1,-20}", "Author:", $"{bookDetail.BookAuthor}");
            Console.WriteLine("{0,-15}{1,-20}", "Year: ", $"{bookDetail.BookCreateDate}");
            Console.WriteLine("{0,-15}{1,-20}", "Book type: ", $"{bookDetail.BookType}");
        }

        Escape();
    }
    public void ShowBooksByType()
    {
        List<string> bookTypes = _bookDetailsManager.GetAllPossibleTypes();

        string text = "Show Books By Type";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();

        int selectedOption = 0;

        bool exitOptionMenu = false;

        while (!exitOptionMenu)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            text = "Choose type you need:";
            Console.WriteLine(text);
            Console.ResetColor();
            Console.CursorVisible = false;

            for (int i = 0; i < bookTypes.Count; i++)
            {
                if (i == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine($"{bookTypes[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{bookTypes[i]}");
                    Console.ResetColor();
                }
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedOption = (selectedOption - 1 + bookTypes.Count) % bookTypes.Count;
                    break;
                case ConsoleKey.Escape:
                    return;
                case ConsoleKey.DownArrow:
                    selectedOption = (selectedOption + 1) % bookTypes.Count;
                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    List<BookDetail> bookDetails = _bookDetailsManager.GetBooksByType(bookTypes[selectedOption]);

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("{0,-25}{1,-20}{2,-20}", "Title", "Author", "Is Book Available now");
                    Console.ResetColor();
                    string isBookAvailableNow;

                    foreach (BookDetail bookDetail in bookDetails)
                    {
                        isBookAvailableNow = "Yes";

                        if (!CheckIfBookAvailable(bookDetail.BookTitle))
                        {
                            isBookAvailableNow = "No";
                        }

                        Console.WriteLine("{0,-25}{1,-20}{2,-20}", $"{bookDetail.BookTitle}", $"{bookDetail.BookAuthor}", $"{isBookAvailableNow}");
                    }
                    Escape();
                    break;
                default:
                    break;
            }
        }
    }
    public void AddNewSchool()
    {
        string? schoolNumber = null;
        string? schoolPhone = null;
        string? schoolEmail = null;

        string text = "Add new school to the system";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();

        ConsoleKeyInfo key;
        Console.Write("Write number of new school: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                schoolNumber += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && schoolNumber.Length > 0)
            {
                schoolNumber = schoolNumber.Substring(0, schoolNumber.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        if(_schoolManager.CheckIfSchoolExistByNumber(schoolNumber))
        {
            Console.ForegroundColor= ConsoleColor.DarkRed;
            Console.WriteLine("\nSchool with this number is already exist");
            Console.ResetColor();
        }
        else
        {
            Console.Write("\nWrite phone of new school: ");
            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
                else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    schoolPhone += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && schoolPhone.Length > 0)
                {
                    schoolPhone = schoolPhone.Substring(0, schoolPhone.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.Write("\nWrite email of new school: ");
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
                else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    schoolEmail += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && schoolEmail.Length > 0)
                {
                    schoolEmail = schoolEmail.Substring(0, schoolEmail.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter) ;

            _schoolManager.AddSchool(schoolNumber, schoolPhone, schoolEmail);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nNew school was added");
            Console.ResetColor();
        }
       
        Escape();
    }
    public void UpdateSchoolContacts()
    { 
        string? schoolNumber = null;
        string? schoolPhone = null;
        string? schoolEmail = null;

        string text = "Update School's Contacts";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();

        ConsoleKeyInfo key;
        Console.Write("Write number of school: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                schoolNumber += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && schoolNumber.Length > 0)
            {
                schoolNumber = schoolNumber.Substring(0, schoolNumber.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        School? school = _schoolManager.GetSchoolByNumber(schoolNumber);
        if (school == null)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nSchool was not found");
            Console.ResetColor();
        }
        else
        {
            Console.Write("\nWrite new phone of school: ");
            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
                else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    schoolPhone += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && schoolPhone.Length > 0)
                {
                    schoolPhone = schoolPhone.Substring(0, schoolPhone.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.Write("\nWrite new email of school: ");
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
                else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    schoolEmail += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && schoolEmail.Length > 0)
                {
                    schoolEmail = schoolEmail.Substring(0, schoolEmail.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter) ;

            _schoolManager.UpdateSchoolContact(school.Id, schoolPhone, schoolEmail);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("School's contacts were updated");
            Console.ResetColor();
        }
        Escape();
    }
    public void ShowInformationAboutSchool()
    {
        string? schoolNumber = null;

        string text = "Get Information About School";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine();

        ConsoleKeyInfo key;
        Console.Write("Write number of school: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                schoolNumber += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && schoolNumber.Length > 0)
            {
                schoolNumber = schoolNumber.Substring(0, schoolNumber.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        School? school = _schoolManager.GetSchoolByNumber(schoolNumber);
        if (school == null)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nSchool was not found");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\nAll information: ");
            Console.ResetColor();
            Console.WriteLine("{0,-10}{1,-20}", "School#:", $"{schoolNumber}");
            Console.WriteLine("{0,-10}{1,-20}", "Phone:", $"{school.Phone}");
            Console.WriteLine("{0,-10}{1,-20}", "Email:", $"{school.Email}");
        }
        Escape();
    }
    public void ShowReaderHistory()
    {
        string? lastName =  null;
        string? firstName = null;
        string massageIfReaderReturnBook = "No";

        string text = "Show Reader History";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine();

        ConsoleKeyInfo key;
        Console.Write("Write lastname: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                lastName += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && lastName.Length > 0)
            {
                lastName = lastName.Substring(0, lastName.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.Write("\nWrite firstname: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                firstName += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && firstName.Length > 0)
            {
                firstName = firstName.Substring(0, firstName.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Reader? reader = _readerManager.GetReaderByName(lastName, firstName);

        if (reader == null)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nReader does not exist");
            Console.ResetColor();
        }
        else
        {
            List<HistoryRecord> readerHistory = _bookCopiesManager.GetHistoryManager.GetHistoryByReader(reader.Id);
            if (readerHistory == null)
            {
                Console.WriteLine("\nReader has not history yet");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("{0,-30}{1,-20}{2,-20}", "BookTitle", "|Data of Borrowing", "|If reader return book");
                Console.WriteLine("-------------------------------------------------------------------------");
                foreach (HistoryRecord record in readerHistory)
                {
                    BookCopy? bookCopy = _bookCopiesManager.GetBookCopyById(record.BookCopyId);
                    BookDetail? bookDetail = _bookDetailsManager.GetBookDetailsById(bookCopy?.BookDetailId);
                    if(bookCopy == null)
                    {
                        continue;
                    }
                    else
                    {
                        if (bookCopy?.ReaderId != null)
                        {
                            massageIfReaderReturnBook = "No";
                        }

                        Console.WriteLine("{0,-30}{1,-20}{2,-20}", $"{bookDetail.BookTitle}", $"|{record.Date}", $"|{massageIfReaderReturnBook}");
                        massageIfReaderReturnBook = "Yes";
                    }                   
                }
            }
        }

        Escape();
    }
    public void ShowBookCopyHistory()
    {
        string? bookCopyId = null;

        string? text = "Show Book Copy History";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine();

        ConsoleKeyInfo key;
        Console.Write("Write book copy ID: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                bookCopyId += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && bookCopyId.Length > 0)
            {
                bookCopyId = bookCopyId.Substring(0, bookCopyId.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        BookCopy? bookCopy = _bookCopiesManager.GetBookCopyById(bookCopyId);

        if (bookCopy == null)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nBook copy with this ID does not exist");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine();
            List<HistoryRecord> readerHistory = _bookCopiesManager.GetHistoryManager.GetHistoryByBook(bookCopyId);

            if (readerHistory == null)
            {
                Console.WriteLine("\nThis book copy has not history yet");
            }
            else
            {
                Console.WriteLine("{0,-30}{1,-20}{2,-20}{3,-20}", "Lastname and Firstname", "|Data of Borrowing", "|Phone", "|Email");
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                foreach (HistoryRecord record in readerHistory)
                {
                    Reader? reader = _readerManager.GetReaderById(record.ReaderId);
                    Console.WriteLine("{0,-30}{1,-20}{2,-20}{3,-20}", $"{reader.LastName} {reader.FirstName}", $"|{record.Date}", $"|{reader.Phone}", $"|{reader.Email}");
                }

                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine();

                if (bookCopy.ReaderId != null)
                {
                    Console.WriteLine($"Now {_readerManager.GetReaderById(bookCopy.ReaderId).LastName} {_readerManager.GetReaderById(bookCopy.ReaderId).LastName} has this book");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("The book copy is in the library now");
                }
            }
        }        
        Escape();
    }
    public void BorrowBookCopyToReader()
    {
        string? lastName = null;
        string? firstName = null;
        string? bookTitle = null;
        string? date = null;

        string? text = "Borrow book to reader";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine();

        ConsoleKeyInfo key;
        Console.Write("Write lastname: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                lastName += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && lastName.Length > 0)
            {
                lastName = lastName.Substring(0, lastName.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.Write("\nWrite firstname: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                firstName += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && firstName.Length > 0)
            {
                firstName = firstName.Substring(0, firstName.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Reader? reader = _readerManager.GetReaderByName(lastName, firstName);
        if (reader == null)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n\nReader was not found");
            Console.ResetColor();
        }
        else
        {
            Console.Write("\nWrite book title: ");
            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
                else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    bookTitle += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && bookTitle.Length > 0)
                {
                    bookTitle = bookTitle.Substring(0, bookTitle.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            if (_bookDetailsManager.GetBookDetailsByName(bookTitle) == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\nBook with this title does not exist");
                Console.ResetColor();
            }
            else if(!CheckIfBookAvailable(bookTitle))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\nBook is not available now");
                Console.ResetColor();
            }
            else
            {
                Console.Write("\nWrite today's date in format 01.01.2004: ");
                do
                {
                    key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                    else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        date += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                    else if (key.Key == ConsoleKey.Backspace && date.Length > 0)
                    {
                        date = date.Substring(0, date.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                while (key.Key != ConsoleKey.Enter);

                Console.WriteLine();

                List<BookCopy>? allBookCopies = _bookCopiesManager.GetAvailableBooksByBookDetailId(_bookDetailsManager.GetBookDetailsByName(bookTitle).BookId);

                _bookCopiesManager.ResereveBookCopyToReader(allBookCopies.Last().BookCopyId, reader.Id, date);

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nBook was borrowing to reader");
                Console.ResetColor();
            }           
        }
        Escape();
    }
    public void DropBookCopyFromReader()
    {
        string? lastName = null;
        string? firstName = null;
        string? bookTitle = null;

        string? text = "Borrow book to reader";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine();

        ConsoleKeyInfo key;
        Console.Write("Write lastname: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                lastName += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && lastName.Length > 0)
            {
                lastName = lastName.Substring(0, lastName.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.Write("\nWrite firstName: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                firstName += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && firstName.Length > 0)
            {
                firstName = firstName.Substring(0, firstName.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Reader? reader = _readerManager.GetReaderByName(lastName, firstName);
        if (reader == null)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nReader was not found");
            Console.ResetColor();
        }
        else
        {
            Console.Write("\nWrite book title: ");
            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
                else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    bookTitle += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && bookTitle.Length > 0)
                {
                    bookTitle = bookTitle.Substring(0, bookTitle.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            if(_bookDetailsManager.GetBookDetailsByName(bookTitle) == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\nBook with this title was not found");
                Console.ResetColor();
            }
            else
            {
                List<HistoryRecord> history = _bookCopiesManager.GetHistoryManager.GetHistoryByReader(reader.Id);
                if(history == null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("History record was not found");
                    Console.ResetColor();
                }
                else
                {
                    List<BookCopy> bookCopies = _bookCopiesManager.GetBooksByReaderId(reader.Id);

                    foreach (BookCopy bookCopy in bookCopies)
                    {
                        if (_bookDetailsManager.GetBookDetailsById(bookCopy.BookDetailId).BookTitle == bookTitle)
                        {
                            _bookCopiesManager.DropBookCopyFromReader(bookCopy.BookCopyId);
                            break;
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\n\nBook copy was droped from reader");
                    Console.ResetColor();
                }               
            }           
        }
        Escape();
    }
    public void ShowBorrowingBooksBySchool()
    {
        string schoolNumber = "";

        string text = "Show Reserved Books By School";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine();

        ConsoleKeyInfo key;
        Console.Write("Write school number: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                schoolNumber += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && schoolNumber.Length > 0)
            {
                schoolNumber = schoolNumber.Substring(0, schoolNumber.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        School? school = _schoolManager.GetSchoolByNumber(schoolNumber);
        if (school == null)
        {
            Console.WriteLine("School was not found");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine();
            bool ifAnyHistoryRecordExist = false;
            List<Student> students = _readerManager.GetListOfAllStudents;
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine();
                Student student = students[i];
                if (student.SchoolNumber == schoolNumber && _bookCopiesManager.CheckIfReaderHasBorrowingBooks(student.Id) == true)
                {
                    ShowBorrowingBooksByReader(student.LastName, student.FirstName);
                    ifAnyHistoryRecordExist = true;
                }
            }
            if (!ifAnyHistoryRecordExist)
            {
                Console.WriteLine("There is any history record yet");
            }
        }

        Escape();
    }
    public void ShowBorrowingBooksByAdults()
    {
        string text = "Show Reserved Books By Adults Readers";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine();

        List<Adult> adultReaders = _readerManager.GetListOfAllAdults;

        foreach (Adult adult in adultReaders)
        {
            if (_bookCopiesManager.CheckIfReaderHasBorrowingBooks(adult.Id) == true)
            {
                Console.WriteLine();
                ShowBorrowingBooksByReader(adult.LastName, adult.FirstName);
            }
        }
        Escape();
    }
    public void ShowBorrowingBooksByReader()
    {
        string? lastName = null;
        string? firstName = null;
        string? schoolNumber;

        string text = "Show reader's borrowing books";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine();

        ConsoleKeyInfo key;
        Console.Write("Write lastname: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                lastName += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && lastName.Length > 0)
            {
                lastName = lastName.Substring(0, lastName.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.Write("\nWrite firstname: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                firstName += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && firstName.Length > 0)
            {
                firstName = firstName.Substring(0, firstName.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Reader? reader = _readerManager.GetReaderByName(lastName, firstName);
        if (reader == null)
        {
            Console.WriteLine("\nReader was not found");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("\nReader's contacts: " + reader.Phone + ", " + reader.Email);
            if (reader is Student student)
            {
                schoolNumber = student.SchoolNumber;
                string schoolPhone = _schoolManager.GetSchoolByNumber(student.SchoolNumber).Phone;
                string schoolEmail = _schoolManager.GetSchoolByNumber(student.SchoolNumber).Email;
                Console.WriteLine("School number " + schoolNumber + ": " + schoolPhone + ", " + schoolEmail);
            }

            List<BookCopy> bookCopies = _bookCopiesManager.GetBooksByReaderId(reader.Id);

            if (bookCopies.Count > 0)
            {
                List<HistoryRecord> historyByReader = _bookCopiesManager.GetHistoryManager.GetHistoryByReader(reader.Id);
                Console.WriteLine();
                Console.WriteLine("{0,-30}{1,-20}", "BookName", "|Data of Borrowing");
                Console.WriteLine("----------------------------------------------------");
                foreach (BookCopy bookCopy in bookCopies)
                {
                    HistoryRecord? record = historyByReader.FindLast(record => record.BookCopyId == bookCopy.BookCopyId);
                    BookDetail? bookDetail = _bookDetailsManager.GetBookDetailsById(bookCopy?.BookDetailId);
                    Console.WriteLine("{0,-30}{1,-20}", $"{bookDetail?.BookTitle}", $"|{record?.Date}");
                }
            }
            else
            {
                Console.WriteLine("Reader has no debt");
            }
        }

        Escape();
    }
    private void ShowBorrowingBooksByReader(string lastName, string firstName)
    {
        Reader? reader = _readerManager.GetReaderByName(lastName, firstName);
        if (reader == null)
        {
            Console.WriteLine("Reader was not found");
            return;
        }

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine(reader.LastName + " " + reader.FirstName);
        Console.ResetColor();

        Console.WriteLine("Reader's contacts: " + reader.Phone + ", " + reader.Email);
        List<BookCopy> bookCopies = _bookCopiesManager.GetBooksByReaderId(reader.Id);
        if (bookCopies.Count > 0)
        {
            List<HistoryRecord> historyByReader = _bookCopiesManager.GetHistoryManager.GetHistoryByReader(reader.Id);
            Console.WriteLine();
            Console.WriteLine("{0,-30}{1,-20}", "BookName", $"|Data of Borrowing");
            Console.WriteLine("------------------------------------------------------");

            foreach (BookCopy bookCopy in bookCopies)
            {
                HistoryRecord? record = historyByReader.FindLast(record => record.BookCopyId == bookCopy.BookCopyId);
                BookDetail? bookDetail = _bookDetailsManager.GetBookDetailsById(bookCopy?.BookDetailId);
                Console.WriteLine("{0,-30}{1,-20}", $"{bookDetail?.BookTitle},", $"|{record?.Date}");
            }
        }
        Console.WriteLine("------------------------------------------------------");
        Escape();
    }
    public void SearchWhoBorrowBook()
    {
        string? bookCopyId = null;
        string? readerId;

        string text = "Surch Who Borrow Book";
        int leftMargin = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(leftMargin, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.WriteLine();

        ConsoleKeyInfo key;
        Console.Write("Write book ID: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                bookCopyId += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && bookCopyId.Length > 0)
            {
                bookCopyId = bookCopyId.Substring(0, bookCopyId.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        BookCopy? bookCopy = _bookCopiesManager.GetBookCopyById(bookCopyId);

        if (bookCopy == null)
        {
            Console.WriteLine("\nBook was not found");
        }
        else
        {
            readerId = _bookCopiesManager.FindWhoBorrowBook(bookCopyId);
            if (readerId != null)
            {
                Console.WriteLine();
                Reader? reader = _readerManager.GetReaderById(readerId);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\nAll information about reader");
                Console.ResetColor();
                Console.WriteLine("{0,-10}{1,-30}", "Name: ", $"{reader.LastName} {reader.FirstName}");
                Console.WriteLine("{0,-10}{1,-30}", "Phone: ", $"{reader.Phone}");
                Console.WriteLine("{0,-10}{1,-30}", "Email: ", $"{reader.Email}");
                if(reader is Student student)
                {
                    Console.WriteLine("{0,-10}{1,-30}", "School#:", $"{student.SchoolNumber}");
                }
            }
            else
            {
                Console.WriteLine("\nBook is in library now");
            }
        }
        Escape();
    }
    private static void Escape()
    {
        Console.CursorVisible = false;
        while (true)
        {
            ConsoleKeyInfo key;
            key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
        }
    }
    public bool CheckIfBookAvailable(string bookTitle)
    {
        BookDetail? bookDetail = _bookDetailsManager.GetBookDetailsByName(bookTitle);
        List<BookCopy>? allBookCopies = bookDetail == null ? null : _bookCopiesManager.BookCopiesDictionary[bookDetail.BookId];
        if (allBookCopies == null)
            return false;

        List<BookCopy>? availableBookCopies = new();
        foreach(BookCopy bookCopy in allBookCopies)
        {
            if(bookCopy.ReaderId == "")
            {
                availableBookCopies.Add(bookCopy);
            }
        }

        if (availableBookCopies.Count == 0)
            return false;
        else
            return true;
    }
}
class Program
{
    public static void Main(string[] args)
    {
        int selectedOption = 1;
        Library library = new();

        string[] menuOptions =
        {
            "Management of the reader base",
            "Add new reader to the system",
            "Upgrade reader's contact data",
            "Show information about reader",

            "Management of the book base",
            "Add a copy of an existing book",
            "Delete a copy of an existing book",
            "Add details for new book",
            "Show information about book",
            "Show books by type",

            "Management of the school",
            "Add new school to base",
            "Update school's contacts",
            "Show information about school",

            "Management of the history",
            "Show book copy history",
            "Show reader history",

            "Managment of borrowed books",
            "Show borrowing books by reader",
            "Borrow book copy to reader",
            "Drop book copy from reader",
            "Show borrowing books by school",
            "Show borrowing books by adults",
            "Search who borrow book"
        };

        bool exitOptionMenu = false;
        List<int> skipOptions = new() { 0, 4, 10, 14, 17 };

        while (!exitOptionMenu)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            string text = "Choose function you need:";
            Console.WriteLine(text);
            Console.ResetColor();
            Console.WriteLine();
            Console.CursorVisible = false;

            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedOption && i != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine($"{menuOptions[i]}");
                    Console.ResetColor();
                }
                else if(skipOptions.Contains(i))
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"{menuOptions[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{menuOptions[i]}");
                    Console.ResetColor();
                }
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    do
                    {
                        selectedOption = (selectedOption - 1 + menuOptions.Length) % menuOptions.Length;
                    } 
                    while (skipOptions.Contains(selectedOption));
                    break;
                case ConsoleKey.DownArrow:
                    do
                    {
                        selectedOption = (selectedOption + 1) % menuOptions.Length;
                    } 
                    while (skipOptions.Contains(selectedOption));
                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    Console.CursorVisible = true;
                    HandleOption(selectedOption, library);
                    break;
                default:
                    break;
            }
        }
    }
    private static void HandleOption(int selectedOption, Library library)
    {
        switch (selectedOption)
        {
            case 1:
                library.AddNewReader();
                break;
            case 2:
                library.UpgradeReaderContacts();
                break;
            case 3:
                library.ShowInformationAboutReader();
                break;
            case 5:
                library.AddBookCopy();
                break;
            case 6:
                library.DeleteBookCopy();
                break;
            case 7:
                library.AddDetailsForNewBook();
                break;
            case 8:
                library.ShowInformationAboutBook();
                break;
            case 9:
                library.ShowBooksByType();
                break;
            case 11:
                library.AddNewSchool();
                break;
            case 12:
                library.UpdateSchoolContacts();
                break;
            case 13:
                library.ShowInformationAboutSchool();
                break;
            case 15:
                library.ShowBookCopyHistory();
                break;
            case 16:
                library.ShowReaderHistory();
                break;
            case 18:
                library.ShowBorrowingBooksByReader();
                break;
            case 19:
                library.BorrowBookCopyToReader();
                break;
            case 20:
                library.DropBookCopyFromReader();
                break;
            case 21:
                library.ShowBorrowingBooksBySchool();
                break;
            case 22:
                library.ShowBorrowingBooksByAdults();
                break;
            case 23:
                library.SearchWhoBorrowBook();
                break;
            case 0:
                Environment.Exit(0);              
                break;
            default:
                break;
        }
    }
}