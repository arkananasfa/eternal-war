public class Swordsman : Creature
{

    public Swordsman() : base()
    {

        HP = 56;
        
        Armor = 5;
        Resist = 0;
        AttackDistance = 1;
        SetDamage(12, DamageType.Physical, RangeType.Melee, "Sword");

        Name = "Мечник";
        SpriteName = "Swordsman";

        var bigShield = new PassiveSkill("Большой щит", "CreaturesAttacks/Shield", $"Уменьшает полученый урон соседних союзников от физических атак дальнего боя на {ProtectMultiply*100}%.", true);
        Skills.AddSkill(bigShield);
        bigShield.OnBeforeSmbdAttack = BigShield;
        bigShield.CD = BigShieldCD;
    }

    private decimal ProtectMultiply = 0.3m;
    private CoolDown BigShieldCD = new CoolDown(2);
    public Damage BigShield(Creature attacker, Creature defender, Damage preDmg)
    {
        Damage psevdoDmg = new Damage(0, DamageType.Clear, RangeType.Melee, "Shield", 15);
        if (!Silenced && BigShieldCD.CanUse())
        {
            if (isNeighbour(defender) && OneTeam(defender) && !OneTeam(attacker) && preDmg.Range == RangeType.Range && preDmg.Type == DamageType.Physical)
            {
                GiveDamage(defender, psevdoDmg);
                preDmg.Value *= 1-ProtectMultiply;
                BigShieldCD.Update();
            }
        }
        return preDmg;
    }

}