public class DruidsBear : Creature
{

    public DruidsBear() : base()
    {
        HP = 60;
        Armor = 0;
        Resist = 0;
        AttackDistance = 1;
        SetDamage(10, DamageType.Physical, RangeType.Melee, "Sword");

        Name = "Медведь";
        SpriteName = "Bear";
    }

}