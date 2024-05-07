using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class UIPopup_Dialog : UIPopup
{
    private DialogData data;
    public float typingSpeed;
    private bool isTyping;

    public override bool Init()
    {
        if (!base.Init()) return false;
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));

        BindEvent(GetButton((int)Buttons.Button_Next).gameObject, Managers.Dialog.OnClick_NextButton);
        BindEvent(gameObject, OnClick_Skip);

        return true;
    }

    public void Redraw(DialogData _data)
    {
        data = _data;
        GetButton((int)Buttons.Button_Next).gameObject.SetActive(false);
        GetText((int)Texts.Text_Name).text = data.name;
        GetText((int)Texts.Text_Next).text = data.buttonString;
        Managers.Resource.Load<Sprite>($"Illust_{data.name}", (_sprite) => { GetImage((int)Images.Image_Illust).sprite = _sprite; });

        StartCoroutine("SentenceRoutine");
    }

    public IEnumerator SentenceRoutine()
    {
        isTyping = true;
        for (int i = 0; i < data.sentence.Length+1; i++)
        {
            yield return new WaitForSeconds(typingSpeed);
            GetText((int)Texts.Text_Sentence).text = data.sentence.Substring(0, i);
        }

        yield return new WaitForSeconds(typingSpeed);

        Managers.Dialog.CheckEvent((_bool) => 
        {
            if(!_bool) 
                GetButton((int)Buttons.Button_Next).gameObject.SetActive(true);
        });
        isTyping = false;
    }

    public void OnClick_Skip()
    {
        if(isTyping)
        {
            isTyping = false;
            StopCoroutine("SentenceRoutine");
            GetText((int)Texts.Text_Sentence).text = data.sentence;
            Managers.Dialog.CheckEvent((_bool) =>
            {
                if (!_bool)
                    GetButton((int)Buttons.Button_Next).gameObject.SetActive(true);
            });
        }
        else
        {
            Managers.Dialog.OnClick_NextButton();
        }
    }

    public void OnDisable()
    {
        GetButton((int)Buttons.Button_Next).gameObject.SetActive(false);
        GetImage((int)Images.Image_Illust).sprite = null;
        GetText((int)Texts.Text_Name).text = string.Empty;
        GetText((int)Texts.Text_Next).text = string.Empty;
        GetText((int)Texts.Text_Sentence).text = string.Empty;
    }

    private enum Texts
    {
        Text_Name, Text_Sentence, Text_Next
    }

    private enum Buttons
    {
        Button_Next
    }

    private enum Images
    {
        Image_Illust, Image_Panel,
    }
}
