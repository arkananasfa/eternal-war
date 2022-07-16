using System.Collections.Generic;

public class Catapult : Creature
{

    public Catapult() : base()
    {

        HP = 45;
        Armor = 3;
        Resist = 0;
        AttackDistance = 6;
        SetDamage(14, DamageType.Physical, RangeType.Range, "HugeStone", 40);
        AttackName = "Точный расчёт";
        AttackDescription = "Может стрелять по любой клетке на расстаянии от 2 до 6 клеток вперёд. Не может стрелять по клетке перед собой.";

        SpriteName = "Catapult";
        Name = "Катапульта";

    }

    public override bool CanAttack()
    {
        if (Attacked() || Dead)
            return false;
        Cage addC = Cage.Front(Team, 2);
        for (int i = 0; i < 4 && addC != null; i++)
        {
            if (addC.HasCreature && !OneTeam(addC.Creature))
                return true;
            addC = addC.Front(Team);
        }
        return false;
    }

    public override void Attack(List<Cage> cages)
    {
        SendAttackToServer(cages);
        GiveDamage(cages[0].Creature, Damage);
        AttackPoints--;
        UpdateUnitInterface();
    }

    public override void PreAttack()
    {
        CagePack pack = new CagePack();
        Cage addC = Cage;
        addC = addC.Front(Team, 2);
        for (int i = 0; i < 4 && addC != null; i++)
        {
            if (addC.HasCreature && !OneTeam(addC.Creature))
                pack.Add(addC);
            addC = addC.Front(Team);
        }
        MapChooseCages.Get.ActivateL(Attack, pack, new List<Cage>());
    }

}