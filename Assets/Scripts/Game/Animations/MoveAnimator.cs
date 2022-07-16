using System.Collections;
using System;
using UnityEngine;

public class MoveAnimator : MonoBehaviour
{

    public System.Action OnEndAnimation;
    private Vector3 frameStep;
    private Vector3 target;
    private int frameCount;
    private RectTransform trans;
    private float animSpeed;

    public void StartAnimation(Vector3 _target, float _animSpeed)
    {
        target = _target;
        trans = gameObject.GetComponent<RectTransform>();
        Vector3 direction = target - trans.localPosition;
        direction.Normalize();
        animSpeed = _animSpeed;
        frameStep = direction*animSpeed;

        StartCoroutine("Move");
    }

    private IEnumerator Move()
    {
        while ((target - trans.localPosition).magnitude>=animSpeed)
        {
            gameObject.GetComponent<RectTransform>().localPosition += frameStep;
            yield return new WaitForSeconds(0.02f);
        }
        EndAnimation();
    }

    private void EndAnimation()
    {
        trans.localPosition = target;
        OnEndAnimation?.Invoke();
        Interface.Get.UpdateInterface();
        Destroy(this);
    }
}