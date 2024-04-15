using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAttack
{
    public PlayerController controller;
    public bool isCanAttack = true;
    public abstract bool CheckAttack();
    public abstract void LeftAttack();
    public abstract void RightAttack();
    public abstract void UpAttack();
    public abstract void DownAttack();
    public abstract IEnumerator AttackRoutine();
}

namespace PlayerAttacks
{

    public class One : PlayerAttack
    {
        public One(PlayerController _controller)
        {
            controller = _controller;
        }

        public override bool CheckAttack()
        {
            if (!isCanAttack) return false ;
            if (Managers.Input.actions.Player.Attack.ReadValue<float>() != 0)
            {
                if(!controller.IsGround)
                {
                    if(Managers.Input.actions.Player.Move.ReadValue<Vector2>().y == 1f)
                    {
                        UpAttack();
                        return true;
                    }

                    else if(Managers.Input.actions.Player.Move.ReadValue<Vector2>().y == -1f)
                    {
                        DownAttack();
                        return true;
                    }
                }


                if(controller.currentDirection == Define.Direction.Left)
                {
                    LeftAttack();
                    return true;
                }

                if (controller.currentDirection == Define.Direction.Right)
                {
                    RightAttack();
                    return true;
                }
            }

            return false;
        }

        public override void LeftAttack()
        {
            isCanAttack = false;
            controller.anim.SetInteger("AttackDirection", (int)Define.PlayerAttackDirection.Left);
            controller.anim.SetTrigger("Attack");
            NormalAttack attack = Managers.Object.SpawnAttack(controller, controller.leftAttackTrans, Define.PlayerAttackDirection.Left);
            attack.Attack((_isHit) => 
            { 
                //TODO :: 맞으면 뒤로 넉백 처리
            });
            controller.StartCoroutine(AttackRoutine());
        }

        public override void RightAttack()
        {
            isCanAttack = false;
            controller.anim.SetInteger("AttackDirection", (int)Define.PlayerAttackDirection.Right);
            controller.anim.SetTrigger("Attack");
            NormalAttack attack = Managers.Object.SpawnAttack(controller, controller.rightAttackTrans, Define.PlayerAttackDirection.Right);
            attack.Attack((_isHit) =>
            {
                //TODO :: 맞으면 뒤로 넉백 처리
            });
            controller.StartCoroutine(AttackRoutine());
        }

        public override void UpAttack()
        {
            isCanAttack = false;
            controller.anim.SetInteger("AttackDirection", (int)Define.PlayerAttackDirection.Up);
            controller.anim.SetTrigger("Attack");
            NormalAttack attack = Managers.Object.SpawnAttack(controller, controller.upAttackTrans, Define.PlayerAttackDirection.Up);
            attack.Attack((_isHit) =>
            {
                //TODO :: 맞으면 뒤로 넉백 처리
            });
            controller.StartCoroutine(AttackRoutine());
        }

        public override void DownAttack()
        {
            isCanAttack = false;
            controller.anim.SetInteger("AttackDirection", (int)Define.PlayerAttackDirection.Down);
            controller.anim.SetTrigger("Attack");
            NormalAttack attack = Managers.Object.SpawnAttack(controller, controller.downAttackTrans, Define.PlayerAttackDirection.Down);
            attack.Attack((_isHit) =>
            {
                //TODO :: 맞으면 뒤로 넉백 처리
            });
            controller.StartCoroutine(AttackRoutine());
        }

        public override IEnumerator AttackRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            isCanAttack = true;
        }
    }


}
