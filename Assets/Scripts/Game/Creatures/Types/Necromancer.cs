public class Necromancer : Creature
{

    public Necromancer() : base()
    {

        HP = 42;
        Armor = 2;
        Resist = 3;
        AttackDistance = 3;
        SetDamage(14, DamageType.Magical, RangeType.Range, "Skull");
        Damage.AnimOrientation = AnimationOrientation.No;

        Name = "Некромант";
        SpriteName = "Necromancer";

        var skill = new PassiveSkill("Некромантия", "Sceleton", "При смерти вражеского существа в квадрате 5х5, с центром на некроманте, на клетке жертвы появляется скелет (28 здоровья, 10 физ урона, 2 брони).", true);
        Skills.AddSkill(skill);
        skill.OnSmbdAttack = Necromancy;
        skill.CD = NecromancyCD;
    }

    private CoolDown NecromancyCD = new CoolDown(2);
    public void Necromancy(Creature attacker, Creature defender)
    {
        if (!Silenced && NecromancyCD.CanUse())
        {
            if (!OneTeam(defender) && defender.Dead && !defender.Cage.HasCreature && System.Math.Abs(X - defender.X) <= 2 && System.Math.Abs(Y - defender.Y) <= 2)
            {
                Team.SpawnCreature(CreatureType.Sceleton, defender.Cage);
                NecromancyCD.Update();
            }
        }
    }

}