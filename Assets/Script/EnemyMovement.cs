using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private float actualSpeed;
    public float maxSpeed; 
    public Rigidbody2D rigid;
    public EnemyVariable enemyVariable;

    public delegate void OnWallReached();
    public event OnWallReached ReachedWall;

    // Use this for initialization
    void Start () {
        actualSpeed = maxSpeed;
        rigid.velocity = new Vector2(-actualSpeed, 0);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall") {
            Stop();
            ReachedWall();
        }
        
    }

    private void Stop() {
        rigid.velocity = new Vector2(0,0);
    }
}
