public class AngryRider : Creature
{

    public AngryRider() : base()
    {
        HP = 32;
        Armor = 2;
        Resist = 0;
        AttackDistance = 1;
        SetDamage(16, DamageType.Physical, RangeType.Melee, "Sword");

        Name = "Разъярённый всадник";
        SpriteName = "AngryRider";
    }

}