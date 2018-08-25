using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {


    public float bulletSpeed;
    public Rigidbody2D rigid;
    public int perforante;
    public float damage;
    public GameObject spruzzo;
    //QUANTI ALLA VOLTA
    public int count;
    //VARIAZIONE DEL'ANGOLO A SECONDA DI QUANTI SONO
    public float spread;

    public void SetBulletStats(float danno,  int perfor, int countA, float spreadA, int index) {
        perforante = perfor;
        count = countA;
        spread = spreadA;
        damage = danno;
        StartBulletMovement(index);
    }

    public void StartBulletMovement(int index) {
        float angoloInziale = (count / 2) * spread;
        float angle = (angoloInziale - index * spread)*Mathf.Deg2Rad ;
        rigid.velocity = new Vector2(bulletSpeed * Mathf.Cos(angle), bulletSpeed * Mathf.Sin(angle));
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            collision.GetComponent<EnemyVariable>().ApplyDamage(damage);
            collision.GetComponent<EnemyVariable>().SpawnSpotOnBody(transform.position);
            GameObject spruzzoInstance = Instantiate(spruzzo, transform.position, Quaternion.identity);
            Destroy(spruzzoInstance, 1f);
            perforante--;
            if (perforante <= 0) {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
