using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestKnight_DownAttack : MonoBehaviour
{
    public float usingTime;
    public BossController bossController;
    public void Attack(BossController _bossController, Action _endCallback)
    {
        bossController = _bossController;
        StartCoroutine(AttackRoutine(_endCallback));
    }

    private IEnumerator AttackRoutine(Action _endCallback)
    {
        yield return new WaitForSeconds(usingTime);
        _endCallback.Invoke();
        Managers.Resource.Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            collision.GetComponent<IHitable>().Hit(bossController.status.CurrentDamageForce, bossController);
    }
}
