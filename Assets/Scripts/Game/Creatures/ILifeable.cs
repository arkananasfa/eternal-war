using System.Collections;

public interface ILifeable
{

    decimal HP { get; set; }

    decimal Armor { get; set; }

    decimal Resist { get; set; }

    void GetDamage(decimal dmg);

    decimal Protect(Damage dmg);

    void Death();

}