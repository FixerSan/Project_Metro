using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool canWorkAgain;
    protected bool isWorked = false;
    public int index;
    
    public void WorkTrigger()
    {
        Managers.Event.TriggerEvent(index);
        isWorked = true;
    }
}
