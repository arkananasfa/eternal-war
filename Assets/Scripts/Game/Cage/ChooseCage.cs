using UnityEngine;
using System.Collections.Generic;

public class ChooseCage : MonoBehaviour
{

    public Cage SelectedCage;

    public void DoAction()
    {
        MapChooseCages.Get.UseChoose(SelectedCage);
    }

    public void DoActionL()
    {
        MapChooseCages.Get.UseChooseL(SelectedCage);
    }

}