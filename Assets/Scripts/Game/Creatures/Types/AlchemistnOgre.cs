using UnityEngine;
using System.Collections.Generic;

public class AlchemistnOgre : Creature
{

    public AlchemistnOgre() : base()
    {
        HP = 90;
        Armor = 2;
        Resist = 0;
        AttackDistance = 1;
        ActionCD.CD = 2;
        SetDamage(20, DamageType.Physical, RangeType.Melee, "Ax", 30);

        Name = "Алхимик на огре";
        SpriteName = "AlchemistnOgre";

        ThrowSkill = new ActiveSkill(PreThrow, Throw, "Запуск", "ThrowAlchemist", $"Кидает алхимика на 2-3 клетки вперёд, получая за каждого врага, вокруг алхимика по 2 монеты и отравляя этих же врагов на 3 хода. Каждый ход враги теряют по 10% от своего максимального здоровья");
        Skills.AddSkill(ThrowSkill);
        ThrowSkill.CD = ActionCD;
    }

    private ActiveSkill ThrowSkill;
    #region Throw
    CoolDown ActionCD = new CoolDown(500);
    //Throw
    private void PreThrow()
    {
        CagePack pack = new CagePack();
        Cage addCage = this.Cage;

        addCage = addCage.Front(Team, 2);

        if (addCage != null && !addCage.HasCreature)
        {
            pack.Add(addCage);
        }

        addCage = addCage.Front(Team);

        if (addCage != null && !addCage.HasCreature)
        {
            pack.Add(addCage);
        }

        MapChooseCages.Get.ActivateL(ThrowSkill.EndAction, pack, new List<Cage>());
    }

    private void Throw(List<Cage> cages)
    {
        SetSprite("Ogre");
        Name = "Огр";
        Creature alchemist = Team.SpawnCreature(CreatureType.Alchemist, Cage);
        (alchemist as Alchemist).ThrowToCage(cages[0]);
        Cage.Creature = this;
        Skills.RemoveAt(0);
        Skills.ActiveSkills.RemoveAt(0);
        Interface.Get.UpdateInterface();
    }
    #endregion
}