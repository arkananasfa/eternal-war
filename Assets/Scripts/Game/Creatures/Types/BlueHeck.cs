public class BlueHeck : Creature
{

    public BlueHeck() : base()
    {
        HP = 20;
        Armor = 1;
        Resist = 1;
        AttackDistance = 1;
        SetDamage(10, DamageType.Physical, RangeType.Melee, "DaemonsHand");

        Name = "Чёрт-мечник";
        SpriteName = "BlueHeck";
    }

}