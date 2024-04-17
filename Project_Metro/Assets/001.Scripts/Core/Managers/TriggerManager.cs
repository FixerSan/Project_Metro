using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager
{
    public void WorkTrigger(int _index)
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
}
