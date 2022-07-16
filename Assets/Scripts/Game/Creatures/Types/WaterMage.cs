/*public class WaterMage : Creature
{

    public WaterMage() : base()
    {
        HP = 80;
        Armor = 1;
        Resist = 2;
        AttackDistance = 3;

        SetDamage(16, DamageType.Magical, RangeType.Range, "HugeWave", 25);

        Name = "Водяной маг";
        SpriteName = "WaterMage";

        var hugeWave = new ActiveSkill("Призыв волны", "CreaturesAttacks/HugeWave", HugeWawe,
            "Создает сильный поток воды, который отталкивает 3 передних существ на клетку вперед и наносит 100% ближнего физического урона врагам.", true);
        HugeWaveCD.Set(0);
        this.Skills.AddSkill(hugeWave);
        hugeWave.CD = HugeWaveCD;
        FriendlyWave = new Damage(0, DamageType.Clear, RangeType.Clear, "HugeWave", 25);
    }


    Creature cr1, cr2, cr3, push;
    bool go1, go2, go3;
    CoolDown HugeWaveCD = new CoolDown(6);
    Damage FriendlyWave;
    private void HugeWawe()
    {
        Cage c4 = Cage.Front(Team, 4);
        Cage c3 = Cage.Front(Team, 3);
        Cage c2 = Cage.Front(Team, 2);
        Cage c1 = Cage.Front(Team);
        go1 = false;
        go2 = false;
        go3 = false;
        if (c4 != null && !c4.HasCreature && c3.HasCreature)
        {
            go3 = true;
            cr3 = c3.Creature;
        }
        if (((c3 != null && !c3.HasCreature) || go3) && c2.HasCreature)
        {
            go2 = true;
            cr2 = c2.Creature;
        }
        if (((c2 != null && !c2.HasCreature) || go2) && c1.HasCreature)
        {
            go1 = true;
            cr1 = c1.Creature;
        }
        Wave1();
        HugeWaveCD.Update();
        UpdateUnitInterface();
    }

    private void Wave1()
    {
        if (go1)
        {
            push = cr1;
            if (OneTeam(cr1))
                GiveDamage(cr1, FriendlyWave, AnimationData.CreateStandartAttackAnimation(this, cr1).AddAnimationEndEvent(Wave2).AddAnimationEndEvent(PushCreature));
            else
                GiveDamage(cr1, Damage, AnimationData.CreateStandartAttackAnimation(this, cr1).AddAnimationEndEvent(Wave2).AddAnimationEndEvent(PushCreature));
        }
        else
        {
            Cage c = Cage.Front(Team);
            if (c.HasCreature && !OneTeam(c.Creature))
                GiveDamage(cr1, Damage, Wave2);
            else
                GiveDamage(this, FriendlyWave, AnimationData.CreateStandartAttackAnimation(this, this).ChangeToCage(Cage.Front(Team)).AddAnimationEndEvent(Wave2));
        }
    }

    private void Wave2()
    {
        if (go2)
        {
            push = cr2;
            if (OneTeam(cr2))
                GiveDamage(cr2, FriendlyWave, AnimationData.CreateStandartAttackAnimation(this, cr2).ChangeFromCage(Cage.Front(Team)).AddAnimationEndEvent(Wave3).AddAnimationEndEvent(PushCreature));
            else
                GiveDamage(cr2, Damage, AnimationData.CreateStandartAttackAnimation(this, cr2).ChangeFromCage(Cage.Front(Team)).AddAnimationEndEvent(Wave3).AddAnimationEndEvent(PushCreature));
        }
        else
        {
            Cage c = Cage.Front(Team).Front(Team);
            if (c == null)
                return;
            if (c.HasCreature && !OneTeam(c.Creature))
                GiveDamage(cr2, Damage, AnimationData.CreateStandartAttackAnimation(this, this).ChangeFromCage(Cage.Front(Team)).ChangeToCage(c).AddAnimationEndEvent(Wave3));
            else
                GiveDamage(this, FriendlyWave, AnimationData.CreateStandartAttackAnimation(this, this).ChangeFromCage(Cage.Front(Team)).ChangeToCage(c).AddAnimationEndEvent(Wave3));
        }
    }

    private void Wave3()
    {
        if (go3)
        {
            push = cr3;
            if (OneTeam(cr3))
                GiveDamage(cr3, FriendlyWave, AnimationData.CreateStandartAttackAnimation(this, cr3).ChangeFromCage(Cage.Front(Team).Front(Team)).AddAnimationEndEvent(PushCreature));
            else
                GiveDamage(cr3, Damage, AnimationData.CreateStandartAttackAnimation(this, cr3).ChangeFromCage(Cage.Front(Team).Front(Team)).AddAnimationEndEvent(PushCreature));
        }
        else
        {
            Cage c = Cage.Front(Team).Front(Team)?.Front(Team);
            if (c == null)
                return;
            if (c.HasCreature && !OneTeam(c.Creature))
                GiveDamage(cr3, Damage, AnimationData.CreateStandartAttackAnimation(this, this).ChangeFromCage(c.Back(Team)).ChangeToCage(c));
            else
                GiveDamage(this, FriendlyWave, AnimationData.CreateStandartAttackAnimation(this, this).ChangeFromCage(c.Back(Team)).ChangeToCage(c));
        }
    }

    private void PushCreature()
    {
        push.ReplaceTo(push.Cage.Front(Team));
    }
    
}*/