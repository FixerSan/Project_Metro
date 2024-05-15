using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlot_HPCount : UIBase
{
    private Animator anim;
    private bool isDestroyed = false;
    public override bool Init()
    {
        if(!base.Init()) return false;
        anim = Util.FindChild<Animator>(gameObject, "Image_HPCount", true);

        Appear();
        return true;
    }

    public void Appear()
    {
        if (!isDestroyed) return;
        anim.Play("Create");
        isDestroyed = false;
    }

    public void Destroy()
    {
        if (isDestroyed) return;
        anim.Play("Destroy");
        isDestroyed = true;
    }

}
