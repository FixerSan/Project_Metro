using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Dictionary<Define.PlayerMovementType, int> movementLevels = new Dictionary<Define.PlayerMovementType, int>();
    public int attackLevel = 1;

    public void Awake()
    {
        GameStart();
    }

    public void GameStart()
    {
        Managers.Resource.LoadAllAsync<Object>("default");
    }
}
