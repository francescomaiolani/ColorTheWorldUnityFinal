using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariable : MonoBehaviour{

    
    private float actualLife;
    private float actualShieldLife;
    private bool shielded; 
    bool hit;

    public EnemyData data;

    Color32 lastColorHit;

    public SpriteMask spriteMask;
    public SpriteRenderer[] enemyPieces;
    public Transform shadow;

    public Animator animator;
    public CapsuleCollider2D capsuleCollider;
    public EnemyMovement enemyMovement;


    //EVENTS
    public delegate void OnEnemyDeadGold(int gold);
    public delegate void OnEnemyDeadSpecialPoint(float specialPoint);
    public delegate void OnEnemyDeadResourceText(int gold, Vector2 position, string tipo);
    public delegate void OnAttackDone(float amount);

    public static event OnEnemyDeadGold EnemyDeadGold;
    public static event OnEnemyDeadSpecialPoint EnemyDeadSpecialPoint;
    public static event OnEnemyDeadResourceText EnemyDeadSpawnResourceText;
    public static event OnAttackDone ApplyWallDamage;


    // Use this for initialization
    void Start () {
        actualLife = data.maxLife;
        shielded = data.shielded;
        if (data.shielded)
            actualShieldLife = data.maxShieldLife;
        enemyMovement.ReachedWall += WallReached;
        enemyMovement.NotReachedWall += WallNotReached;

    }

    //ORDINA I PEZZI NEL GIUSTO ORDINE
    public void SetCorrectZOrder(int lane, int positionInLane) {
        int laneOrder = (lane - 1) * 200 + positionInLane;
        foreach (SpriteRenderer spr in enemyPieces)
        {
            spr.sortingOrder += laneOrder;
        }
        spriteMask.frontSortingOrder = enemyPieces[0].sortingOrder + 1;
        spriteMask.backSortingOrder = enemyPieces[0].sortingOrder;
    }

    public void ApplyDamage(float damage) {
        if (!hit) {
            hit = true;
            spriteMask.enabled = true;
        }

        if (actualLife > 0 && data.shielded && actualShieldLife > 0)
        {
            actualShieldLife -= damage;
            if (actualShieldLife <= 0)
                shielded = false;
        }
        else if (actualLife > 0 && !shielded) {
            actualLife -= damage;
            if (actualLife <= 0)
                Dead();
        }
    }


    public void Dead() {

        animator.SetTrigger("Death");
        DeactivatePhysics();
        Destroy(gameObject, data.timeToDestroy);

        EnemyDeadGold(data.goldGiven);
        EnemyDeadSpecialPoint(data.specialPointGiven);
        EnemyDeadSpawnResourceText(data.goldGiven, transform.position, "gold");

        enemyMovement.ReachedWall -= WallReached;
        enemyMovement.NotReachedWall -= WallNotReached;

    }

    public void DeactivatePhysics() {
        capsuleCollider.enabled = false;
        enemyMovement.Stop();
        enemyMovement.enabled = false;
        GetComponent<Rigidbody2D>().Sleep();
    }
 
    public void Attack() {
        ApplyWallDamage(data.damage);
        GameObject spruzzoNero = Instantiate(data.attackParticle, enemyPieces[0].transform.position + data.attackParticleOffset, Quaternion.identity);
        Destroy(spruzzoNero, 1f);
    }

    public void Explode()
    {
        ApplyWallDamage(data.damage); 
        enemyMovement.ReachedWall -= WallReached;
        GameObject spruzzoNero = Instantiate(data.attackParticle, enemyPieces[0].transform.position + data.attackParticleOffset , Quaternion.identity);
        Destroy(spruzzoNero, 1f);
        Destroy(gameObject);
    }

    public void WallReached() {
        //se ti fai esplodere con l'attacco
        if (data.kamikadze) {
            DeactivatePhysics();
        }
        animator.SetTrigger("Attack");
    }

    //SE SONO STATO SBALZATO INDIETRO DAL MURO RITORNA A CAMMINARE
    public void WallNotReached()
    {
        animator.SetTrigger("Walk");
    }

    void SpawnSpot()
    {
        GameObject spotInstance = Instantiate(data.floorStain, shadow.position, Quaternion.identity);
        spotInstance.GetComponent<SpriteRenderer>().color = lastColorHit;
    }

    public void SpawnSpotOnBody(Vector3 position, Color32 color)
    {
        GameObject spotInstance = Instantiate(data.bodyStain, position, Quaternion.identity, enemyPieces[0].transform);
        SpriteRenderer spotSprite = spotInstance.GetComponent<SpriteRenderer>();
        spotSprite.color = color;
        spotSprite.sortingOrder = enemyPieces[0].sortingOrder + 1;
        lastColorHit = color;
    }
}
