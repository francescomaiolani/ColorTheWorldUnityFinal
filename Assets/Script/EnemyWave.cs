using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CLASSE CHE DESCRIVE LE CARATTERISTICHE DI UNA SPECIFICA ONDATA DI NEMICI 
//OGNI WAVE E' COMPLESSIVAMENTE COSTITUITA DA PIU' ENEMYWAVE
public class EnemyWave {

    string enemyName;
    bool burst;
    float initialDelay;
    float inBetweenDelay;
    float randomPercentageBetweenDelay;
    bool started;

    public EnemyWave(string enemyName, bool burst, float initialDelay, float inBetweenDelay, float randomPercentageBetweenDelay)
    {
        this.enemyName = enemyName;
        this.burst = burst;
        this.initialDelay = initialDelay;
        this.inBetweenDelay = inBetweenDelay;
        this.randomPercentageBetweenDelay = randomPercentageBetweenDelay;
    }

    public void SetStarted() {
        started = true;
    }

    public bool GetStarted() {
        return started;
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

    public float GetRandomPercentageBetweenDelay() {
        return randomPercentageBetweenDelay;
    }
}
