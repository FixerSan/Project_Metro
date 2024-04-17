using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacle : Obstacle
{
    public override void Work()
    {
        WorkEffect();
        Managers.Resource.Destroy(gameObject);
    }
}
