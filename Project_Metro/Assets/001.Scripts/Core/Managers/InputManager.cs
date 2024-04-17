using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager 
{
    public PlayerAction actions;
    public Vector2 MoveAxis { get { return actions.Player.Move.ReadValue<Vector2>(); } }

    public InputManager()
    {
        actions = new PlayerAction();
        actions.Player.Move.Enable();
        actions.Player.Jump.Enable();
        actions.Player.Attack.Enable();
    }

    public void SetCanControl(bool _isCanControl)
    {
        if (_isCanControl) actions.Player.Enable();
        else actions.Player.Disable();
    }
}
