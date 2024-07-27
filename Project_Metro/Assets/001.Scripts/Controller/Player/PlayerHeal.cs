using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeal
{
    public PlayerController controller;
    public int level;

    public int currentCanHealCount;
    public int maxHealCount;
    public int healForce;

    public float healSpeed;

    public Coroutine healCoroutine;

    public PlayerHeal(PlayerController _controller)
    {
        controller = _controller;

        level = 1;
        currentCanHealCount = 3;
        maxHealCount = 3;
        healForce = 1;
        healSpeed = 2;
    }

    public bool IsCanHeal
    {
        get
        {
            if (currentCanHealCount == 0) return false;
            if (Managers.Game.player.status.currentHP == Managers.Game.player.status.CurrentMaxHP) return false;
            return true;
        }
    }

    public bool CheckHeal()
    {
        if (!IsCanHeal) return false;
        if (!Managers.Input.GetHealKey) return false;
        controller.ChangeState(Define.PlayerState.Heal);
        return true;
    }

    public void StartHeal()
    {
        currentCanHealCount--;
        Managers.UI.SceneUI?.RedrawUI();
        healCoroutine = controller.StartCoroutine(HealRoutine());
    }

    public IEnumerator HealRoutine()
    {
        yield return new WaitForSeconds(healSpeed);
        Heal();
        controller.ChangeState(Define.PlayerState.Idle);
        healCoroutine = null;
    }

    public void Heal()
    {
        controller.Player.GetHP(healForce);
    }

    public void CancelHeal()
    {
        if(healCoroutine != null) controller.StopCoroutine(healCoroutine);
    }

    public void SetAnimation()
    {
        //TODO::���⼭ �����̴� ���̸� PlayerMove�� isMove�� �����ͼ� �ִϸ��̼ǿ� �߰����ְ�
        //�ִϸ��̼ǿ����� isMove�� ���� �����̸鼭 �� �ϴ� ��, �ƴѰ� �����ؼ� ����
    }
}
