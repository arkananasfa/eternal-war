using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Game : MonoBehaviour
{

    public Team CurrentTeam { get; set; }

    public Creature CurrentCreature { get; set; }

    public void UpdateUnitsInterface()
    {
        foreach (Creature cr in Team.Team1.Creatures.Pack)
        {
            cr.MapObject.UpdateInterface();
        }
        foreach (Creature cr in Team.Team2.Creatures.Pack)
        {
            cr.MapObject.UpdateInterface();
        }
    }

    public void NextMove()
    {
        if (Generall.GameMode == GameMode.Multiplayer && Team.MultiplayerCanMove())
        {
            Action a = new Action(new Cage(), Action.ActionType.MoveEnd, new List<Cage>());
            NetworkManager.Get.SendAction(a);
        }
        MapChooseCages.Get.ClearChooseCages();
        Team.C.Creatures.NextMove();
        Team.ChangeTeam();
        Team.C.Gold += Generall.MoneyPerTurn;
        foreach (Creature cr in Team.C.Creatures.Pack.ToArray())
        {
            if (!cr.Dead)
            {
                cr.MoveStart();
                cr.PostMoveStart();
            }
        }
        foreach (Creature cr in Team.E.Creatures.Pack.ToArray())
        {
            if (!cr.Dead)
            {
                cr.MoveStart();
                cr.PostMoveStart();
            }
        }
        Interface.Get.ChangeCreature();
        Interface.Get.UpdateInterface();
    }

    public void AfterAttack(Creature attacker, Creature defender)
    {
        foreach (Creature c in Team.C.Creatures.Pack.ToArray())
        {
            if (!c.Dead)
                c.Skills.AfterAttack(attacker, defender);
        }
        foreach (Creature c in Team.E.Creatures.Pack.ToArray())
        {
            if (!c.Dead)
                c.Skills.AfterAttack(attacker, defender);
        }
    }
    
    public Damage BeforeAttack(Creature attacker, Creature defender, Damage preDmg)
    {
        foreach (Creature c in attacker.Team.Creatures.Pack.ToArray())
        {
            if (!c.Dead)
                preDmg = c.Skills.BeforeAttack(attacker, defender, preDmg);
        }
        foreach (Creature c in attacker.Team.Opposite.Creatures.Pack.ToArray())
        {
            if (!c.Dead)
                preDmg = c.Skills.BeforeAttack(attacker, defender, preDmg);
        }
        return preDmg;
    }

    #region Unlimited money
    public void AddUnlimitedMoney()
    {
        Team.C.Gold = 100000;
        Team.E.Gold = 100000;
        Interface.Get.UpdateInterface();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            AddUnlimitedMoney();
    }
    #endregion

    #region Singleton
    private void Start()
    {
        Get = this;
    }

    public static Game Get;
    #endregion

}