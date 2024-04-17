using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenObstacle : Obstacle
{
    public Animator anim;
    public override void Work()
    {
        WorkEffect();
        anim.Play("Open");
    }
}
