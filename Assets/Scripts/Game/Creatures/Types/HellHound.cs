using System.Collections.Generic;

public class HellHound : Creature
{

    public HellHound() : base()
    {

        HP = 58;
        Armor = 3;
        Resist = 0;
        AttackDistance = 1;
        SetDamage(18, DamageType.Physical, RangeType.Melee, "DaemonsHand", 10);
        AttackName = "Бешенство";
        AttackDescription = $"С каждой атакой увеличивает свой урон на {HellsFuryBonus}.";

        SpriteName = "HellHound";
        Name = "Адская гончая";

    }
    
    // HELL'S FURY

    private const int HellsFuryBonus = 3;
    public override void Attack(List<Cage> cages)
    {
        if (CanAttack())
        {
            SendAttackToServer(cages);
            StandartAttack();
            Damage.Value += HellsFuryBonus;
            UpdateUnitInterface();
        }
    }

    // HELL'S FURY

}