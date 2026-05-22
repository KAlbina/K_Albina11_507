namespace CustomsControl;

public abstract class Handler<T>
{
    private Handler<T>? next;

    public Handler<T> SetNext(Handler<T> handler)
    {
        next = handler;
        return handler;
    }

    public void Handle(T item)
    {
        if (Check(item))
            next?.Handle(item);
    }

    protected abstract bool Check(T item);
}

public class RadiationHandler : Handler<Cargo>
{
    protected override bool Check(Cargo cargo)
    {
        Console.WriteLine($"[Радиация] Проверяем '{cargo.Name}'...");
        if (cargo.IsRadioactive)
        {
            Console.WriteLine("  Радиоактивный! Цепочка прервана.");
            return false;
        }
        Console.WriteLine("  Чисто.");
        return true;
    }
}

public class ContrabandHandler : Handler<Cargo>
{
    protected override bool Check(Cargo cargo)
    {
        Console.WriteLine($"[Контрабанда] Проверяем '{cargo.Name}'...");
        if (cargo.HasContraband)
        {
            Console.WriteLine("  Контрабанда обнаружена! Цепочка прервана.");
            return false;
        }
        Console.WriteLine("  Чисто. Груз пропущен.");
        return true;
    }
}
