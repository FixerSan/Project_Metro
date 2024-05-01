using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossState 
{
    namespace ForestKnight
    {
        public class Create : State<BossController>
        {
            public override void Enter(BossController _entity)
            {
                _entity.CreateEffect();
            }

            public override void Exit(BossController _entity)
            {

            }

            public override void FixedUpdate(BossController _entity)
            {

            }

            public override void Update(BossController _entity)
            {

            }
        }

        public class Idle : State<BossController>
        {
            public override void Enter(BossController _entity)
            {
                
            }

            public override void Exit(BossController _entity)
            {

            }

            public override void FixedUpdate(BossController _entity)
            {

            }


            public override void Update(BossController _entity)
            {
                if (_entity.BossAction[Define.BossState.ActionOne].CheckAction()) return;
            }
        }

        public class ActionOne : State<BossController>
        {
            public override void Enter(BossController _entity)
            {
                _entity.BossAction[Define.BossState.ActionOne].Action();
            }

            public override void Exit(BossController _entity)
            {

            }

            public override void FixedUpdate(BossController _entity)
            {

            }

            public override void Update(BossController _entity)
            {

            }
        }
    }
}
