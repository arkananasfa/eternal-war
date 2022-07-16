using System.Collections.Generic;

public class Assasin : Creature
{

    public Assasin() : base()
    {

        HP = 36;
        Armor = 0;
        Resist = 0;
        AttackDistance = 1;
        SetDamage(16, DamageType.Physical, RangeType.Melee, "ShadowSword", 15);
        AttackName = "Атака с теней";

        SpriteName = "Assasin";
        Name = "Асасин";

        Skills.AddSkill(new AssasinativeBlink());
        Skills.AddSkill(new ShadowAttack());

    }

}