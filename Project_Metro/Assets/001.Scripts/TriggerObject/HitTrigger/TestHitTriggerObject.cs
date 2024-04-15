using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHitTriggerObject : HitTrigger
{
    public override void TriggerEffect()
    {
        Managers.Resource.Destroy(gameObject);
    }
}
