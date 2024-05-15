using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossState 
{
    namespace ForestKnights
    {
        public class Create : State<ForestKnight>
        {
            public override void Enter(ForestKnight _entity)
            {
                _entity.CreateEffect();
            }

            public override void Exit(ForestKnight _entity)
            {
                _entity.Stop();
            }

            public override void FixedUpdate(ForestKnight _entity)
            {

            }

            public override void Update(ForestKnight _entity)
            {

            }
        }

        public class Idle : State<ForestKnight>
        {
            public override void Enter(ForestKnight _entity)
            {
            }

            public override void Exit(ForestKnight _entity)
            {
                _entity.Stop();
            }

            public override void FixedUpdate(ForestKnight _entity)
            {
                _entity.SelectAction();

            }


            public override void Update(ForestKnight _entity)
            {

            }
        }

        public class ActionOne : State<ForestKnight>
        {
            public override void Enter(ForestKnight _entity)
            {
                _entity.bossActions[Define.BossAction.ActionOne].StartAction();
            }

            public override void Exit(ForestKnight _entity)
            {
                _entity.Stop();
                _entity.bossActions[Define.BossAction.ActionOne].EndAction();
            }

            public override void FixedUpdate(ForestKnight _entity)
            {

            }


            public override void Update(ForestKnight _entity)
            {

            }
        }

        public class ActionTwo : State<ForestKnight>
        {
            public override void Enter(ForestKnight _entity)
            {
                _entity.bossActions[Define.BossAction.ActionTwo].StartAction();
            }

            public override void Exit(ForestKnight _entity)
            {
                _entity.Stop();
            }

            public override void FixedUpdate(ForestKnight _entity)
            {

            }

            public override void Update(ForestKnight _entity)
            {

            }
        }

        public class ActionThree : State<ForestKnight>
        {
            public override void Enter(ForestKnight _entity)
            {
                _entity.bossActions[Define.BossAction.ActionThree].StartAction();
            }

            public override void Exit(ForestKnight _entity)
            {
                _entity.Stop();
            }

            public override void FixedUpdate(ForestKnight _entity)
            {

            }

            public override void Update(ForestKnight _entity)
            {

            }
        }

        public class ActionFour : State<ForestKnight>
        {
            public override void Enter(ForestKnight _entity)
            {
                _entity.bossActions[Define.BossAction.ActionFour].StartAction();
            }

            public override void Exit(ForestKnight _entity)
            {
                _entity.Stop();
            }

            public override void FixedUpdate(ForestKnight _entity)
            {

            }

            public override void Update(ForestKnight _entity)
            {

            }
        }
    }
}
