using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cine;
    public Transform target;
    public float defaultCameraSize;

    private CinemachineBasicMultiChannelPerlin shake;
    private float shakeTimer;

    //public Vector3 offset;
    //public Vector2 maxDistance;
    //public Vector2 minRange, maxRange;

    //public float followSpeed;

    public void Awake()
    {
        Managers.Screen.SetCamera(this);
        cine = GetComponentInChildren<CinemachineVirtualCamera>();
        shake = cine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
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

    public void ShakeCamera(float _intensity, float _time)
    {
        shake = cine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        shake.m_AmplitudeGain = _intensity;
        shakeTimer = _time;
    }

    public void Update()
    {
        CheckShakeTime();
    }

    public void CheckShakeTime()
    {
        if (shakeTimer != 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
                StopShake();
        }

    }

    public void StopShake()
    {
        shake.m_AmplitudeGain = 0;
        transform.eulerAngles = Vector3.zero;
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
}
