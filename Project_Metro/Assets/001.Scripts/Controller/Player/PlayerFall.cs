using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerFall
{
    public PlayerController controller;
    public int level;

    public abstract bool CheckFall();
    public abstract bool CheckEndFall();
}

namespace PlayerFalls
{
    public class One : PlayerFall
    {
        public One(PlayerController _controller)
        {
            controller = _controller;
            level = 1;
        }

        public override bool CheckFall()
        {
            if (controller.rb.velocity.y < -0.01f)
            {
                controller.ChangeState(Define.PlayerState.Fall);
                return true;
            }
            return false;
        }

        public override bool CheckEndFall()
        {
            if (controller.IsGround)
            {
                controller.Jump.isCanJump = true;
                controller.ChangeState(Define.PlayerState.Idle);
                return true;
            }
            return false;
        }
    }
}

