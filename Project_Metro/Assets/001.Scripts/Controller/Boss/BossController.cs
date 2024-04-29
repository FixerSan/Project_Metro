using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossController : Actor
{
    public override void Death()
    {
        throw new System.NotImplementedException();
    }

    public abstract void Init();
}
