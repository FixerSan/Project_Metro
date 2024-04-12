using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAttack
{
    public PlayerController controller;
    public bool isCanAttack = true;
    public abstract bool CheckAttack();
    public abstract void Attack();
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
            Debug.Log(Managers.Input.actions.Player.Attack.ReadValue<float>());
            Debug.Log(Managers.Input.actions.Player.Jump.ReadValue<float>());
            if (!isCanAttack) return false ;
            if (Managers.Input.actions.Player.Attack.ReadValue<float>() > 0)
            {
                Attack();
                return true;
            }

            return false;
        }

        public override void Attack()
        {
            isCanAttack = false;
            NormalAttack attack = Managers.Object.SpawnAttack(controller, controller.attackTrans.position, controller.attackTrans);
            attack.Attack((_isHit) => 
            { 
                //TODO :: 맞으면 뒤로 넉백 처리
                controller.StartCoroutine(AttackRoutine());
            });
        }

        public override IEnumerator AttackRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            isCanAttack = true;
        }
    }


}
