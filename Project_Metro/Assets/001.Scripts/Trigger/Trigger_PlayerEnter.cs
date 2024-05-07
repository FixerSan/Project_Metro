using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger_PlayerEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerEffect(collision);
        }
    }

    protected abstract void TriggerEffect(Collider2D collision);
}
