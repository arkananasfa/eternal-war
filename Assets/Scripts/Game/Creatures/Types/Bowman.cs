using System.Collections.Generic;

internal class Bowman : Creature
{

    public Bowman() : base()
    {
        HP = 35;
        Armor = 0;
        Resist = 0;
        AttackDistance = 5;
        ArrowsRainCD.CD = 2;
        SetDamage(14, DamageType.Physical, RangeType.Range, "Arrow", 30);

        Name = "Лучник";
        SpriteName = "Bowman";

        // Arrow's rain

        ArrowsRainSkill = new ActiveSkill(PreArrowsRain, ArrowsRain, "Дождь из стрел", "ArrowsRain", "Наносит 50% урона всем существам в область 3х3 с центром в выбраной точке от 3 до 6 клеток впереди.");
        this.Skills.AddSkill(ArrowsRainSkill);
        ArrowsRainSkill.CD = ArrowsRainCD;

        // Arrow's rain

    }

    #region ArrowsRain
    private ActiveSkill ArrowsRainSkill;
    CoolDown ArrowsRainCD = new CoolDown(6);

    private void PreArrowsRain()
    {
        if (!Attacked())
        {
            CagePack pack = new CagePack();
            Cage addCage = this.Cage;

            addCage = addCage.Front(Team, 3);

            int i = 0;
            while (addCage != null & i<3)
            {
                pack.Add(addCage);
                addCage = addCage.Front(Team);
                i++;
            }

            MapChooseCages.Get.ActivateL(ArrowsRainSkill.EndAction, pack, new List<Cage>());
        }
    }

    decimal MultiplyDamage = 0.5m;
    private void ArrowsRain(List<Cage> cages)
    {
        int x = cages[0].X;
        int y = cages[0].Y;
        AttackPoints--;
        Cage damagedCage;
        Damage arrowDmg = new Damage(Damage.Value*MultiplyDamage, DamageType.Physical, RangeType.Range, "Arrow", AnimationOrientation.Full);
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                damagedCage = Cage.CageExist(x + i, y + j);
                if (damagedCage != null && damagedCage.HasCreature)
                {
                    GiveDamage(damagedCage.Creature, arrowDmg);

                }
            }
        }
        ArrowsRainCD.Update();
    }
    #endregion

}