using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollisionAttack : MonoBehaviour
{
    public MonsterController controller;
    public void Init(MonsterController _controller)
    {
        controller = _controller;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Managers.Object.playerController.Hit(controller.status.CurrentDamageForce, controller);
    }
}
