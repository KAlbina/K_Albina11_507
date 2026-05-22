namespace GhostTrap;

public class Publisher
{
    public event Action<string>? OnDataReceived;
    public void Emit(string data) => OnDataReceived?.Invoke(data);
}

public class Subscriber : IDisposable
{
    private readonly Publisher publisher;

    public Subscriber(Publisher publisher)
    {
        this.publisher = publisher;
        publisher.OnDataReceived += Handle;
    }

    private void Handle(string data) => Console.WriteLine($"  Subscriber получил: {data}");

    ~Subscriber()
    {
        Console.WriteLine("  Объект Subscriber удалён из памяти");
    }

    public void Dispose()
    {
        publisher.OnDataReceived -= Handle;
        GC.SuppressFinalize(this);
    }
}

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Без Dispose: утечка через событие ===");
        var pub1 = new Publisher();
        CreateOrphan(pub1);

        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("После GC.Collect + WaitForPendingFinalizers:");
        Console.WriteLine("  финализатор не вызвался — подписчик жив.");
        Console.WriteLine("  Событие Publisher.OnDataReceived держит делегат, который ссылается на Subscriber,");
        Console.WriteLine("  поэтому GC не считает объект мусором.");
        Console.WriteLine("  Доказательство — публикуем сообщение, подписчик его поймает:");
        pub1.Emit("сообщение 1");

        Console.WriteLine("\n=== С using/Dispose: подписчик корректно отписывается ===");
        var pub2 = new Publisher();
        using (var sub = new Subscriber(pub2))
        {
            pub2.Emit("сообщение внутри using");
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("После using блок подписчик отписан от события.");
        Console.WriteLine("Финализатор не вызывается (SuppressFinalize в Dispose), но это и не нужно:");
        Console.WriteLine("объект больше никому не нужен и будет собран обычным GC.");
        pub2.Emit("сообщение после using — никто его не ловит");
    }

    private static void CreateOrphan(Publisher publisher)
    {
        _ = new Subscriber(publisher);
    }
}
