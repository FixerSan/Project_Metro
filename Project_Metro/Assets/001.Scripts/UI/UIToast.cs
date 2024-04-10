using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIToast : UIBase
{
    enum Images
    {
        //BackgroundImage
    }

    enum Texts
    {
        Text_Description,
    }

    public void OnEnable()
    {
        
    }

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.UI.SetCanvas(gameObject, _isToast: true);
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        Refresh();
        return true;
    }

    public void SetInfo(string _description)
    {
        // 메시지 변경
        Refresh();
        transform.localScale = Vector3.one;
        GetText((int)Texts.Text_Description).text = _description;
        GetText((int)Texts.Text_Description).rectTransform.DOAnchorPosY(500f,3);
        GetText((int)Texts.Text_Description).DOFade(0, 3).onComplete += () => 
        {
            Managers.UI.CloseToastUI();
        };
    }

    public void SetColor(Color _color)
    {
        GetText((int)Texts.Text_Description).color = _color;
    }

    public void Refresh()
    {
        GetText((int)Texts.Text_Description).text = string.Empty;
        GetText((int)Texts.Text_Description).color = Color.white;
        GetText((int)Texts.Text_Description).rectTransform.DOAnchorPosY(280, 0);
    }
}
