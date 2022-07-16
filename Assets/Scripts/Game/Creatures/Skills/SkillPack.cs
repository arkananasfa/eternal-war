using System.Collections.Generic;

public class SkillPack : List<Skill>
{

    public Creature Owner;

    public List<ActiveSkill> ActiveSkills { get; set; }
    public List<PassiveSkill> PassiveSkills { get; set; }

    public int ActiveSkillCount { get { return ActiveSkills.Count; } }

    public void AddSkill(Skill skill)
    {
        Add(skill);
        if (skill is ActiveSkill)
            ActiveSkills.Add(skill as ActiveSkill);
        else if (skill is PassiveSkill)
            PassiveSkills.Add(skill as PassiveSkill);
        skill.Owner = Owner;
    }

    public void Buy(Creature newCreature)
    {
        foreach (PassiveSkill skill in PassiveSkills)
        {
            skill.OnBuy(newCreature);
        }
    }

    public void MoveStart()
    {
        foreach (Skill skill in this)
        {
            if (skill is PassiveSkill)
                (skill as PassiveSkill).OnMoveStart();
            skill.CD?.Decrease();
        }
    }

    public Damage BeforeAttack(Creature attacker, Creature defender, Damage preDmg)
    {
        foreach (PassiveSkill skill in PassiveSkills)
        {
            preDmg = skill.OnBeforeSmbdAttack(attacker, defender, preDmg);
        }
        return preDmg;
    }

    public void AfterAttack(Creature attacker, Creature defender)
    {
        foreach (PassiveSkill skill in PassiveSkills)
        {
            skill.OnSmbdAttack(attacker, defender);
        }
    }

    public SkillPack() : base()
    {
        ActiveSkills = new List<ActiveSkill>();
        PassiveSkills = new List<PassiveSkill>();
    }

}