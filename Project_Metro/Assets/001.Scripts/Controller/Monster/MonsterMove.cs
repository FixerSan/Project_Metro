using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterMove
{
    public MonsterController controller;
    public abstract bool CheckMove();
    public abstract void Move();

    public abstract bool CheckFollow();
    public abstract void Follow();
    public virtual void Stop()
    {
        controller.rb.velocity = new Vector2(0, controller.rb.velocity.y);
    }
}

namespace MonsterMovemets
{
    public class Test : MonsterMove
    {
        public Test(MonsterController _controller)
        {
            controller = _controller;
        }

        public override bool CheckFollow()
        {
            if (controller.status.attackCooltime > 0) return false;

            if (controller.attackTarget != null)
            {
                controller.ChangeState(Define.MonsterState.Follow);
                return true;
            }

            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(controller.detectRangeTrans.position, controller.detectRangeTrans.localScale, 0);
            for (int i = 0; i < collider2Ds.Length; i++)
            {
                if (collider2Ds[i].CompareTag("Player"))
                {
                    controller.attackTarget = collider2Ds[i].GetComponent<PlayerController>();
                    controller.ChangeState(Define.MonsterState.Follow);
                    return true;
                }
            }
            return false;
        }

        public override bool CheckMove()
        {
            return false;
        }

        public override void Follow()
        {
            if (controller.attackTarget.transform.position.x - controller.transform.position.x <= 0) controller.ChangeDirection(Define.Direction.Left);
            else if (controller.attackTarget.transform.position.x - controller.transform.position.x > 0) controller.ChangeDirection(Define.Direction.Right);

            controller.rb.velocity = new Vector2((int)controller.currentDirection * controller.status.CurrentSpeed, controller.rb.velocity.y);
        }

        public override void Move()
        {

        }
    }
}