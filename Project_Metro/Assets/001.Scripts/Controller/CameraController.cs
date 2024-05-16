using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector2 maxDistance;
    public Vector2 minRange, maxRange;

    public float followSpeed;

    public void Awake()
    {
        Managers.Screen.SetCamera(this);
    }

    void LateUpdate()
    {
        if (target == null) return;
        FollowTarget();
    }

    public void SetCameraRange(Vector2 _min, Vector2 _max)
    {
        minRange = _min; maxRange = _max;
    }

    public void SetCameraOffset(Vector3 _offset)
    {
        offset = _offset;
    }

    public void FollowTarget()
    {
        Vector3 followPos;
        
        //���� �̵�
        followPos.x = Mathf.Lerp(transform.position.x, target.position.x + offset.x, followSpeed * Time.fixedDeltaTime);
        followPos.y = Mathf.Lerp(transform.position.y, target.position.y + offset.y, followSpeed * Time.fixedDeltaTime);

        //���� �̵� Ŭ����
        followPos.x = Mathf.Clamp(followPos.x, (target.position.x + offset.x) - maxDistance.x, (target.position.x + offset.x) + maxDistance.x);
        followPos.y = Mathf.Clamp(followPos.y, (target.position.y + offset.y) - maxDistance.y, (target.position.y + offset.y) + maxDistance.y);

        //������ Ŭ����
        followPos.x = Mathf.Clamp(followPos.x, minRange.x, maxRange.x);
        followPos.y = Mathf.Clamp(followPos.y, minRange.y, maxRange.y);
        followPos.z = offset.z;

        transform.position = followPos;
    }
}
