namespace OrderPipeline;

public abstract class Step<TIn, TOut>
{
    public abstract TOut Execute(TIn input);
}

public class WarehouseStep : Step<int, Product>
{
    private readonly Dictionary<int, Product> warehouse = new()
    {
        [1] = new Product { Name = "Молоко",   Price = 89m },
        [2] = new Product { Name = "Хлеб",     Price = 45m },
        [3] = new Product { Name = "Шоколад",  Price = 120m },
    };

    public override Product Execute(int id)
    {
        if (!warehouse.TryGetValue(id, out var product))
            throw new InvalidOperationException($"Товар с id={id} не найден на складе");
        return product;
    }
}

public class PaymentStep : Step<Product, Receipt>
{
    private decimal balance = 1000m;

    public override Receipt Execute(Product product)
    {
        if (balance < product.Price)
            throw new InvalidOperationException($"Недостаточно средств: на счету {balance}, нужно {product.Price}");

        balance -= product.Price;
        return new Receipt
        {
            ProductName = product.Name,
            FinalPrice = product.Price,
            Date = DateTime.Now
        };
    }
}

public class LoggerStep : Step<Receipt, string>
{
    public override string Execute(Receipt receipt)
        => $"Заказ на \"{receipt.ProductName}\" от {receipt.Date:dd.MM.yyyy HH:mm} успешно оплачен.";
}
