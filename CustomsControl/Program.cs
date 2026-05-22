namespace CustomsControl;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var radiation = new RadiationHandler();
        var contraband = new ContrabandHandler();
        radiation.SetNext(contraband);

        var cargos = new[]
        {
            new Cargo { Name = "Плюшевый мишка" },
            new Cargo { Name = "Уран-235", IsRadioactive = true },
            new Cargo { Name = "Коробка со шпионским оборудованием", HasContraband = true }
        };

        foreach (var c in cargos)
        {
            Console.WriteLine($"\n=== {c.Name} ===");
            radiation.Handle(c);
        }
    }
}
