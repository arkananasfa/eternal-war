using System.Collections.Generic;

public class CreaturePack
{

    public List<Creature> Pack;

    public void NextMove()
    {
        bool success = true;
        while (success)
        {
            success = false;
            foreach (Creature c in Pack.ToArray())
            {
                if (c.CanMoveForward())
                {
                    c.MoveForward();
                    success = true;
                }
            }
        }
    }

    public void DeleteCreature(Creature cr)
    {
        Pack.Remove(cr);
    }

    public void Add(Creature cr)
    {
        Pack.Add(cr);
    }

    public void AfterAttack(Creature attacker, Creature defender)
    {
        foreach (Creature c in Pack)
        {
            c.Skills.AfterAttack(attacker, defender);
        }
    }

}