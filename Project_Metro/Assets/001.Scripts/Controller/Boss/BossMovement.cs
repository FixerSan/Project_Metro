using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMovement : MonoBehaviour
{
    public BossController controller;
}

namespace BossMovements
{
    public class One : BossMovement
    {
        public One(BossController _controller)
        {
            controller = _controller;
        }
    }
}