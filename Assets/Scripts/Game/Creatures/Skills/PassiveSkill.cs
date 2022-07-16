public class PassiveSkill : Skill
{
    public delegate void MoveStartDelegate();
    public delegate void BuyDelegate(Creature newCreature);
    public delegate Damage PreAttackDelegate(Creature attacker, Creature defender, Damage preDmg);
    public delegate void AttackDelegate(Creature attacker, Creature defender);

    public BuyDelegate OnBuy;
    public MoveStartDelegate OnMoveStart;
    public PreAttackDelegate OnBeforeSmbdAttack;
    public AttackDelegate OnSmbdAttack;

    public PassiveSkill(string skillName, string spriteName, string description, bool fullPath = false) : base(skillName, spriteName, description, fullPath)
    {
        OnBuy = Nothing;
        OnMoveStart = Nothing;
        OnBeforeSmbdAttack = Nothing;
        OnSmbdAttack = Nothing;
    }

    private void Nothing() { }

    private void Nothing(Creature newCreature) { }

    private Damage Nothing(Creature attacker, Creature defender, Damage preDmg) { return preDmg; }

    private void Nothing(Creature attacker, Creature defender) { }

}