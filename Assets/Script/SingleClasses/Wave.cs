using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave {

    public List<EnemyWave> allMicroWaveInBigWave = new List<EnemyWave>();

    /*public Wave(List<EnemyWave> enemyWaves) {
        allMicroWaveInBigWave = enemyWaves;
    }*/

    public Wave(WaveData miniWaves) {
        for (int i = 0; i < miniWaves.waves.Length; i++) {
            EnemyWave actualMiniWave = new EnemyWave(miniWaves.waves[i]);
            allMicroWaveInBigWave.Add(actualMiniWave);
        }
    }

    public void StartWave(TimerManager timerManager) {

        foreach (EnemyWave wave in allMicroWaveInBigWave) {
            wave.StartEnemyWave(timerManager);
        }
    }	
}
