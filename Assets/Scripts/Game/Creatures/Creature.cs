using System;
using System.Collections.Generic;
using UnityEngine;

public class Creature : EWEventsObject
{

    #region Properties
    public CreatureObject MapObject { get; set; }

    public Team Team { get; set; }

    public Cage Cage { get; set; }

    public int X { get { return Cage.X; } }
    public int Y { get { return Cage.Y; } }

    public string Name { get; set; }
    public string SpriteName { get; set; }

    public Damage Damage { get; set; }
    public decimal HP { get; set; }
    public decimal MaxHP { get; set; }
    public int Armor { get; set; }
    public int Resist { get; set; }
    public int AttackDistance { get; set; }
    public bool Movable { get; set; }
    public bool Dead { get; set; }
    public string AttackName { get; set; }
    public string AttackDescription { get; set; }
    public Sprite AttackIconSprite { get; set; }

    public SkillPack Skills { get; set; }

    public bool Silenced
    {
        get { return Silence != 0; }
    }
    private int silence;
    public int Silence
    {
        get { return silence; }
        set {
            if (value < 0)
                silence = 0;
            else
                silence = value;
        }
    }

    public int AttackPoints { get; set; }
    public int MovePoints { get; set; }

    public bool AnimatingNow { get; set; }
    #endregion

    #region Constructor
    public Creature()
    {
        Effects = new EffectPack();
        Skills = new SkillPack();
        Skills.Owner = this;
        Effects.Creature = this;
        AttackIconSprite = Resources.Load<Sprite>("CreaturesAttacks/Sword");
        Movable = true;
        AttackName = "Стандартная атака";
        AttackDescription = "Атака без использования каких-либо способностей.";
    }
    #endregion

    #region Start function
    public virtual void MoveStart()
    {
        Silence--;
        if (Movable)
            MovePoints = 1;

        AttackPoints = 1;
        Skills.MoveStart();
        Effects.MoveStart();
        UpdateUnitInterface();
    }

    public virtual void PostMoveStart(){}

    #endregion

    #region Replace functions
    /// <summary>
    /// Move creature front if it can
    /// </summary>
    public void MoveForward()
    {
        if (CanMoveForward())
        {
            if (Generall.GameMode == GameMode.Multiplayer && Team == Team.M)
            {
                Action a = new Action(Cage, Action.ActionType.Move, new List<Cage>());
                NetworkManager.Get.SendAction(a);
            }
            MovePoints--;
            Replace(0, Team.C.Forward);
        }
    }
    /// <summary>
    /// True if creature can normaly move to front cage
    /// </summary>
    /// <returns></returns>
    public bool CanMoveForward()
    {
        return !Moved() && Cage.Front(Team) != null && !Cage.Front(Team).HasCreature && !Dead;
    }

    /// <summary>
    /// Replace by some cages
    /// </summary>
    /// <param name="x">By x coordinate</param>
    /// <param name="y">By y coordinate</param>
    public void Replace(int x, int y)
    {
        ReplaceWithoutAnimation(x, y);
        AnimatingNow = true;

        // Set position of sprite
        var anim = MapObject.gameObject.AddComponent<MoveAnimator>();
        anim.OnEndAnimation = CheckBaseDamage;
        anim.OnEndAnimation += () => { AnimatingNow = false; };
        anim.StartAnimation(Map.Get.GetCageLocation(Cage), 10);
    }
    public void ReplaceWithoutAnimation(int x, int y)
    {
        if (!Cage.IsCageExist(X + x, Y + y))
            return;

        // Clear cage
        Cage.Creature = null;

        // Set cage
        Cage = Map.Get.Cages[X + x, Y + y];
        Cage.Creature = this;
    }
    /// <summary>
    /// Replace to some cage
    /// </summary>
    /// <param name="x">x coordinate</param>
    /// <param name="y">y coordinate</param>
    public void ReplaceTo(int x, int y)
    {
        Replace(x - X, y - Y);
    }
    public void ReplaceToWithoutAnimation(int x, int y)
    {
        ReplaceWithoutAnimation(x - X, y - Y);
    }
    /// <summary>
    /// Replace to same cage
    /// </summary>
    /// <param name="c">The cage to replace</param>
    public void ReplaceTo(Cage c)
    {
        ReplaceTo(c.X, c.Y);
    }
    public void ReplaceToWithoutAnimation(Cage c)
    {
        ReplaceToWithoutAnimation(c.X, c.Y);
    }
    public void SetToPosition()
    {
        MapObject.GetComponent<RectTransform>().localPosition = Map.Get.GetCageLocation(Cage);
    }
    #endregion

    #region Attack/damage functions
    // Set attack (damage/type/animation)
    public void SetDamage(decimal dmg, DamageType type, RangeType range, string AttackedName)
    {
        Damage = new Damage(dmg, type, range, AttackedName);
        AttackIconSprite = Resources.Load<Sprite>("CreaturesAttacks/"+AttackedName);
    }
    public void SetDamage(decimal dmg, DamageType type, RangeType range, string AttackedName, float animationSpeed)
    {
        Damage = new Damage(dmg, type, range, AttackedName, animationSpeed);
        AttackIconSprite = Resources.Load<Sprite>("CreaturesAttacks/" + AttackedName);
    }
    public void SetDamage(Damage dmg)
    {
        Damage = dmg;
        AttackIconSprite = Resources.Load<Sprite>("CreaturesAttacks/" + Damage.AnimationType);
    }

    // Attacking
    public Creature StandartAttack()
    {
        if (CanAttack())
        {
            Creature cr = FindAttackableEnemy();
            GiveDamage(cr);
            AttackPoints--;
            return cr;
        }
        return null;
    }
    public virtual bool CanAttack()
    {
        return FindAttackableEnemy() != null;
    }
    public virtual Creature FindAttackableEnemy()
    {
        if (Attacked() || Dead)
            return null;
        Cage enemysCage = Cage;
        for (int i = 1; i <= AttackDistance; i++)
        {
            enemysCage = enemysCage.Front(Team);
            if (enemysCage == null)
                break;
            if (enemysCage.HasCreature && !OneTeam(enemysCage.Creature))
                return enemysCage.Creature;
        }
        return null;
    }
    public virtual void Attack(List<Cage> cages)
    {
        if (CanAttack())
        {
            SendAttackToServer(cages);
            StandartAttack();
            if (Generall.GameMode == GameMode.Multiplayer && Team == Team.M)
            {
                Action a = new Action(Cage, Action.ActionType.Attack, new List<Cage>());
                NetworkManager.Get.SendAction(a);
            }
        }
    }
    public virtual void PreAttack()
    {
        Attack(new List<Cage>());
    }
    public void SendAttackToServer(List<Cage> cages)
    {
        if (Generall.GameMode == GameMode.Multiplayer)
        {
            NetworkManager.Get.SendAttack(this, cages);
        }
    }
    public void GiveDamage(Creature target)
    {        
        GiveDamage(target, Damage.Copy());
    }
    public void GiveDamage(Creature target, Damage dmg)
    {
        dmg = dmg.Copy();
        dmg = Game.Get.BeforeAttack(this, target, dmg);
        decimal realDmg = System.Math.Round(target.Protect(dmg), 2);
        MapObject.CreateMoveAnimation(new AnimationData(MapObject.GetComponent<RectTransform>().localPosition,
            target.MapObject.GetComponent<RectTransform>().localPosition,
            true, true, realDmg, this, target), dmg);
        InterfaceUpdator.Get.UpdateUnitsInterface();
        Debug.Log("Attacked");
    }
    public void GiveDamage(Creature target, Damage dmg, System.Action pastAttackEffect)
    {
        dmg = dmg.Copy();
        dmg = Game.Get.BeforeAttack(this, target, dmg);
        decimal realDmg = System.Math.Round(target.Protect(dmg), 2);
        MapObject.CreateMoveAnimation(new AnimationData(MapObject.GetComponent<RectTransform>().localPosition,
            target.MapObject.GetComponent<RectTransform>().localPosition,
            true, true, realDmg, this, target, pastAttackEffect), dmg);
        InterfaceUpdator.Get.UpdateUnitsInterface();
        Debug.Log("Attacked");
    }
    public void GiveDamage(Creature target, Damage dmg, AnimationData data)
    {
        dmg = dmg.Copy();
        dmg = Game.Get.BeforeAttack(this, target, dmg);
        decimal realDmg = System.Math.Round(target.Protect(dmg), 2);
        data.Damage = realDmg;
        MapObject.CreateMoveAnimation(data, dmg);
        InterfaceUpdator.Get.UpdateUnitsInterface();
        Debug.Log("Attacked");
    }
    public virtual void PastAttackEffect() { }
    public void CheckBaseDamage()
    {
        if (Y == Team.Opposite.BuyLine - 1)
        {
            Team.Opposite.BaseHP -= System.Convert.ToInt32(Damage.Value);
            OrdinaryDeath();

            if (Team.Opposite.BaseHP == 0)
                Team.Win();
            return;
        }
    }

    // Getting damage
    public virtual void GetDamage(decimal dmg)
    {
        HP -= dmg;
        if (HP <= 0)
            Death();
        Debug.Log("Damaged");
    }
    public virtual void GetDamageFull(Damage dmg)
    {
        decimal realDmg = System.Math.Round(Protect(dmg), 2);
        GetDamage(realDmg);
    }
    public decimal Protect(Damage dmg)
    {
        switch (dmg.Type)
        {
            case DamageType.Physical:
                return (dmg.Value - dmg.Value * Armor / 10m);
            case DamageType.Magical:
                return (dmg.Value - dmg.Value * Resist / 10m);
            case DamageType.Clear:
                return dmg.Value;
            default:
                return 0;
        }
    }
    public virtual void Death()
    {
        OrdinaryDeath();
    }
    private void OrdinaryDeath()
    {
        if (!Dead)
        {
            Dead = true;
            if (Game.Get.CurrentCreature == this)
                Interface.Get.ChangeCreature();
            Team.Creatures.DeleteCreature(this);
            Cage.Creature = null;
            GameObject.Destroy(MapObject.gameObject);
        }
    }
    #endregion

    #region Support functions
    /// <summary>
    /// Return true if the creature's team move now
    /// </summary>
    /// <returns></returns>
    public bool CurrentMove()
    {
        return Team == Team.C;
    }
    /// <summary>
    /// Return true if creature from the same team
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public bool OneTeam(Creature c)
    {
        return this.Team == c.Team;
    }
    /// <summary>
    /// Return true if the front cage's creature from the same team
    /// </summary>
    /// <returns></returns>
    public bool FrontCreatureOneTeam()
    {
        return OneTeam(FrontCreature());
    }
    /// <summary>
    /// Return true if the creature is neighbour
    /// </summary>
    /// <param name="cr"></param>
    /// <returns></returns>
    public bool isNeighbour(Creature cr)
    {
        return cr.Y == Y && System.Math.Abs(cr.X - X) == 1;
    }
    /// <summary>
    /// Return front cage
    /// </summary>
    /// <returns></returns>
    public Cage Front()
    {
        return Cage.Front(Team);
    }
    /// <summary>
    /// Return creature, who stay front
    /// </summary>
    /// <returns></returns>
    public Creature FrontCreature()
    {
        return Cage.Front(Team).Creature;
    }
    /// <summary>
    /// Return true if the creature can't move now
    /// </summary>
    /// <returns></returns>
    public bool Moved()
    {
        return MovePoints <= 0;
    }
    /// <summary>
    /// Return true if the creature can't attack now
    /// </summary>
    /// <returns></returns>
    public bool Attacked()
    {
        return AttackPoints <= 0;
    }
    /// <summary>
    /// Update interface about unit
    /// </summary>
    public void UpdateUnitInterface()
    {
        if (!AnimatingNow)
            MapObject.UpdateInterface();
    }
    /// <summary>
    /// Set creatures sprite to other, that in the SpriteName
    /// </summary>
    public void SetSprite()
    {
        MapObject.SetImage(Resources.Load<Sprite>(SpriteName));
    }
    /// <summary>
    /// Set creatures sprite to spriteName
    /// </summary>
    public void SetSprite(string spriteName)
    {
        SpriteName = spriteName;
        MapObject.SetImage(Resources.Load<Sprite>(SpriteName));
    }
    #endregion

}