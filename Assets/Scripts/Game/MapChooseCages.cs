using UnityEngine;
using System.Collections.Generic;

public class MapChooseCages : MonoBehaviour
{

    public delegate void _ChooseDelegate(Cage c);

    public event _ChooseDelegate Choose;

    public delegate void _ChooseDelegateL(List<Cage> cages);

    public event _ChooseDelegateL ChooseL;

    public List<Cage> PreCages;

    public void ActivateChooseCages(CagePack pack)
    {
        foreach (Cage c in pack.SelectedCages)
        {
            GameObject go = Instantiate(Archive.Get.ChooseCagePrefab, Archive.Get.MapObject.transform);
            go.GetComponent<ChooseCage>().SelectedCage = c;
            go.GetComponent<RectTransform>().localPosition = Map.Get.GetCageLocation(c);
        }
    }

    public void ActivateChooseCagesL(CagePack pack)
    {
        foreach (Cage c in pack.SelectedCages)
        {
            GameObject go = Instantiate(Archive.Get.ChooseCagePrefabNew, Archive.Get.MapObject.transform);
            go.GetComponent<ChooseCage>().SelectedCage = c;
            go.GetComponent<RectTransform>().localPosition = Map.Get.GetCageLocation(c);
        }
    }

    public void ClearChooseCages()
    {
        GameObject[] goes = GameObject.FindGameObjectsWithTag("ChooseCage");
        foreach (GameObject go in goes)
        {
            Destroy(go);
        }
    }

    public void Activate(_ChooseDelegate func, CagePack pack)
    {
        if (pack.SelectedCages.Count != 0)
        {
            ClearChooseCages();
            Choose = func;
            ActivateChooseCages(pack);
        }
    }

    public void ActivateL(_ChooseDelegateL func, CagePack pack, List<Cage> pre)
    {
        if (pack.SelectedCages.Count != 0)
        {
            ClearChooseCages();
            PreCages = pre;
            ChooseL = func;
            ActivateChooseCagesL(pack);
        }
    }

    public void UseChoose(Cage c)
    {
        ClearChooseCages();
        Choose(c);
    }

    public void UseChooseL(Cage c)
    {
        ClearChooseCages();
        PreCages.Add(c);
        ChooseL(PreCages);
    }

    //Singleton
    public static MapChooseCages Get;

    private void Start()
    {
        Get = this;
    }

}