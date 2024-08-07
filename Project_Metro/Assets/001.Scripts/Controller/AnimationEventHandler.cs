using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public Action<int> animationAction;

    public void AnimationEvent(int i)
    {
        animationAction?.Invoke(i);
    }
}
