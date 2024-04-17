using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour 
{
    public bool canWorkAgain;
    public int index;

    public abstract void Work();
    public void WorkEffect()
    {
        Managers.Obstacle.WorkObstacle(index);
    }
}
