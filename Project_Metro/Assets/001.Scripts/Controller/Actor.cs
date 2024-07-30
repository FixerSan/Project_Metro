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
    public Define.SoulSkill soulSkill;

    public bool IsGround { get { return CheckIsGround(); } }

    public Define.Direction currentDirection;
    public bool init = false;

    public float defaultGravity;
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

    public int KnockbackLevel { get { return status.CurrentKnockbackLevel; } }

    public virtual void Hit(int _damage, Actor _attacker)
    {
        if (status.currentHP <= 0) return;
        status.currentHP -= _damage;
        Managers.UI.SceneUI?.RedrawUI();
        if (status.currentHP <= 0)
            Death();
    }

    public abstract void Death();
    public abstract void GetEatenSoul();

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

    public void RemoveGravity()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
    }

    public void SetGravity(float _gravityScale)
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = _gravityScale;
    }

    public void InitGravity()
    {
        rb.gravityScale = defaultGravity;
    }

}

[System.Serializable]
public class ActorStatus
{
    public int defaultMaxHP;
    public int plusMaxHP;
    public int CurrentMaxHP { get { return defaultMaxHP + plusMaxHP; } }

    public int currentHP;

    public float defaultSpeed;
    public float plusSpeed;
    public float CurrentSpeed { get { return defaultSpeed + plusSpeed; } }

    public float defaultJumpForce;
    public float plusJumpForce;

    public float CurrentJumpForce { get { return defaultJumpForce + plusJumpForce; } }

    public int defaultDamageForce;
    public int plusDamageForce;

    public int CurrentDamageForce { get { return defaultDamageForce + plusDamageForce; } }

    public float defaultAttackSpeed;
    public float plusAttackSpeed;
    public float CurrentAttackSpeed { get { return defaultAttackSpeed + plusAttackSpeed; } }

    public float attackCooltime;

    public float currentATB;

    public float defaultMaxATB;
    public float plusMaxATB;
    public float CurrentMaxATB { get { return defaultMaxATB + plusMaxATB; } }

    public float defaultRegenerationATBForce;
    public float plusRegenerationATBForce;
    public float CurrentRegenerationATBForce { get { return defaultRegenerationATBForce + plusRegenerationATBForce; } }

    public int defaultKnockBackLevel;
    public int plusKnockBackLevel;

    public int CurrentKnockbackLevel { get{ return defaultKnockBackLevel + plusKnockBackLevel; } }

    public ActorStatus()
    {
        defaultSpeed = 3;
        defaultJumpForce = 3;
        defaultRegenerationATBForce = 0.1f;
        defaultMaxATB = 3;
    }
}
