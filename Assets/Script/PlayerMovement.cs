using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody2D rigid;
    public float movementSpeed;
    public float maxHeight;
    public float minHeight;
    public bool moveUp;
    public bool moveDown;


    private void Update()
    {
        if (transform.position.y >= maxHeight && moveUp) {
            moveUp = false;
            Stop();
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.x);
        }
        else if (transform.position.y <=  minHeight && moveDown)
        {
            moveDown = false;
            Stop();
            transform.position = new Vector3(transform.position.x, minHeight, transform.position.x);
        }



    }
    public void StartMoveUp() {
        moveUp = true;
        rigid.velocity = new Vector2(0, movementSpeed);
    }

    public void StartMoveDown() {
        moveDown = true;
        rigid.velocity = new Vector2(0, -movementSpeed);
    }

    public void EndMoveUp()  {
        moveUp = false;
        Stop();
    }

    public void EndMoveDown() {
        moveDown = false;
        Stop();
    }

    public void Stop() {
        rigid.velocity = new Vector2(0, 0);
    }

}
