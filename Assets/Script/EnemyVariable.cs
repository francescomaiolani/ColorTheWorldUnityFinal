using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariable : MonoBehaviour {

    public float maxLife;
    [SerializeField]
    private float actualLife;
    public bool shielded;
    public float shieldLife;
    public float damage;
    public float attackRate;
    public int goldGiven;
    public float specialPointGiven;

    bool wallReached;

    public Timer attackRateTimer;
    public TimerManager timerManager;

    public CapsuleCollider2D capsuleCollider;
    public BoxCollider2D wallCollider;
    public EnemyMovement enemyMovement;

    public delegate void OnEnemyDeadGold(int gold);
    public delegate void OnEnemyDeadSpecialPoint(float specialPoint);
    public delegate void OnEnemyDeadRemoveTimer(Timer timer);
    public delegate void OnEnemyDeadResourceText(int gold, Vector2 position, string tipo);

    public delegate void OnTimerCreated(Timer timer);
    public delegate void OnAttackDone(float amount);


    public static event OnEnemyDeadGold EnemyDeadGold;
    public static event OnEnemyDeadSpecialPoint EnemyDeadSpecialPoint;
    public static event OnEnemyDeadRemoveTimer EnemyDeadRemoveTimer;
    public static event OnEnemyDeadResourceText EnemyDeadSpawnResourceText;

    public static event OnTimerCreated TimerCreated;
    public static event OnAttackDone ApplyWallDamage;


    // Use this for initialization
    void Start () {
        actualLife = maxLife;
        enemyMovement.ReachedWall += WallReached;
    }

    public void ApplyDamage(float damage) {
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
        EnemyDeadGold(goldGiven);
        EnemyDeadSpecialPoint(specialPointGiven);
        EnemyDeadSpawnResourceText(goldGiven, transform.position, "gold");

        if (attackRateTimer != null)
            EnemyDeadRemoveTimer(attackRateTimer);

        capsuleCollider.enabled = false;
        wallCollider.enabled = false;

        if (wallReached)
        {
            attackRateTimer.TimerEnded -= AttackTimerOver;
        }
        enemyMovement.ReachedWall -= WallReached;
        Destroy(this.gameObject);
    }

    public void Attack() {
        ApplyWallDamage(damage);
    }

    public void WallReached() {
        attackRateTimer = new Timer(attackRate, true);
        attackRateTimer.TimerEnded += AttackTimerOver;
        TimerCreated(attackRateTimer);
        wallReached = true;
    }

    public void AttackTimerOver() {
        Attack();
    }

}
