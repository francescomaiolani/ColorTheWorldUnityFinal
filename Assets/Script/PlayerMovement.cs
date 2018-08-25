using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PlayerMovement : MonoBehaviour
{

    public Transform player;
    public Rigidbody2D rigid;
    public float movementSpeed;
    public float maxHeight;
    public float minHeight;

    bool moveUp;
    bool moveDown;

    //public Slider slider;
    //float sliderMidValue;


    /* float offset = 0.5f;
     Vector3 globalMousePos;
     bool move;
     public RectTransform slider;
     public RectTransform handle;
     float initialOffset;
     bool dragging;
     float maxRange;
     PointerEventData globalData;*/
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
    /*
    private void Start()
    {
        initialOffset = slider.anchoredPosition.y;
        maxRange = slider.sizeDelta.y / 2;
    }
    public void Stop()
    {
       // moveUp = false;
        //moveDown = false;
        rigid.velocity = new Vector2(0, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        globalData = eventData;
    }

    public void OnDrag(PointerEventData data)
    {
        globalData = data;
    }

    private void Update()
    {
        if (dragging)
            DragHandler(globalData);

        if ((moveUp && player.position.y <= maxHeight) || (moveDown && player.position.y >= -minHeight))
            rigid.velocity = new Vector2(0, (handle.anchoredPosition.y / maxRange) * movementSpeed);
        else if ((moveUp && player.position.y > maxHeight))
            Stop();
        else if (moveDown && player.position.y < -minHeight)
            Stop();
    }

    void DragHandler(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
        if (eventData.position.y > handle.position.y)
        {
            if (handle.anchoredPosition.y < maxRange)
                handle.anchoredPosition = new Vector2(handle.anchoredPosition.x, eventData.position.y - maxRange);
            else
                handle.anchoredPosition = new Vector2(handle.anchoredPosition.x, maxRange);

            moveUp = true;
            moveDown = false;
        }

        else if (eventData.position.y < handle.position.y)
        {
            if (handle.anchoredPosition.y > -maxRange)
                handle.anchoredPosition = new Vector2(handle.anchoredPosition.x,eventData.position.y - maxRange);
            else
                handle.anchoredPosition = new Vector2(handle.anchoredPosition.x,-maxRange);

            moveDown = true;
            moveUp = false;
        } 
    }

   
    public void OnEndDrag(PointerEventData eventData)
    {
        moveDown = false;
        moveUp = false;
        dragging = false;
        handle.anchoredPosition = new Vector2(0, 0);
        Stop();


    }
    
         
    public void OnEndDrag(PointerEventData data) {
        Stop();
    }*/

    private void Start()
    {
        //sliderMidValue = slider.maxValue / 2;
    }

    private void Update()
    {
        /*
        if (slider.value > sliderMidValue)
        {
            moveUp = true;
            moveDown = false;
        }
        else if (slider.value < sliderMidValue) {
            moveUp = false;
            moveDown = true;
        }

        if (moveUp && player.position.y < maxHeight)
            rigid.velocity = new Vector2(0, (slider.value - sliderMidValue) / sliderMidValue * movementSpeed);
        else if (moveDown && player.position.y > minHeight)
            rigid.velocity = new Vector2(0, (sliderMidValue - slider.value) / sliderMidValue * -movementSpeed);
        else
            Stop();
            */
        if (moveUp && player.position.y < maxHeight)
            rigid.velocity = new Vector2(0, movementSpeed);
        else if (moveDown && player.position.y > minHeight)
            rigid.velocity = new Vector2(0,-movementSpeed);
        else
            Stop();

        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1)
            Time.timeScale = 0;
        else if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0)
            Time.timeScale = 1;
    }

    public void StartMoveUp() {
        moveUp = true;
    }
    public void StartMoveDown()
    {
        moveDown = true;
    }

    public void EndMoveUp() {
        moveUp = false;
    }

    public void EndMoveDown() {
        moveDown = false;
    }
    public void Stop()
    {
        moveUp = false;
        moveDown = false;
        rigid.velocity = new Vector2(0, 0);
        //slider.value = sliderMidValue;
    }

    

}

