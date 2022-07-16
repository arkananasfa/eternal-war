using UnityEngine;

public class Archive : MonoBehaviour
{
    // Creature Create Info
    public GameObject CreaturePrefab;
    public GameObject CreatureParent;
    public GameObject AttackAnimationPrefab;
    public GameObject ChooseCagePrefab;
    public GameObject ChooseCagePrefabNew;
    public GameObject MapObject;
    public GameObject EffectIconPrefab;
    
    //Singleton
    private void Start()
    {
        Get = this;
    }

    public static Archive Get { get; set; }

}