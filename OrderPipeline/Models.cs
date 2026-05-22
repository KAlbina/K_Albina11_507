namespace OrderPipeline;

public class Product
{
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}

public class Receipt
{
    public string ProductName { get; set; } = "";
    public decimal FinalPrice { get; set; }
    public DateTime Date { get; set; }
}
