namespace OrderPipeline;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var pipeline = Pipeline.Start(new WarehouseStep())
            .Then(new PaymentStep())
            .Then(new LoggerStep());

        var ids = new[] { 1, 2, 3 };
        foreach (var id in ids)
        {
            try
            {
                Console.WriteLine(pipeline.Run(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
