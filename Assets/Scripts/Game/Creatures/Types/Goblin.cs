public class Goblin : Creature
{

    public Goblin() : base()
    {
        HP = 32;
        Armor = 0;
        Resist = 0;
        AttackDistance = 2;
        SetDamage(18, DamageType.Physical, RangeType.Melee, "Sword");

        Name = "Гоблин";
        SpriteName = "Goblin";
    }

}