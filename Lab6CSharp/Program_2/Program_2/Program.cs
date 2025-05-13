using System;
using System.Collections.Generic;

namespace PublicationCatalog
{
    // Інтерфейс видання
    public interface IВидання : ICloneable, IComparable, IDisposable
    {
        void ShowInfo();
        bool IsMatch(string authorSurname);
    }

    // Клас "Книга"
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
    }

    // Клас "Стаття"
    public class Стаття : IВидання
    {
        public string Назва { get; set; }
        public string Автор { get; set; }
        public string Журнал { get; set; }
        public int Номер { get; set; }
        public int Рік { get; set; }

        public void ShowInfo()
        {
            Console.WriteLine($"[Стаття] Назва: {Назва}, Автор: {Автор}, Журнал: {Журнал}, №{Номер}, Рік: {Рік}");
        }

        public bool IsMatch(string authorSurname)
        {
            return Автор.Equals(authorSurname, StringComparison.OrdinalIgnoreCase);
        }

        public object Clone() => this.MemberwiseClone();

        public int CompareTo(object obj)
        {
            if (obj is Стаття other)
                return this.Рік.CompareTo(other.Рік);
            return -1;
        }

        public void Dispose() { }
    }

    // Клас "Електронний ресурс"
    public class ЕлектроннийРесурс : IВидання
    {
        public string Назва { get; set; }
        public string Автор { get; set; }
        public string Посилання { get; set; }
        public string Анотація { get; set; }

        public void ShowInfo()
        {
            Console.WriteLine($"[Е-ресурс] Назва: {Назва}, Автор: {Автор}, Посилання: {Посилання}\nАнотація: {Анотація}");
        }

        public bool IsMatch(string authorSurname)
        {
            return Автор.Equals(authorSurname, StringComparison.OrdinalIgnoreCase);
        }

        public object Clone() => this.MemberwiseClone();

        public int CompareTo(object obj)
        {
            if (obj is ЕлектроннийРесурс other)
                return string.Compare(this.Назва, other.Назва);
            return -1;
        }

        public void Dispose() { }
    }

    // Головна програма
    class Program
    {
        static void Main(string[] args)
        {
            List<IВидання> каталог = new List<IВидання>
            {
                new Книга { Назва = "Програмування на C#", Автор = "Іваненко", Рік = 2020, Видавництво = "Наука" },
                new Стаття { Назва = "Алгоритми сортування", Автор = "Петренко", Журнал = "ІТ та Ми", Номер = 3, Рік = 2021 },
                new ЕлектроннийРесурс { Назва = "Machine Learning", Автор = "Іваненко", Посилання = "http://ml.example.com", Анотація = "Матеріали з машинного навчання" }
            };

            Console.WriteLine("=== Повна інформація з каталогу ===");
            foreach (var item in каталог)
            {
                item.ShowInfo();
                Console.WriteLine();
            }

            Console.Write("Введіть прізвище автора для пошуку: ");
            string пошук = Console.ReadLine();

            Console.WriteLine($"\n=== Результати пошуку за автором '{пошук}' ===");
            bool знайдено = false;
            foreach (var item in каталог)
            {
                if (item.IsMatch(пошук))
                {
                    item.ShowInfo();
                    знайдено = true;
                }
            }

            if (!знайдено)
                Console.WriteLine("Видання не знайдено.");
        }
    }
}
