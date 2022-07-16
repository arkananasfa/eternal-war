using System.Collections.Generic;

public class Invoker : Creature
{

    public Invoker() : base()
    {
        HP = 38;
        Armor = 2;
        Resist = 0;
        AttackDistance = 2;
        RedHeckCD.Set(2);
        BlueHeckCD.Set(2);
        GreenHeckCD.Set(2);
        SetDamage(12, DamageType.Magical, RangeType.Range, "Meteor");

        Name = "Призыватель";
        SpriteName = "Invoker";

        RedHeckInvokeSkill = new ActiveSkill(PreRedHeckInvoke, RedHeckInvoke, "Призыв черта-защитника", "RedHeck",  
            "Призывает черта-защитника на одну из передних клеток. Характеристики: 26 здоровья, 8 физического урона и 2 брони. Ставит задержку другим призывам.", true);
        this.Skills.AddSkill(RedHeckInvokeSkill);
        RedHeckInvokeSkill.CD = RedHeckCD;
        BlueHeckInvokeSkill = new ActiveSkill(PreBlueHeckInvoke, BlueHeckInvoke, "Призыв черта-мечника", "BlueHeck", 
            "Призывает черта-мечника на одну из соседних клеток. Характеристики: 20 здоровья, 10 физического урона и 1 брони и 1 резист. Ставит задержку другим призывам.", true);
        this.Skills.AddSkill(BlueHeckInvokeSkill);
        BlueHeckInvokeSkill.CD = BlueHeckCD;
        GreenHeckInvokeSkill = new ActiveSkill(PreGreenHeckInvoke, GreenHeckInvoke, "Призыв черта-мага", "GreenHeck",
            "Призывает черта-мага на одну из задних клеток. Характеристики: 12 здоровья, 10 магического урона, 2 резиста и 3 дальности. Ставит задержку другим призывам.", true);
        this.Skills.AddSkill(GreenHeckInvokeSkill);
        GreenHeckInvokeSkill.CD = GreenHeckCD;

    }

    private ActiveSkill RedHeckInvokeSkill;
    private ActiveSkill BlueHeckInvokeSkill;
    private ActiveSkill GreenHeckInvokeSkill;

    #region Red heck
    CoolDown RedHeckCD = new CoolDown(4);
    private void PreRedHeckInvoke()
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

        MapChooseCages.Get.ActivateL(RedHeckInvokeSkill.EndAction, pack, new List<Cage>());
    }

    private void RedHeckInvoke(List<Cage> cages)
    {
        Creature cr = Team.SpawnCreature(CreatureType.RedHeck, Cage);
        cr.ReplaceTo(cages[0].X, cages[0].Y);
        PastInvoke(cr);
    }
    #endregion

    #region Blue heck
    CoolDown BlueHeckCD = new CoolDown(4);
    private void PreBlueHeckInvoke()
    {

        CagePack pack = new CagePack();

        Cage lCage = Cage.Left(Team);
        if (lCage != null && !lCage.HasCreature)
            pack.Add(lCage);

        Cage rCage = Cage.Right(Team);
        if (rCage != null && !rCage.HasCreature)
            pack.Add(rCage);

        MapChooseCages.Get.ActivateL(BlueHeckInvokeSkill.EndAction, pack, new List<Cage>());
    }

    private void BlueHeckInvoke(List<Cage> cages)
    {
        Creature cr = Team.SpawnCreature(CreatureType.BlueHeck, Cage);
        cr.ReplaceTo(cages[0].X, cages[0].Y);
        PastInvoke(cr);
    }
    #endregion

    #region Green heck
    CoolDown GreenHeckCD = new CoolDown(4);
    private void PreGreenHeckInvoke()
    {
        CagePack pack = new CagePack();
        Cage addCage = this.Cage;

        addCage = addCage.Back(Team);

        if (addCage != null)
        {

            if (addCage != null && !addCage.HasCreature)
                pack.Add(addCage);

            Cage lCage = addCage.Left(Team);
            if (lCage != null && !lCage.HasCreature)
                pack.Add(lCage);

            Cage rCage = addCage.Right(Team);
            if (rCage != null && !rCage.HasCreature)
                pack.Add(rCage);

            MapChooseCages.Get.ActivateL(GreenHeckInvokeSkill.EndAction, pack, new List<Cage>());
        }
    }

    private void GreenHeckInvoke(List<Cage> cages)
    {
        Creature cr = Team.SpawnCreature(CreatureType.GreenHeck, Cage);
        cr.ReplaceTo(cages[0].X, cages[0].Y);
        PastInvoke(cr);
    }
    #endregion

    private void PastInvoke(Creature cr)
    {
        cr.AttackPoints = 1;
        cr.MovePoints = 1;
        Cage.Creature = this;
        RedHeckCD.Update();
        BlueHeckCD.Update();
        GreenHeckCD.Update();
    }

}