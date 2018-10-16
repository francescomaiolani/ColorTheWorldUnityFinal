using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonMovement : MonoBehaviour {

    public Rigidbody2D rigid;
    public Baloon actualBaloon;
    public GameObject spruzzo;

    public GameObject floorStain;
    Color32 actualColor;

    bool launched;

    public void ShootBaloon(float angle, float shootDistance, Baloon baloon, Color32 color) {

        rigid.WakeUp();
        rigid.simulated = true;
        actualColor = color;
        //pallincino normale
        actualBaloon = baloon;
        float adaptiveVelocity = actualBaloon.GetMaxSpeed() * shootDistance;
        rigid.velocity = new Vector2(adaptiveVelocity * Mathf.Cos(angle), adaptiveVelocity * Mathf.Sin(angle));
        launched = true;
    }

    private void FixedUpdate()
    {
        if (launched) {
            float angle = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void ApplyImpulseToEnemy(Rigidbody2D rigid) {

        float angleOfImpulse = (Mathf.Abs(Vector2.Angle(rigid.velocity.normalized, Vector2.right)) + 10) * Mathf.Deg2Rad;
        Vector2 impulseVector = new Vector2(Mathf.Cos(angleOfImpulse) * actualBaloon.GetImpactImpulse(), Mathf.Sin(angleOfImpulse) * actualBaloon.GetImpactImpulse());
        rigid.AddForce(impulseVector, ForceMode2D.Impulse);
    }

    private void ApplyDamageAndStain(EnemyVariable enemyHit) {
        enemyHit.ApplyDamage(actualBaloon.GetDamage());
        enemyHit.SpawnSpotOnBody(transform.position, actualColor);
    }

    private void InstantiateParticleSpruzzo()
    {
        GameObject spruzzoInstance = Instantiate(spruzzo, transform.position, Quaternion.identity);
        spruzzoInstance.GetComponent<ParticleSystem>().startColor = actualColor;
        Destroy(spruzzoInstance, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ApplyImpulseToEnemy(collision.gameObject.GetComponent<Rigidbody2D>());
            ApplyDamageAndStain(collision.gameObject.GetComponent<EnemyVariable>());
            InstantiateParticleSpruzzo();
           
            Destroy(gameObject);
        }

        else if (collision.gameObject.tag == "Floor") {
            InstantiateParticleSpruzzo();
            SpawnSpot();
            Destroy(gameObject);
        }
    }


    void SpawnSpot()
    {
        GameObject spotInstance = Instantiate(floorStain, transform.position, Quaternion.identity);
        spotInstance.GetComponent<SpriteRenderer>().color = actualColor;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
