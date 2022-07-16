using System.Collections.Generic;

internal class Centaur : Creature
{

    public Centaur() : base()
    {
        HP = 40;
        Armor = 0;
        Resist = 2;
        AttackDistance = 3;
        ManeuverCD.CD = 2;
        SetDamage(14, DamageType.Physical, RangeType.Range, "Arrow", 30);

        Name = "Кентавр-лучник";
        SpriteName = "Centaur";

        ManeuverSkill = new ActiveSkill(PreManeuver, Maneuver, "Маневр", "Maneuver", "Перемещается на одну из соседних клеток.");
        this.Skills.AddSkill(ManeuverSkill);
        ManeuverSkill.CD = ManeuverCD;
        
    }

    private ActiveSkill ManeuverSkill;
    CoolDown ManeuverCD = new CoolDown(2);
    private void PreManeuver()
    {
        CagePack pack = new CagePack();
        if (Cage.Left(Team) != null && !Cage.Left(Team).HasCreature)
            pack.Add(Cage.Left(Team));
        if (Cage.Right(Team) != null && !Cage.Right(Team).HasCreature)
            pack.Add(Cage.Right(Team));
        MapChooseCages.Get.ActivateL(ManeuverSkill.EndAction, pack, new List<Cage>());
    }

    private void Maneuver(List<Cage> cages)
    {
        this.ReplaceTo(cages[0]);
        ManeuverCD.Update();
    }

}