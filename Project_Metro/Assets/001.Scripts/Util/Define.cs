using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Define 
{
    public enum UIEventType
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
        BeginDrag,
        Drag,
        EndDrag,
        Drop
    }

    public enum UIType
    {

    }

    public enum Scene
    {
        Login, Main, Stage, Test
    }

    public enum SoundType
    {

    }

    public enum PlayerState
    {
        Idle = 0,
        Move = 1,
        Jump = 2,
        JumpMove,
        Fall,
        FallMove,
    }

    public enum PlayerMovementType
    {
        Walk, Dash
    }
}
