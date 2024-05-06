using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour, IHitable
{
    public ActorStatus status;
    public Dictionary<System.Type, Coroutine> coroutines = new Dictionary<System.Type, Coroutine>();
    public Transform groundCheckTrans;
    public Rigidbody2D rb;
    public Animator anim;

    public bool IsGround { get { return CheckIsGround(); } }

    public Define.Direction currentDirection;
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

    public string Tag { get { return gameObject.tag; } }

    public virtual void Hit(int _damage)
    {
        status.nowHP -= _damage;
        if (status.nowHP <= 0)
            Death();
    }

    public abstract void Death();

    private bool CheckIsGround()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(groundCheckTrans.position, groundCheckTrans.localScale.x);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if (collider2Ds[i].CompareTag("Ground"))
            {
                anim.SetBool("IsGround", true);
                return true;
            }
        }
        anim.SetBool("IsGround", false);
        return false;
    }

    //public new Coroutine StartCoroutine(IEnumerator _enumerator)
    //{
    //    if(coroutines.ContainsKey(_enumerator.GetType()))
    //        StopCoroutine(_enumerator);

    //    coroutines.Add(_enumerator.GetType(), base.StartCoroutine(_enumerator));
    //    return coroutines[_enumerator.GetType()];
    //}

    //public new void StopCoroutine(IEnumerator _enumerator)
    //{
    //    if (coroutines.ContainsKey(_enumerator.GetType()))
    //    {
    //        base.StopCoroutine(coroutines[_enumerator.GetType()]);
    //        coroutines.Remove(_enumerator.GetType());
    //    }
    //}
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

    public float defaultJumpForce;
    public float plusJumpForce;

    public float CurrentJumpForce
    {
        get
        {
            return defaultJumpForce + plusJumpForce;
        }
    }

    public int defaultDamageForce;
    public int plusDamageForce;

    public int CurrentDamageForce
    {
        get
        {
            return defaultDamageForce + plusDamageForce;
        }
    }

    public float defaultAttackSpeed;
    public float plusAttackSpeed;
    public float CurrentAttackSpeed
    {
        get
        {
            return defaultAttackSpeed + plusAttackSpeed;
        }
    }

    public float attackCooltime;

    public ActorStatus()
    {
        defaultSpeed = 3;
        defaultJumpForce = 3;
    }
}
