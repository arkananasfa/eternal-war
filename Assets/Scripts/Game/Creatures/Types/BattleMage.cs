public class BattleMage : Creature
{

    public BattleMage() : base()
    {

        HP = 50;
        Armor = 4;
        Resist = 1;
        AttackDistance = 1;
        SetDamage(16, DamageType.Magical, RangeType.Melee, "Meteor", 25);

        Name = "Боевой маг";
        SpriteName = "BattleMage";
    }

}