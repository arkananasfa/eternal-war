public class RedHeck : Creature
{

    public RedHeck() : base()
    {
        HP = 26;
        Armor = 2;
        Resist = 0;
        AttackDistance = 1;
        SetDamage(8, DamageType.Physical, RangeType.Melee, "Ax", 15);

        Name = "Чёрт-секирщик";
        SpriteName = "RedHeck";
    }

}