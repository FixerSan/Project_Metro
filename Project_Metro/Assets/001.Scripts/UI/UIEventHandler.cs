using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Action OnClickHandler = null;
    public Action OnPressedHandler = null;
    public Action OnPointerDownHandler = null;
    public Action OnPointerUpHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnBeginDragHandler = null;
    public Action<PointerEventData> OnEndDragHandler = null;
    public Action<PointerEventData> OnDropHandler = null;

    private bool isPressed = false;
    private void Update()
    {
        if (isPressed)
            OnPressedHandler?.Invoke();
    }
    public void OnPointerClick(PointerEventData _eventData)
    {
        OnClickHandler?.Invoke();
    }

    public void OnBeginDrag(PointerEventData _eventData)
    {
        OnBeginDragHandler?.Invoke(_eventData);
    }


    public void OnEndDrag(PointerEventData _eventData)
    {
        OnEndDragHandler?.Invoke(_eventData);
    }

    public void OnDrag(PointerEventData _eventData)
    {
        isPressed = true;
        OnDragHandler?.Invoke(_eventData);
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        isPressed = true;
        OnPointerDownHandler?.Invoke();
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        isPressed = true;
        OnPointerUpHandler?.Invoke();
    }

    public void OnDrop(PointerEventData _eventData)
    {
        OnDropHandler?.Invoke(_eventData);
    }

}