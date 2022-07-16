using System.Collections.Generic;
using UnityEngine.UIElements;

class ShadowAttack : ActiveSkill
{

	private static int blinkRadius = 2;

	public ShadowAttack() : base ("Удар с теней", "CreaturesAttacks/ShadowSword",$"Использует уязвимость ничего не подозревающего врага сзади, чтобы нанести ему двойной урон.", true)
	{
		CanUseAdditional += CanUseSkill;
		CD = new CoolDown(4);
		CD.Set(2);

		Directed = false;
		StartPoint = ShadowStrike;
	}

	public bool CanUseSkill ()
	{
		Cage c = Owner.Cage.Back(Owner.Team);
		return c != null && c.HasCreature && !Owner.OneTeam(c.Creature);
	}

	private void ShadowStrike ()
	{
		Creature cr = Owner.Cage.Back(Owner.Team).Creature;
		Owner.GiveDamage(cr, new Damage(2 * Owner.Damage.Value, DamageType.Physical, RangeType.Melee, "ShadowSword", 10));
		Owner.AttackPoints--;
		CD.Update();
	}

}