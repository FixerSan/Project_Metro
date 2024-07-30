using PlayerState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public abstract class PlayerClimb
{
    public PlayerController controller;
    protected Coroutine endClimbCoroutine;
    public bool isCanClimb = true;

    public bool CheckRightClimb()
    {
        if (!isCanClimb) return false;
        bool isWall = false;
        //오른쪽 벽 검사
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(controller.rightAttackTrans.position, controller.rightAttackTrans.localScale.x,LayerMask.GetMask("Wall"));
        if (collider2Ds.Length != 0) 
        {
            isWall = true;
            controller.ChangeState(Define.PlayerState.Climb);
            controller.ChangeDirection(Define.Direction.Left);
        }
        return isWall;
    }

    public bool CheckLeftClimb()
    {
        if (!isCanClimb) return false;
        bool isWall = false;
        //왼쪽 벽 검사
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(controller.leftAttackTrans.position, controller.leftAttackTrans.localScale.x, LayerMask.GetMask("Wall"));
        if (collider2Ds.Length != 0)
        {
            isWall = true;
            controller.ChangeState(Define.PlayerState.Climb);
            controller.ChangeDirection(Define.Direction.Right);
        }
        return isWall;
    }

    public abstract void StartClimb();
    public abstract void Climb();
    public abstract void EndClimb();
    protected IEnumerator EndClimbRoutine()
    {
        yield return new WaitForSeconds(0.25f);
        isCanClimb = true;
        controller.Move.isCanMove = true;
    }
    public abstract bool CheckClimbJump();
    public abstract bool CheckEndClimb();
}

namespace PlayerClimbs
{
    public class One : PlayerClimb
    {
        public One(PlayerController _playerController)
        {
            controller = _playerController;
        }

        public override void StartClimb()
        {
            controller.Move.Stop();
        }

        public override void Climb()
        {
            controller.rb.velocity = new Vector2(0, -controller.status.CurrentSpeed * 0.75f);
        }

        public override bool CheckClimbJump()
        {
            if(Managers.Input.GetJumpKey)
            {
                controller.ChangeState(Define.PlayerState.Jump);
                return true;
            }
            return false;
        }

        public override void EndClimb()
        {
            controller.Move.isCanMove = false;
            isCanClimb = false;
            controller.rb.AddForce(Vector2.right * controller.status.CurrentSpeed * (int)controller.currentDirection, ForceMode2D.Impulse);
            endClimbCoroutine = controller.StartCoroutine(EndClimbRoutine());
        }

        public override bool CheckEndClimb()
        {
            if (controller.IsGround)
            {
                controller.ChangeState(Define.PlayerState.Idle);
                return true;
            }
            return false;
        }
    }
}
