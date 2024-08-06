using System;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EventManager
{
    public void DialogEvent(int _index)
    {
        switch (_index)
        {
            case 1:
                Managers.Game.player.level.dashLevel = 1;
                Managers.Game.player.LevelUpPlayer();
                break;
        }
    }

    public void TriggerEvent(int _index)
    {
        switch (_index)
        {
            case 0:
                Managers.Object.obstacles[0].gameObject.SetActive(!Managers.Object.obstacles[0].gameObject.activeSelf);
                break;
            case 1:
                Managers.Object.obstacles[1].Work();
                break;
        }
    }

    public void KillMonsterEvent(int _index, MonsterController _controller)
    {
        
    }
}
