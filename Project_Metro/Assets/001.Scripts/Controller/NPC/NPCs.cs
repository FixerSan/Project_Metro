using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public abstract class  NPC
{
    protected NPCController controller;
    public abstract void Interaction();
}

namespace NPCs
{
    namespace Test
    {
        public class One : NPC
        {
            private bool isInteracted = false;
            public One(NPCController _controller)
            {
                controller = _controller;
            }

            public override void Interaction()
            {
                if (isInteracted) return;
                isInteracted = true;
                Managers.Dialog.Call(0, () => 
                {
                    controller.npc = new Two(controller);
                });
            }
        }

        public class Two : NPC
        {
            private bool isInteracted = false;
            public Two(NPCController _controller)
            {
                controller = _controller;
            }
            public override void Interaction()
            {
                if (isInteracted) return;
                isInteracted = true;
                Managers.Dialog.Call(2,()=> { isInteracted = false; });
            }

        }
    }
}
