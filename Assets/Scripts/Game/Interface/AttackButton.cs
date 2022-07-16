
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackButton : Button, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject SkillInfoPanel;
    private Image Icon { get { return transform.GetChild(0).GetComponent<Image>(); } }
    private GameObject HeaderPanel { get { return SkillInfoPanel.transform.GetChild(0).gameObject; } }
    private Image SkillIcon { get { return HeaderPanel.transform.GetChild(0).GetComponent<Image>(); } }
    private Text SkillName { get { return HeaderPanel.transform.GetChild(1).GetComponent<Text>(); } }
    private Text SkillDescription { get { return SkillInfoPanel.transform.GetChild(1).GetComponent<Text>(); } }

    public void Set(Sprite icon, string name, string description)
    {
        Icon.sprite = icon;
        SkillIcon.sprite = icon;
        SkillName.text = name;
        SkillDescription.text = description;
        SkillDescription.text += " ";
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        SkillDescription.text += " ";
        SkillInfoPanel.SetActive(true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        SkillInfoPanel.SetActive(false);
    }

}