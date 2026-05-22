using System.Numerics;

namespace GenericOrder;

public class Order<T> where T : INumber<T>
{
    public int Id { get; set; }
    public T BasePrice { get; set; } = T.Zero;
    public T CurrentPrice { get; set; } = T.Zero;

    public override string ToString() => $"Заказ #{Id}: базовая {BasePrice}, итог {CurrentPrice}";
}
