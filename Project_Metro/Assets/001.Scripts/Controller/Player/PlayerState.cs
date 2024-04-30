using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class Idle : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            _entity.move.Stop();
        }

        public override void Exit(PlayerController _entity)
        {

        }

        public override void FixedUpdate(PlayerController _entity)
        {
            
        }

        public override void Update(PlayerController _entity)
        {
            if (_entity.dash != null && _entity.dash.CheckDash()) return;
            if (_entity.defence != null && _entity.defence.CheckDefence()) return;
            if (_entity.fall.CheckFall()) return;
            if (_entity.move.CheckMove()) return;
            if (_entity.jump.CheckJump()) return;
            _entity.attack.CheckAttack();
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
            _entity.move.Move();
        }

        public override void Update(PlayerController _entity)
        {
            if (_entity.dash != null && _entity.dash.CheckDash()) return;
            if (_entity.defence != null && _entity.defence.CheckDefence()) return;
            if (_entity.fall.CheckFall()) return;
            if (_entity.move.CheckStop()) return;
            if (_entity.jump.CheckJump()) return;
            _entity.attack.CheckAttack();
        }
    }

    public class Jump : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            _entity.jump.StartJump();
            _entity.anim.SetBool("IsJump", true);
        }

        public override void Exit(PlayerController _entity)
        {
            _entity.jump.EndJump();
            _entity.anim.SetBool("IsJump", false);
        }

        public override void FixedUpdate(PlayerController _entity)
        {
            _entity.move.Move();
            _entity.jump.Jump();
        }

        public override void Update(PlayerController _entity)
        {
            if (_entity.dash != null && _entity.dash.CheckDash()) return;
            _entity.attack.CheckAttack();
            if (_entity.jump.CheckEndJump()) return;
            if (_entity.move.CheckStopInJump()) return;
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
            _entity.move.Stop();

        }

        public override void FixedUpdate(PlayerController _entity)
        {
            _entity.move.Move();
        }

        public override void Update(PlayerController _entity)
        {
            if (_entity.dash != null && _entity.dash.CheckDash()) return;
            if (_entity.fall.CheckEndFall()) return;
            _entity.attack.CheckAttack();
        }
    }

    public class Dash : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            _entity.anim.SetBool("IsDash", true);
            _entity.dash.StartDash();
        }

        public override void Exit(PlayerController _entity)
        {
            _entity.anim.SetBool("IsDash", false);
            _entity.dash.EndDash();
        }

        public override void FixedUpdate(PlayerController _entity)
        {

        }

        public override void Update(PlayerController _entity)
        {
            _entity.dash.Dash();
        }
    }

    public class Defence : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            _entity.anim.SetBool("IsDefence", true);
            _entity.move.Stop();
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
            if (_entity.defence.CheckEndDefence()) return;
        }
    }
}
