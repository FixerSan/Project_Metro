using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTrigger : Trigger, IHitable
{
    public string Tag { get { return gameObject.tag; } }
    public int HP { get { return hp; } set { hp = value; } }
    public int hp;

    public void Hit(int _damage)
    {
        HP -= _damage;
        CheckHP();
    }

    public void CheckHP()
    {
        if (isWorked && !canWorkAgain) return;
        if (HP <= 0)
            WorkTrigger();
    }
}
