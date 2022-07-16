using UnityEngine;
using System.Collections.Generic;

public class Team : MonoBehaviour
{

    public static Team Team1 { get; set; }
    public static Team Team2 { get; set; }
    public static Team C { get { return Game.Get.CurrentTeam; } }
    public static Team E { get { return C == Team1 ? Team2 : Team1; } }
    public static Team M { get; set; }
    public static Team V { get { return GameObject.Find("ViewTeam").GetComponent<Team>(); } }

    public CreaturePack Creatures { get; set; }

    // Data
    private int gold;
    public int Gold
    {
        get { return gold; }
        set
        {
            if (value > 10000) gold = value;
            else if (value < 0) gold = 0;
            else if (value > Generall.MaxMoney) gold = Generall.MaxMoney;
            else gold = value;
        }
    }
    private int baseHP;
    public int BaseHP
    {
        get { return baseHP; }
        set
        {
            if (value < 0) baseHP = 0;
            else baseHP = value;
        }
    }

    // In editor
    public string Name;
    public int Forward;
    public int BuyLine;
    public Color CreaturesColor;
    public Team Opposite { get; set; }

    private void Start()
    {
        Creatures = new CreaturePack();
        Creatures.Pack = new List<Creature>();
        if (Generall.GameMode == GameMode.Multiplayer)
        {
            M = NetworkManager.Get.TeamIndex == 1 ? Team1 : Team2;
        }
    }

    public void Win()
    {
        Interface.Get.Win(this);
    }

    public Creature SpawnCreature(CreatureType type, Cage cage)
    {
        Creature cr = CreaturesFactory.Get.CreateCreature(type, cage, this);
        cr.CheckBaseDamage();
        InterfaceUpdator.Get.UpdateUnitsInterface();
        return cr;
    }

    public static void ChangeTeam()
    {
        Game.Get.CurrentTeam = E;
    }

    public static bool MultiplayerCanMove()
    {
        return Generall.GameMode == GameMode.Singleplayer || (M == C);
    }

}