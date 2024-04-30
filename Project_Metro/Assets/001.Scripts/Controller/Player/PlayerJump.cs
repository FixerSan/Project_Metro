using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerJump
{
    public PlayerController controller;
    public int level;

    public Coroutine checkEndJumpCoroutine = null;
    public bool isCanJump;
    public float canJumpTime;

    public abstract bool CheckJump();
    public abstract void StartJump();
    public abstract void Jump();
    public abstract bool CheckEndJump();
    public abstract void EndJump();
    public IEnumerator CheckEndJumpRoutine()
    {
        yield return new WaitForSeconds(canJumpTime);
        controller.ChangeState(Define.PlayerState.Idle);
    }
}

namespace PlayerJumps
{
    public class One : PlayerJump
    {
        public One(PlayerController _controller)
        {
            controller = _controller;
            canJumpTime = 0.35f;
            isCanJump = true;
            level = 1;
        }


        public override bool CheckJump()
        {
            if (!isCanJump) return false;
            if (controller.IsGround && Managers.Input.GetJumpKey)
            {
                controller.ChangeState(Define.PlayerState.Jump);
                return true;
            }

            return false;
        }

        public override void StartJump()
        {
            isCanJump = false;
            checkEndJumpCoroutine = controller.StartCoroutine(CheckEndJumpRoutine());
        }

        public override void Jump()
        {
            controller.rb.velocity = new Vector2(controller.rb.velocity.x, controller.status.CurrentJumpForce);
        }

        public override bool CheckEndJump()
        {
            if (Managers.Input.actions.Player.Jump.ReadValue<float>() == 0f)
            {
                controller.ChangeState(Define.PlayerState.Idle);
                return true;
            }

            return false;
        }

        public override void EndJump()
        {
            if (checkEndJumpCoroutine != null) controller.StopCoroutine(checkEndJumpCoroutine);
            checkEndJumpCoroutine = null;
            controller.rb.velocity = new Vector2(controller.rb.velocity.x, 0f);
            isCanJump = true;
        }
    }
}

