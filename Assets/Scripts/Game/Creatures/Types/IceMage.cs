using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceMage : Creature
{

    public IceMage() : base()
    {
        HP = 32;
        Armor = 0;
        Resist = 2;
        AttackDistance = 3;
        FrozeCD.CD = 2;
        SetDamage(15, DamageType.Magical, RangeType.Range, "BlueMeteor", 30);

        Name = "Ледяной маг";
        SpriteName = "IceMage";

        FrozeSkill = new ActiveSkill(PreFroze, Froze, "Обморожение", "Froze", "Замораживает существо на один ход. Целевое существо не может двигатся, атаковать и применять большинство способностей.");
        this.Skills.AddSkill(FrozeSkill);
        FrozeSkill.CD = FrozeCD;

    }

    #region Frozing
    private ActiveSkill FrozeSkill;
    CoolDown FrozeCD = new CoolDown(4);
    private void PreFroze()
    {

        CagePack pack = new CagePack();
        Cage addCage = this.Cage;

        addCage = addCage.Front(Team);

        if (addCage != null && addCage.HasCreature && !OneTeam(addCage.Creature))
            pack.Add(addCage);

        Cage lCage = addCage.Left(Team);
        if (lCage != null && lCage.HasCreature && !OneTeam(lCage.Creature))
            pack.Add(lCage);

        Cage rCage = addCage.Right(Team);
        if (rCage != null && rCage.HasCreature && !OneTeam(rCage.Creature))
            pack.Add(rCage);

        addCage = addCage.Front(Team);

        if (addCage != null)
        {

            if (addCage.HasCreature && !OneTeam(addCage.Creature))
                pack.Add(addCage);

            lCage = addCage.Left(Team);
            if (lCage != null && lCage.HasCreature && !OneTeam(lCage.Creature))
                pack.Add(lCage);

            rCage = addCage.Right(Team);
            if (rCage != null && rCage.HasCreature && !OneTeam(rCage.Creature))
                pack.Add(rCage);
        }

        MapChooseCages.Get.ActivateL(FrozeSkill.EndAction, pack, new List<Cage>());
    }

    private void Froze(List<Cage> cages)
    {
        Cage c = cages[0];
        Damage Froze = new Damage(0, DamageType.Clear, RangeType.Clear, "Froze", 30, AnimationOrientation.Full);
        GiveDamage(c.Creature, Froze, AnimationData.CreateStandartAttackAnimation(this, c.Creature).AddAnimationEndEvent(() =>
        {

            if (!c.HasCreature)
                return;
            Creature cr = c.Creature;
            Effect.AddNewEffect(cr, new CoolDown(2), "Froze", IconType.BotCage, () => {
                cr.Silence = 2;
            }).AddNewMoveStartEvent(() => {
                if (cr.CurrentMove())
                {
                    cr.AttackPoints = 0;
                    cr.MovePoints = 0;
                }
            });

            FrozeCD.Update();
        }));        
    }
    #endregion

}