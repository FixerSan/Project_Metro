using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterState 
{
    namespace TestMonster
    {
        public class Idle : State<MonsterController>
        {
            public override void Enter(MonsterController _entity)
            {

            }

            public override void Exit(MonsterController _entity)
            {

            }

            public override void FixedUpdate(MonsterController _entity)
            {

            }

            public override void Update(MonsterController _entity)
            {
                if (_entity.attack.CheckAttack()) return;
                if (_entity.move.CheckFollow()) return;
            }
        }

        public class Move : State<MonsterController>
        {
            public override void Enter(MonsterController _entity)
            {

            }

            public override void Exit(MonsterController _entity)
            {

            }

            public override void FixedUpdate(MonsterController _entity)
            {

            }

            public override void Update(MonsterController _entity)
            {

            }
        }

        public class Follow : State<MonsterController>
        {
            public override void Enter(MonsterController _entity)
            {
                _entity.anim.SetBool("IsFollow", true);
            }

            public override void Exit(MonsterController _entity)
            {
                _entity.anim.SetBool("IsFollow", false);
                _entity.move.Stop();
            }

            public override void FixedUpdate(MonsterController _entity)
            {
                _entity.move.Follow();
            }

            public override void Update(MonsterController _entity)
            {
                if (_entity.attack.CheckAttack()) return;
            }
        }

        public class Attack : State<MonsterController>
        {
            public override void Enter(MonsterController _entity)
            {
                _entity.anim.SetBool("IsAttack", true);
                _entity.attack.StartAttack();
            }

            public override void Exit(MonsterController _entity)
            {
                _entity.anim.SetBool("IsAttack", false);
                _entity.attack.EndAttack();
            }

            public override void FixedUpdate(MonsterController _entity)
            {

            }

            public override void Update(MonsterController _entity)
            {

            }
        }

        public class Death : State<MonsterController>
        {
            public override void Enter(MonsterController _entity)
            {
                _entity.DeathEffect();
            }

            public override void Exit(MonsterController _entity)
            {
               
            }

            public override void FixedUpdate(MonsterController _entity)
            {

            }

            public override void Update(MonsterController _entity)
            {

            }
        }
    }
}
