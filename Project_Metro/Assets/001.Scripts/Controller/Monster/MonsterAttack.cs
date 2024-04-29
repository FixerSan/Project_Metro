using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering.Universal;

public abstract class MonsterAttack
{
    public MonsterController controller;

    public abstract void Attack();
    public abstract bool CheckAttack();
}

namespace MonsterAttacks
{
    public class Test : MonsterAttack
    {
        public Test(MonsterController _controller)
        {
            controller = _controller;
        }

        public override void Attack()
        {
            controller.status.attackCooltime = 1f / controller.status.CurrentAttackSpeed;
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(controller.attackTrans.position, controller.attackTrans.localScale, 0);
            IHitable player = null;
            for (int i = 0; i < collider2Ds.Length; i++)
            {
                player = collider2Ds[i].GetComponent<IHitable>();
                if (player != null && player.Tag == "Player")
                {
                    player.Hit(controller.status.CurrentDamageForce);
                }
            }
        }

        public override bool CheckAttack()
        {
            if (controller.attackTarget == null) return false;

            if(controller.status.attackCooltime > 0)
            {
                controller.status.attackCooltime -= Time.deltaTime;
                return false;
            }

            if(Vector2.Distance(controller.attackTarget.transform.position, controller.transform.position) <= controller.data.canAttackDistance)
            {
                controller.ChangeState(Define.MonsterState.Attack);
                return true;
            }
            return false;
        }
    }
}
