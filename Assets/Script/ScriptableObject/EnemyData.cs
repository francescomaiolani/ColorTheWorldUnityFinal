using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : ScriptableObject {

    //DATA
    public string enemyName;
    public float maxLife;
    public bool shielded;
    public float maxShieldLife;
    public float damage;
    public bool kamikadze;
    public int goldGiven;
    public float specialPointGiven;
    public float timeToDestroy;


    //MOVIMENTO
    public float thrust;
    public float maxSpeed;
    public float wallDistance;

    //GAMEOBJECT
    public GameObject floorStain;
    public GameObject bodyStain;
    public GameObject attackParticle;
    public Vector3 attackParticleOffset;

}
