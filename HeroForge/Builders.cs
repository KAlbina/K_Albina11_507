namespace HeroForge;

public static class HeroBuilder
{
    public static INameStep<IHealthStep<IAttackStep<IWeaponStep>>> Warrior() => new WarriorBuilder();
    public static INameStep<IHealthStep<IAttackStep<IManaStep>>> Mage() => new MageBuilder();
    public static INameStep<IHealthStep<IAttackStep<IShotDistanceStep>>> Archer() => new ArcherBuilder();
}

internal class WarriorBuilder :
    INameStep<IHealthStep<IAttackStep<IWeaponStep>>>,
    IHealthStep<IAttackStep<IWeaponStep>>,
    IAttackStep<IWeaponStep>,
    IWeaponStep,
    IBuildStep<Warrior>
{
    private readonly Warrior hero = new();

    public IHealthStep<IAttackStep<IWeaponStep>> Name(string name) { hero.Name = name; return this; }
    public IAttackStep<IWeaponStep> Health(int hp) { hero.Health = hp; return this; }
    public IWeaponStep Attack(int atk) { hero.Attack = atk; return this; }
    public IBuildStep<Warrior> WeaponName(string name) { hero.WeaponName = name; return this; }
    public Warrior Build() => hero;
}

internal class MageBuilder :
    INameStep<IHealthStep<IAttackStep<IManaStep>>>,
    IHealthStep<IAttackStep<IManaStep>>,
    IAttackStep<IManaStep>,
    IManaStep,
    IBuildStep<Mage>
{
    private readonly Mage hero = new();

    public IHealthStep<IAttackStep<IManaStep>> Name(string name) { hero.Name = name; return this; }
    public IAttackStep<IManaStep> Health(int hp) { hero.Health = hp; return this; }
    public IManaStep Attack(int atk) { hero.Attack = atk; return this; }
    public IBuildStep<Mage> Mana(int mana) { hero.Mana = mana; return this; }
    public Mage Build() => hero;
}

internal class ArcherBuilder :
    INameStep<IHealthStep<IAttackStep<IShotDistanceStep>>>,
    IHealthStep<IAttackStep<IShotDistanceStep>>,
    IAttackStep<IShotDistanceStep>,
    IShotDistanceStep,
    IBuildStep<Archer>
{
    private readonly Archer hero = new();

    public IHealthStep<IAttackStep<IShotDistanceStep>> Name(string name) { hero.Name = name; return this; }
    public IAttackStep<IShotDistanceStep> Health(int hp) { hero.Health = hp; return this; }
    public IShotDistanceStep Attack(int atk) { hero.Attack = atk; return this; }
    public IBuildStep<Archer> ShotDistance(int distance) { hero.ShotDistance = distance; return this; }
    public Archer Build() => hero;
}
