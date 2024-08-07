using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossController : Actor
{
    public BossData data;
    /// <summary>
    /// 0 :: �� �ٱ�
    /// </summary>
    public Dictionary<Define.BossAction, BossAction> bossActions;
    

    public virtual bool Init()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = Util.FindChild<Animator>(gameObject, "Sprite", true); 
        status.currentHP = status.defaultMaxHP;
        init = true;
        return true;
    }

    public override void Death()
    {
        GameObject go = Managers.Resource.Instantiate("Soul");
        go.transform.position = transform.position;
        go.transform.SetParent(transform);
        
        Soul soul = go.GetComponent<Soul>();
        soul.Init(this);
    }

    public void ChangeDirection(Define.Direction _direction)
    {
        if (currentDirection == _direction) return;
        if (_direction == Define.Direction.Left) anim.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        else if (_direction == Define.Direction.Right) anim.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        currentDirection = _direction;
    }

    public void LookAtPlayer()
    { 
        if(Managers.Object.playerController.transform.position.x - transform.position.x >= 0) ChangeDirection(Define.Direction.Right);
        else ChangeDirection(Define.Direction.Left);
    }

    public override void Hit(int _damage, Actor _attacker)
    {
        base.Hit(_damage, _attacker);
    }

    public abstract void CreateEffect();

    public void Stop()
    {

    }
}
