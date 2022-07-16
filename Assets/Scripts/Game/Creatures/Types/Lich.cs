using System.Collections.Generic;
using System;

public class Lich : Creature
{

    public Lich() : base()
    {
        HP = 44;
        Armor = 2;
        Resist = 2;
        AttackDistance = 3;
        SetDamage(16, DamageType.Magical, RangeType.Range, "IceStone", 15);
        AttackName = "Ледянящее прикосновение";
        AttackDescription = "Может наносить атаку также на соседние полосы. При атаке на этом ходу враг полностью теряет броню.";

        Name = "Лич";
        SpriteName = "Lich";

    }

    public override bool CanAttack()
    {
        if (Attacked() || Dead || GetUsableCages().SelectedCages.Count == 0)
            return false;
        else
            return true;
    }

    public override void Attack(List<Cage> cages)
    {
        SendAttackToServer(cages);
        Cage c = cages[0];
        GiveDamage(c.Creature, Damage, AnimationData.CreateStandartAttackAnimation(this, c.Creature).AddAnimationEndEvent(() =>
        {
            if (c.HasCreature)
            {
                Creature cr = c.Creature;
                int wasArmor = cr.Armor;
                Effect.AddNewEffect(c.Creature, new CoolDown(2), "Bane", IconType.BotCage, () =>
                {
                    cr.Armor = 0;
                }).
                AddNewEndEvent(() =>
                {
                    cr.Armor = wasArmor;
                });
            }
        }));
        AttackPoints--;
        UpdateUnitInterface();
    }

    public override void PreAttack()
    {
        MapChooseCages.Get.ActivateL(Attack, GetUsableCages(), new List<Cage>());
    }

    private CagePack GetUsableCages()
    {
        CagePack pack = new CagePack();
        Cage startC = Cage.Front(Team);
        if (startC == null)
            return pack;
        Cage addC = startC;
        for (int i = 0; i < AttackDistance && addC != null; i++)
        {
            if (addC.HasCreature && !OneTeam(addC.Creature))
            {
                pack.Add(addC);
                break;
            }
            addC = addC.Front(Team);
        }
        addC = startC.Left(Team);
        for (int i = 0; i < AttackDistance && addC != null; i++)
        {
            if (addC.HasCreature && !OneTeam(addC.Creature))
            {
                pack.Add(addC);
                break;
            }
            addC = addC.Front(Team);
        }
        addC = startC.Right(Team);
        for (int i = 0; i < AttackDistance && addC != null; i++)
        {
            if (addC.HasCreature && !OneTeam(addC.Creature))
            {
                pack.Add(addC);
                break;
            }
            addC = addC.Front(Team);
        }
        return pack;
    }

}