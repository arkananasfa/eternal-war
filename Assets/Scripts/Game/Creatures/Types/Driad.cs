using System.Collections.Generic;

public class Driad : Creature
{

    public Driad() : base()
    {
        HP = 40;
        Armor = 0;
        Resist = 6;
        AttackDistance = 1;
        SetDamage(12, DamageType.Magical, RangeType.Melee, "FairiesDust");
        Damage.AnimOrientation = AnimationOrientation.No;
        AttackName = "Помощь леса";
        AttackDescription = "Наносит полный урон юниту спереди и 30% дальнего магического урона остальным юнитам врага.";

        Name = "Дриада";
        SpriteName = "Driad";
    }

    // Attack

    decimal DamageMultiply = 0.3m;
    public override void Attack(List<Cage> cages)
    {
        SendAttackToServer(cages);
        if (CanAttack())
        {
            if (!Silenced)
            {
                GiveDamage(Cage.Front(Team).Creature, Damage, PastAttackEffect);
                AttackPoints--;
            }
            else
                StandartAttack();

        }
    }

    public override void PastAttackEffect()
    {
        Cage attacked = Cage.Front(Team);
        Creature[] EnemisCreatures = Team.E.Creatures.Pack.ToArray();
        Damage dmg = new Damage(Damage.Value * DamageMultiply, DamageType.Magical, RangeType.Range, "FairiesDust", 30, AnimationOrientation.No);
        foreach (Creature c in EnemisCreatures)
        {
            if (c == attacked.Creature)
                continue;
            GiveDamage(c, dmg, AnimationData.CreateStandartAttackAnimation(this, c).ChangeFromCage(attacked));
        }
        UpdateUnitInterface();
    }
    // Attack

}