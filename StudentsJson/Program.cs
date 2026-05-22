using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace StudentsJson;

public class Program
{
    private static readonly string Path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "students.json");

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    private static List<Student> students = new();

    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        Load();
        if (students.Count == 0)
        {
            students.Add(new Student { Name = "Иван Петров",  Age = 19, Group = "ИВТ-101" });
            students.Add(new Student { Name = "Мария Сидорова", Age = 20, Group = "ИВТ-101" });
            students.Add(new Student { Name = "Пётр Иванов",  Age = 18, Group = "ИВТ-102" });
            Save();
        }

        Print();

        while (true)
        {
            Console.WriteLine("\n1 - список   2 - добавить   3 - удалить   0 - выход");
            Console.Write("> ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1": Print(); break;
                case "2": Add(); break;
                case "3": Delete(); break;
                case "0": return;
                default:  Console.WriteLine("Неизвестная команда"); break;
            }
        }
    }

    private static void Load()
    {
        if (!File.Exists(Path))
        {
            File.WriteAllText(Path, "[]");
            return;
        }
        var json = File.ReadAllText(Path);
        students = JsonSerializer.Deserialize<List<Student>>(json) ?? new();
    }

    private static void Save()
    {
        File.WriteAllText(Path, JsonSerializer.Serialize(students, JsonOptions));
    }

    private static void Print()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("Список пуст.");
            return;
        }
        Console.WriteLine("Студенты:");
        for (int i = 0; i < students.Count; i++)
            Console.WriteLine($"  [{i}] {students[i]}");
    }

    private static void Add()
    {
        Console.Write("Имя: ");
        var name = Console.ReadLine() ?? "";
        Console.Write("Возраст: ");
        int.TryParse(Console.ReadLine(), out var age);
        Console.Write("Группа: ");
        var group = Console.ReadLine() ?? "";

        students.Add(new Student { Name = name, Age = age, Group = group });
        Save();
        Console.WriteLine("Добавлен.");
    }

    private static void Delete()
    {
        Print();
        Console.Write("Номер для удаления: ");
        if (int.TryParse(Console.ReadLine(), out var index) && index >= 0 && index < students.Count)
        {
            var removed = students[index];
            students.RemoveAt(index);
            Save();
            Console.WriteLine($"Удалён: {removed}");
        }
        else
        {
            Console.WriteLine("Некорректный номер.");
        }
    }
}
