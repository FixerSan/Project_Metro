using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScene_Main : UIScene
{
    private List<UISlot_HPCount> slots = new List<UISlot_HPCount>();
    private Transform slotPlace;
    private int nowHP;
    private int nowGold;

    public float goldRedrawSpeed = 0.05f;
    public Vector2 GoldUIWolrdPosition
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(GetImage((int)Images.Image_GoldIcon).rectTransform.position);
        }
    }
    private WaitForSeconds goldRedrawWaitForSeconds;

    private Coroutine redrawGoldCoroutine;

    public override bool Init()
    {
        if(!base.Init()) return false;
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindSlider(typeof(Sliders));
        Bind<CanvasGroup>(typeof(CanvasGroups));
        slotPlace = Util.FindChild<Transform>(gameObject, "Trans_SlotPlace", true);

        for (int i = 0; i < Managers.Game.player.status.CurrentMaxHP; i++)
        {
            UISlot_HPCount slot = Managers.Resource.Instantiate("UISlot_HPCount").GetComponent<UISlot_HPCount>();
            slot.transform.SetParent(slotPlace);
            slot.Init();
            slots.Add(slot);
        }
        goldRedrawWaitForSeconds = new WaitForSeconds(goldRedrawSpeed);
        nowHP = Managers.Game.player.status.CurrentMaxHP;
        nowGold = Managers.Game.player.gold;
        RedrawUI();
        return true;
    }

    public override void RedrawUI()
    {
        //HP ¾÷
        if(Managers.Game.player.status.currentHP > nowHP)
        {
            for (int i = 0; i < Managers.Game.player.status.currentHP; i++)
            {
                slots[i].Appear();
            }

            nowHP = Managers.Game.player.status.currentHP;
        }

        //HP ´Ù¿î
        else if(Managers.Game.player.status.currentHP < nowHP)
        {
            for (int i = 0; i < nowHP - Managers.Game.player.status.currentHP; i++)
            {
                slots[(nowHP - 1) - i].Destroy();
            }

            nowHP = Managers.Game.player.status.currentHP;
        }

        //ATB
        if (Managers.Game.player.isBattle)
        {
            if(!GetSlider((int)Sliders.Slider_ATB).gameObject.activeSelf)
            {
                Get<CanvasGroup>((int)CanvasGroups.Slider_ATB).alpha = 0;
                Get<CanvasGroup>((int)CanvasGroups.Slider_ATB).DOFade(1 , 0.5f);
                GetSlider((int)Sliders.Slider_ATB).gameObject.SetActive(true);
            }
            GetSlider((int)Sliders.Slider_ATB).value = Managers.Game.player.status.currentATB / Managers.Game.player.status.CurrentMaxATB;
        }

        else
        {
            Get<CanvasGroup>((int)CanvasGroups.Slider_ATB).alpha = 1;
            Get<CanvasGroup>((int)CanvasGroups.Slider_ATB).DOFade(0, 0.5f).onComplete += ()=> 
            {
                GetSlider((int)Sliders.Slider_ATB).gameObject.SetActive(false);
            };
        }

        //HealCount
        GetText((int)Texts.Text_HealCount).text = $"{Managers.Game.player.heal.currentCanHealCount}/{Managers.Game.player.heal.maxHealCount}";
        if (Managers.Game.player.heal.currentCanHealCount == 0)
            GetImage((int)Images.Image_HealCountDisablePanel).gameObject.SetActive(true);
        else if(GetImage((int)Images.Image_HealCountDisablePanel).gameObject.activeSelf)
            GetImage((int)Images.Image_HealCountDisablePanel).gameObject.SetActive(false);

        GetText((int)Texts.Text_PlayerState).text = Managers.Object.playerController.CurrentState.ToString();

        ////Gold
        DOTween.To(() => nowGold, x => 
        {
            nowGold = x;
            GetText((int)Texts.Text_Gold).text = nowGold.ToString(); 
        }, Managers.Game.player.gold, 0.5f);

    }

    public enum Images
    {
        Image_HealCountDisablePanel, Image_GoldIcon
    }

    public enum Texts
    {
        Text_HealCount, Text_PlayerState, Text_Gold
    }

    public enum Sliders
    {
        Slider_ATB
    }

    public enum CanvasGroups
    {
        Slider_ATB
    }
}
