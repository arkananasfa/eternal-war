public interface IDamageable
{

    Damage Damage { get; set; }

    int AttackDistance { get; set; }

    void Attack();

    Creature StandartAttack();

}