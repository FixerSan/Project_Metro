using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_TestBoss : SceneBase
{
    public override void Init()
    {
        Managers.Object.SpawnBoss(0, new Vector3(41.97f,-27.49f, 0));
        Managers.Object.Camera.target = Managers.Object.SpawnPlayerController(new Vector2(27.85f, -27.49f)).transform;
    }

    public override void Clear()
    {

    }

}
