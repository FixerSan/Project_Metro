using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitTrigger : MonoBehaviour, IHitable
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
        if (HP <= 0)
            TriggerEffect();
    }

    public abstract void TriggerEffect();
}
