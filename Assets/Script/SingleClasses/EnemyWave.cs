using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CLASSE CHE DESCRIVE LE CARATTERISTICHE DI UNA SPECIFICA ONDATA DI NEMICI 
//OGNI WAVE E' COMPLESSIVAMENTE COSTITUITA DA PIU' ENEMYWAVE
public class EnemyWave {

    string enemyName;
    bool burst;
    int maxAmount;
    int actualAmount;
    float initialDelay;
    float inBetweenDelay;

    bool completed;
    TimerManager timerManager;

    public Timer allTimer;

    public delegate void SpawnEnemy(string nome, bool bursting, int amount);
    public event SpawnEnemy EnemyToSpawn;

    public EnemyWave(string enemyName, bool burst, int maxAmount, float initialDelay, float inBetweenDelay)
    {

        this.enemyName = enemyName;
        this.burst = burst;
        this.maxAmount = maxAmount;
        this.initialDelay = initialDelay;
        this.inBetweenDelay = inBetweenDelay;
    }

    public EnemyWave(EnemyWaveData data) {
        this.enemyName = data.enemyName;
        this.burst = data.burst;
        this.maxAmount = data.maxNumber;
        this.initialDelay = data.initialSpawnTime;
        this.inBetweenDelay = data.interSpawnTime;
    }

    //SOLO PER SAPERE QUANDO INIZIA L'ONDATA NULLA DI PIU'
    public void StartEnemyWave(TimerManager timerManagerParam) {

        actualAmount = 0;
        timerManager = timerManagerParam;
        allTimer = new Timer(initialDelay, false);
        timerManager.AddTimer(allTimer);
        allTimer.TimerEnded += EnemyWaveHasToSpawn;
    }

    public void EnemyWaveHasToSpawn() {

        if (burst)
        {
            EnemyToSpawn(enemyName, burst, maxAmount);
            EndEnemyWave();
        }
        else {
            //SE DEVO LANCIARNE UNO ALLA VOLTA 
            if (actualAmount < maxAmount)
            {
                EnemyToSpawn(enemyName, burst, 1);
                allTimer.ReassignTimer(inBetweenDelay);
                actualAmount++;
            }
            else {
                EndEnemyWave();
            }
        }
    }

    public void EndEnemyWave() {
        timerManager.RemoveTimer(allTimer);
        completed = true;
        Debug.Log("WaveCompleted");
    }



    public string GetEnemyName() {
        return enemyName;
    }

    public bool GetBurst() {
        return burst;
    }

    public float GetInitialDelay() {
        return initialDelay;
    }

    public float GetInBetweenDelay() {
        return inBetweenDelay;
    }
}
