using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreatureObject : MonoBehaviour
{

    public GameObject HPLine { get { return transform.GetChild(1).GetChild(0).gameObject; } }
    public GameObject CanAttackImage { get { return transform.GetChild(2).gameObject; } }
    public GameObject CanMoveImage { get { return transform.GetChild(3).gameObject; } }

    public Creature Creature;

    public void SetImage(Sprite sprite)
    {
        transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        GetComponent<Image>().color = Creature.Team.CreaturesColor;
    }

    public void SetPosition(int x, int y)
    {
        GetComponent<RectTransform>().localPosition = Map.Get.GetCageLocation(x, y);
    }

    public void Choose()
    {
        Game.Get.CurrentCreature = Creature;
        Interface.Get.ChooseCreature(Creature);
    }

    public void CreateMoveAnimation(AnimationData data, Damage dmg)
    {
        var GO = Instantiate(Archive.Get.AttackAnimationPrefab, Archive.Get.MapObject.transform);
        var anim = GO.AddComponent<MoveAttackAnimation>();
        anim.Data = data;
        anim.StartAnimation(data, dmg);
    }

    public void PastAttackEffect()
    {
        Creature.PastAttackEffect();
    }

    public void UpdateInterface()
    {
        //Update HP line
        float percent = System.Convert.ToSingle(Creature.HP / Creature.MaxHP) * 100f;
        HPLine.GetComponent<RectTransform>().localPosition = new Vector2((100-percent)*0.5f*-1, 0); 
        HPLine.GetComponent<RectTransform>().sizeDelta = new Vector2(percent, 6);

        //Update CanAttack image
        if (!Creature.CurrentMove())
            CanAttackImage.SetActive(false);
        else
            CanAttackImage.SetActive(true);

        if (Creature.CanAttack())
            CanAttackImage.GetComponent<Image>().color = Color.green;
        else
            CanAttackImage.GetComponent<Image>().color = Color.red;

        //Update CanMove image
        if (!Creature.CurrentMove())
            CanMoveImage.SetActive(false);
        else
            CanMoveImage.SetActive(true);

        if (Creature.CanMoveForward())
            CanMoveImage.GetComponent<Image>().color = Color.green;
        else
            CanMoveImage.GetComponent<Image>().color = Color.red;
    }
    
}