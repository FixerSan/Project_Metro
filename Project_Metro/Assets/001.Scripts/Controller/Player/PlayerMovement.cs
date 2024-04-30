using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
public abstract class PlayerMove
{
    public PlayerController controller;
    public int level;
    public bool isCanMove;

    public abstract bool CheckMove();
    public abstract void Move();
    public abstract bool CheckStop();
    public abstract bool CheckStopInJump();
    public void Stop()
    {
        controller.rb.velocity = new Vector2(0, controller.rb.velocity.y);
    }
}

namespace PlayerMoves
{
    public class One : PlayerMove
    {
        public One(PlayerController _controller)
        {
            controller = _controller;
            level = 1;
            isCanMove = true;
        }

        public override bool CheckMove()
        {
            if (!isCanMove) return false;
            if (Managers.Input.MoveAxis.x != 0f)
            {
                controller.ChangeState(Define.PlayerState.Move);
                return true;
            }

            return false;
        }

        public override void Move()
        {
            if (Managers.Input.MoveAxis.x == 0f) return;
            controller.ChangeDirection((Define.Direction)(int)Managers.Input.MoveAxis.x);
            controller.rb.velocity = new Vector2(controller.status.CurrentSpeed * Managers.Input.MoveAxis.x, controller.rb.velocity.y);
        }

        public override bool CheckStop()
        {
            if (Managers.Input.MoveAxis.x == 0f)
            {
                controller.ChangeState(Define.PlayerState.Idle);
                Stop();
                return true;
            }

            return false;
        }

        public override bool CheckStopInJump()
        {
            if (Managers.Input.MoveAxis.x == 0f)
            {
                controller.rb.velocity = new Vector2(0, controller.rb.velocity.y);
                return true;
            }

            return false;
        }
    }
}