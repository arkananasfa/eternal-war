using UnityEngine;

public class Map : MonoBehaviour
{

    public float FirstCageX;
    public float FirstCageY;
    public float TransX;
    public float TransY;

    public int SizeX;
    public int SizeY;

    public Cage[,] Cages;

    public Vector2 GetCageLocation(int x, int y)
    {
        return new Vector2(FirstCageX + x * TransX, FirstCageY + y * TransY);
    }

    public Vector2 GetCageLocation(Cage c)
    {
        return GetCageLocation(c.X, c.Y);
    }

    public static Map Get;

    private void Start()
    {
        Get = this;
        Cages = new Cage[SizeX, SizeY];
        for (int i = 0; i < SizeX; i++)
        {
            for (int j = 0; j < SizeY; j++)
            {
                Cages[i, j] = new Cage();
                Cages[i, j].X = i;
                Cages[i, j].Y = j;
            }
        }
    }

    public CagePack GetLine(int num)
    {
        CagePack pack = new CagePack();
        for (int i = 0; i < SizeX; i++)
            pack.SelectedCages.Add(Cages[i, num-1]);
        return pack;
    }

}