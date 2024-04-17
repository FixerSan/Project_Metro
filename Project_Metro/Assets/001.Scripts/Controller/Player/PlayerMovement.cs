using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace PlayerMovement
{
    public abstract class PlayerMove
    {
        public PlayerController controller;
        public int level;
        public abstract bool CheckMove();
        public abstract void Move();
        public abstract bool CheckStop();
        public abstract bool CheckStopInJump();
        public void Stop()
        {
            controller.rb.velocity = new Vector2(0, controller.rb.velocity.y);
        }
    }

    namespace Moves
    {
        public class One : PlayerMove
        {
            public One(PlayerController _controller)
            {
                controller = _controller;
                level = 1;
            }

            public override bool CheckMove()
            {
                Debug.Log(Managers.Input.MoveAxis);
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

    public abstract class PlayerJump
    {
        public PlayerController controller;
        public int level;

        public Coroutine checkEndJumpCoroutine = null;
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

    namespace Jumps
    {
        public class One : PlayerJump
        {
            public One(PlayerController _controller)
            {
                controller = _controller;
                canJumpTime = 0.35f;
                level = 1;
            }


            public override bool CheckJump()
            {
                if (controller.IsGround && Managers.Input.actions.Player.Jump.triggered)
                {
                    controller.ChangeState(Define.PlayerState.Jump);
                    return true;
                }

                return false;
            }

            public override void StartJump()
            {
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
                if(checkEndJumpCoroutine != null) controller.StopCoroutine(checkEndJumpCoroutine);
                checkEndJumpCoroutine = null;
                controller.rb.velocity = new Vector2(controller.rb.velocity.x, 0f);
            }
        }
    }
    public abstract class PlayerFall
    {
        public PlayerController controller;
        public int level;

        public abstract bool CheckFall();
        public abstract bool CheckEndFall();
    }

    namespace Falls
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
                if(controller.rb.velocity.y < -0.01f)
                {
                    controller.ChangeState(Define.PlayerState.Fall);
                    return true;
                }
                return false;
            }

            public override bool CheckEndFall()
            {
                if(controller.IsGround)
                {
                    controller.ChangeState(Define.PlayerState.Idle);
                    return true;
                }
                return false;
            }
        }
    }
}