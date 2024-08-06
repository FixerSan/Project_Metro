using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public int gold;
    public float moveSpeed;
    bool isUsed = false;

    public void Init(int _gold)
    {
        gold = _gold;
        isUsed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUsed) return;
        if(collision.CompareTag("Player"))
        {
            isUsed = true;
        }
    }

    private void MoveToUI()
    {
        Vector2 moveDirection = (Managers.UI.GetSceneUI<UIScene_Main>().GoldUIWolrdPosition - (Vector2)transform.position).normalized;
       
        transform.position += (Vector3)moveDirection * moveSpeed;
    }

    private void Update()
    {
        if (!isUsed) return;
        MoveToUI();
        CheckEndMove();
    }

    private void CheckEndMove()
    {
        if(Vector2.Distance((Vector3)Managers.UI.GetSceneUI<UIScene_Main>().GoldUIWolrdPosition, transform.position) < 0.1f)
        {
            GetGold();
            Managers.Resource.Destroy(gameObject);
        }
    }

    private void GetGold()
    {
        Managers.Game.player.GetGold(gold);
    }
}
