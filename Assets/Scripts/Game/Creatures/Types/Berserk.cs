using System.Collections.Generic;

public class Berserk : Creature
{

    public Berserk() : base()
    {
        HP = 60;
        Armor = 0;
        Resist = 0;
        AttackDistance = 2;
        SetDamage(6, DamageType.Physical, RangeType.Melee, "Ax", 25);
        AttackName = "Ярость берсерка";
        AttackDescription = "Метает столько топоров, сколько у берсерка не хватает десятков здоровья от 80 в начале хода. Топоров в наличии: 0.";

        Name = "Берсерк";
        SpriteName = "Berserk";
    }

    public override void Attack(List<Cage> cages)
    {
        if (CanAttack())
        {
            SendAttackToServer(cages);
            AttackAx();
        }
    }

    private void AttackAx()
    {
        Creature target = FindAttackableEnemy();
        if (target != null)
        {
            GiveDamage(target, Damage, AnimationData.CreateStandartAttackAnimation(this, target).AddAnimationEndEvent(AttackAx));
            AttackPoints--;
            AttackDescription = $"Метает столько топоров, сколько у берсерка не хватает десятков здоровья от 80 в начале хода. Топоров в наличии: {AttackPoints}.";
            UpdateUnitInterface();
        }
    }
    
    public override void PostMoveStart()
    {
        if (AttackPoints >= 1)
            AttackPoints = (80 - (int)System.Math.Ceiling(HP)) / 10;
        AttackDescription = $"Метает столько топоров, сколько у берсерка не хватает десятков здоровья от 80 в начале хода. Топоров в наличии: {AttackPoints}.";
        UpdateUnitInterface();
    }

}