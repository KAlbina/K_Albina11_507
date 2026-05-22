namespace HeroForge;

public abstract class Hero
{
    public string Name { get; set; } = "";
    public int Health { get; set; }
    public int Attack { get; set; }

    public override string ToString() => $"{GetType().Name} {Name}: HP={Health}, ATK={Attack}";
}

public class Warrior : Hero
{
    public string WeaponName { get; set; } = "";
    public override string ToString() => base.ToString() + $", оружие={WeaponName}";
}

public class Mage : Hero
{
    public int Mana { get; set; }
    public override string ToString() => base.ToString() + $", мана={Mana}";
}

public class Archer : Hero
{
    public int ShotDistance { get; set; }
    public override string ToString() => base.ToString() + $", дальность={ShotDistance}";
}
