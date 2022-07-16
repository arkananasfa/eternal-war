using UnityEngine;

public class Rider : Creature
{

    public Rider() : base()
    {
        HP = 42;
        Armor = 0;
        Resist = 0;
        AttackDistance = 1;
        SetDamage(14, DamageType.Physical, RangeType.Melee, "Sword", 10);

        Name = "Всадник";
        SpriteName = "Rider";

        GallopSkill = new ActiveSkill(Gallop, "Галоп", "Gallop", "Добавляет одну единицу перемещения.");
        this.Skills.AddSkill(GallopSkill);
        GallopSkill.CD = GallopCD;

        var passiveSkill = new PassiveSkill("Прыжок", "AngryRider", "После смерти коня, спрыгивает с него на соседнюю клетку с приоритетом на правую (с точки взгляда существа). Характеристики: 32 здоровья, 16 урона, 2 брони.", true);
        Skills.AddSkill(passiveSkill);

    }

    #region Gallop
    private ActiveSkill GallopSkill;
    CoolDown GallopCD = new CoolDown(2);
    private void Gallop()
    {
        MovePoints++;
        GallopCD.Set(2);
        Interface.Get.UpdateInterface();
    }
    #endregion

    #region Unique death
    public override void Death()
    {
        if (!Dead)
        {
            base.Death();
            Cage c = Cage.Right(Team);
            if (c != null && !c.HasCreature)
            {
                Creature cr = Team.SpawnCreature(CreatureType.AngryRider, Cage);
                cr.ReplaceTo(c);
            }
            else
            {
                c = Cage.Left(Team);
                if (c != null && !c.HasCreature)
                {
                    Creature cr = Team.SpawnCreature(CreatureType.AngryRider, Cage);
                    cr.ReplaceTo(c);
                }
            }
        }
    }
    #endregion

}