using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public void Init()
    {
        Managers.Object.SpawnPlayerController(new Vector3(-2f, 10.5f, 0f));
        Managers.Object.SpawnObstacle(0, new Vector3(-3.94f, -3.1f, 0));
        Managers.Object.SpawnObstacle(1, new Vector3(3.1f, -3.11f, 0));
        Managers.Object.SpawnTrigger(0, new Vector3(-10, -3f, 0));
        Managers.Object.SpawnTrigger(1, new Vector3(2.75f, -5.5f, 0));
        Managers.Object.SpawnMonster(0, new Vector3(-9.37f, -3.12f));
        Managers.Object.camera.target = Managers.Object.player.transform;
    }
}
