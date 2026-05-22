using System.Numerics;

namespace GenericOrder;

public abstract class OrderHandler<T> where T : INumber<T>
{
    private OrderHandler<T>? next;

    public OrderHandler<T> SetNext(OrderHandler<T> handler)
    {
        next = handler;
        return handler;
    }

    public Order<T> Handle(Order<T> order)
    {
        var result = Process(order);
        return next is null ? result : next.Handle(result);
    }

    protected abstract Order<T> Process(Order<T> order);
}

public class DiscountHandler<T> : OrderHandler<T> where T : INumber<T>
{
    private readonly T amount;
    public DiscountHandler(T amount) => this.amount = amount;

    protected override Order<T> Process(Order<T> order)
    {
        order.CurrentPrice -= amount;
        Console.WriteLine($"  Скидка -{amount}: цена = {order.CurrentPrice}");
        return order;
    }
}

public class TaxHandler<T> : OrderHandler<T> where T : INumber<T>
{
    private readonly T factor;
    public TaxHandler(double factor) => this.factor = T.CreateChecked(factor);

    protected override Order<T> Process(Order<T> order)
    {
        order.CurrentPrice *= factor;
        Console.WriteLine($"  Налог x{factor}: цена = {order.CurrentPrice}");
        return order;
    }
}

public class ValidationHandler<T> : OrderHandler<T> where T : INumber<T>
{
    protected override Order<T> Process(Order<T> order)
    {
        if (order.CurrentPrice < T.Zero)
            throw new InvalidOperationException($"Итоговая цена отрицательная: {order.CurrentPrice}");
        Console.WriteLine($"  Валидация пройдена: {order.CurrentPrice}");
        return order;
    }
}
