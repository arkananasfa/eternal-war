using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seraphime : Creature
{

    public Seraphime() : base()
    {
        HP = 100;
        Armor = 1;
        Resist = 2;
        AttackDistance = 1;

        SetDamage(20, DamageType.Physical, RangeType.Melee, "Sword",15);

        Name = "Серафим";
        SpriteName = "Seraph";

        HellsFireActivator = new ActiveSkill(HellsFireActivate, "Адское пламя", "CreaturesAttacks/HellsFire",
            "Окутывает серафима пламенем из ада. Все вражеские юниты вокруг получают по 8, а серафим по 10 магического урона каждый ход", true);
        HellsFireCD.Set(0);
        Skills.AddSkill(HellsFireActivator);
        HellsFireActivator.CD = HellsFireCD;

        hellsFireOn = false;
        FireSkill = new PassiveSkill("Обжигание", "CreaturesAttacks\\Fire", "Наносит по 3 магического урона врагам вокруг", true);
        Skills.AddSkill(FireSkill);
        FireSkill.Silencable = true;
        FireSkill.OnMoveStart = Firing;
    }

    #region Hell's fire skills

    #region StateChange

    bool hellsFireOn;
    private ActiveSkill HellsFireActivator;
    CoolDown HellsFireCD = new CoolDown(0);
    private void HellsFireActivate()
    {
        hellsFireOn = !hellsFireOn;
        if (hellsFireOn)
        {
            FireSkill.OnMoveStart = HellsFiring;
            FireSkill.SetSprite("CreaturesAttacks\\HellsFire", true);
            HellsFireActivator.SetSprite("CreaturesAttacks\\Fire", true);
            FireSkill.Description = "Наносит по 10 магического урона врагам вокруг и 8 серафиму";
            HellsFireActivator.Description = "Закрывает портал с ада и серафим снова наносит только 3 урона врагам вокруг";
            SetSprite("SeraphsRage");
        }
        else
        {
            FireSkill.OnMoveStart = Firing;
            FireSkill.SetSprite("CreaturesAttacks\\Fire", true);
            HellsFireActivator.SetSprite("CreaturesAttacks\\HellsFire", true);
            FireSkill.Description = "Наносит по 3 магического урона врагам вокруг";
            HellsFireActivator.Description = "Окутывает серафима пламенем из ада. Все вражеские юниты вокруг получают по 10, а серафим по 8 магического урона каждый ход";
            SetSprite("Seraph");
        }
        HellsFireCD.Update();
        UpdateUnitInterface();
        Interface.Get.UpdateInterface();
    }
    #endregion

    #region Firing
    PassiveSkill FireSkill;
    CoolDown FiringCD = new CoolDown(2);
    
    private void Firing()
    {
        if (!CurrentMove() || Silenced)
            return;
        Damage fireDamage = new Damage(3, DamageType.Magical, RangeType.Melee, "Fire", AnimationOrientation.No);
        for (int y = Y - 1; y < Y + 2; y++)
        {
            for (int x = X - 1; x < X + 2; x++)
            {
                if (x == X && y == Y)
                    continue;
                if (CurrentMove() && Cage.IsCageExist(x, y) && Map.Get.Cages[x, y].HasCreature && !OneTeam(Map.Get.Cages[x, y].Creature))
                    GiveDamage(Map.Get.Cages[x, y].Creature, fireDamage);
            }
        }
    }
    private void HellsFiring()
    {
        if (!CurrentMove() || Silenced)
            return;
        Damage fireDamage = new Damage(8, DamageType.Magical, RangeType.Melee, "HellsFire", AnimationOrientation.No);
        Damage fireherself = new Damage(10, DamageType.Magical, RangeType.Melee, "HellsFire", AnimationOrientation.No);
        for (int y = Y - 1; y < Y + 2; y++)
        {
            for (int x = X - 1; x < X + 2; x++)
            {
                if (x == X && y == Y)
                    continue;
                if (Cage.IsCageExist(x, y) && Map.Get.Cages[x, y].HasCreature && !OneTeam(Map.Get.Cages[x, y].Creature))
                    GiveDamage(Map.Get.Cages[x, y].Creature, fireDamage);
            }
        }
        GiveDamage(this, fireherself, AnimationData.CreateStandartAttackAnimation(this, this));
    }
    #endregion

    #endregion

    #region Soon
    /*private void FireCage()
    {
        for (int y = -3; y <= 3; y++)
        {
            for(int x = -3; x <= 3; x++)
            {
                if (Cage.CageExist(X + x, Y + y)!=null)
                {

                }
            }
        }
    }*/
    #endregion

}