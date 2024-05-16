using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Hit : Trigger, IHitable
{
    public string Tag { get { return gameObject.tag; } }
    public int HP { get { return hp; } set { hp = value; } }

    public int KnockbackLevel { get { return knockbackLevel; } }

    public int knockbackLevel;

    public int hp;

    public void Hit(int _damage, Actor _attacker)
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
