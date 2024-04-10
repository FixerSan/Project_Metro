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
            if (_entity.movement.CheckMove()) return;
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
            _entity.movement.Move();
        }

        public override void Update(PlayerController _entity)
        {
            if (_entity.movement.CheckStop()) return;
        }
    }
}
