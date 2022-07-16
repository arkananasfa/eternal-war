using System;
using UnityEngine;

public class AnimationData
{

    public Vector3 From;
    public Vector3 To;

    // Attack options
    public bool Attackable;
    public bool Damagable;
    public decimal Damage;
    public Creature Attacker;
    public Creature Defender;

    // Event
    public event System.Action OnAnimationEnd;

    public AnimationData(Vector3 from, Vector3 to, bool damagable, bool attackable, decimal damage, Creature attacker, Creature defender)
    {
        From = from;
        To = to;
        Attacker = attacker;
        Defender = defender;
        Damagable = damagable;
        Attackable = attackable;
        Damage = damage;
        Defender = defender;
    }

    public AnimationData ChangeFromCreature(Creature cr)
    {
        From = cr.MapObject.GetComponent<RectTransform>().localPosition;
        return this;
    }
    public AnimationData ChangeFromCage(Cage c)
    {
        From = Map.Get.GetCageLocation(c);
        return this;
    }
    public AnimationData ChangeToCage(Cage c)
    {
        To = Map.Get.GetCageLocation(c);
        return this;
    }

    public AnimationData AddAnimationEndEvent(System.Action action)
    {
        OnAnimationEnd += action;
        return this;
    }

    public AnimationData(Vector3 from, Vector3 to, bool damagable, bool attackable, decimal damage, Creature attacker, Creature defender, System.Action pastAttackAction) : this(from, to, damagable, attackable, damage, attacker, defender)
    {
        OnAnimationEnd += pastAttackAction;
    }

    public static AnimationData CreateStandartAttackAnimation(Creature attacker, Creature defender)
    {
        return new AnimationData
        (
            attacker.MapObject.GetComponent<RectTransform>().localPosition,
            defender.MapObject.GetComponent<RectTransform>().localPosition,
            true,
            true,
            0,
            attacker,
            defender
        );
    }

    internal void EndAnimation()
    {
        OnAnimationEnd?.Invoke();
    }
}