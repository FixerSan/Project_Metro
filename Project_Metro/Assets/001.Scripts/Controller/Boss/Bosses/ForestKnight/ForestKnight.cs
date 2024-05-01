using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestKnight : BossController
{
    public float createTime;

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (!base.Init()) return false;
        Managers.Boss.InitBoss(0, this);
        return true;
    }

    public override void CreateEffect()
    {
        StartCoroutine(CreateEffectCoroutine());
    }

    public IEnumerator CreateEffectCoroutine()
    {
        yield return new WaitForSeconds(createTime);
        ChangeState(Define.BossState.Idle);
    }
}
