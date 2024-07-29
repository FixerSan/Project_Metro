using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    private Actor actor;
    public GameObject uiSpriteObject;

    public void Start()
    {
        Init(null);
    }

    public void Init(Actor _actor)
    {
        actor = _actor;
        uiSpriteObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            uiSpriteObject.SetActive(true);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Managers.Input.GetInteractionKey)
        {
            Managers.Game.player.GetSoulSkill(Define.SoulSkill.VineHeart);
            //Managers.Game.GetSoulSkill(actor.soulSkill);
            //actor.GetEatenSoul();
            Managers.Resource.Destroy(this.gameObject);
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiSpriteObject.SetActive(false);
        }
    }
}
