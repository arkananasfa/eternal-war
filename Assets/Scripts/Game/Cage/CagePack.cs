using System.Collections.Generic;

public class CagePack 
{

    public List<Cage> SelectedCages { get; set; }

    public CagePack()
    {
        SelectedCages = new List<Cage>();
    }

    public Cage Add(Cage c)
    {
        SelectedCages.Add(c);
        return c;
    }

    public CagePack Remove(Cage c)
    {
        SelectedCages.Remove(c);
        return this;
    }

}