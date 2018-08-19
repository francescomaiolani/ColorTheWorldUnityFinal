using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PlayerMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Transform player;
    public Rigidbody2D rigid;
    public float movementSpeed;
    public float maxHeight;
    public float minHeight;
    bool moveUp;
    bool moveDown;

    float offset = 0.5f;
    Vector3 globalMousePos;
    bool move;
    public RectTransform slider;
    public RectTransform handle;
    float initialOffset;
    bool dragging;
    float maxRange;
    /*
    public void OnBeginDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }


    public void OnDrag(PointerEventData data)
    {
        SetDraggedPosition(data);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Stop();
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, data.position, data.pressEventCamera, out globalMousePos))
        {
            if (globalMousePos.y > player.transform.position.y + offset && player.position.y <= maxHeight)         
                moveUp = true;
            
            else if (globalMousePos.y < player.transform.position.y - offset && player.position.y >= minHeight)
                moveDown = true;
        }
        else
            Stop();
    }

    private void Update()
    {
        if (moveUp && globalMousePos.y > player.transform.position.y + offset && player.position.y <= maxHeight)
            rigid.velocity = new Vector2(0, movementSpeed);
        else if (moveDown && globalMousePos.y < player.transform.position.y - offset && player.position.y >= minHeight)
            rigid.velocity = new Vector2(0, -movementSpeed);
        else
            Stop();

    }*/
    private void Start()
    {
        initialOffset = slider.anchoredPosition.y;
        maxRange = slider.sizeDelta.y / 2;
    }
    public void Stop()
    {
        moveUp = false;
        moveDown = false;
        rigid.velocity = new Vector2(0, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        DragHandler(eventData);
    }

    void DragHandler(PointerEventData eventData)
    {
        if (eventData.position.y > handle.position.y)
        {
            handle.anchoredPosition = new Vector2(handle.anchoredPosition.x, Mathf.Clamp(eventData.position.y - maxRange, 0, maxRange));
        }
        
        else if (eventData.position.y < handle.position.y)
        {
            handle.anchoredPosition = new Vector2(handle.anchoredPosition.x, Mathf.Clamp(-eventData.position.y + maxRange, -maxRange, 0));
        }
    }

    public void OnDrag(PointerEventData data)
    {
        DragHandler(data);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        handle.anchoredPosition = new Vector2(0, 0);
        Stop();
    }

}

