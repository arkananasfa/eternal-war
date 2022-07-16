using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InterfaceUpdator : MonoBehaviour
{

    public void UpdateInterface()
    {
        var f = Interface.Get;

        //////////////////////////////////// UPDATE CREATURE'S INTERFACE ////////////////////////////////////
        Creature cr = Game.Get.CurrentCreature;        

        if (cr != null && !cr.Dead)
        {

            //Statistic's interface options
            f.StatsPanel.SetActive(true);
            f.CreatureImage.sprite = cr.MapObject.transform.GetChild(0).GetComponent<Image>().sprite;
            f.CreatureImageBack.color = cr.Team == Team.C ? Color.blue : Color.red;
            f.HPText.text = cr.HP.ToString();
            f.DamageText.text = cr.Damage.Value.ToString();
            f.ArmorText.text = cr.Armor.ToString();
            f.ResistText.text = cr.Resist.ToString();
            f.DistanceText.text = cr.AttackDistance.ToString();
            f.NameText.text = cr.Name;
            f.AttackButton.Set(cr.AttackIconSprite, cr.AttackName, cr.AttackDescription);

            //Set damage's icon
            switch (cr.Damage.Type)
            {
                case DamageType.Physical:
                    f.DamageImage.sprite = f.PhysicalDamageSprite;
                    break;
                case DamageType.Magical:
                    f.DamageImage.sprite = f.MagicalDamageSprite;
                    break;
                default:
                    f.DamageImage.sprite = f.ClearDamageSprite;
                    break;
            }
            switch (cr.Damage.Range)
            {
                case RangeType.Melee:
                    f.DamageRangeImage.sprite = f.MeleeDamageSprite;
                    break;
                case RangeType.Range:
                    f.DamageRangeImage.sprite = f.RangeDamageSprite;
                    break;
                default:
                    f.DamageRangeImage.sprite = f.MeleeDamageSprite;
                    break;
            }

            //Attack & Move
            f.MoveButton.interactable = cr.CanMoveForward() && f.CanBeInterctible(cr);
            f.AttackButton.interactable = cr.CanAttack() && f.CanBeInterctible(cr);

            //Skills

            //Clear buttons
            foreach (var skillButton in f.ActiveSkills)
            {
                skillButton.onClick.RemoveAllListeners();
                skillButton.gameObject.SetActive(false);
            }

            SkillPack skills = cr.Skills;
            int count = skills.ActiveSkillCount;
            for (int i = 0; i < count; i++)
            {
                ActiveSkill skill = skills.ActiveSkills[i];
                f.ActiveSkills[i].SkillInfoPanel.SetActive(true);
                f.ActiveSkills[i].onClick.AddListener(skill.ButtonAction);
                f.ActiveSkills[i].Set(skill.SkillSprite, skill.SkillName, skill.Description, skill.CD);
                f.ActiveSkills[i].interactable = skill.CanUse();
                f.ActiveSkills[i].SkillInfoPanel.SetActive(false);
                f.ActiveSkills[i].gameObject.SetActive(true);
            }

            foreach (var skillButton in f.PassiveSkills)
            {
                skillButton.gameObject.SetActive(false);
            }

            count = skills.PassiveSkills.Count;
            for (int i = 0; i < count; i++)
            {
                PassiveSkill skill = skills.PassiveSkills[i];
                f.PassiveSkills[i].gameObject.SetActive(true);
                f.PassiveSkills[i].Set(skill.SkillSprite, skill.SkillName, skill.Description, skill.CD);
            }

        }
        //////////////////////////////////// UPDATE CREATURE'S INTERFACE ////////////////////////////////////
       
        //////////////////////////////////// UPDATE TEAM'S INTERFACE ////////////////////////////////////////

        // Display the data
        f.GoldTeam1.text = Team.Team1.Gold.ToString();
        f.GoldTeam2.text = Team.Team2.Gold.ToString();
        f.BaseHPTeam1.text = Team.Team1.BaseHP.ToString();
        f.BaseHPTeam2.text = Team.Team2.BaseHP.ToString();

        f.NextMoveButton.interactable = true;

        //////////////////////////////////// UPDATE TEAM'S INTERFACE ////////////////////////////////////////

        //Clear Squares for choosing cages
        MapChooseCages.Get.ClearChooseCages();
    }

    public void UpdateUnitsInterface()
    {
        foreach (Creature cr in Team.Team1.Creatures.Pack)
        {
            cr.MapObject.UpdateInterface();
        }
        foreach (Creature cr in Team.Team2.Creatures.Pack)
        {
            cr.MapObject.UpdateInterface();
        }
    }

    #region Singleton
    public static InterfaceUpdator Get { get; set; }

    private void Awake()
    {
        Get = this;
    }
    #endregion

}