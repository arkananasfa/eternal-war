using UnityEngine;
using System.Collections.Generic;

public class ActiveSkill : Skill
{

    public delegate void ActiveSkillsCageChooserDelegate();
    public delegate void ActiveSkillsDelegate(List<Cage> cages);

    public ActiveSkillsCageChooserDelegate StartPoint { get; set; }
    public ActiveSkillsDelegate EndPoint { get; set; }

    public bool Directed { get; set; }

    public bool Targeted { get; set; }

    /// <summary>
    /// Create Skill without actions
    /// </summary>
    /// <param name="skillName"></param>
    /// <param name="spriteName"></param>
    /// <param name="description"></param>
    /// <param name="fullPath"></param>
    public ActiveSkill(string skillName, string spriteName, string description, bool fullPath = false) : base(skillName, spriteName, description, fullPath) {}

    /// <summary>
    /// Create a directed skill
    /// </summary>
    /// <param name="preAction"></param>
    /// <param name="Action"></param>
    /// <param name="skillName"></param>
    /// <param name="spriteName"></param>
    /// <param name="description"></param>
    /// <param name="fullPath"></param>
    public ActiveSkill(ActiveSkillsCageChooserDelegate preAction, ActiveSkillsDelegate Action, string skillName, string spriteName, string description, bool fullPath = false) : base(skillName, spriteName, description, fullPath)
    {
        StartPoint = preAction;
        EndPoint = Action;
        Directed = true;
    }

    /// <summary>
    /// Create an undirected skill
    /// </summary>
    /// <param name="preAction"></param>
    /// <param name="Action"></param>
    /// <param name="skillName"></param>
    /// <param name="spriteName"></param>
    /// <param name="description"></param>
    /// <param name="fullPath"></param>
    public ActiveSkill(ActiveSkillsCageChooserDelegate preAction, string skillName, string spriteName, string description, bool fullPath = false) : base(skillName, spriteName, description, fullPath)
    {
        StartPoint = preAction;
        Directed = false;
    }

    public void ButtonAction()
    {
        if (Generall.GameMode == GameMode.Multiplayer && !Directed)
        {
            NetworkManager.Get.SendSkill(Owner, this, new List<Cage>());
        }
        StartPoint();
    }

    public void EndAction(List<Cage> cages)
    {
        if (Generall.GameMode == GameMode.Multiplayer)
        {
            NetworkManager.Get.SendSkill(Owner, this, cages);
        }
        EndPoint(cages);
    }

    public virtual bool CanUse()
    {
        if (Team.MultiplayerCanMove() && CD.CanUse() && Owner.CurrentMove() && (!Silencable || !Owner.Silenced) && (CanUseAdditional==null || CanUseAdditional()))
            return true;
        else
            return false;
    }

    public delegate bool CanUseDelegate();
    public CanUseDelegate CanUseAdditional;

}