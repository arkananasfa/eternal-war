using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Effect : EWEventsObject
{

    public EWEventsObject Owner { get; set; }

    public CoolDown CD { get; set; }
    public GameObject IconObject { get; set; }
    public Image Icon { get { return IconObject.GetComponent<Image>(); } }

    public System.Action OnEnd;

    public Effect()
    {
        CD = new CoolDown(2); ;
        OnMoveStart = () => { };
        OnAttack = (at, de) => { };
        OnPreAttack = (dmg, at, de) => { return dmg; };
        OnEnd = () => { };
    }

    public void SetIconToCreature(string spriteName, IconType type, bool fullPath = false)
    {
        if (fullPath)
            Icon.sprite = Resources.Load<Sprite>(spriteName);
        else
            Icon.sprite = Resources.Load<Sprite>("Effects\\" + spriteName); 
    }

    public static Effect AddNewEffect(EWEventsObject owner, CoolDown cd, string spriteName, IconType iconType, System.Action set, bool fullPath = false)
    {
        var eff = new Effect();
        eff.Owner = owner;
        owner.Effects.Add(eff);
        eff.CD = cd;        
        if (owner is Creature)
        {
            eff.IconObject = GameObject.Instantiate(Archive.Get.EffectIconPrefab, (eff.Owner as Creature).MapObject.transform);
            eff.SetIconToCreature(spriteName, iconType, fullPath);
        }
        set();
        return eff;
    }

    public Effect AddNewEndEvent(System.Action endEvent)
    {
        OnEnd += endEvent;
        return this;
    }

    public Effect AddNewMoveStartEvent(System.Action moveStartEvent)
    {
        OnMoveStart += moveStartEvent;
        return this;
    }

    public Effect AddNewAttackEffect(PreAttackAction preAttackEvent)
    {
        OnPreAttack += preAttackEvent;
        return this;
    }

    public Effect AddNewAttackEffect(AttackAction attackEvent)
    {
        OnAttack += attackEvent;
        return this;
    }

    public void Destroy()
    {
        GameObject.Destroy(Icon);
    }

}

public enum IconType
{
    AllCage,
    BotCage
}