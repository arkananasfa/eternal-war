using System;

public abstract class EWEventsObject
{

    public delegate void AttackAction(Creature attacker, Creature defender);
    public delegate decimal PreAttackAction(decimal damage, Creature attacker, Creature defender);

    public System.Action OnMoveStart;
    public AttackAction OnAttack;
    public PreAttackAction OnPreAttack;

    public EffectPack Effects { get; set; }

}