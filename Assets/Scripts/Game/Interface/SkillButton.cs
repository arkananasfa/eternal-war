using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButton : Button, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject SkillInfoPanel;
    private Image Icon { get { return transform.GetChild(0).GetChild(0).GetComponent<Image>(); } }
    private Text IconCD { get { return transform.GetChild(1).GetComponent<Text>(); } }
    private GameObject HeaderPanel { get { return SkillInfoPanel.transform.GetChild(0).gameObject; } }
    private Image SkillIcon { get { return HeaderPanel.transform.GetChild(0).GetComponent<Image>(); } }
    private Text SkillName { get { return HeaderPanel.transform.GetChild(1).GetComponent<Text>(); } }
    private Text SkillCD { get { return HeaderPanel.transform.GetChild(2).GetComponent<Text>(); } }
    private Text SkillDescription { get { return SkillInfoPanel.transform.GetChild(1).GetComponent<Text>(); } }

    public void Set(Sprite icon, string name, string description, CoolDown cd)
    {
        Icon.sprite = icon;
        SkillIcon.sprite = icon;
        SkillName.text = name;
        SkillDescription.text = description;
        SkillDescription.text += " ";
        IconCD.text = cd.CD < 2 ? "" : System.Math.Floor(cd.CD / 2.0m).ToString();
        SkillCD.text = System.Math.Floor(cd.CD / 2.0m) + "/" + System.Math.Floor(cd.StartCD / 2.0m);
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