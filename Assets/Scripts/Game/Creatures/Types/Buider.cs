using UnityEngine;
using System.Collections.Generic;

public class Builder : Creature
{

    public Builder() : base()
    {
        HP = 36;
        Armor = 0;
        Resist = 0;
        AttackDistance = 1;
        TowerCD.CD = 2;
        SetDamage(8, DamageType.Physical, RangeType.Melee, "Hammer", 20);

        Name = "Строитель";
        SpriteName = "Builder";

        BuildTowerSkill = new ActiveSkill(PreBuilding, Building, "Постройка башни", "Tower", "В течении 3 ходов строит башню (60 здоровья, 4 брони и 18 урона в радиусе 2). Процесс строительства может быть прерван, если на клетку кто-то станет или строитель умрёт.", true);
        Skills.AddSkill(BuildTowerSkill);
        BuildTowerSkill.CD = TowerCD;
    }

    #region Build tower
    private ActiveSkill BuildTowerSkill;
    CoolDown TowerCD = new CoolDown(1000);
    private void PreBuilding()
    {

        CagePack pack = new CagePack();

        Cage addCage = Cage.CageExist(X, Y + 1);
        if (addCage != null && !addCage.HasCreature)
            pack.Add(addCage);

        addCage = Cage.CageExist(X, Y - 1);
        if (addCage != null && !addCage.HasCreature)
            pack.Add(addCage);

        addCage = Cage.CageExist(X + 1, Y);
        if (addCage != null && !addCage.HasCreature)
            pack.Add(addCage);

        addCage = Cage.CageExist(X - 1, Y);
        if (addCage != null && !addCage.HasCreature)
            pack.Add(addCage);

        MapChooseCages.Get.ActivateL(BuildTowerSkill.EndAction, pack, new List<Cage>());
    }

    private void Building(List<Cage> cages)
    {
        Effect.AddNewEffect(this, new CoolDown(6), "BuildingEffect", IconType.BotCage, () =>
        {
            MovePoints = 0;
            AttackPoints = 0;
            UpdateUnitInterface();
        }).AddNewMoveStartEvent(() =>
        {
            if (CurrentMove())
            {
                AttackPoints = 0;
                MovePoints = 0;
            }
        }).AddNewEndEvent(() =>
        {
            Team.SpawnCreature(CreatureType.BuildersTower, cages[0]);
        });
        TowerCD.Update();
        Interface.Get.UpdateInterface();
    }
    #endregion

}