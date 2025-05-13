using System;
using System.Collections;
using System.Collections.Generic;

class Rectangle : IEnumerable<int>
{
    protected int a, b;
    protected int color;

    public Rectangle(int a, int b, int color)
    {
        this.a = a;
        this.b = b;
        this.color = color;
    }

    public void PrintSides()
    {
        Console.WriteLine($"Сторони: {a} x {b}");
    }

    public int GetPerimeter() => 2 * (a + b);
    public int GetArea() => a * b;
    public bool IsSquare() => a == b;

    public int A { get => a; set => a = value; }
    public int B { get => b; set => b = value; }
    public int Color { get => color; }

    public int this[int index]
    {
        get
        {
            return index switch
            {
                0 => a,
                1 => b,
                2 => color,
                _ => throw new IndexOutOfRangeException("Індекс має бути 0, 1 або 2.")
            };
        }
        set
        {
            switch (index)
            {
                case 0: a = value; break;
                case 1: b = value; break;
                case 2: color = value; break;
                default: throw new IndexOutOfRangeException("Індекс має бути 0, 1 або 2.");
            }
        }
    }

    public static Rectangle operator ++(Rectangle r)
    {
        r.a++;
        r.b++;
        return r;
    }

    public static Rectangle operator --(Rectangle r)
    {
        r.a--;
        r.b--;
        return r;
    }

    public static bool operator true(Rectangle r) => r.IsSquare();
    public static bool operator false(Rectangle r) => !r.IsSquare();

    public static Rectangle operator *(Rectangle r, int scalar)
    {
        return new Rectangle(r.a * scalar, r.b * scalar, r.color);
    }

    public static implicit operator string(Rectangle r)
    {
        return $"{r.a} {r.b} {r.color}";
    }

    public static explicit operator Rectangle(string s)
    {
        var parts = s.Split(' ');
        if (parts.Length != 3)
            throw new FormatException("Неправильний формат рядка. Очікується: 'a b color'");

        int a = int.Parse(parts[0]);
        int b = int.Parse(parts[1]);
        int color = int.Parse(parts[2]);

        return new Rectangle(a, b, color);
    }

    // Реалізація IEnumerable<int> для foreach
    public IEnumerator<int> GetEnumerator()
    {
        yield return a;
        yield return b;
        yield return color;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введіть кількість прямокутників: ");
        int n = int.Parse(Console.ReadLine());

        Rectangle[] rectangles = new Rectangle[n];
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nВведіть дані для прямокутника {i + 1}:");
            Console.Write("Сторона a: ");
            int a = int.Parse(Console.ReadLine());
            Console.Write("Сторона b: ");
            int b = int.Parse(Console.ReadLine());
            Console.Write("Колір (числовий код): ");
            int color = int.Parse(Console.ReadLine());

            rectangles[i] = new Rectangle(a, b, color);
        }

        int squareCount = 0;
        foreach (Rectangle rect in rectangles)
        {
            rect.PrintSides();
            Console.WriteLine($"Периметр: {rect.GetPerimeter()}");
            Console.WriteLine($"Площа: {rect.GetArea()}");
            Console.WriteLine($"Колір: {rect.Color}");

            if (rect)
            {
                Console.WriteLine("Це квадрат!");
                squareCount++;
            }

            Console.WriteLine("Властивості через foreach:");
            foreach (int value in rect)
                Console.WriteLine($"  Значення: {value}");

            Console.WriteLine("----------------");
        }

        Console.WriteLine($"\nКількість квадратів: {squareCount}");

        // Демонстрація нових можливостей
        Console.WriteLine("\n--- Демонстрація нових можливостей ---");
        Rectangle demo = new Rectangle(4, 4, 7);

        Console.WriteLine("Доступ через індексатор:");
        Console.WriteLine($"a = {demo[0]}, b = {demo[1]}, color = {demo[2]}");

        Console.WriteLine("Оператор ++:");
        ++demo;
        Console.WriteLine($"Після ++: a = {demo.A}, b = {demo.B}");

        Console.WriteLine("Оператор * (масштабування на 2):");
        Rectangle scaled = demo * 2;
        Console.WriteLine($"Масштабований: a = {scaled.A}, b = {scaled.B}");

        Console.WriteLine("Перетворення в рядок:");
        string str = demo;
        Console.WriteLine($"Рядок: {str}");

        Console.WriteLine("Перетворення з рядка:");
        Rectangle fromStr = (Rectangle)"5 8 3";
        Console.WriteLine($"З рядка: a = {fromStr.A}, b = {fromStr.B}, color = {fromStr.Color}");

        Console.WriteLine("\nПеребір властивостей через foreach:");
        foreach (int val in fromStr)
        {
            Console.WriteLine($"  {val}");
        }
    }
}
