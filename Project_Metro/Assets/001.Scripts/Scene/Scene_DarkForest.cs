using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_DarkForest : SceneBase
{
    public override void Clear()
    {

    }

    public override void Init()
    {
        Managers.Object.SpawnMonster(0, new Vector2(20, 0));
    }
}
