using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Test : SceneBase
{
    public override void Init()
    {
        Managers.Object.SpawnPlayerController(new Vector3(0, 10.5f, 0));
        Managers.UI.ShowSceneUI<UIScene_Main>();
        Managers.Object.SpawnObstacle(0, new Vector3(-3.94f, -3.1f, 0));
        Managers.Object.SpawnObstacle(1, new Vector3(3.1f, -3.11f, 0));
        Managers.Object.SpawnTrigger(0, new Vector3(-10, -3f, 0));
        Managers.Object.SpawnTrigger(1, new Vector3(2.75f, -5.5f, 0));
        Managers.Object.SpawnMonster(0, new Vector3(-9.37f, -3.12f));
        Managers.Object.SpawnMonster(0, new Vector3(6f, -3f));
        Managers.Object.Camera.target = Managers.Object.player.transform;
    }

    public override void Clear()
    {

    }
}
