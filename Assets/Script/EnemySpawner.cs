using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] enemyArray;

    public List<EnemyWave> enemyLevelWave;

    public float UpPosition;
    public float DownPosition;

    public int MaxNormalEnemy = 5;
    public int MaxGrandeEnemy = 2;
    public int actualNormalEnemy = 0;
    public int actualGrandeEnemy = 0;


    // Use this for initialization
    void Start () {

        enemyLevelWave = new List<EnemyWave>();

        Invoke("SpawnNemicoNormale", 0);
        Invoke("SpawnNemicoGrande", 1);

    }

    void SpawnNemicoNormale() {
        if (actualNormalEnemy < MaxNormalEnemy) {
            Instantiate(enemyArray[0], new Vector3(16f, Random.Range(DownPosition, UpPosition), 0), Quaternion.identity);
            actualNormalEnemy++;
            Invoke("SpawnNemicoNormale", 5);
        }
    }
    void SpawnNemicoGrande()
    {
        if (actualGrandeEnemy < MaxGrandeEnemy)
        {
            Instantiate(enemyArray[1], new Vector3(16f, Random.Range(DownPosition, UpPosition), 0), Quaternion.identity);
            actualGrandeEnemy++;
            Invoke("SpawnNemicoGrande", 10);
        }
    }


}
