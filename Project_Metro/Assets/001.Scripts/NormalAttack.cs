using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class NormalAttack : MonoBehaviour
{
    public Actor controller;
    public Transform attackRangeTrans;
    public Coroutine endCoroutine;

    public float endTime = 0.3f;

    private Transform attackPos;
    private bool init = false;

    public void Init(Actor _controller, Transform _attackPos)
    {
        controller = _controller;
        attackPos = _attackPos;
        transform.position = attackPos.position;
        endCoroutine = StartCoroutine(EndRoutine());
        init = true;
    }

    public void Attack(Action<bool> _callback)
    {
        if (!init) return;
        //TODO :: 애니메이션 실행
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackRangeTrans.position, attackRangeTrans.localScale, 0);

        IHitable hitter = null;
        for (int i = 0; i < collider2Ds.Length; i++) 
        {
            hitter = collider2Ds[i].GetComponent<IHitable>();
            if(hitter != null)
            {
                if (hitter.Tag == "Player") continue;
                hitter.Hit(controller.status.CurrentDamageForce);
            }
        }

        _callback?.Invoke(hitter != null);
    }

    public IEnumerator EndRoutine()
    {
        yield return new WaitForSeconds(endTime);
        Managers.Resource.Destroy(this.gameObject);
    }

    private void Update()
    {
        if (!init) return;
        transform.position = attackPos.position;
    }

    public void OnDisable()
    {
        controller = null;
        attackPos = null;
        endCoroutine = null;

        init = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackRangeTrans.position, attackRangeTrans.localScale);
    }
}


