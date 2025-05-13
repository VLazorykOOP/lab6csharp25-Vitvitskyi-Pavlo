using System;
using System.Collections.Generic;

namespace PublicationCatalog
{
    // Власний виняток
    public class InvalidPublicationException : Exception
    {
        public InvalidPublicationException(string message) : base(message) { }
    }

    public interface IВидання : ICloneable, IComparable, IDisposable
    {
        void ShowInfo();
        bool IsMatch(string authorSurname);
    }

    public class Книга : IВидання
    {
        public string Назва { get; set; }
        public string Автор { get; set; }
        public int Рік { get; set; }
        public string Видавництво { get; set; }

        public void ShowInfo()
        {
            Console.WriteLine($"[Книга] Назва: {Назва}, Автор: {Автор}, Рік: {Рік}, Видавництво: {Видавництво}");
        }

        public bool IsMatch(string authorSurname)
        {
            return Автор.Equals(authorSurname, StringComparison.OrdinalIgnoreCase);
        }

        public object Clone() => this.MemberwiseClone();

        public int CompareTo(object obj)
        {
            if (obj is Книга other)
                return this.Рік.CompareTo(other.Рік);
            return -1;
        }

        public void Dispose() { }

        // Конструктор з перевіркою
        public Книга(string назва, string автор, int рік, string видавництво)
        {
            if (string.IsNullOrWhiteSpace(назва) || string.IsNullOrWhiteSpace(автор))
                throw new InvalidPublicationException("Назва та автор книги не можуть бути порожніми!");
            Назва = назва;
            Автор = автор;
            Рік = рік;
            Видавництво = видавництво;
        }
    }

    class Program
    {
        static void Main()
        {
            List<IВидання> каталог = new List<IВидання>();

            try
            {
                // Додамо коректну книгу
                каталог.Add(new Книга("Програмування C#", "Іваненко", 2020, "Наука"));

                // Спроба створити некоректну книгу
                каталог.Add(new Книга("", "", 2022, "NoName"));

            }
            catch (InvalidPublicationException ex)
            {
                Console.WriteLine("❌ Користувацька помилка: " + ex.Message);
            }

            try
            {
                Console.WriteLine("\n=== Доступ до елементів каталогу ===");

                for (int i = 0; i <= каталог.Count; i++) // помилка тут
                {
                    каталог[i].ShowInfo();
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("⚠️ Сталася помилка: звернення за межі каталогу.");
            }

            Console.WriteLine("\nПрограма завершилася без фатальних помилок.");
        }
    }
}
