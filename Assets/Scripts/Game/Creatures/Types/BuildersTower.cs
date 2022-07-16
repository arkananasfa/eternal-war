using System.Collections.Generic;

public class BuildersTower : Creature
{

    public BuildersTower() : base()
    {
        HP = 60;
        Armor = 0;
        Resist = 0;
        AttackDistance = 2;
        Movable = false;
        SetDamage(18, DamageType.Physical, RangeType.Range, "Arrow");
        Damage.AnimOrientation = AnimationOrientation.Full;

        Name = "Башня";
        SpriteName = "Tower";

        PassUnitSkill = new ActiveSkill(PassUnit, "Пропуск", "TowerPass", "Пропускает юнит сзади на следующую клетку, если у него есть очки перемещения. После этого юнит не больше не может перемещатся и атаковать в этом ходу.");
        PassUnitSkill.CanUseAdditional += PassUnitCanUse;
        Skills.AddSkill(PassUnitSkill);
        PassUnitSkill.CD = PassCD;
    }

    #region Unique Attack
    public override bool CanAttack()
    {
        if (AttackPoints == 0 || Dead)
            return false;
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                Cage c = Cage.CageExist(X + x, Y + y);
                if (c != null && c.HasCreature && !OneTeam(c.Creature))
                    return true;
            }
        }
        return false;
    }
    public override void Attack(List<Cage> cages)
    {
        SendAttackToServer(cages);
        GiveDamage(cages[0].Creature);
        AttackPoints--;
    }
    public override void PreAttack()
    {
        CagePack pack = new CagePack();
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                Cage c = Cage.CageExist(X + x, Y + y);
                if (c != null && c.HasCreature && !OneTeam(c.Creature))
                    pack.Add(c);
            }
        }
        MapChooseCages.Get.ActivateL(Attack, pack, new List<Cage>());
    }
    #endregion

    #region Pass unit
    private ActiveSkill PassUnitSkill;
    private CoolDown PassCD = new CoolDown(2);
    private void PassUnit()
    {
        Cage back = Cage.Back(Team);
        Cage front = Cage.Front(Team);
        if (back.HasCreature && OneTeam(back.Creature) && !front.HasCreature && back.Creature.Movable)
        {
            back.Creature.ReplaceTo(front);
            front.Creature.AttackPoints = 0;
            front.Creature.MovePoints = 0;
        }
    }
    private bool PassUnitCanUse ()
    {
        Cage back = Cage.Back(Team);
        Cage front = Cage.Front(Team);
        return back.HasCreature && OneTeam(back.Creature) && !front.HasCreature && back.Creature.Movable;
    }
    #endregion

}