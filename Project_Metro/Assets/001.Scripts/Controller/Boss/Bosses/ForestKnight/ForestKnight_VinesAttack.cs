using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestKnight_VinesAttack : MonoBehaviour
{
    private bool isAttack = false;
    private GameObject thorn;

    public float firstAttackDelay;
    public float secondAttackDelay;

    public float destroyDelay;

    private Collider2D coll_FirstAttack;

    private BossController controller;

    private void OnEnable()
    {
        thorn = Util.FindChild<Transform>(gameObject, "Sprite_Thorn", true).gameObject;
        coll_FirstAttack = Util.FindChild<Collider2D>(gameObject, "Sprite_Vine", true);
        thorn.SetActive(false);
        coll_FirstAttack.enabled = false;
    }

    public void Attack(BossController _controller)
    {
        isAttack = true;
        controller = _controller;
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(firstAttackDelay);
        coll_FirstAttack.enabled = true;
        yield return new WaitForSeconds(secondAttackDelay);
        thorn.SetActive(true);
        yield return new WaitForSeconds(destroyDelay);
        Managers.Resource.Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isAttack) return;
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<IHitable>().Hit(controller.status.CurrentDamageForce);
            Debug.Log("°ø°Ý µÊ");
        }
    }

    public void OnDisable()
    {
        isAttack = false;
    }
}
