public class MoveAttackAnimation : AttackAnimation
{

    public void StartAnimation(AnimationData data, Damage dmg)
    {
        StartCustomAnimation(dmg.AnimationType, data.From, data.To, dmg.AnimOrientation);
        var moveAnim = gameObject.AddComponent<MoveAnimator>();
        moveAnim.OnEndAnimation = EndAnimation;
        moveAnim.StartAnimation(data.To, dmg.AnimationSpeed);
    }

    public MoveAttackAnimation(AnimationData data) : base (data) {}

}