using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour, IHitable
{
    public ActorStatus status;
    public bool init = false;
    public virtual int HP 
    {
        get
        {
            return status.nowHP;
        }
        set 
        {
            status.nowHP = value;
        }
    }

    public virtual void Hit(int _damage)
    {
        status.nowHP -= _damage;
        if (status.nowHP <= 0)
            Die();
            
    }

    public abstract void Die();
}

[System.Serializable]
public class ActorStatus
{
    public int defaultMaxHP;
    public int plusMaxHP;
    public int CurrentMaxHP 
    {
        get
        {
            return defaultMaxHP + plusMaxHP;
        }
    }

    public int nowHP;

    public float defaultSpeed;
    public float plusSpeed;
    public float CurrentSpeed 
    {
        get
        {
            return defaultSpeed + plusSpeed;
        }
    }

    public ActorStatus()
    {
        defaultSpeed = 3;
    }
}
