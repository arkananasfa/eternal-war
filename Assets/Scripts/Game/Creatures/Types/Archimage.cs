using System.Collections.Generic;

public class Archimage : Creature
{

    public Archimage() : base()
    {
        HP = 26;
        Armor = 0;
        Resist = 3;
        AttackDistance = 3;
        FireballCD.CD = 2;
        SetDamage(20, DamageType.Magical, RangeType.Range, "Meteor", 30);

        Name = "Архимаг";
        SpriteName = "Archimage";

        FireballSkill = new ActiveSkill(PreFireball, Fireball, "Фаербол", "CreaturesAttacks/Fireball", $"Кидает фаербол на 2-5 клетки вперёд, нанося врагу {fireballMultiplyDamage * 100}% дальнего физического урона. Также наносит {nearFireballMultiplyDamage*100}% урона окружающим его врагам.", true);
        this.Skills.AddSkill(FireballSkill);
        FireballSkill.CD = FireballCD;

    }

    #region Fireball
    private ActiveSkill FireballSkill;
    CoolDown FireballCD = new CoolDown(6);
    private void PreFireball()
    {
        if (!Attacked())
        {
            CagePack pack = new CagePack();
            Cage addCage = this.Cage;

            addCage = addCage.Front(Team, 2);

            int i = 0;
            while (addCage != null && i < 3)
            {
                if (addCage.HasCreature && !OneTeam(addCage.Creature))
                {
                    pack.Add(addCage);
                }
                addCage = addCage.Front(Team);
                i++;
            }

            MapChooseCages.Get.ActivateL(FireballSkill.EndAction, pack, new List<Cage>());
        }
    }

    private Cage attackedCage;
    readonly decimal fireballMultiplyDamage = 1.5m;
    readonly decimal nearFireballMultiplyDamage = 0.4m;
    private void Fireball(List<Cage> cages)
    {
        AttackPoints--;
        attackedCage = cages[0];
        Damage fireballDmg = new Damage(Damage.Value * fireballMultiplyDamage, DamageType.Physical, RangeType.Range, "Fireball", 40);
        Creature cr = cages[0].Creature;
        GiveDamage(cr, fireballDmg, PastAttackEffect);
        FireballCD.Update();
    }

    public override void PastAttackEffect()
    {
        int x = attackedCage.X;
        int y = attackedCage.Y;
        Damage nearFireballDmg = new Damage(Damage.Value * nearFireballMultiplyDamage, DamageType.Physical, RangeType.Range, "Meteor", AnimationOrientation.Full);
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                Cage damagedCage = Cage.CageExist(x + i, y + j);
                if (damagedCage != null && damagedCage.HasCreature && !OneTeam(damagedCage.Creature))
                    GiveDamage(damagedCage.Creature, nearFireballDmg, AnimationData.CreateStandartAttackAnimation(this, damagedCage.Creature).ChangeFromCage(attackedCage));
            }
        }
    }
    #endregion

}