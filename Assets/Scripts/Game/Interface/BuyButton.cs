using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{

    public CreatureType Type;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Buy);
    }

    private void Buy()
    {
        Cage cg = new Cage();
        cg.X = 12;
        cg.Y = 12;
        Creature cr = CreaturesFactory.Get.CreateCreature(Type, cg, Team.V);
        Interface.Get.ViewCreature(cr);
        int price = System.Convert.ToInt32(transform.GetChild(1).GetComponent<Text>().text);
        if (Team.C.Gold >= price && Team.MultiplayerCanMove())
        {
            CagePack pack = new CagePack();
            for (int i = 0; i < 8; i++)
            {
                Cage c = Map.Get.Cages[i, Game.Get.CurrentTeam.BuyLine - 1];
                if (!c.HasCreature)
                    pack.Add(c);
            }
            CreatureSpawner.ChooseCagesBuy(Type, pack, price);
        }
    }

}