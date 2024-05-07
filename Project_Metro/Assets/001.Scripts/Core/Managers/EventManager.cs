using System;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EventManager
{
    // 이벤트 액션 변수 선언
    public Action<VoidEventType> OnVoidEvent;
    public Action<IntEventType, int> OnIntEvent;

    public Dictionary<VoidEventType, Action> voidEvents;
    public Action updateEvent;

    public EventManager()
    {
        voidEvents = new Dictionary<VoidEventType, Action>();
    }

    public void AddVoidEvent(VoidEventType _type, Action _eventAction)
    {
        if (voidEvents.TryGetValue(_type, out Action eventAction))
        {
            eventAction -= _eventAction;
            eventAction += _eventAction;
        }

        else
        {
            voidEvents.Add(_type, new Action(_eventAction));
        }
    }

    public void InvokeVoidEvent(VoidEventType _type)
    {
        if (voidEvents.TryGetValue(_type, out Action eventAction))
            eventAction?.Invoke();
    }

    public void RemoveVoidEvent(VoidEventType _type, Action _eventAction)
    {
        if (voidEvents.TryGetValue(_type, out Action eventAction))
        {
            eventAction -= _eventAction;
            if (eventAction.Target == null)
                voidEvents.Remove(_type);
        }
    }

    public void AddUpdate(Action _eventAction)
    {
        updateEvent -= _eventAction;
        updateEvent += _eventAction;
    }

    public void Update()
    {
        updateEvent?.Invoke();
    }

    public void RemoveUpdate(Action _eventAction)
    {
        updateEvent -= _eventAction;
    }

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
}
