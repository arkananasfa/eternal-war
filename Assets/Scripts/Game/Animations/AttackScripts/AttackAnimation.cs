using UnityEngine;
using UnityEngine.UI;

public class AttackAnimation : BaseAnimation
{

    public void StartCustomAnimation(string animName, Vector3 from, Vector3 to, AnimationOrientation orientation)
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("CreaturesAttacks\\" + animName);
        GetComponent<RectTransform>().localPosition = from;

        switch (orientation)
        {
            case AnimationOrientation.Full:
                transform.up = to - from;
                break;
            case AnimationOrientation.Vertical:
                transform.up *= from.y < to.y ? 1 : -1;
                break;
        }
    }

    public override void EndAnimation()
    {
        if (Data.Damagable)
            Data.Defender.GetDamage(Data.Damage);
        if (Data.Attackable)
            Game.Get.AfterAttack(Data.Attacker, Data.Defender as Creature);
        Data.EndAnimation();
        OnEndAnimation?.Invoke();
        Interface.Get.UpdateInterface();
        InterfaceUpdator.Get.UpdateUnitsInterface();
        Destroy(gameObject);
    }

    public AttackAnimation(AnimationData data) : base(data) { }

}

public enum AnimationOrientation
{
    Full,
    Vertical,
    No
}