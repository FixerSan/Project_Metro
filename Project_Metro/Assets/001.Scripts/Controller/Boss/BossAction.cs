using DG.Tweening;
using MonsterState.TestMonster;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public abstract class BossAction 
{
    public BossController controller;
    public Define.BossState stateType;
    public bool IsCanAction { get { return CheckAction(); } }
    public bool isCooltime = false;
    public virtual bool CheckAction()
    {
        if (isCooltime) return false;
        return true;
    }
    public abstract void StartAction();
    public abstract void Action();
    public abstract void EndAction();
    public IEnumerator CooltimeCheckRoutine(float _cooltime)
    {
        yield return new WaitForSeconds(_cooltime);
        isCooltime = false;
    }
}

namespace BossActions
{
    public abstract class ForestKnightAction : BossAction
    {
        public new ForestKnight controller;
    }

    namespace ForestKnights
    {
        public class Climbing : ForestKnightAction
        {
            private Transform startPoint;
            private float gravityScale;
            private Vector2 direction;

            private ForestKnight_VinesToMove action;


            public Climbing(ForestKnight _controller, Define.BossState _stateType)
            {
                controller = _controller;
                stateType = _stateType;
                startPoint = Util.FindChild<Transform>(_controller.gameObject, "Trans_ActionOneStartPoint", true);
            }


            public override bool CheckAction()
            {
                if (!base.CheckAction()) return false;
                return controller.IsGround;
            }

            public override void StartAction()
            {
                Vector3 moveDirection = new Vector3(FindMoveDirection(), 0.5f).normalized;
                action = Managers.Resource.Instantiate($"{nameof(ForestKnight_VinesToMove)}", _pooling:true).GetComponent<ForestKnight_VinesToMove>();
                action.StartMove(startPoint, moveDirection, (_direction) => 
                {
                    //TODO :: 움직이는 애니메이션 적용
                    direction = _direction;
                    gravityScale = controller.rb.gravityScale;
                    controller.rb.gravityScale = 0;
                    controller.StartCoroutine(ActionRoutine());
                });
            }

            private IEnumerator ActionRoutine()
            {
                bool isStop = false;
                Define.Direction _direction;
                if (direction.x >= 0) _direction = Define.Direction.Right;
                else _direction = Define.Direction.Left;

                while (!isStop)
                {
                    yield return null;
                    controller.rb.velocity = direction * controller.status.CurrentSpeed;
                    isStop = controller.CheckSide(_direction);
                }

                //TODO :: 도착한 애니메이션 적용
                controller.rb.velocity = Vector2.zero;
                Managers.Resource.Destroy(action.gameObject);

                yield return new WaitForSeconds(0.75f);
                controller.ChangeState(Define.BossState.Idle);
                controller.StartCoroutine(CooltimeCheckRoutine(controller.actionOneCooltime));
            }

            public int FindMoveDirection()
            {
                RaycastHit2D leftHit = Physics2D.Raycast(controller.leftWallCheckTrans.position, Vector2.left, LayerMask.GetMask("Wall")); 
                RaycastHit2D rightHit = Physics2D.Raycast(controller.rightWallCheckTrans.position, Vector3.right, LayerMask.GetMask("Wall"));

                if (leftHit.distance > rightHit.distance)
                    return -1;
                else
                    return 1;
            }

            public override void EndAction()
            {
                controller.rb.gravityScale = gravityScale;
            }

            public override void Action()
            {
                throw new System.NotImplementedException();
            }
        }
        public class JumpAndDownAttack : ForestKnightAction
        {
            private Transform downAttackTrans;
            public JumpAndDownAttack(ForestKnight _controller, Define.BossState _stateType)
            {
                controller = _controller;
                stateType = _stateType;
                downAttackTrans = Util.FindChild<Transform>(controller.gameObject, "Trans_DownAttackPosition", true);
            }

            public override void StartAction()
            {
                Action();
            }

            //바닥을 향에 돌진
            private IEnumerator ActionRoutine()
            {
                yield return new WaitForSeconds(0.25f);
                while (!controller.IsGround)
               {
                    yield return null;
                    controller.rb.velocity = new Vector3(0, -20);
               }

                Debug.Log("스킬 소환");
                ForestKnight_DownAttack attack = Managers.Resource.Instantiate("ForestKnight_DownAttack", _pooling: true).GetComponent<ForestKnight_DownAttack>();
                attack.transform.position = downAttackTrans.position;
                attack.Attack(controller, () => 
                {
                    controller.ChangeState(Define.BossState.Idle);
                    controller.StartCoroutine(CooltimeCheckRoutine(controller.actionTwoCooltime));
                });
            }


            public override bool CheckAction()
            {
                if (!base.CheckAction()) return false;

                if (controller.isRightSide || controller.isLeftSide)
                    if (!controller.IsGround)
                        return true;
                return false;
            }

            public override void EndAction()
            {

            }

            public override void Action()
            {
                //TODO :: 점프 애니메이션 적용
                controller.transform.DOJump(new Vector3(Managers.Object.player.transform.position.x, controller.transform.position.y, 0), 1, 1,
                    Mathf.Abs(controller.transform.position.x - Managers.Object.player.transform.position.x) * 0.05f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        controller.StartCoroutine(ActionRoutine());
                    });
            }
        }
        public class Three : ForestKnightAction
        {
            public Three(ForestKnight _controller, Define.BossState _stateType)
            {
                controller = _controller;
                stateType = _stateType;
            }

            public override void StartAction()
            {
                Action();
            }

            public override bool CheckAction()
            {
                if (!base.CheckAction()) return false;
                return controller.IsGround;
            }

            public override void EndAction()
            {
                throw new System.NotImplementedException();
            }

            public override void Action()
            {
                isCooltime = true;
                controller.StartCoroutine(ActionRoutine());
            }

            private IEnumerator ActionRoutine()
            {
                Vector2 spawnPosition = new Vector2(Managers.Object.player.transform.position.x, controller.anim.transform.position.y);
                ForestKnight_VinesAttack attack;
                
                yield return new WaitForSeconds(controller.actionThreeStartDelay);
                for (int i = 0; i < 3; i++)
                {
                    attack = Managers.Resource.Instantiate("ForestKnight_VinesAttack", _pooling: true).GetComponent<ForestKnight_VinesAttack>();
                    spawnPosition = new Vector2(Managers.Object.player.transform.position.x, controller.anim.transform.position.y);
                    attack.transform.position = spawnPosition;
                    attack.Attack(controller);
                    yield return new WaitForSeconds(controller.actionThreeRepeatDelay);
                }
                yield return new WaitForSeconds(controller.actionThreeStartDelay);
                controller.ChangeState(Define.BossState.Idle);

                controller.StartCoroutine(CooltimeCheckRoutine(controller.actionThreeCooltime));
            }
        }
        public class Four : ForestKnightAction
        {
            public Four(ForestKnight _controller, Define.BossState _stateType)
            {
                controller = _controller;
                stateType = _stateType;
            }

            public override void StartAction()
            {
                Action();
            }

            public override bool CheckAction()
            {
                if (!base.CheckAction()) return false;
                return controller.IsGround;
            }

            public override void EndAction()
            {
                throw new System.NotImplementedException();
            }

            public override void Action()
            {
                isCooltime = true;
                controller.LookAtPlayer();
                controller.StartCoroutine(ActionRoutine());
            }

            private IEnumerator ActionRoutine()
            {
                bool isStop = false;
                float movedDistance = 0;
                controller.defaultGravityScale = controller.rb.gravityScale;
                controller.rb.gravityScale = 0;

                while (!isStop && movedDistance < controller.actionFourMoveDistance)
                {
                    yield return null;
                    controller.rb.velocity = new Vector2((int)controller.currentDirection * controller.actionFourDashForce, 0);
                    movedDistance += (Time.deltaTime * controller.actionFourDashForce);
                    if(controller.currentDirection == Define.Direction.Right && controller.isRightSide)
                        isStop = true;
                    if (controller.currentDirection == Define.Direction.Left && controller.isLeftSide)
                        isStop = true;
                }

                controller.rb.gravityScale = controller.defaultGravityScale;
                controller.rb.velocity = Vector2.zero;
                controller.ChangeState(Define.BossState.Idle);

                controller.StartCoroutine(CooltimeCheckRoutine(controller.actionThreeCooltime));
            }
        }

        public class Five : ForestKnightAction
        {
            public Five(ForestKnight _controller, Define.BossState _stateType)
            {
                controller = _controller;
                stateType = _stateType;
            }

            public override void StartAction()
            {
                Action();
            }

            public override bool CheckAction()
            {
                if (!base.CheckAction()) return false;
                return controller.IsGround;
            }

            public override void EndAction()
            {
                throw new System.NotImplementedException();
            }

            public override void Action()
            {
                isCooltime = true;
                controller.StartCoroutine(ActionRoutine());
            }

            private IEnumerator ActionRoutine()
            {
                Vector2 spawnPosition = new Vector2(Managers.Object.player.transform.position.x, controller.anim.transform.position.y);
                ForestKnight_VinesAttack attack;

                yield return new WaitForSeconds(controller.actionThreeStartDelay);
                for (int i = 0; i < 3; i++)
                {
                    attack = Managers.Resource.Instantiate("ForestKnight_VinesAttack", _pooling: true).GetComponent<ForestKnight_VinesAttack>();
                    spawnPosition = new Vector2(Managers.Object.player.transform.position.x, controller.anim.transform.position.y);
                    attack.transform.position = spawnPosition;
                    attack.Attack(controller);
                    yield return new WaitForSeconds(controller.actionThreeRepeatDelay);
                }
                yield return new WaitForSeconds(controller.actionThreeStartDelay);
                controller.ChangeState(Define.BossState.Idle);

                controller.StartCoroutine(CooltimeCheckRoutine(controller.actionThreeCooltime));
            }
        }

    }

}
