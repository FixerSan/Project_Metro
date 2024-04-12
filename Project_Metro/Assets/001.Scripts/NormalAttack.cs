using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    public Actor controller;
    public Transform attackRangeTrans;
    public Coroutine endCoroutine;

    public float endTime = 1;

    public void Init(Actor _controller)
    {
        controller = _controller;
        endCoroutine = StartCoroutine(EndRoutine());
    }

    public void Attack(Action<bool> _callback)
    {
        //TODO :: 애니메이션 실행
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackRangeTrans.position, attackRangeTrans.localScale, 0);
        IHitable hitter = null;
        for (int i = 0; i < collider2Ds.Length; i++) 
        {
            hitter = collider2Ds[i].GetComponent<IHitable>();
            if(hitter != null)
                hitter.Hit(controller.status.CurrentDamageForce);
        }

        _callback?.Invoke(hitter != null);
    }

    public IEnumerator EndRoutine()
    {
        yield return new WaitForSeconds(endTime);
        Managers.Resource.Destroy(this.gameObject);
    }

    public void OnDisable()
    {
        controller = null;
        endCoroutine = null;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackRangeTrans.position, transform.localScale);
    }
}
