using Cinemachine;
using DG.Tweening;
using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cine;
    public Transform target;
    public float defaultCameraSize;
    public Vector3 defaultOffset;

    private CinemachineBasicMultiChannelPerlin shake;
    private float shakeTimer;

    private CinemachineFramingTransposer offset;
    private CinemachineConfiner2D movePos;


    private Action shakeCallback; 

    //public Vector3 offset;
    //public Vector2 maxDistance;
    //public Vector2 minRange, maxRange;

    //public float followSpeed;

    public void Awake()
    {
        Managers.Screen.SetCamera(this);
        cine = GetComponentInChildren<CinemachineVirtualCamera>();
        offset = cine.GetCinemachineComponent<CinemachineFramingTransposer>();
        shake = cine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        movePos = cine.GetComponentInChildren<CinemachineConfiner2D>();
        defaultCameraSize = cine.m_Lens.OrthographicSize;
    }

    //void LateUpdate()
    //{
    //    if (target == null) return;
    //    FollowTarget();
    //}

    //public void SetCameraRange(Vector2 _min, Vector2 _max)
    //{
    //    minRange = _min; maxRange = _max;
    //}

    //public void SetCameraOffset(Vector3 _offset)
    //{
    //    offset = _offset;
    //}

    public void SetTarget(Transform _target)
    {
        target = _target;
        cine.Follow = _target;
    }

    //public void FollowTarget()
    //{
    //    Vector3 followPos;

    //    //보간 이동
    //    followPos.x = Mathf.Lerp(transform.position.x, target.position.x + offset.x, followSpeed * Time.fixedDeltaTime);
    //    followPos.y = Mathf.Lerp(transform.position.y, target.position.y + offset.y, followSpeed * Time.fixedDeltaTime);

    //    //보간 이동 클램핑
    //    followPos.x = Mathf.Clamp(followPos.x, (target.position.x + offset.x) - maxDistance.x, (target.position.x + offset.x) + maxDistance.x);
    //    followPos.y = Mathf.Clamp(followPos.y, (target.position.y + offset.y) - maxDistance.y, (target.position.y + offset.y) + maxDistance.y);

    //    //레인지 클램핑
    //    followPos.x = Mathf.Clamp(followPos.x, minRange.x, maxRange.x);
    //    followPos.y = Mathf.Clamp(followPos.y, minRange.y, maxRange.y);
    //    followPos.z = offset.z;

    //    transform.position = followPos;
    //}

    public void SetOffset(Vector3 _offset)
    {
        offset.m_TrackedObjectOffset = _offset;
    }

    public void SetOffset(Vector3 _offset, float _time, Action _callback = null)
    {
        DOTween.To(()=>offset.m_TrackedObjectOffset, x => offset.m_TrackedObjectOffset = x, _offset, _time);
    }

    public void Flip(Define.Direction _direction)
    {
        if (_direction == Define.Direction.Right) SetOffset(new Vector3(defaultOffset.x, defaultOffset.y));
        if (_direction == Define.Direction.Left) SetOffset(new Vector3(-defaultOffset.x, defaultOffset.y));
    }

    public void ShakeCamera(float _intensity, float _time, System.Action _callback = null)
    {
        shakeCallback = _callback;

        shake.m_AmplitudeGain = _intensity;
        shakeTimer = _time;
    }

    public void Update()
    {
        CheckShakeTime();
    }


    private void CheckShakeTime()
    {
        if (shakeTimer != 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
                StopShake();
        }

    }

    private void StopShake()
    {
        shake.m_AmplitudeGain = 0;
        transform.eulerAngles = Vector3.zero;

        shakeCallback?.Invoke();
        shakeCallback = null;
    }

    public void TweeningCameraSize(float _changeSize, float _time, TweenCallback _callback = null)
    {
        DOTween.To(()=> cine.m_Lens.OrthographicSize, x => cine.m_Lens.OrthographicSize = x, _changeSize, _time).OnComplete(_callback);
    }
    
    public void InitCameraSize()
    {
        DOTween.To(() => cine.m_Lens.OrthographicSize, x => cine.m_Lens.OrthographicSize = x, defaultCameraSize, 0.5f);
    }

    public void InitCameraSize(float _time)
    {
        DOTween.To(() => cine.m_Lens.OrthographicSize, x => cine.m_Lens.OrthographicSize = x, defaultCameraSize, _time);
    }

    public void ChangeCanMoveOffset(Collider2D _coll)
    {
        movePos.m_BoundingShape2D = _coll;
    }
}
