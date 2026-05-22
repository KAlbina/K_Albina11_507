namespace LinqTasks;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== LQ1: циклический сдвиг ===");
        var arr = new[] { 1, 2, 3, 4, 5 };
        Console.WriteLine($"  Исходный:    [{string.Join(", ", arr)}]");
        Console.WriteLine($"  Сдвиг на 2:  [{string.Join(", ", Solutions.CycleLeft(arr, 2))}]");
        Console.WriteLine($"  Сдвиг на 7:  [{string.Join(", ", Solutions.CycleLeft(arr, 7))}]");
        Console.WriteLine($"  Сдвиг на -1: [{string.Join(", ", Solutions.CycleLeft(arr, -1))}]");

        Console.WriteLine("\n=== LQ2: 1-окрестность точек ===");
        var s1 = new[] { new Point(0, 0) };
        var n1 = Solutions.Neighborhood(s1).ToList();
        Console.WriteLine($"  S = {{(0,0)}} -> {n1.Count} точек");

        var s2 = new[] { new Point(0, 0), new Point(0, 1) };
        var n2 = Solutions.Neighborhood(s2).ToList();
        Console.WriteLine($"  S = {{(0,0),(0,1)}} -> {n2.Count} точек");

        Console.WriteLine("\n=== LQ3: ни одна буква не повторяется > 2 раз ===");
        var strings = new[] { "abc", "aabbcc", "aaabbb", "hello", "good", "tooo" };
        foreach (var s in Solutions.AtMostTwice(strings))
            Console.WriteLine($"  {s}");

        Console.WriteLine("\n=== LQ4: слова, отличающиеся на одну букву ===");
        var words = new[] { "кот", "кит", "кол", "сом", "сок", "сон", "лес" };
        var wIndex = new WildcardIndex(words);
        foreach (var query in new[] { "кот", "сон" })
            Console.WriteLine($"  '{query}' -> [{string.Join(", ", wIndex.NeighborsOf(query))}]");
    }
}
