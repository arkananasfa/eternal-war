public class Monk : Creature
{

    public Monk() : base()
    {
        HP = 30;
        Armor = 0;
        Resist = 2;
        AttackDistance = 1;
        HealCD.CD = 0;
        SetDamage(10, DamageType.Magical, RangeType.Melee, "GreenMeteor", 30);

        Name = "Монах";
        SpriteName = "Monk";

        HealSkill = new ActiveSkill(Consecration, "Воодушивление", "Heal", $"Лечит всех союзников вокруг на {HealPercents*100}% от их полного здоровья.");
        this.Skills.AddSkill(HealSkill);
        HealSkill.CD = HealCD;

    }

    #region Heal
    private decimal HealPercents = 0.08m;
    private ActiveSkill HealSkill;
    CoolDown HealCD = new CoolDown(2);
    private void Consecration()
    {
        if (!Silenced)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Cage target = Cage.CageExist(X + i, Y + j);
                    if ((i == 0 && j == 0) || target == null || !target.HasCreature || !OneTeam(target.Creature))
                        continue;
                    Creature cr = target.Creature;
                    if (cr.HP < cr.MaxHP)
                    {
                        Damage Heal;
                        if (cr.HP + HealPercents * cr.MaxHP >= cr.MaxHP)
                            Heal = new Damage(-1 * (cr.MaxHP - cr.HP), DamageType.Clear, RangeType.Clear, "Heal", 15, AnimationOrientation.No);
                        else
                            Heal = new Damage(-1 * HealPercents * cr.MaxHP, DamageType.Clear, RangeType.Clear, "Heal", 15, AnimationOrientation.No);
                        GiveDamage(cr, Heal);
                        if (cr.HP > cr.MaxHP)
                            cr.HP = cr.MaxHP;
                        HealCD.Update();
                        Interface.Get.UpdateInterface();
                    }
                }
            }                
        }
    }
    #endregion

}