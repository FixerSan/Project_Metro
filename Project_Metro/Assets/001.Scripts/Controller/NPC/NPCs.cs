using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class  NPC
{
    public abstract void Interaction();
}

namespace NPCs
{
    namespace Test
    {
        public class One : NPC
        {
            public override void Interaction()
            {
                Managers.Game.player.level.dashLevel = 1;
                Managers.Game.player.LevelUpPlayer();
            }
        }
    }
}
