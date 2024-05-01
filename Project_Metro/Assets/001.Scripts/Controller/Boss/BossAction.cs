using System.Collections;
using UnityEngine;

public abstract class BossAction 
{
    public BossController controller;
    public abstract bool CheckAction();
    public abstract void Action();
}

namespace BossActions
{
    namespace ForestKnight
    {
        public class One : BossAction
        {
            private Transform startPoint;
            private Transform leftWallCheckTrans;
            private Transform rightWallCheckTrans;

            private float gravityScale;
            private Vector2 direction;

            private ForestKight_VinesToMove action;

            public One(BossController _controller)
            {
                controller = _controller;
                startPoint = Util.FindChild<Transform>(_controller.gameObject, "Trans_ActionOneStartPoint", true);
                leftWallCheckTrans = Util.FindChild<Transform>(_controller.gameObject, "Trans_LeftWallCheck", true);
                rightWallCheckTrans = Util.FindChild<Transform>(_controller.gameObject, "Trans_RightWallCheck", true);
            }


            public override bool CheckAction()
            {
                controller.ChangeState(Define.BossState.ActionOne);
                return true;
            }

            public override void Action()
            {
                //bool isHit = false;
                //RaycastHit2D hit;
                //Vector2 rayDirection = new Vector2(FindMoveDirection(), 0);

                //while(!isHit)
                //{
                //    rayDirection.y = Random.Range(-0.5f, 0.5f);
                //    hit = Physics2D.Raycast(startPoint.position, rayDirection, 50, LayerMask.GetMask("Wall"));
                //    isHit = hit;
                //}

                Vector3 moveDirection = new Vector3(FindMoveDirection(), Random.Range(0, 0.6f));
                action = Managers.Resource.Instantiate($"{nameof(ForestKight_VinesToMove)}", _pooling:true).GetComponent<ForestKight_VinesToMove>();
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
                bool isMove = false;
                while (!isMove)
                {
                    yield return null;
                    controller.rb.velocity = direction * controller.status.CurrentSpeed;
                    isMove = CheckSide();
                }

                //TODO :: 도착한 애니메이션 적용
                controller.rb.velocity = Vector2.zero;
                Managers.Resource.Destroy(action.gameObject);
                controller.ChangeState(Define.BossState.Idle);
            }

            public bool CheckSide()
            {
                Collider2D[] collider2Ds;
                if (direction.x >= 0) 
                    collider2Ds = Physics2D.OverlapCircleAll(rightWallCheckTrans.position, 0.1f);
                else
                    collider2Ds = Physics2D.OverlapCircleAll(leftWallCheckTrans.position, 0.1f);
                for (int i = 0; i < collider2Ds.Length; i++)
                {
                    if (collider2Ds[i].CompareTag("Wall"))
                        return true;
                }
                return false;
            }

            public int FindMoveDirection()
            {
                RaycastHit2D leftHit = Physics2D.Raycast(leftWallCheckTrans.position, Vector2.left, LayerMask.GetMask("Wall")); 
                RaycastHit2D rightHit = Physics2D.Raycast(rightWallCheckTrans.position, Vector3.right, LayerMask.GetMask("Wall"));

                if (leftHit.distance > rightHit.distance)
                    return -1;
                else
                    return 1;
            }
        }
    }
}
