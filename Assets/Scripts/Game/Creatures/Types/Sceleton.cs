public class Sceleton : Creature
{

    public Sceleton() : base()
    {
        HP = 24;
        Armor = 2;
        Resist = 0;
        AttackDistance = 1;
        SetDamage(8, DamageType.Physical, RangeType.Melee, "Sword", 10);

        Name = "Скелет";
        SpriteName = "Sceleton";
    }

}