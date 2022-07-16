public class GreenHeck : Creature
{

    public GreenHeck() : base()
    {
        HP = 12;
        Armor = 0;
        Resist = 2;
        AttackDistance = 3;
        SetDamage(10, DamageType.Magical, RangeType.Range, "GreenMeteor", 30);

        Name = "Чёрт-маг";
        SpriteName = "GreenHeck";


        //GiveDamage(this, FriendlyWave, AnimationData.CreateStandartAttackAnimation(this, cr2).ChangeFromCage(Cage.Front(Team)).AddAnimationEndEvent(Wave3).AddAnimationEndEvent(PushCreature));

    }

}