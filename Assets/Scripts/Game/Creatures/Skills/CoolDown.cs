public class CoolDown
{
    public int CD { get; set; }

    public int StartCD { get; set; }

    public bool CanUse()
    {
        return CD == 0;
    }

    public void Decrease()
    {
        if (CD != 0)
            CD--;
    }

    public void Set(int i)
    {
        CD = i;
    }

    public void Update()
    {
        CD = StartCD;
    }

    public CoolDown(int cd)
    {
        StartCD = cd;
        CD = cd;
    }
}