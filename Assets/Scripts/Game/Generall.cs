public class Generall
{

    // Money settings
    public static int MoneyPerTurn = 35;
    public static int FirstPlayerBonus = 0;
    public static int StartMoney = 10;
    public static int MaxMoney = 100;

    // Game settings
    public static int BaseHP = 60;
    public static GameMode GameMode = GameMode.Singleplayer;

}

public enum GameMode
{
    Singleplayer,
    Multiplayer
}