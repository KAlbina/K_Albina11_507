namespace GCGenerations;

public class HeavyObject
{
    private readonly byte[] data = new byte[100 * 1024];
}

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var heavy = new HeavyObject();
        Console.WriteLine($"Сразу после создания:");
        Console.WriteLine($"  поколение: {GC.GetGeneration(heavy)}");
        Console.WriteLine($"  память в куче: {GC.GetTotalMemory(false):N0} байт");

        GC.Collect();
        Console.WriteLine($"\nПосле GC.Collect():");
        Console.WriteLine($"  поколение: {GC.GetGeneration(heavy)}");
        Console.WriteLine($"  память в куче: {GC.GetTotalMemory(false):N0} байт");

        for (int i = 0; i < 100_000; i++)
            _ = new object();

        Console.WriteLine($"\nПосле создания 100 000 короткоживущих объектов:");
        Console.WriteLine($"  поколение: {GC.GetGeneration(heavy)}");
        Console.WriteLine($"  память в куче: {GC.GetTotalMemory(false):N0} байт");

        Console.WriteLine("\nОбъект переехал в старшее поколение, потому что давление на Gen0 заставило среду");
        Console.WriteLine("автоматически запустить сборку — пережившие её объекты повышают своё поколение.");
    }
}
