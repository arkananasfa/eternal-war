using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Interface : MonoBehaviour
{

    #region Elements
    public Creature VCreature { get; set; }

    public Image CreatureImage;
    public Image CreatureImageBack;
    public Text HPText;
    public Text DamageText;
    public Text ArmorText;
    public Text ResistText;
    public Text DistanceText;
    public Text NameText;
    public Image DamageImage;
    public Image DamageRangeImage;
    public GameObject StatsPanel;

    public Text GoldTeam1;
    public Text GoldTeam2;
    public Text BaseHPTeam1;
    public Text BaseHPTeam2;

    public GameObject ResultPanel;
    public Text ResultText;
    public Image ResultImage;

    public Sprite PhysicalDamageSprite;
    public Sprite MagicalDamageSprite;
    public Sprite ClearDamageSprite;
    public Sprite MeleeDamageSprite;
    public Sprite RangeDamageSprite;
    public Sprite Nothing;

    public Button MoveButton;
    public AttackButton AttackButton;
    public Button NextMoveButton;

    public List<SkillButton> ActiveSkills;
    public List<SkillImage> PassiveSkills;

    public List<GameObject> SkillInfoPanels;
    #endregion

    #region Start actions
    private void Start()
    {
        for (int i = 0; i < ActiveSkills.Count; i++)
        {
            ActiveSkills[i].SkillInfoPanel = SkillInfoPanels[i];
        }
        for (int i = 0; i < PassiveSkills.Count; i++)
        {
            PassiveSkills[i].SkillInfoPanel = SkillInfoPanels[ActiveSkills.Count + i];
        }
        AttackButton.SkillInfoPanel = SkillInfoPanels[ActiveSkills.Count+PassiveSkills.Count];
    }
    #endregion

    #region Update interface
    public void UpdateInterface()
    {
        InterfaceUpdator.Get.UpdateInterface();
        InterfaceUpdator.Get.UpdateUnitsInterface();
    }
    #endregion

    #region Chose creature to show
    public void ChangeCreature()
    {
        Creature c = Game.Get.CurrentCreature;
        if (Team.C.Creatures.Pack.Count != 0 && (Team.C.Creatures.Pack.Count != 1 || Team.C.Creatures.Pack[0] != c))
        {
            if (Team.C.Creatures.Pack[0] == c)
                ChooseCreature(Team.C.Creatures.Pack[1]);
            else
                ChooseCreature(Team.C.Creatures.Pack[0]);
        }
        else if (Team.E.Creatures.Pack.Count != 0 && (Team.E.Creatures.Pack.Count != 1 || Team.E.Creatures.Pack[0] != c))
        {
            if (Team.E.Creatures.Pack[0] == c)
                ChooseCreature(Team.E.Creatures.Pack[1]);
            else
                ChooseCreature(Team.E.Creatures.Pack[0]);
        }
        else
            Close();
    }
    public void ChooseCreature(Creature cr)
    {
        Game.Get.CurrentCreature = cr;
        InterfaceUpdator.Get.UpdateInterface();
    }
    public void Close()
    {
        CreatureImage.sprite = Nothing;
        CreatureImageBack.color = Color.blue;
        StatsPanel.SetActive(false);
    }
    #endregion

    #region Button actions
    public void MoveFront()
    {
        if (Game.Get.CurrentCreature.CanMoveForward())
        {
            Game.Get.CurrentCreature.MoveForward();
            InterfaceUpdator.Get.UpdateInterface();
        }
    }
    public void Attack()
    {
        if (Game.Get.CurrentCreature.CanAttack())
        {
            Game.Get.CurrentCreature.PreAttack();
            //UpdateInterface();
        }
    }
    public void NextMove()
    {
        if (Team.MultiplayerCanMove())
        {
            Game.Get.NextMove();
            InterfaceUpdator.Get.UpdateInterface();
        }
    }
    public bool CanBeInterctible(Creature cr)
    {
        return cr.CurrentMove() && Team.MultiplayerCanMove();
    }
    #endregion

    #region Other methods
    public void ViewCreature(Creature cr)
    {
        if (VCreature != null)
        {
            Destroy(VCreature.MapObject.gameObject);
            Cage c = VCreature.Cage;
            VCreature.Cage = null;
            c.Creature = null;
        }
        VCreature = cr;
    }
    public void Win(Team team)
    {
        ResultPanel.SetActive(true);
        ResultText.text = team.Name + " won!";
    }
    #endregion

    #region Singleton
    public static Interface Get { get; set; }
    private void Awake()
    {
        Get = this;
    }
    #endregion

}