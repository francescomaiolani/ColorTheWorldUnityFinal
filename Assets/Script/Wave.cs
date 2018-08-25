using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave {

    public List<EnemyWave> allMicroWaveInBigWave = new List<EnemyWave>();

    public Wave(List<EnemyWave> enemyWaves) {
        allMicroWaveInBigWave = enemyWaves;
    }

    public void StartWave(TimerManager timerManager) {

        foreach (EnemyWave wave in allMicroWaveInBigWave) {
            wave.StartEnemyWave(timerManager);
        }
    }	
}
