using UnityEngine;
using System.Collections.Generic;

public class Alchemist : Creature
{

    public Alchemist() : base()
    {

        HP = 32;
        Armor = 0;
        Resist = 8;
        AttackDistance = 1;
        SetDamage(6, DamageType.Physical, RangeType.Melee, "AlchemistsPoison", 10);
        AttackName = "Отравление";
        AttackDescription = $"Атаки дополнительно наносят {AttackPoisonMultiply*100}% от максимального здоровья жертвы в виде чистого урона.";

        SpriteName = "Alchemist";
        Name = "Алхимик";

    }


    private const decimal ThrowPoisonMultiply = 0.1m;
    private const decimal AttackPoisonMultiply = 0.08m;
    private const int HellsFuryBonus = 3;
    public override void Attack(List<Cage> cages)
    {
        SendAttackToServer(cages);
        Creature enemy = FindAttackableEnemy();
        if (enemy != null)
        {
            GiveDamage(enemy);
            enemy.GetDamageFull(new Damage(enemy.MaxHP*AttackPoisonMultiply, DamageType.Clear, RangeType.Clear, ""));
            AttackPoints--;
        }
    }

    public void ThrowToCage(Cage c)
    {
        ReplaceToWithoutAnimation(c);
        var anim = MapObject.gameObject.AddComponent<MoveAnimator>();
        anim.OnEndAnimation += CheckBaseDamage;
        anim.OnEndAnimation += Arrive;
        anim.StartAnimation(Map.Get.GetCageLocation(c), 10);
        Cage = c;
    }

    private void Arrive()
    {
        SetToPosition();
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (y == 0 && x == 0)
                    continue;
                Cage c = Cage.CageExist(X + x, Y + y);
                if (c != null && c.HasCreature && !OneTeam(c.Creature))
                {
                    Team.Gold += 2;
                    Creature cr = c.Creature;
                    GiveDamage(c.Creature, new Damage(0, DamageType.Clear, RangeType.Clear, "AlchemistsPoison"), AnimationData.CreateStandartAttackAnimation(this, c.Creature).AddAnimationEndEvent(() => {
                        Effect.AddNewEffect(cr, new CoolDown(6), "CreaturesAttacks/AlchemistsPoison", IconType.BotCage, () => { }, true).AddNewMoveStartEvent(() => {
                            if (cr.CurrentMove())
                            {
                                cr.GetDamageFull(new Damage(cr.MaxHP * ThrowPoisonMultiply, DamageType.Clear, RangeType.Clear, ""));
                            }
                        });
                    }));
                }
            }
        }
    }

}