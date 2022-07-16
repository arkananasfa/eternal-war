using UnityEngine;

public class Starter : MonoBehaviour
{
    private void Start()
    {
        //Set Teams
        Team.Team1 = GameObject.Find("Team1").GetComponent<Team>();
        Team.Team2 = GameObject.Find("Team2").GetComponent<Team>();

        Team.Team1.Gold = Generall.StartMoney + Generall.FirstPlayerBonus + Generall.MoneyPerTurn;
        Team.Team2.Gold = Generall.StartMoney;
        
        Team.Team1.BaseHP = Generall.BaseHP;
        Team.Team2.BaseHP = Generall.BaseHP;

        Team.Team1.Opposite = Team.Team2;
        Team.Team2.Opposite = Team.Team1;

        Game.Get.CurrentTeam = Team.Team1;

        Interface.Get.StatsPanel.SetActive(false);
        Interface.Get.CreatureImage.sprite = Interface.Get.Nothing;
        InterfaceUpdator.Get.UpdateInterface();
    }
}