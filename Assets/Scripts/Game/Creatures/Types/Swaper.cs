using System.Collections.Generic;

internal class Swaper : Creature
{

    public Swaper() : base()
    {
        HP = 46;
        Armor = 0;
        Resist = 0;
        AttackDistance = 2;
        SwapCD.CD = 2;
        SetDamage(6, DamageType.Magical, RangeType.Range, "Meteor", 30);

        Name = "Маг пространства";
        SpriteName = "Swaper";

        SwapSkill = new ActiveSkill(PreSwap1, Swap, "Свап", "Swap", "Меняет двух выбраных существ местами.");
        this.Skills.AddSkill(SwapSkill);
        SwapSkill.CD = SwapCD;

    }

    #region Swap

    private ActiveSkill SwapSkill;
    CoolDown SwapCD = new CoolDown(6);
    private void PreSwap1()
    {
        CagePack pack = new CagePack();
        foreach (Creature c in Team.Creatures.Pack)
            pack.Add(c.Cage);
        MapChooseCages.Get.ActivateL(PreSwap2, pack, new List<Cage>());
    }

    private void PreSwap2(List<Cage> cages)
    {
        Creature target1 = cages[0].Creature;
        CagePack pack = new CagePack();
        foreach (Creature cr in Team.Creatures.Pack)
        {
            if (cr != target1)
                pack.Add(cr.Cage);
        }
        MapChooseCages.Get.ActivateL(SwapSkill.EndAction, pack, cages);
    }

    private void Swap(List<Cage> cages)
    {
        Creature target1 = cages[0].Creature;
        Creature target2 = cages[1].Creature;
        Cage c1 = target1.Cage;
        Cage c2 = target2.Cage;
        target2.ReplaceToWithoutAnimation(c1);
        target1.ReplaceToWithoutAnimation(c2);
        target1.SetToPosition();
        target2.SetToPosition();
        c1.Creature = target2;
        target1.UpdateUnitInterface();
        target2.UpdateUnitInterface();
        SwapCD.Update();
        Interface.Get.UpdateInterface();
    }
    #endregion

}