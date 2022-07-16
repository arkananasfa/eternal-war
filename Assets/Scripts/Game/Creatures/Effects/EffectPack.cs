using System.Collections.Generic;

public class EffectPack : List<Effect>
{

    public Creature Creature { get; set; }

    public EffectPack() : base() { }

    public void MoveStart()
    {
        foreach (Effect effect in this.ToArray())
        {
            effect.CD.Decrease();
            if (effect.CD.CanUse())
            {
                effect.OnEnd();
                effect.Destroy();
                Remove(effect);
            }
            else
                effect.OnMoveStart();
        }
    }

    public void SmbdAttack(Creature attacker, Creature defender)
    {
        foreach (Effect effect in this)
        {
            effect.OnAttack(attacker, defender);
        }
    }

}