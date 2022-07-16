using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class Action
{

    public enum ActionType
    {
        Attack,
        Move,
        Skill,
        Buy,
        MoveEnd
    }

    #region Properties
    public Cage ActionMakerCage;
    public ActionType Type;
    public List<Cage> Cages;
    public CreatureType CType;
    public int SkillNumber;
    #endregion

    public void Apply() 
    {
        #region Debuging
        Debug.Log("Applied!");
        Debug.Log(Type);
        #endregion

        if (Type == ActionType.Buy)
        {
            CreatureSpawner.CreatureSpawnType = CType;
            CreatureSpawner.BuyCreatureWithoutMultiplayer(ActionMakerCage);
        }
        else if (Type == ActionType.MoveEnd)
        {
            Game.Get.NextMove();
        }
        else if (Type == ActionType.Move)
        {
            ActionMakerCage.Creature.MoveForward();
        }
        else if (Type == ActionType.Attack)
        {
            ActionMakerCage.Creature.Attack(Cages);
        }
        else if (Type == ActionType.Skill)
        {
            ActiveSkill skill = ActionMakerCage.Creature.Skills.ActiveSkills[SkillNumber];
            if (skill.Directed)
            {
                skill.EndAction(Cages);
            }
            else
            {
                skill.ButtonAction();
            }
        }
    }

    #region Constructors
    public Action () { }
    public Action(Cage maker, ActionType type, List<Cage> cages)
    {
        ActionMakerCage = maker;
        Type = type;
        Cages = cages;
    }
    public Action(Cage maker, CreatureType cType)
    {
        ActionMakerCage = maker;
        Type = ActionType.Buy;
        CType = cType; ;
    }
    public Action(Cage maker, int skillIdx, List<Cage> cages)
    {
        ActionMakerCage = maker;
        Type = ActionType.Skill;
        SkillNumber = skillIdx;
        Cages = cages;
    }
    #endregion

    #region JSON converters
    public JSONObject ToJSON ()
    {
        JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
        j.AddField("actionType", (int)Type);
        if (Type != ActionType.MoveEnd )
        {
            j.AddField("cageX", ActionMakerCage.X);
            j.AddField("cageY", ActionMakerCage.Y);
        }
        if (Type == ActionType.Buy)
        {
            j.AddField("creatureType", (int)CType);
        }
        else
        {
            for (int i = 0; i < Cages.Count; i++)
            {
                j.AddField("cage" + (i + 1).ToString() + "X", Cages[i].X);
                j.AddField("cage" + (i + 1).ToString() + "Y", Cages[i].Y);
            }
            if (Type == ActionType.Skill)
            {
                j.AddField("skillIndex", SkillNumber);
            }
        }
        return j;
    }
    public static Action ToAction (JSONObject j)
    {
        Action a = new Action();
        a.Type = (ActionType)JSONConverter.JSONToInt(j, "actionType");
        if (a.Type != ActionType.MoveEnd)
        {
            a.ActionMakerCage = Cage.CageExist(JSONConverter.JSONToInt(j, "cageX"), JSONConverter.JSONToInt(j, "cageY"));
        }
        if (a.Type == ActionType.Buy)
        {
            a.CType = (CreatureType)JSONConverter.JSONToInt(j, "creatureType");
        }
        else
        {
            a.Cages = new List<Cage>();
            for (int i = 0; i < 3; i++)
            {
                if (j.HasField("cage" + (i + 1).ToString() + "X"))
                {
                    a.Cages.Add(Cage.CageExist(JSONConverter.JSONToInt(j, "cage" + (i + 1).ToString() + "X"), JSONConverter.JSONToInt(j, "cage" + (i + 1).ToString() + "Y")));
                }
            }
            if (a.Type == ActionType.Skill)
            {
                a.SkillNumber = JSONConverter.JSONToInt(j, "skillIndex");
            }
        }
        return a;
    }
    #endregion

}