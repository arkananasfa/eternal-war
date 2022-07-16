public class Cage : EWEventsObject
{

    #region Coordinates
    public Creature Creature { get; set; }

    public bool HasCreature
    {
        get
        {
            return Creature != null;
        }
    }

    public int X { get; set; }
    public int Y { get; set; }
    #endregion

    #region Sides
    public Cage Front(Team t)
    {
        return CageExist(X, Y + t.Forward);
    }

    public Cage Front(Team t, int len)
    {
        return CageExist(X, Y + t.Forward * len);
    }

    public Cage Right(Team t)
    {
        return CageExist(X + t.Forward, Y);
    }

    public Cage Right(Team t, int len)
    {
        return CageExist(X + t.Forward * len, Y);
    }

    public Cage Back(Team t)
    {
        return CageExist(X, Y - t.Forward);
    }

    public Cage Back(Team t, int len)
    {
        return CageExist(X, Y - t.Forward * len);
    }

    public Cage Left(Team t)
    {
        return CageExist(X - t.Forward, Y);
    }

    public Cage Left(Team t, int len)
    {
        return CageExist(X - t.Forward * len, Y);
    }
    #endregion

    #region Get cage
    public static Cage CageExist(int x, int y)
    {
        if (x < 8 && x > -1 && y < 8 && y > -1)
            return Map.Get.Cages[x, y];
        else return null;
    }

    public static bool IsCageExist(int x, int y)
    {
        return (x < 8 && x > -1 && y < 8 && y > -1);
    }
    #endregion

    #region Constructor
    public Cage()
    {
        Creature = null;
    }
    #endregion

    public override string ToString()
    {
        return "Cage: {\n X: " + X.ToString() + ",\nY: " + Y.ToString() + "\n}";
    }

}