using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ForestKight_VinesToMove : MonoBehaviour
{
    public LineRenderer line;
    private Transform startPoint;
    public Transform endPoint;

    public float minMoveAngle;
    public float maxMoveAngle;

    public float endPointCollisionDistance;
    public float endPointMoveSpeed;

    private Vector3 moveDirection;

    public bool isDetectWall = false;

    public void StartMove(Transform _startPoint, Vector3 _moveDirection, Action<Vector3> _callback = null)
    {
        isDetectWall = false;
        transform.position = Vector3.zero;
        line.positionCount = 2;

        startPoint = _startPoint;
        moveDirection = _moveDirection;
        endPoint.position = _startPoint.position;

        StartCoroutine(MoveRoutine(_callback));
    }
    
    private IEnumerator MoveRoutine(Action<Vector3> _callback = null)
    {
        while (!isDetectWall)
        {
            yield return null;
            endPoint.position = endPoint.position + moveDirection.normalized * endPointMoveSpeed * Time.deltaTime;
            isDetectWall = CheckCollision();
        }
        _callback.Invoke((endPoint.position - startPoint.position).normalized);
    }

    private bool CheckCollision()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(endPoint.position, endPointCollisionDistance);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if (collider2Ds[i].CompareTag("Wall"))
                return true;
        }
        return false;
    }

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        //StartMove(new Vector3(41.9700012f, -27.4899998f, -0.0610577241f));
    }

    private void Update()
    {
        line.SetPosition(0, startPoint.position);
        line.SetPosition(1, endPoint.position);
    }
}
