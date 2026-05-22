using System.Numerics;

namespace GenericOrder;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var decimalOrder = OrderBuilder<decimal>.Start()
            .SetId(1)
            .SetBasePrice(500m)
            .Build();

        var doubleOrder = OrderBuilder<double>.Start()
            .SetId(2)
            .SetBasePrice(250.0)
            .Build();

        Console.WriteLine("=== Order<decimal> (обычная покупка) ===");
        Console.WriteLine(Process(decimalOrder));

        Console.WriteLine("\n=== Order<double> (научные расчёты) ===");
        Console.WriteLine(Process(doubleOrder));
    }

    private static Order<T> Process<T>(Order<T> order) where T : INumber<T>
    {
        var discount = new DiscountHandler<T>(T.CreateChecked(100));
        var tax = new TaxHandler<T>(1.2);
        var validation = new ValidationHandler<T>();

        discount.SetNext(tax).SetNext(validation);
        return discount.Handle(order);
    }
}
