using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuInterface : MonoBehaviour
{

    public Text MaxMoneyValue;
    public Text StartMoneyValue;
    public Text FirstPlayerBonusValue;
    public Text MoneyPerTurnValue;

    public Text BaseHPValue;

    public void StartSinglePlayer()
    {
        Generall.StartMoney = 1;
        Generall.StartMoney = Convert.ToInt32(StartMoneyValue.text);
        Generall.FirstPlayerBonus = Convert.ToInt32(FirstPlayerBonusValue.text);
        Generall.MaxMoney = Convert.ToInt32(MaxMoneyValue.text);
        Generall.MoneyPerTurn = Convert.ToInt32(MoneyPerTurnValue.text);
        Generall.BaseHP = Convert.ToInt32(BaseHPValue.text);
        Generall.GameMode = GameMode.Singleplayer;
        SceneManager.LoadScene(1);
    }

    public void StartMultiPlayer()
    {
        Generall.StartMoney = 1;
        Generall.StartMoney = Convert.ToInt32(StartMoneyValue.text);
        Generall.FirstPlayerBonus = Convert.ToInt32(FirstPlayerBonusValue.text);
        Generall.MaxMoney = Convert.ToInt32(MaxMoneyValue.text);
        Generall.MoneyPerTurn = Convert.ToInt32(MoneyPerTurnValue.text);
        Generall.BaseHP = Convert.ToInt32(BaseHPValue.text);
        Generall.GameMode = GameMode.Multiplayer;
        SceneManager.LoadScene(2);
    }

    public void ExitButton ()
    {
        Application.Quit();
    }

}