using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public EnemyData data;

    private float actualSpeed;
    float actualThrust;
    bool wallReached = false;
    bool ReachedWallOnce;
    bool onFloor = false;

    public Rigidbody2D rigid;
    public EnemyVariable enemyVariable;
    LayerMask wallLayer;

    public delegate void OnWallReached();
    public event OnWallReached ReachedWall;
    public delegate void OnWallNotReached();
    public event OnWallReached NotReachedWall;

    

    // Use this for initialization
    void Start () {
        actualThrust = data.thrust;
        actualSpeed = data.maxSpeed;
        rigid.AddForce(new Vector2(-actualSpeed, 0), ForceMode2D.Force);
        wallLayer = LayerMask.NameToLayer("Wall");
    }

    private void Update()
    {
        if (!wallReached && onFloor)
        {
            rigid.AddForce(new Vector2(actualThrust, 0) * Vector2.left, ForceMode2D.Force);
            if (rigid.velocity.x <= -actualSpeed)
                rigid.velocity = new Vector2(-actualSpeed, rigid.velocity.y);         
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, data.wallDistance, 1 << wallLayer.value);
        if (hit.collider != null)
        {
            wallReached = true;
            ReachedWallOnce = true;
        }
        else
            wallReached = false;

        if (wallReached)
        {
            Stop();
            ReachedWall();
        }
        else if (!wallReached && ReachedWallOnce){
            NotReachedWall();
            Restart();
        }
            
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
            onFloor = true;
        if (collision.gameObject.tag == "Armi")
            Restart();

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
            onFloor = false;
    }

    public void Stop() {
        actualThrust = 0;
    }

    public void Restart() {
        actualThrust = data.thrust;
    }
}
