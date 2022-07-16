using System.Collections.Generic;

public class Druid : Creature
{

    public Druid() : base()
    {
        HP = 24;
        Armor = 0;
        Resist = 4;
        AttackDistance = 3;
        BearCD.CD = 2;
        SetDamage(12, DamageType.Magical, RangeType.Range, "GreenMeteor", 30);

        Name = "Друид";
        SpriteName = "Druid";

        BearInvokeSkill = new ActiveSkill(PreBearInvoke, BearInvoke, "Призыв медведя", "Bear", "Призывает медведя в одну из передних клеток. Характеристики медведя: 60 здоровья, 10 урона, 0 брони и резиста.");
        this.Skills.AddSkill(BearInvokeSkill);
        BearInvokeSkill.CD = BearCD;

    }

    #region BearInvoke
    private ActiveSkill BearInvokeSkill;
    CoolDown BearCD = new CoolDown(1000);
    private void PreBearInvoke()
    {

        CagePack pack = new CagePack();
        Cage addCage = this.Cage;

        addCage = addCage.Front(Team);

        if (addCage != null && !addCage.HasCreature)
            pack.Add(addCage);

        Cage lCage = addCage.Left(Team);
        if (lCage != null && !lCage.HasCreature)
            pack.Add(lCage);

        Cage rCage = addCage.Right(Team);
        if (rCage != null && !rCage.HasCreature)
            pack.Add(rCage);

        MapChooseCages.Get.ActivateL(BearInvokeSkill.EndAction, pack, new List<Cage>());
    }

    private void BearInvoke(List<Cage> cages)
    {
        Creature cr = Team.SpawnCreature(CreatureType.DruidsBear, Cage);
        cr.ReplaceTo(cages[0].X, cages[0].Y);
        Cage.Creature = this;
        BearCD.Update();
    }
    #endregion

}