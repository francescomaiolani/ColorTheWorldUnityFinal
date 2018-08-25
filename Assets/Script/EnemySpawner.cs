using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemyArray;
    public GameController gameController;
    public TimerManager timerManager;


    public List<Wave> allEnemyLevelWaves = new List<Wave>();
    public Wave actualLevelWave;

    public float upPosition;
    public float downPosition;
    float rangePosition;
    float range;

    public int maxLaneNumber;

    // Use this for initialization
    void Start()
    {
        rangePosition = upPosition - downPosition;
        range = rangePosition / maxLaneNumber;

        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        timerManager = GameObject.Find("TimerManager").GetComponent<TimerManager>();

        CreateAllWaves();

        actualLevelWave = allEnemyLevelWaves[gameController.actualWave - 1];
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
                int lane = i + (maxLaneNumber - amount) / 2;
                InstantiateEnemy(FindEnemyPrefab(nome), lane);
            }
        else {
            int lane = Random.Range(0,maxLaneNumber);
            InstantiateEnemy(FindEnemyPrefab(nome), lane);
        }
    }

    //METODO CHE EFFETTIVAMENTE SPAWNA IL NEMICO
    void InstantiateEnemy(GameObject enemy, int lane) {
        GameObject enemyInstance = Instantiate(enemy, new Vector3(26, downPosition + lane * range, 0), Quaternion.identity);
        enemyInstance.GetComponent<EnemyVariable>().SetCorrectZOrder(maxLaneNumber - lane);
    }

    //CERCA IL PREFAB DEL NEMICO GIUSTO E LO RESTUTUISCE
    public GameObject FindEnemyPrefab(string nome)
    {
        switch (nome)
        {
            case "Pugile":
                return enemyArray[0];
            case "Piccolo":
                return enemyArray[1];
            case "Grande":
                return enemyArray[2];
        }
        return null;
    }

    //METODO ENORME CHE CREA LA LISTA CON TUTTE LE ONDATE DI NEMICI
    void CreateAllWaves() {

        //PRIMO LIVELLO DEL GIOCO
        List<EnemyWave> firstEnemyWave = new List<EnemyWave>   {
            new EnemyWave("Pugile", false, 20, 0, 1f),
            new EnemyWave("Piccolo", false, 5, 4, 2),
            new EnemyWave("Grande", false, 3, 3, 10)
        };
        allEnemyLevelWaves.Add(new Wave(firstEnemyWave));

        //PRIMO LIVELLO DEL GIOCO
        List<EnemyWave> secondEnemyWave = new List<EnemyWave>   {
            new EnemyWave("Pugile", false, 40, 0, 1f),
            new EnemyWave("Piccolo", false, 10, 4, 0)
        };
        allEnemyLevelWaves.Add(new Wave(secondEnemyWave));

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
