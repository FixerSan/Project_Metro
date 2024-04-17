using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public void Init()
    {
        Managers.Object.SpawnObstacle(0, new Vector3(-3.94f, -3.1f, 0));
        Managers.Object.SpawnObstacle(1, new Vector3(3.1f, -3.11f, 0));
        Managers.Object.SpawnTrigger(0, new Vector3(-10, -3f, 0));
        Managers.Object.SpawnTrigger(1, new Vector3(2.75f, -5.5f, 0));
    }
}
