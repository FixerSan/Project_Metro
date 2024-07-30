using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
public abstract class PlayerMove
{
    public PlayerController controller;
    public int level;
    public bool isCanMove;
    public bool isMove;

    public abstract bool CheckMove();
    public abstract void Move(float _speed);
    public abstract bool CheckStop();
    public abstract bool CheckStopInJump();
    public void StopX()
    {
        controller.rb.velocity = new Vector2(0, controller.rb.velocity.y);
    }

    public void Stop()
    {
        controller.rb.velocity = Vector2.zero;
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
            isMove = false;
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

        public override void Move(float _speed)
        {
            if (Managers.Input.MoveAxis.x == 0f || !isCanMove)
            {
                if (isMove) isMove = false;
                return;
            }
            
            controller.ChangeDirection((Define.Direction)(int)Managers.Input.MoveAxis.x);
            controller.rb.velocity = new Vector2(_speed * Managers.Input.MoveAxis.x, controller.rb.velocity.y);
            if (!isMove) isMove = true;
        }

        public override bool CheckStop()
        {
            if (Managers.Input.MoveAxis.x == 0f)
            {
                controller.ChangeState(Define.PlayerState.Idle);
                StopX();
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