using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace PlayerState
{
    public class Idle : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            _entity.Move.StopX();
        }

        public override void Exit(PlayerController _entity)
        {

        }

        public override void FixedUpdate(PlayerController _entity)
        {
            
        }

        public override void Update(PlayerController _entity)
        {
            if (_entity.Dash != null && _entity.Dash.CheckDash()) return;
            if (_entity.Defence != null && _entity.Defence.CheckDefence()) return;
            if (_entity.Fall.CheckFall()) return;
            if (_entity.Jump.CheckJump()) return;
            if (_entity.Heal.CheckHeal()) return;
            if (_entity.Move.CheckMove()) return;
            _entity.Attack.CheckAttack();
        }
    }

    public class Move : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            _entity.anim.SetBool("IsRun", true);
        }

        public override void Exit(PlayerController _entity)
        {
            _entity.anim.SetBool("IsRun", false);
        }

        public override void FixedUpdate(PlayerController _entity)
        {
            _entity.Move.Move(_entity.status.CurrentSpeed);
        }

        public override void Update(PlayerController _entity)
        {
            if (_entity.Dash != null && _entity.Dash.CheckDash()) return;
            if (_entity.Defence != null && _entity.Defence.CheckDefence()) return;
            if (_entity.Move.CheckStop()) return;
            if (_entity.Fall.CheckFall()) return;
            if (_entity.Jump.CheckJump()) return;
            if (_entity.Heal.CheckHeal()) return;
            _entity.Attack.CheckAttack();
        }
    }

    public class Jump : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            _entity.Jump.StartJump();
            _entity.anim.SetBool("IsJump", true);
        }

        public override void Exit(PlayerController _entity)
        {
            _entity.Jump.EndJump();
            _entity.anim.SetBool("IsJump", false);
        }

        public override void FixedUpdate(PlayerController _entity)
        {
            _entity.Move.Move(_entity.status.CurrentSpeed);
            _entity.Jump.Jump();
        }

        public override void Update(PlayerController _entity)
        {
            if (_entity.Dash != null && _entity.Dash.CheckDash()) return;
            _entity.Attack.CheckAttack();
            //if (_entity.Climb != null && _entity.Climb.CheckRightClimb()) return;
            //if (_entity.Climb != null && _entity.Climb.CheckLeftClimb()) return;
            if (_entity.Jump.CheckEndJump()) return;
            if (_entity.Move.CheckStopInJump()) return;
        }
    }

    public class Fall : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            _entity.anim.SetBool("IsFall", true);

        }

        public override void Exit(PlayerController _entity)
        {
            _entity.anim.SetBool("IsFall", false);
            _entity.Move.StopX();

        }

        public override void FixedUpdate(PlayerController _entity)
        {
            _entity.Move.Move(_entity.status.CurrentSpeed);
        }

        public override void Update(PlayerController _entity)
        {
            if (_entity.Dash != null && _entity.Dash.CheckDash()) return;
            if (_entity.Climb != null && _entity.Climb.CheckRightClimb()) return;
            if (_entity.Climb != null &&_entity.Climb.CheckLeftClimb()) return;
            if (_entity.Fall.CheckEndFall()) return;
            _entity.Attack.CheckAttack();
        }
    }

    public class Dash : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            _entity.anim.SetBool("IsDash", true);
            _entity.Dash.StartDash();
        }

        public override void Exit(PlayerController _entity)
        {
            _entity.anim.SetBool("IsDash", false);
            _entity.Dash.EndDash();
        }

        public override void FixedUpdate(PlayerController _entity)
        {

        }

        public override void Update(PlayerController _entity)
        {
            _entity.Dash.Dash();
            if (_entity.Climb.CheckRightClimb()) return;
            if (_entity.Climb.CheckLeftClimb()) return;
        }
    }

    public class Defence : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            _entity.anim.SetBool("IsDefence", true);
            _entity.Move.StopX();
        }

        public override void Exit(PlayerController _entity)
        {
            _entity.anim.SetBool("IsDefence", false);
        }
        public override void FixedUpdate(PlayerController _entity)
        {

        }

        public override void Update(PlayerController _entity)
        {
            if (_entity.Defence.CheckEndDefence()) return;
        }
    }

    public class Heal : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            _entity.Heal.StartHeal();
        }

        public override void Exit(PlayerController _entity)
        {
            _entity.Heal.CancelHeal();
        }

        public override void FixedUpdate(PlayerController _entity)
        {
            _entity.Move.Move(_entity.status.CurrentSpeed * 0.5f);
        }

        public override void Update(PlayerController _entity)
        {
            _entity.Heal.SetAnimation();
        }
    }

    public class Hit : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            
        }

        public override void Exit(PlayerController _entity)
        {
            _entity.Move.StopX();
        }

        public override void FixedUpdate(PlayerController _entity)
        {

        }

        public override void Update(PlayerController _entity)
        {

        }
    }

    public class Save : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            _entity.Move.StopX();
        }

        public override void Exit(PlayerController _entity)
        {

        }

        public override void FixedUpdate(PlayerController _entity)
        {

        }

        public override void Update(PlayerController _entity)
        {

        }
    }

    public class Climb : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            _entity.Climb.StartClimb();
        }

        public override void Exit(PlayerController _entity)
        {
            _entity.Climb.EndClimb();
        }

        public override void FixedUpdate(PlayerController _entity)
        {
            _entity.Climb.Climb();
        }

        public override void Update(PlayerController _entity)
        {
            if (_entity.Climb.CheckEndClimb()) return;
            if (_entity.Climb.CheckClimbJump()) return;
        }
    }
}
