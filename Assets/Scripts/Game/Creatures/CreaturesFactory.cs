using UnityEngine;
using UnityEngine.UI;

public class CreaturesFactory : MonoBehaviour
{

    public Creature CreateCreature(CreatureType type, Cage cage)
    {

        Creature cr = ReturnCreatureByType(type);

        // Set new CreatureObject
        var GO = Instantiate(Archive.Get.CreaturePrefab, Archive.Get.CreatureParent.transform);
        cr.MapObject = GO.GetComponent<CreatureObject>();
        cr.MapObject.Creature = cr;

        // Set Position
        cr.MapObject.gameObject.GetComponent<RectTransform>().localPosition = Map.Get.GetCageLocation(cage);

        // Set Sprite
        cr.SetSprite();

        // Set Cage
        cr.Cage = cage;
        cage.Creature = cr;

        // Add creature to team's pack
        Team.C.Creatures.Add(cr);
        cr.Team = Team.C;

        //Setup it for start
        cr.MaxHP = cr.HP;
        cr.AttackPoints = 0;
        cr.MovePoints = 0;
        cr.UpdateUnitInterface();

        // Choose the creature
        Interface.Get.ChooseCreature(cr);

        // Return Prepeared Creature
        return cr;
    }

    public Creature CreateCreature(CreatureType type, Cage cage, Team team)
    {

        Creature cr = ReturnCreatureByType(type);

        // Set new CreatureObject
        var GO = Instantiate(Archive.Get.CreaturePrefab, Archive.Get.CreatureParent.transform);
        cr.MapObject = GO.GetComponent<CreatureObject>();
        cr.MapObject.Creature = cr;

        // Set Position
        cr.MapObject.gameObject.GetComponent<RectTransform>().localPosition = Map.Get.GetCageLocation(cage);

        // Set Cage
        cr.Cage = cage;
        cage.Creature = cr;

        // Add creature to team's pack
        team.Creatures.Add(cr);
        cr.Team = team;

        // Set Sprite
        cr.SetSprite();

        //Setup it for start
        cr.MaxHP = cr.HP;
        cr.AttackPoints = 0;
        cr.MovePoints = 0;
        cr.UpdateUnitInterface();

        // Choose the creature
        Interface.Get.ChooseCreature(cr);

        // Return Prepeared Creature
        return cr;
    }

    public Creature ReturnCreatureByType(CreatureType type)
    {
        switch (type)
        {
            case CreatureType.AngryRider:
                return new AngryRider();
            case CreatureType.Archimage:
                return new Archimage();
            case CreatureType.Assasin:
                return new Assasin();
            case CreatureType.BattleMage:
                return new BattleMage();
            case CreatureType.Berserk:
                return new Berserk();
            case CreatureType.BlueHeck:
                return new BlueHeck();
            case CreatureType.Bowman:
                return new Bowman();
            case CreatureType.Builder:
                return new Builder();
            case CreatureType.BuildersTower:
                return new BuildersTower();
            case CreatureType.Catapult:
                return new Catapult();
            case CreatureType.Centaur:
                return new Centaur();
            case CreatureType.Driad:
                return new Driad();
            case CreatureType.Druid:
                return new Druid();
            case CreatureType.DruidsBear:
                return new DruidsBear();
            case CreatureType.ElectricDragon:
                return new ElectricDragon();
            case CreatureType.Goblin:
                return new Goblin();
            case CreatureType.GreenHeck:
                return new GreenHeck();
            case CreatureType.HellHound:
                return new HellHound();
            case CreatureType.IceMage:
                return new IceMage();
            case CreatureType.Invoker:
                return new Invoker();
            case CreatureType.Lancer:
                return new Lancer();
            case CreatureType.Lich:
                return new Lich();
            case CreatureType.Monk:
                return new Monk();
            case CreatureType.Necromancer:
                return new Necromancer();
            case CreatureType.RedHeck:
                return new RedHeck();
            case CreatureType.Rider:
                return new Rider();
            case CreatureType.Sceleton:
                return new Sceleton();
            case CreatureType.Seraphime:
                return new Seraphime();
            case CreatureType.Swordsman:
                return new Swordsman();
            case CreatureType.Vampire:
                return new Vampire();
            case CreatureType.Swaper:
                return new Swaper();
            case CreatureType.Alchemist:
                return new Alchemist();
            case CreatureType.AlchemistnOgre:
                return new AlchemistnOgre();
            default:
                return new Swordsman();
        }
    }

    public static CreaturesFactory Get;

    private void Start()
    {
        Get = this;
    }

}