using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemyArray;
    public GameController gameController;
    public TimerManager timerManager;


    public List<Wave> allEnemyLevelWaves = new List<Wave>();

    public WaveData[] allWaves;

    public Wave actualLevelWave;

    LayerMask nemici1;
    LayerMask nemici2;
    LayerMask nemici3;

    public int maxLaneNumber;

    int[] waveZOrderCount;

    // Use this for initialization
    void Start()
    {
        nemici1 = LayerMask.NameToLayer("Nemici1");
        nemici2 = LayerMask.NameToLayer("Nemici2");
        nemici3 = LayerMask.NameToLayer("Nemici3");
        waveZOrderCount = new int[maxLaneNumber];

        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        timerManager = GameObject.Find("TimerManager").GetComponent<TimerManager>();

        actualLevelWave = new Wave(allWaves[gameController.actualWave - 1]);

        //actualLevelWave = allWaves[gameController.actualWave - 1];
        SubscribeToEvent();
        actualLevelWave.StartWave(timerManager);
    }

    void SubscribeToEvent() {
        for (int i = 0; i < actualLevelWave.allMicroWaveInBigWave.Count; i++) {
            actualLevelWave.allMicroWaveInBigWave[i].EnemyToSpawn += SpawnEnemy;
        }
    }

    //METODO INVOCATO CHE SELEZIONA IL TIPO DI SPAWN DA EFFETTUARE
    public void SpawnEnemy(string nome, bool burst, int amount)
    {
        if (burst)
            for (int i = 0; i < amount; i++)
            {
                int lane = Random.Range(1, maxLaneNumber + 1 );
                InstantiateEnemy(FindEnemyPrefab(nome), lane);
            }
        else {
            int lane = Random.Range(1,maxLaneNumber + 1);
            InstantiateEnemy(FindEnemyPrefab(nome), lane);
        }
    }

    //METODO CHE EFFETTIVAMENTE SPAWNA IL NEMICO
    void InstantiateEnemy(GameObject enemy, int lane) {
        GameObject enemyInstance = Instantiate(enemy, new Vector3(26, 5, 0), Quaternion.identity);
        enemyInstance.GetComponent<EnemyVariable>().SetCorrectZOrder(maxLaneNumber - lane, waveZOrderCount[lane -1] );
        if (lane == 1)
            enemyInstance.layer = nemici1;
        else if (lane == 2)
            enemyInstance.layer = nemici2;
        else if (lane == 3)
            enemyInstance.layer = nemici3;

        waveZOrderCount[lane - 1] += 5;
        if (waveZOrderCount[lane - 1] >= 190)
            waveZOrderCount[lane - 1] = 0;

    }

    //CERCA IL PREFAB DEL NEMICO GIUSTO E LO RESTUTUISCE
    public GameObject FindEnemyPrefab(string nome)
    {
        switch (nome)
        {
            case "Pugile":
                return enemyArray[0];
            case "Grande":
                return enemyArray[1];
            case "Piccolo":
                return enemyArray[2];
            case "Cappuccio":
                return enemyArray[3];
        }
        return null;
    }

    //SOLO PER NON ISCRIVERTI PIU' AGLI EVENTI DI SPAWN DEI NEMICI
    private void OnDisable()
    {
        UnsubscribeToEvent();
    }
    void UnsubscribeToEvent()
    {
        for (int i = 0; i < actualLevelWave.allMicroWaveInBigWave.Count; i++)
        {
            actualLevelWave.allMicroWaveInBigWave[i].EnemyToSpawn -= SpawnEnemy;
        }
    }
}
