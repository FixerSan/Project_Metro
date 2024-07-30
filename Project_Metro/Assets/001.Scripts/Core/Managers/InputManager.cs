using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager 
{
    public PlayerAction actions;

    public Vector2 MoveAxis { get { return actions.Player.Move.ReadValue<Vector2>(); } }
    public bool GetJumpKey { get { return actions.Player.Jump.triggered; } }
    public bool GetAttackKey { get { return actions.Player.Attack.ReadValue<float>() != 0f; } }
    public bool GetDashKey { get { return actions.Player.Dash.ReadValue<float>() != 0f; } }
    public bool GetDefenceKey { get { return actions.Player.Defence.ReadValue<float>() != 0f; } }
    public bool GetHealKey { get { return actions.Player.Heal.ReadValue<float>() != 0f; } }

    public bool GetInteractionKey { get { return actions.Player.Move.ReadValue<Vector2>().y > 0; } }

    public InputManager()
    {
        actions = new PlayerAction();
        actions.Player.Move.Enable();
        actions.Player.Jump.Enable();
        actions.Player.Attack.Enable();
        actions.Player.Dash.Enable();
        actions.Player.Defence.Enable();
        actions.Player.Heal.Enable();
    }

    public void SetCanControl(bool _isCanControl)
    {
        if (_isCanControl) actions.Player.Enable();
        else actions.Player.Disable();
    }
}
