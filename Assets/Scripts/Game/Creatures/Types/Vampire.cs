using System.Collections.Generic;

public class Vampire : Creature
{

    public Vampire() : base()
    {

        HP = 72;
        Armor = 2;
        Resist = 0;
        AttackDistance = 1;
        SetDamage(12, DamageType.Physical, RangeType.Melee, "Sword", 15);
        AttackName = "Укус вампира";
        AttackDescription = "При атаке востанавливает своё здоровье, в размере умноженого на 2 резиста жертвы и наносит столько же дополнительного урона.";

        Name = "Вампир";
        SpriteName = "Vampire";
    }

    public override void Attack(List<Cage> cages)
    {
        if (CanAttack())
        {
            SendAttackToServer(cages);
            Creature target = FindAttackableEnemy();
            HP += target.Resist * 2;
            Damage dmg = Damage.Copy();
            dmg.Value += target.Resist*2;
            GiveDamage(target, dmg);
            AttackPoints--;
            UpdateUnitInterface();
        }
    }

}