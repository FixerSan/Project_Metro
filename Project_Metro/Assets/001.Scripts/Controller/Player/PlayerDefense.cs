using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDefense 
{
    public PlayerController controller;
}

namespace PlayerDefenses
{
    public class One : PlayerDefense
    {
        public One(PlayerController _controller)
        {
            controller = _controller;
        }
    }
}
