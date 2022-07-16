using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner
{
    public static CreatureType CreatureSpawnType { get; set; }
    public static int CreatureSpawnPrice { get; set; }

    public static void ChooseCagesBuy(CreatureType type, CagePack pack, int price)
    {
        CreatureSpawnType = type;
        CreatureSpawnPrice = price;
        MapChooseCages.Get.Activate(BuyCreature, pack);
    }
    public static void BuyCreature(Cage c)
    {
        if (Generall.GameMode == GameMode.Multiplayer)
        {
            Action a = new Action(c, CreatureSpawnType);
            NetworkManager.Get.SendAction(a);
        }
        Team.C.Gold -= CreatureSpawnPrice;
        Game.Get.CurrentTeam.SpawnCreature(CreatureSpawnType, c);      
    }

    public static void BuyCreatureWithoutMultiplayer(Cage c)
    {
        Team.C.Gold -= CreatureSpawnPrice;
        Game.Get.CurrentTeam.SpawnCreature(CreatureSpawnType, c);
    }

}