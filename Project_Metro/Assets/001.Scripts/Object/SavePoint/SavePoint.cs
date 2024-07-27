using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public int savePointIndex;

    public void Save()
    {
        Managers.Game.SaveGame(savePointIndex);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Managers.Input.MoveAxis.y > 0)
        {
            Save();
        }
    }
}
