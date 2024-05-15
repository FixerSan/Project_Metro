using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDash
{
    public PlayerController controller;
    protected bool isCanDash = true;
    protected float dashTime;

    protected float dashCooltime = 0.5f;


    protected float gravityScale;

    public abstract bool CheckDash();
    public abstract void StartDash();
    public abstract void Dash();
    public abstract void EndDash();

    public IEnumerator StartDashRoutine()
    {
        yield return new WaitForSeconds(dashTime);
        controller.ChangeState(Define.PlayerState.Idle);
    }

    public IEnumerator EndDashRoutine()
    {
        yield return new WaitForSeconds(dashCooltime);
        isCanDash = true;
    }
}

namespace PlayerDashes
{
    public class One : PlayerDash
    {

        public One(PlayerController _controller) 
        {
            controller = _controller;
            dashTime = 0.3f;
        }

        public override bool CheckDash()
        {
            if(isCanDash && Managers.Input.GetDashKey)
            {
                controller.ChangeState(Define.PlayerState.Dash);
                if (Managers.Input.MoveAxis.x != 0)
                    controller.ChangeDirection((Define.Direction)Managers.Input.MoveAxis.x);
                return true;
            }
            return false;
        }

        public override void Dash()
        {
            controller.rb.velocity = new Vector2 (20f * (int)controller.currentDirection, 0f);
        }

        public override void EndDash()
        {
            controller.rb.gravityScale = gravityScale;
            controller.Move.Stop();
            controller.StartCoroutine(EndDashRoutine());
        }

        public override void StartDash()
        {
            controller.Attack.CancleAttackKnockback();
            isCanDash = false;
            gravityScale = controller.rb.gravityScale;
            controller.rb.gravityScale = 0;
            controller.StartCoroutine(StartDashRoutine());
        }

        
    }
}
