namespace HeroForge;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var warrior = HeroBuilder.Warrior()
            .Name("Конан")
            .Health(150)
            .Attack(25)
            .WeaponName("Двуручный меч")
            .Build();

        var mage = HeroBuilder.Mage()
            .Name("Гэндальф")
            .Health(80)
            .Attack(15)
            .Mana(200)
            .Build();

        var archer = HeroBuilder.Archer()
            .Name("Леголас")
            .Health(100)
            .Attack(20)
            .ShotDistance(300)
            .Build();

        Console.WriteLine(warrior);
        Console.WriteLine(mage);
        Console.WriteLine(archer);
    }
}
