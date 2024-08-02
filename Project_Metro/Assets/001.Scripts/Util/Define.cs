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
        Scene_Test, Scene_TestBoss, Scene_DarkForest
    }

    public enum SoundType
    {

    }

    public enum PlayerState
    {
        Idle = 0,
        Move = 1,
        Jump = 2,
        Fall = 3,
        Dash = 4,
        Defence = 5,
        Heal = 6,
        Hit = 7,
        Save = 8,
        Climb = 9
    }

    public enum PlayerAttack
    {
        NormalAttack
    }

    public enum PlayerMovementType
    {
        Walk, Dash
    }

    public enum Direction
    {
        Left = -1, Right = 1
    }

    public enum PlayerAttackDirection
    {
        Left, Right, Up, Down
    }

    public enum MonsterState
    {
        Idle,
        Move,
        Follow,
        Attack,
        Death
    }

    public enum BossState
    {
         Create, Idle, ActionOne, ActionTwo, ActionThree, ActionFour, Death
    }

    public enum BossAction
    {
        ActionOne, ActionTwo, ActionThree, ActionFour
    }

    public enum VoidEventType
    {
        
    }

    public enum IntEventType
    {

    }

    public enum SoulSkill
    {
        SoulEater, VineHeart
    }
}
