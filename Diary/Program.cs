namespace Diary;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        var directory = Directory.GetCurrentDirectory();
        var path = Path.Combine(directory, "diary.txt");

        if (!File.Exists(path))
            File.Create(path).Dispose();

        Console.Write("Запись: ");
        var text = Console.ReadLine() ?? "";
        var timestamp = DateTime.Now.ToString("dd.MM.yyyy mm:hh");

        File.AppendAllText(path, $"{timestamp} {text}{Environment.NewLine}");
        Console.WriteLine($"Сохранено в {path}");
    }
}
