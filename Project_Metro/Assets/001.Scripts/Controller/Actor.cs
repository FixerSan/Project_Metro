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
            return status.currentHP;
        }
        set 
        {
            status.currentHP = value;
        }
    }

    public string Tag { get { return gameObject.tag; } }

    public virtual void Hit(int _damage)
    {
        if (status.currentHP <= 0) return;
        status.currentHP -= _damage;
        Managers.UI.SceneUI.RedrawUI();
        if (status.currentHP <= 0)
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

    public int currentHP;

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

    public float currentATB;

    public float defaultMaxATB;
    public float plusMaxATB;
    public float CurrentMaxATB
    {
        get
        {
            return defaultMaxATB + plusMaxATB;
        }
    }

    public float defaultRegenerationATBForce;
    public float plisRegenerationATBForce;
    public float CurrentRegenerationATBForce
    {
        get
        {
            return defaultRegenerationATBForce + plisRegenerationATBForce;
        }
    }

    public ActorStatus()
    {
        defaultSpeed = 3;
        defaultJumpForce = 3;
        defaultRegenerationATBForce = 0.1f;
        defaultMaxATB = 3;
    }
}
