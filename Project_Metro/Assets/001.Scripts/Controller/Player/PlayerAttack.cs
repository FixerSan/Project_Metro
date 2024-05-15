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
    public abstract IEnumerator AttackKnockbackRoutine();
    public virtual void CancleAttackKnockback()
    {
        controller.StopCoroutine(AttackKnockbackRoutine());
        controller.Move.isCanMove = true;
        controller.Jump.isCanJump = true;
    }
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
            if (Managers.Input.GetAttackKey)
            {
                if(!controller.IsGround)
                {

                    if(Managers.Input.actions.Player.Move.ReadValue<Vector2>().y == -1f)
                    {
                        DownAttack();
                        return true;
                    }
                }

                if(Managers.Input.actions.Player.Move.ReadValue<Vector2>().y == 1f)
                {
                    UpAttack();
                    return true;
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
                if (_isHit)
                {
                    controller.Move.isCanMove = false;
                    controller.Jump.isCanJump = false;
                    controller.ChangeState(Define.PlayerState.Idle);
                    controller.rb.velocity = new Vector2(controller.AttackKnockbackForce, controller.rb.velocity.y);
                    controller.StartCoroutine(AttackKnockbackRoutine());
                }
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
                if(_isHit)
                {
                    controller.Move.isCanMove = false;
                    controller.Jump.isCanJump = false;
                    controller.ChangeState(Define.PlayerState.Idle);
                    controller.rb.velocity = new Vector2(-controller.AttackKnockbackForce, controller.rb.velocity.y);
                    controller.StartCoroutine(AttackKnockbackRoutine());
                }
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
                if (_isHit)
                {
                    controller.Move.isCanMove = false;
                    controller.Jump.isCanJump = false;
                    controller.ChangeState(Define.PlayerState.Idle);
                    controller.rb.velocity = new Vector2(controller.rb.velocity.x, -controller.AttackKnockbackForce);
                    controller.StartCoroutine(AttackKnockbackRoutine());
                }
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
                if (_isHit)
                {
                    controller.Jump.isCanJump = false;
                    controller.ChangeState(Define.PlayerState.Idle);
                    controller.rb.velocity = new Vector2(controller.rb.velocity.x, controller.downAttackKnockbackForce);
                    controller.StartCoroutine(AttackKnockbackRoutine());
                }
            });
            controller.StartCoroutine(AttackRoutine());
        } 

        public override IEnumerator AttackRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            isCanAttack = true;
        }

        public override IEnumerator AttackKnockbackRoutine()
        {
            yield return new WaitForSeconds(0.25f);
            controller.Move.Stop();
            controller.Move.isCanMove = true;
            controller.Jump.isCanJump = true;
        }
    }


}
