using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public BossMovement move;

    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        move = new BossMovements.One(this);
    }


}
