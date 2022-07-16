using UnityEngine;
using System.Collections.Generic;

public class ElectricDragon : Creature
{

    public ElectricDragon() : base()
    {

        HP = 80;
        Armor = 2;
        Resist = 3;
        AttackDistance = 1;
        SetDamage(20, DamageType.Magical, RangeType.Melee, "ElectricalField");
        AttackName = "Электрическое поле";
        AttackDescription = "Наносит полный урон юниту спереди и 50% урона вражеским юнитам по соседству и за жертвой.";

        Name = "Электрический дракон";
        SpriteName = "ElectricDragon";

        NearDamage = new Damage(Damage.Value * DamageMultiply, DamageType.Physical, RangeType.Melee, "ElectricalField", 25);
    }

    decimal DamageMultiply = 0.5m;
    Damage NearDamage;
    public override void Attack(List<Cage> cages)
    {
        if (CanAttack())
        {
            SendAttackToServer(cages);
            Creature defender = Cage.Front(Team).Creature;
            GiveDamage(defender, Damage, PastAttackEffect);
            AttackPoints--;
        }
    }

    public override void PastAttackEffect()
    {
        Cage attacked = Cage.Front(Team);

        Cage c = attacked.Front(Team);
        if (c != null && c.HasCreature && !c.Creature.OneTeam(this))
            GiveDamage(c.Creature, NearDamage, AnimationData.CreateStandartAttackAnimation(this, c.Creature).ChangeFromCage(attacked));

        c = attacked.Right(Team);
        if (c != null && c.HasCreature && !c.Creature.OneTeam(this))
            GiveDamage(c.Creature, NearDamage, AnimationData.CreateStandartAttackAnimation(this, c.Creature).ChangeFromCage(attacked));

        c = attacked.Left(Team);
        if (c != null && c.HasCreature && !c.Creature.OneTeam(this))
            GiveDamage(c.Creature, NearDamage, AnimationData.CreateStandartAttackAnimation(this, c.Creature).ChangeFromCage(attacked));

        UpdateUnitInterface();
    }

}