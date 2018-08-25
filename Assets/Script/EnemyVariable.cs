using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariable : MonoBehaviour{

    public string enemyName;
    public float maxLife;
    private float actualLife;
    public bool shielded;
    public float shieldLife;
    public float damage;
    public float attackRate;
    public bool kamikadze;
    public int goldGiven;
    public float specialPointGiven;
    bool hit;

    public float timeToDestroy;
    public SpriteMask spriteMask;
    public SpriteRenderer[] enemyPieces;
    public Transform shadow;

    bool wallReached;

    public GameObject spot;
    public GameObject bodySpot;
    public GameObject attackParticle;
    public Vector3 attackParticleOffset;

    public Animator animator;
    public CapsuleCollider2D capsuleCollider;
    public BoxCollider2D wallCollider;
    public EnemyMovement enemyMovement;
    public Color32[] spotColors;

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
        actualLife = maxLife;
        enemyMovement.ReachedWall += WallReached;
    }
  
    //ORDINA I PEZZI NEL GIUSTO ORDINE
    public void SetCorrectZOrder(int lane) {
        int laneOrder = lane * 50;
        foreach (SpriteRenderer spr in enemyPieces)
        {
            spr.sortingOrder += laneOrder;
        }
    }

    public void ApplyDamage(float damage) {
        if (!hit) {
            hit = true;
            spriteMask.enabled = true;
        }

        if (actualLife > 0 && shielded && shieldLife > 0)
        {
            shieldLife -= damage;
            if (shieldLife <= 0)
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
        EnemyDeadGold(goldGiven);
        EnemyDeadSpecialPoint(specialPointGiven);
        EnemyDeadSpawnResourceText(goldGiven, transform.position, "gold");

        capsuleCollider.enabled = false;
        wallCollider.enabled = false;

        enemyMovement.ReachedWall -= WallReached;
        enemyMovement.Stop();
        Destroy(gameObject, timeToDestroy);
    }
 
    public void Attack() {
        ApplyWallDamage(damage);
        GameObject spruzzoNero = Instantiate(attackParticle, enemyPieces[0].transform.position + attackParticleOffset, Quaternion.identity);
        Destroy(spruzzoNero, 1f);
    }

    public void Explode()
    {
        ApplyWallDamage(damage); 
        enemyMovement.ReachedWall -= WallReached;
        GameObject spruzzoNero = Instantiate(attackParticle, enemyPieces[0].transform.position + attackParticleOffset , Quaternion.identity);
        Destroy(spruzzoNero, 1f);
        Destroy(gameObject);
    }

    public void WallReached() {
        wallReached = true;
        //se ti fai esplodere con l'attacco
        if (kamikadze) {
            capsuleCollider.enabled = false;        
        }
        animator.SetTrigger("Attack");
    }

    void SpawnSpot()
    {
        GameObject spotInstance = Instantiate(spot, shadow.position, Quaternion.identity);
        spotInstance.GetComponent<SpriteRenderer>().color = spotColors[Random.Range(0, spotColors.Length)];
    }

    public void SpawnSpotOnBody(Vector3 position)
    {
        GameObject spotInstance = Instantiate(bodySpot, position, Quaternion.identity, enemyPieces[0].transform);
        SpriteRenderer sporSprite = spotInstance.GetComponent<SpriteRenderer>();
        sporSprite.color = spotColors[Random.Range(0, spotColors.Length)];
        sporSprite.sortingOrder = enemyPieces[0].sortingOrder + 1;
    }



}
