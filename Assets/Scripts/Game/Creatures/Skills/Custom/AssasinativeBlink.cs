using System.Collections.Generic;

class AssasinativeBlink : ActiveSkill
{

	private static int blinkRadius = 2;

	public AssasinativeBlink() : base("Прыжок ассасина", "AssasinativeBlink", $"Ассасин исчезает и появлятся за спиной у врага, в радиусе {blinkRadius}.")
	{
		CanUseAdditional += CanUseSkill;
		CD = new CoolDown(4);
		CD.Set(2);

		Directed = true;
		StartPoint = PreBlink;
		EndPoint = Blink;
	}

	public bool CanUseSkill ()
	{
		return GetUsableCages().SelectedCages.Count != 0;
	}

	private CagePack GetUsableCages ()
	{
		Cage oCage = Owner.Cage;
		CagePack pack = new CagePack();

		for (int y = -blinkRadius; y <= blinkRadius; y++) 
		{
			for (int x = -blinkRadius; x <= blinkRadius; x++)
			{
				if (y == 0 && x == 0)
					continue;

				Cage c = Cage.CageExist(x + oCage.X, y + oCage.Y);

				if (c == null)
					continue;

				Cage bc = c.Front(Owner.Team);

				if (c != null && c.HasCreature && !Owner.OneTeam(c.Creature) && bc != null && !bc.HasCreature && bc.Y + 1 != Owner.Team.Opposite.BuyLine)
					pack.Add(c);
			}
		}
		return pack;
	}

	private void PreBlink ()
	{
		CagePack pack = GetUsableCages();
		MapChooseCages.Get.ActivateL(Blink, pack, new List<Cage>());
	}

	private void Blink (List<Cage> cages)
	{
		Owner.ReplaceToWithoutAnimation(cages[0].Front(Owner.Team));
		Owner.SetToPosition();
		Interface.Get.UpdateInterface();
		CD.Update();
	}
	

}