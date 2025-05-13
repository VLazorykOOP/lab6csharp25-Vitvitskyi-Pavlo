using System;

namespace PersonnelHierarchy
{
    // Інтерфейси
    public interface IShowable
    {
        void Show();
    }

    public interface IPerson : IShowable, ICloneable, IComparable, IDisposable
    {
        string Name { get; set; }
        int Age { get; set; }
    }

    // Базовий клас Worker
    public class Worker : IPerson
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }

        public virtual void Show()
        {
            Console.WriteLine($"Worker: {Name}, Age: {Age}, Department: {Department}");
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public int CompareTo(object obj)
        {
            if (obj is Worker other)
                return this.Age.CompareTo(other.Age);
            return -1;
        }

        public void Dispose()
        {
            // Очистка ресурсів, якщо потрібно
        }
    }

    // Інженер — спеціалізований робітник
    public class Engineer : Worker
    {
        public string Specialization { get; set; }

        public override void Show()
        {
            Console.WriteLine($"Engineer: {Name}, Age: {Age}, Specialization: {Specialization}, Department: {Department}");
        }
    }

    // Кадри — також успадковує Worker
    public class HR : Worker
    {
        public int ManagedEmployees { get; set; }

        public override void Show()
        {
            Console.WriteLine($"HR: {Name}, Age: {Age}, Department: {Department}, Employees Managed: {ManagedEmployees}");
        }
    }

    // Адміністрація
    public class Administrator : Worker
    {
        public string Role { get; set; }

        public override void Show()
        {
            Console.WriteLine($"Administrator: {Name}, Age: {Age}, Role: {Role}, Department: {Department}");
        }
    }

    // Головна програма
    class Program
    {
        static void Main(string[] args)
        {
            Worker w = new Worker { Name = "Іван", Age = 30, Department = "Виробництво" };
            Engineer e = new Engineer { Name = "Олег", Age = 35, Department = "Проектування", Specialization = "Механіка" };
            HR hr = new HR { Name = "Марія", Age = 40, Department = "Кадри", ManagedEmployees = 100 };
            Administrator admin = new Administrator { Name = "Наталія", Age = 45, Department = "Адміністрація", Role = "Директор" };

            w.Show();
            e.Show();
            hr.Show();
            admin.Show();
        }
    }
}
