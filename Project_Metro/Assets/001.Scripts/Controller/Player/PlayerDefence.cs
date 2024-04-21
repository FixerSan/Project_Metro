using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDefence 
{
    public PlayerController controller;
    public int defenceForce;
    public abstract bool CheckDefence();
    public abstract bool CheckEndDefence();
}

namespace PlayerDefenses
{
    public class One : PlayerDefence
    {
        public One(PlayerController _controller)
        {
            controller = _controller;
            defenceForce = 1;
        }

        public override bool CheckDefence()
        {
            if(Managers.Input.GetDefenceKey)
            {
                controller.ChangeState(Define.PlayerState.Defence);
                return true;
            }
            return false;
        }

        public override bool CheckEndDefence()
        {
            if (!Managers.Input.GetDefenceKey)
            {
                controller.ChangeState(Define.PlayerState.Idle);
                return true;
            }
            return false;
        }
    }
}
