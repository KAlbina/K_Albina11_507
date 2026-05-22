namespace HeroForge;

public interface INameStep<TNext>
{
    TNext Name(string name);
}

public interface IHealthStep<TNext>
{
    TNext Health(int hp);
}

public interface IAttackStep<TNext>
{
    TNext Attack(int atk);
}

public interface IBuildStep<T>
{
    T Build();
}

public interface IWeaponStep
{
    IBuildStep<Warrior> WeaponName(string name);
}

public interface IManaStep
{
    IBuildStep<Mage> Mana(int mana);
}

public interface IShotDistanceStep
{
    IBuildStep<Archer> ShotDistance(int distance);
}
