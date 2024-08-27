using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollisionAttack : MonoBehaviour
{
    public MonsterController controller;
    private bool isWalking = false;
    public void Init(MonsterController _controller)
    {
        isWalking = true;
        controller = _controller;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isWalking) return;
        if (collision.CompareTag("Player"))
            Managers.Object.playerController.Hit(controller.status.CurrentDamageForce, controller);
    }

    public void Disable()
    {
        isWalking = false;
    }
}
