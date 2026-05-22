using System.Numerics;

namespace GenericOrder;

public interface IIdStep<T> where T : INumber<T>
{
    IPriceStep<T> SetId(int id);
}

public interface IPriceStep<T> where T : INumber<T>
{
    IFinalStep<T> SetBasePrice(T price);
}

public interface IFinalStep<T> where T : INumber<T>
{
    Order<T> Build();
}

public class OrderBuilder<T> : IIdStep<T>, IPriceStep<T>, IFinalStep<T> where T : INumber<T>
{
    private readonly Order<T> order = new();

    public static IIdStep<T> Start() => new OrderBuilder<T>();

    public IPriceStep<T> SetId(int id)
    {
        order.Id = id;
        return this;
    }

    public IFinalStep<T> SetBasePrice(T price)
    {
        order.BasePrice = price;
        order.CurrentPrice = price;
        return this;
    }

    public Order<T> Build() => order;
}
