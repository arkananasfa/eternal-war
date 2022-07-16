using System.Collections.Generic;

public class Lancer : Creature
{

    public Lancer() : base()
    {
        HP = 50;
        Armor = 1;
        Resist = 2;
        AttackDistance = 3;
        SetDamage(14, DamageType.Physical, RangeType.Range, "Spear", 35);
        AttackName = "Боевой ритуал";
        AttackDescription = $"Ранит все цели на {Range} передних клетках с каждой целью уменьшая урон на {DamageReduce}.";

        Name = "Копейщик";
        SpriteName = "Lancer";

    }


    // Attack

    int Range = 3;
    int DamageReduce = 5;
    public override void Attack(List<Cage> cages)
    {
        SendAttackToServer(cages);
        int reduce = 0;
        if (CanAttack())
        {
            for (int i = 1; i <= 3; i++)
            {
                Cage c = Cage.Front(Team, i);
                if (c!=null && c.HasCreature && !OneTeam(c.Creature))
                {
                    Damage dmg = new Damage(Damage.Value - reduce, DamageType.Physical, RangeType.Melee, "Spear");
                    GiveDamage(c.Creature, dmg);
                    reduce += DamageReduce;
                }
            }
            AttackPoints--;
            UpdateUnitInterface();
        }
    }

    // Attack

}