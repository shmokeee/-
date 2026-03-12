using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleLibrarySystem
{
    // Класс книги
    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public bool IsAvailable { get; set; } = true;

        public override string ToString()
        {
            return $"ID: {Id} | {Title} - {Author} ({Year}) | {(IsAvailable ? "Доступна" : "Выдана")}";
        }
    }

    // Класс читателя
    class Reader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<int> BorrowedBooks { get; set; } = new List<int>();

        public override string ToString()
        {
            return $"ID: {Id} | {Name} - {Email} | Книг на руках: {BorrowedBooks.Count}";
        }
    }

    // Главный класс программы
    class Program
    {
        static List<Book> books = new List<Book>();
        static List<Reader> readers = new List<Reader>();
        static int nextBookId = 1;
        static int nextReaderId = 1;

        static void Main(string[] args)
        {
            // Добавляем тестовые данные
            AddTestData();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ИНФОРМАЦИОННАЯ СИСТЕМА БИБЛИОТЕКИ ===\n");
                Console.WriteLine("1. Управление книгами");
                Console.WriteLine("2. Управление читателями");
                Console.WriteLine("3. Выдача книги");
                Console.WriteLine("4. Возврат книги");
                Console.WriteLine("5. Показать все книги");
                Console.WriteLine("6. Показать всех читателей");
                Console.WriteLine("7. Показать выданные книги");
                Console.WriteLine("0. Выход\n");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ManageBooks(); break;
                    case "2": ManageReaders(); break;
                    case "3": IssueBook(); break;
                    case "4": ReturnBook(); break;
                    case "5": ShowAllBooks(); break;
                    case "6": ShowAllReaders(); break;
                    case "7": ShowBorrowedBooks(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Неверный выбор! Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void AddTestData()
        {
            books.Add(new Book { Id = nextBookId++, Title = "Война и мир", Author = "Лев Толстой", Year = 1869 });
            books.Add(new Book { Id = nextBookId++, Title = "Преступление и наказание", Author = "Федор Достоевский", Year = 1866 });
            books.Add(new Book { Id = nextBookId++, Title = "Мастер и Маргарита", Author = "Михаил Булгаков", Year = 1967 });

            readers.Add(new Reader { Id = nextReaderId++, Name = "Иван Петров", Email = "ivan@mail.com" });
            readers.Add(new Reader { Id = nextReaderId++, Name = "Мария Сидорова", Email = "maria@mail.com" });
        }

        static void ManageBooks()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== УПРАВЛЕНИЕ КНИГАМИ ===\n");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Удалить книгу");
                Console.WriteLine("3. Найти книгу");
                Console.WriteLine("4. Вернуться в главное меню\n");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddBook(); break;
                    case "2": DeleteBook(); break;
                    case "3": FindBook(); break;
                    case "4": return;
                }
            }
        }

        static void AddBook()
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ КНИГИ ===\n");

            Console.Write("Название: ");
            string title = Console.ReadLine();

            Console.Write("Автор: ");
            string author = Console.ReadLine();

            Console.Write("Год издания: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Неверный год! Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }

            books.Add(new Book
            {
                Id = nextBookId++,
                Title = title,
                Author = author,
                Year = year
            });

            Console.WriteLine("\nКнига успешно добавлена!");
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }

        static void DeleteBook()
        {
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ КНИГИ ===\n");
            ShowAllBooks();

            Console.Write("\nВведите ID книги для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID!");
                Console.ReadKey();
                return;
            }

            Book book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine("Книга не найдена!");
                Console.ReadKey();
                return;
            }

            if (!book.IsAvailable)
            {
                Console.WriteLine("Нельзя удалить книгу - она выдана читателю!");
                Console.ReadKey();
                return;
            }

            books.Remove(book);
            Console.WriteLine("Книга удалена!");
            Console.ReadKey();
        }

        static void FindBook()
        {
            Console.Clear();
            Console.WriteLine("=== ПОИСК КНИГИ ===\n");
            Console.Write("Введите название или автора: ");
            string search = Console.ReadLine().ToLower();

            var found = books.Where(b =>
                b.Title.ToLower().Contains(search) ||
                b.Author.ToLower().Contains(search)).ToList();

            Console.WriteLine($"\nНайдено книг: {found.Count}\n");
            foreach (var book in found)
            {
                Console.WriteLine(book);
            }

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        static void ManageReaders()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== УПРАВЛЕНИЕ ЧИТАТЕЛЯМИ ===\n");
                Console.WriteLine("1. Добавить читателя");
                Console.WriteLine("2. Удалить читателя");
                Console.WriteLine("3. Найти читателя");
                Console.WriteLine("4. Вернуться в главное меню\n");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddReader(); break;
                    case "2": DeleteReader(); break;
                    case "3": FindReader(); break;
                    case "4": return;
                }
            }
        }

        static void AddReader()
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ ЧИТАТЕЛЯ ===\n");

            Console.Write("Имя: ");
            string name = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            readers.Add(new Reader
            {
                Id = nextReaderId++,
                Name = name,
                Email = email
            });

            Console.WriteLine("\nЧитатель успешно добавлен!");
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }

        static void DeleteReader()
        {
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ ЧИТАТЕЛЯ ===\n");
            ShowAllReaders();

            Console.Write("\nВведите ID читателя для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID!");
                Console.ReadKey();
                return;
            }

            Reader reader = readers.FirstOrDefault(r => r.Id == id);
            if (reader == null)
            {
                Console.WriteLine("Читатель не найден!");
                Console.ReadKey();
                return;
            }

            if (reader.BorrowedBooks.Count > 0)
            {
                Console.WriteLine("Нельзя удалить читателя - у него есть книги!");
                Console.ReadKey();
                return;
            }

            readers.Remove(reader);
            Console.WriteLine("Читатель удален!");
            Console.ReadKey();
        }

        static void FindReader()
        {
            Console.Clear();
            Console.WriteLine("=== ПОИСК ЧИТАТЕЛЯ ===\n");
            Console.Write("Введите имя или email: ");
            string search = Console.ReadLine().ToLower();

            var found = readers.Where(r =>
                r.Name.ToLower().Contains(search) ||
                r.Email.ToLower().Contains(search)).ToList();

            Console.WriteLine($"\nНайдено читателей: {found.Count}\n");
            foreach (var reader in found)
            {
                Console.WriteLine(reader);
            }

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        static void IssueBook()
        {
            Console.Clear();
            Console.WriteLine("=== ВЫДАЧА КНИГИ ===\n");

            // Показываем доступные книги
            Console.WriteLine("Доступные книги:");
            var availableBooks = books.Where(b => b.IsAvailable).ToList();
            if (availableBooks.Count == 0)
            {
                Console.WriteLine("Нет доступных книг!");
                Console.ReadKey();
                return;
            }

            foreach (var book in availableBooks)
            {
                Console.WriteLine(book);
            }

            Console.Write("\nВведите ID книги: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Неверный ID!");
                Console.ReadKey();
                return;
            }

            Book selectedBook = books.FirstOrDefault(b => b.Id == bookId && b.IsAvailable);
            if (selectedBook == null)
            {
                Console.WriteLine("Книга не найдена или недоступна!");
                Console.ReadKey();
                return;
            }

            // Показываем читателей
            Console.WriteLine("\nЧитатели:");
            foreach (var reader in readers)
            {
                Console.WriteLine(reader);
            }

            Console.Write("\nВведите ID читателя: ");
            if (!int.TryParse(Console.ReadLine(), out int readerId))
            {
                Console.WriteLine("Неверный ID!");
                Console.ReadKey();
                return;
            }

            Reader selectedReader = readers.FirstOrDefault(r => r.Id == readerId);
            if (selectedReader == null)
            {
                Console.WriteLine("Читатель не найден!");
                Console.ReadKey();
                return;
            }

            // Выдаем книгу
            selectedBook.IsAvailable = false;
            selectedReader.BorrowedBooks.Add(selectedBook.Id);

            Console.WriteLine($"\nКнига '{selectedBook.Title}' выдана читателю {selectedReader.Name}");
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }

        static void ReturnBook()
        {
            Console.Clear();
            Console.WriteLine("=== ВОЗВРАТ КНИГИ ===\n");

            // Показываем книги, которые находятся на руках
            Console.WriteLine("Книги на руках:");
            var borrowedBooks = books.Where(b => !b.IsAvailable).ToList();
            if (borrowedBooks.Count == 0)
            {
                Console.WriteLine("Нет выданных книг!");
                Console.ReadKey();
                return;
            }

            foreach (var book in borrowedBooks)
            {
                Reader reader = readers.FirstOrDefault(r => r.BorrowedBooks.Contains(book.Id));
                Console.WriteLine($"{book} - у читателя: {reader?.Name}");
            }

            Console.Write("\nВведите ID книги для возврата: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Неверный ID!");
                Console.ReadKey();
                return;
            }

            Book returnedBook = books.FirstOrDefault(b => b.Id == bookId && !b.IsAvailable);
            if (returnedBook == null)
            {
                Console.WriteLine("Книга не найдена или не была выдана!");
                Console.ReadKey();
                return;
            }

            // Находим читателя и возвращаем книгу
            Reader borrower = readers.FirstOrDefault(r => r.BorrowedBooks.Contains(bookId));
            if (borrower != null)
            {
                borrower.BorrowedBooks.Remove(bookId);
            }

            returnedBook.IsAvailable = true;

            Console.WriteLine($"\nКнига '{returnedBook.Title}' возвращена!");
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }

        static void ShowAllBooks()
        {
            Console.Clear();
            Console.WriteLine("=== ВСЕ КНИГИ ===\n");

            if (books.Count == 0)
            {
                Console.WriteLine("В библиотеке нет книг");
            }
            else
            {
                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
            }

            Console.WriteLine($"\nВсего книг: {books.Count}");
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        static void ShowAllReaders()
        {
            Console.Clear();
            Console.WriteLine("=== ВСЕ ЧИТАТЕЛИ ===\n");

            if (readers.Count == 0)
            {
                Console.WriteLine("Нет зарегистрированных читателей");
            }
            else
            {
                foreach (var reader in readers)
                {
                    Console.WriteLine(reader);
                }
            }

            Console.WriteLine($"\nВсего читателей: {readers.Count}");
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        static void ShowBorrowedBooks()
        {
            Console.Clear();
            Console.WriteLine("=== ВЫДАННЫЕ КНИГИ ===\n");

            var borrowed = books.Where(b => !b.IsAvailable).ToList();

            if (borrowed.Count == 0)
            {
                Console.WriteLine("Нет выданных книг");
            }
            else
            {
                foreach (var book in borrowed)
                {
                    Reader reader = readers.FirstOrDefault(r => r.BorrowedBooks.Contains(book.Id));
                    Console.WriteLine($"{book} -> {reader?.Name}");
                }
            }

            Console.WriteLine($"\nВсего выдано: {borrowed.Count}");
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}