using UnityEngine;

public abstract class BaseAnimation : MonoBehaviour
{

    public AnimationData Data;

    public delegate void EndAnimationDelegate();
    public EndAnimationDelegate OnEndAnimation { get; set; }

    public BaseAnimation(AnimationData data)
    {
        Data = data;
    }

    public abstract void EndAnimation();
    
}