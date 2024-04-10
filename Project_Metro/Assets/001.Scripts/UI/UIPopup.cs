using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPopup : UIBase
{
    public override bool Init()
    {
        if (!base.Init())
            return false;

        Managers.UI.SetCanvas(gameObject, true);
        return true;
    }

    public virtual void ClosePopupUP()
    {
        Managers.UI.ClosePopupUI(this);
    }

    protected virtual void Update()
    {

    }

    private void OnDisable()
    {

    }

    public virtual void RedrawUI()
    {

    }
}
