using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class UIScene_Main : UIScene
{
    private List<UISlot_HPCount> slots = new List<UISlot_HPCount>();
    private Transform slotPlace;
    private int nowHP;

    public override bool Init()
    {
        if(!base.Init()) return false;
        BindImage(typeof(Images));
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

        nowHP = Managers.Game.player.status.CurrentMaxHP;
        RedrawUI();
        return true;
    }

    public override void RedrawUI()
    {
        //HP
        //for (int i = Managers.Game.player.status.CurrentMaxHP; i > Managers.Game.player.status.currentHP; i--)
        //{
        //    slots[i-1].Destroy();
        //}

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
    }

    public enum Images
    {

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
