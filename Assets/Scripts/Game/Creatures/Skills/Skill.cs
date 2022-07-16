using UnityEngine;

public abstract class Skill
{

    public string SkillName { get; set; }
    public Sprite SkillSprite { get; set; }

    public Creature Owner { get; set; }

    public string Description { get; set; }

    public CoolDown CD { get; set; }
    public bool Silencable { get; set; }

    public Skill(string name, string imageName, string description, bool fullPath)
    {
        Silencable = true;
        SkillName = name;
        Description = description;
        if (fullPath)
            SkillSprite = Resources.Load<Sprite>(imageName);
        else
            SkillSprite = Resources.Load<Sprite>("Skills\\" + imageName);
    }

    public void SetSprite(string imageName, bool fullPath)
    {
        if (fullPath)
            SkillSprite = Resources.Load<Sprite>(imageName);
        else
            SkillSprite = Resources.Load<Sprite>("Skills\\" + imageName);
    }
}