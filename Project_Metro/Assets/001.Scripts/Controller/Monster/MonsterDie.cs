using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterDie 
{
    protected MonsterController controller;
    protected Coroutine deathCoroutine;

    public abstract void DieEffect();
    public void DropGold()
    {
        Managers.Object.SpawnGold(controller.dropGoldValue, controller.transform.position);
    }
}

namespace MonsterDies
{
    public class Test : MonsterDie
    {
        public Test(MonsterController _controller)
            {
            controller = _controller;
        }

        public override void DieEffect()
        {
            if (deathCoroutine != null) return;
            controller.isBattle = false;
            controller.anim.SetTrigger("Dead");
            controller.attackTarget = null;
            controller.move.isCanMove = false;
            controller.move.Stop();
            controller.StopAllCoroutines();
            controller.collisionAttack.Disable();
            deathCoroutine = controller.StartCoroutine(DeathRoutine());
            DropGold();
        }


        public IEnumerator DeathRoutine()
        {
            yield return new WaitForSeconds(controller.deathTime);
            Managers.Resource.Destroy(controller.gameObject);
        }
    }
}