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

        }

        public override void Exit(PlayerController _entity)
        {

        }

        public override void FixedUpdate(PlayerController _entity)
        {
            
        }

        public override void Update(PlayerController _entity)
        {
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

        }

        public override void Exit(PlayerController _entity)
        {

        }

        public override void FixedUpdate(PlayerController _entity)
        {
            _entity.move.Move();
        }

        public override void Update(PlayerController _entity)
        {
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
        }

        public override void Exit(PlayerController _entity)
        {
            
            _entity.jump.EndJump();
        }

        public override void FixedUpdate(PlayerController _entity)
        {
            _entity.move.Move();
            _entity.jump.Jump();
        }

        public override void Update(PlayerController _entity)
        {
            if (_entity.jump.CheckEndJump()) return;
            if (_entity.move.CheckStopInJump()) return;
            _entity.attack.CheckAttack();
        }
    }

    public class Fall : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {

        }

        public override void Exit(PlayerController _entity)
        {

        }

        public override void FixedUpdate(PlayerController _entity)
        {
            _entity.move.Move();
        }

        public override void Update(PlayerController _entity)
        {
            if (_entity.fall.CheckEndFall()) return;
            _entity.attack.CheckAttack();
        }
    }
}
